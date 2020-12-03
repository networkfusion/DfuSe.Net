using DfuSe.Core.Windows;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace DfuSe.Core
{
    public class StDevice
    {
		private const uint USB_DEVICE_DESCRIPTOR_size = 18;

		int m_CurrentConfig;
		int m_CurrentInterf;
		int m_CurrentAltSet;

		SafeFileHandle m_DeviceHandle;
		bool m_bDeviceIsOpen;
		uint m_nDefaultTimeOut;

		USB_DEVICE_DESCRIPTOR m_DeviceDescriptor;
		byte[] m_pConfigs;
		//HANDLE* m_pPipeHandles;
		uint m_nbEndPoints;

		public string SymbolicName { get; }

        public StDevice(string symbolicName)
        {
            SymbolicName = symbolicName;
        }

        public StDeviceErrors Open(SafeFileHandle unplugEvent)
        {
			if (m_bDeviceIsOpen)
			{
				return StDeviceErrors.STDEVICE_NOERROR;
			}

			// 1- Opens the device
			var nRet = OpenDevice(unplugEvent);

			if (nRet == StDeviceErrors.STDEVICE_NOERROR)
			{
				// 2- Get the descriptors
				nRet = AllocDescriptors();

				if (nRet != StDeviceErrors.STDEVICE_NOERROR)
				{
					CloseDevice();
				}
				else
				{
					m_bDeviceIsOpen = true;
				}
			}

			return nRet;

		}

        private StDeviceErrors AllocDescriptors()
        {
			bool Success;
			uint ByteCount;
			int i;
			var nRet = StDeviceErrors.STDEVICE_ERRORDESCRIPTORBUILDING;

			ReleaseDescriptors();

			uint returnedBytes = 0;

			object output = default(USB_DEVICE_DESCRIPTOR);
			uint outputSize = (uint)Marshal.SizeOf<USB_DEVICE_DESCRIPTOR>();


			bool success = Win32.DeviceIoControl(
				m_DeviceHandle,
				EIOControlCode.GetDeviceDescriptor,
				null, 
				0, 
				output, 
				outputSize, 
				ref returnedBytes, 
				IntPtr.Zero);


			if (!success)
			{
				int lastError = Marshal.GetLastWin32Error();
				//throw new Win32Exception("Couldn't invoke DeviceIoControl for " + EIOControlCode.GetDeviceDescriptor + ". LastError: " + Utils.GetWin32ErrorMessage(lastError));


			}

			m_DeviceDescriptor = (USB_DEVICE_DESCRIPTOR)output;

			//long DeviceBuffer = 0;
			//uint bytesReturned = 0;
			//IntPtr outBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(m_DeviceDescriptor));

			//// Begin to get Device descriptor
			//Success = Win32.DeviceIoControl(
			//	m_DeviceHandle,
			//	EIOControlCode.GetDeviceDescriptor,
			//	IntPtr.Zero,
			//	0,
			//	ref outBuffer,
			//	Marshal.SizeOf(m_DeviceDescriptor),
			//	out bytesReturned,
			//	IntPtr.Zero);

			//m_DeviceDescriptor = (USB_DEVICE_DESCRIPTOR)Marshal.PtrToStructure(outBuffer, typeof(USB_DEVICE_DESCRIPTOR));
			return StDeviceErrors.STDEVICE_NOERROR;
		}

		private StDeviceErrors OpenDevice(SafeFileHandle unPlugEvent)
        {
			// Close first
			CloseDevice();

			m_DeviceHandle = Win32.CreateFileW(
				SymbolicName,
				EFileAccess.GenericWrite | EFileAccess.GenericRead,
				EFileShare.None,
				IntPtr.Zero,
				ECreationDisposition.OpenExisting,
				EFileAttributes.None,
				IntPtr.Zero);

			if (!m_DeviceHandle.IsInvalid)
			{
				var bFake = m_bDeviceIsOpen;
				StDeviceErrors nRet = StDeviceErrors.STDEVICE_NOERROR;

				m_bDeviceIsOpen = true;

				// BUG BUG: Do not issue a reset as Composite devices do not support this !
				//nRet=Reset();
				
				m_bDeviceIsOpen = bFake;

				// The symbolic name exists. Let's create the disconnect event if needed
				if ((nRet == StDeviceErrors.STDEVICE_NOERROR) && unPlugEvent != null)
				{
					//*phUnPlugEvent = CreateEvent(NULL, FALSE, FALSE, NULL); // Disconnect event;
					
					//if (*phUnPlugEvent)
					//{
					//	DWORD ByteCount;
					//	if (DeviceIoControl(m_DeviceHandle,
					//						PU_SET_EVENT_DISCONNECT,
					//						phUnPlugEvent,
					//						sizeof(HANDLE),
					//						NULL,
					//						0,
					//						&ByteCount,
					//						NULL))
					//		nRet = STDEVICE_NOERROR;
					//	else
					//		nRet = STDEVICE_CANTUSEUNPLUGEVENT;
					//}
				}
				return nRet;
			}

			return StDeviceErrors.STDEVICE_OPENDRIVERERROR;
		}

		public StDeviceErrors Close()
        {
			if (!m_bDeviceIsOpen)
			{
				return StDeviceErrors.STDEVICE_NOERROR;
			}

			// 1- Close the pipes, if needed
			ClosePipes();

			// 2- Release the descriptors
			ReleaseDescriptors();
			
			// 3- Close the device
			CloseDevice();

			m_bDeviceIsOpen = false;

			return StDeviceErrors.STDEVICE_NOERROR;

		}

        private StDeviceErrors CloseDevice()
        {
			if (m_DeviceHandle != null)
			{
				m_DeviceHandle.Close();
				m_DeviceHandle = null;
			}

			return StDeviceErrors.STDEVICE_NOERROR;
		}

        private void ReleaseDescriptors()
        {
			if (m_pConfigs != null)
			{
				//m_pConfigs = new byte[512];
				m_pConfigs = null;
			}
		}

        private StDeviceErrors ClosePipes()
        {
            throw new NotImplementedException();
        }
    }
}
