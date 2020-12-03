using DfuSe.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DfuSeUnitTests
{
    [TestClass]
    public class StDeviceTests
    {
        [TestMethod]
        public void TestOpen()
        {
            const string deviceID = @"\\?\usb#vid_0483&pid_df11#00000008ffff#{a5dcbf10-6530-11d2-901f-00c04fb951ed}";

            var stDevice = new StDevice(deviceID);
            Assert.IsTrue(stDevice.Open(null) == StDeviceErrors.STDEVICE_NOERROR);
        }
    }
}
