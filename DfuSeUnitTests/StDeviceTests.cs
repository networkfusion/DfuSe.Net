using DfuSe.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DfuSeUnitTests
{
    [TestClass]
    public class StDeviceTests
    {
        [TestMethod]
        public void detects_that_the_st_device_is_in_dfu_mode()
        {
            //const string deviceID = @"\\?\usb#vid_0483&pid_df11#00000008ffff#{a5dcbf10-6530-11d2-901f-00c04fb951ed}";
            //const int ProductId = 0xDF11;
            //const int VendorId = 0x0483;
            //var usbDevice = new UsbDevice.FindDevice(VendorId, ProductId);
            //Assert.IsTrue(usbDevice.deviceId == deviceID);
            Assert.IsTrue(1 == 0);
        }

        [TestMethod]
        public void opens_the_st_device_in_dfu_mode()
        {
            const string deviceID = @"\\?\usb#vid_0483&pid_df11#00000008ffff#{a5dcbf10-6530-11d2-901f-00c04fb951ed}";

            var stDevice = new StDevice(deviceID);
            Assert.IsTrue(stDevice.Open(null) == StDeviceErrors.STDEVICE_NOERROR);
        }

        [TestMethod]
        public void closes_the_st_device_in_dfu_mode()
        {
            const string deviceID = @"\\?\usb#vid_0483&pid_df11#00000008ffff#{a5dcbf10-6530-11d2-901f-00c04fb951ed}";

            var stDevice = new StDevice(deviceID);
            //TODO: open device first?
            Assert.IsTrue(stDevice.Close() == StDeviceErrors.STDEVICE_NOERROR);
        }

        [TestMethod]
        public void calculates_the_checksum_of_a_dfu_package_correctly()
        {
            //var crctable
            Assert.IsTrue(1 == 0);
        }

        [TestMethod]
        public void verifies_a_dfu_file_is_valid()
        {
            Assert.IsTrue(1 == 0);
        }

        [TestMethod]
        public void compares_a_dfu_file_matches_device_memory()
        {
            Assert.IsTrue(1 == 0);
        }

        [TestMethod]
        public void sends_a_controlpacket_to_st_device_in_dfu_mode()
        {
            Assert.IsTrue(1 == 0);
        }

        [TestMethod]
        public void obtains_the_status_of_st_device_in_dfu_mode_is_idle()
        {
            Assert.IsTrue(1 == 0);
        }

        [TestMethod]
        public void erases_a_block_of_memory_of_st_device_in_dfu_mode()
        {
            Assert.IsTrue(1 == 0);
        }

        [TestMethod]
        public void mass_erases_the_memory_of_st_device_in_dfu_mode()
        {
            Assert.IsTrue(1 == 0);
        }

        [TestMethod]
        public void writes_a_memory_block_of_st_device_in_dfu_mode()
        {
            Assert.IsTrue(1 == 0);
        }

    }
}
