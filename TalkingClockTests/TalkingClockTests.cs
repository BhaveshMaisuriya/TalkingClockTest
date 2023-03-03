using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class TalkingClockTests
    {

        [TestMethod()]
        public void validTimeTest()
        {
            bool result = TalkingClock.validateNumericTime("1:30");

            Assert.IsTrue(result, "1:30 should be valid time.");

        }

        [TestMethod()]
        public void invalidTimeTest()
        {
            try
            {
                TalkingClock.validateNumericTime("1:mm");

            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is HourMinuteNumericException);

            }


        }

        [TestMethod()]
        public void sholdContainColoninTime()
        {
            try
            {
                TalkingClock.validateNumericTime("130");

            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NoColonSuppliedException);
                // Assert.IsFalse(result, "130 should be invalid time. Should Contains ':' in Time");

            }

        }

        [TestMethod()]
        public void convertHumanReadableTimeTest()
        {
            string result = TalkingClock.convertHumanReadableTime(1, 30);

            Assert.AreEqual(result, "half past one");
        }
    }
}