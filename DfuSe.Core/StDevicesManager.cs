using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;

namespace DfuSe.Core
{
    public class StDevicesManager
    {
        List<StDevice> m_OpenDevices = new List<StDevice>();

        public StDeviceErrors Open(
            string symbName,
            SafeFileHandle device,
            SafeFileHandle unplugEvent)
        {
            if (string.IsNullOrEmpty(symbName))
            {
                throw new ArgumentException($"'{nameof(symbName)}' cannot be null or empty", nameof(symbName));
            }

            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            var stDevice = new StDevice(symbName);

            var ret = stDevice.Open(unplugEvent);



            // OK our STDevice object was successfully created. Let's add it to our collection
            m_OpenDevices.Add(stDevice);

            return StDeviceErrors.STDEVICE_NOERROR;
        }
    }
}
