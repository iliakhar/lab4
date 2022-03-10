using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roman;

namespace lab3Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void RomanNumberTest()
        {
            RomanNumber rn1 = new RomanNumber(20);
            RomanNumber rn2;
            bool isExep = true;
            string expected = "XX";

            try
            {
                rn2 = new RomanNumber(0);
                isExep = false;
            }
            catch (RomanNumberException) { }
            finally
            {
                if (!isExep)
                    Assert.Fail();
                isExep = true;
            }

            try
            {
                rn2 = new RomanNumber(4000);
                isExep = false;
            }
            catch (RomanNumberException) { }
            finally
            {
                if (!isExep)
                    Assert.Fail();
            }

            Assert.AreEqual(expected, rn1.ToString());
        }

        [TestMethod]
        public void ToStringTest()
        {
            RomanNumber rn1 = new RomanNumber(1954);
            RomanNumber rn2 = new RomanNumber(381);

            Assert.AreEqual("MCMLIV", rn1.ToString());
            Assert.AreEqual("CCCLXXXI", rn2.ToString());
        }

        [TestMethod]
        public void RomanNumberExtendTest()
        {
            Assert.ThrowsException<RomanNumberException>(() => new RomanNumberExtend("IIX"));
            Assert.ThrowsException<RomanNumberException>(() => new RomanNumberExtend("asd"));
            Assert.ThrowsException<RomanNumberException>(() => new RomanNumberExtend("IIII"));
            Assert.ThrowsException<RomanNumberException>(() => new RomanNumberExtend("IM"));
            Assert.ThrowsException<RomanNumberException>(() => new RomanNumberExtend("VL"));
            Assert.ThrowsException<RomanNumberException>(() => new RomanNumberExtend("VIV"));

            Assert.AreEqual("LV", (new RomanNumberExtend("LV").ToString()));
            Assert.AreEqual("IX", (new RomanNumberExtend("IX").ToString()));
            Assert.AreEqual("MMI", (new RomanNumberExtend("MMI").ToString()));
            Assert.AreEqual("IV", (new RomanNumberExtend("IV").ToString()));
        }

        [TestMethod]
        public void AddTest()
        {
            RomanNumber rn1 = new RomanNumber(20);
            RomanNumber rn2 = new RomanNumber(11);
            RomanNumber rn3 = new RomanNumber(3990);
            string expected = "XXXI";

            Assert.ThrowsException<RomanNumberException>(() => RomanNumber.Add(rn2, rn3));
            Assert.AreEqual(expected, (rn1 + rn2).ToString());
        }

        [TestMethod]
        public void SubTest()
        {
            RomanNumber rn1 = new RomanNumber(20);
            RomanNumber rn2 = new RomanNumber(11);
            string expected = "IX";
            Assert.ThrowsException<RomanNumberException>(() => RomanNumber.Sub(rn2, rn1));
            Assert.AreEqual(expected, (rn1 - rn2).ToString());
        }

        [TestMethod]
        public void MulTest()
        {
            RomanNumber rn1 = new RomanNumber(1);
            RomanNumber rn2 = new RomanNumber(3990);
            RomanNumber rn3 = new RomanNumber(3990);
            string expected = "MMMCMXC";
            Assert.ThrowsException<RomanNumberException>(() => RomanNumber.Mul(rn2, rn3));
            Assert.AreEqual(expected, (rn1 * rn2).ToString());
        }

        [TestMethod]
        public void DivTest()
        {
            RomanNumber rn1 = new RomanNumber(990);
            RomanNumber rn2 = new RomanNumber(30);
            string expected = "XXXIII";
            Assert.ThrowsException<RomanNumberException>(() => RomanNumber.Div(rn2, rn1));
            Assert.AreEqual(expected, (rn1 / rn2).ToString());
        }

        [TestMethod]
        public void CloneTest()
        {
            RomanNumber rn1 = new RomanNumber(10);
            RomanNumber rn2 = (RomanNumber)rn1.Clone();
            rn1 = rn1 + rn1;
            string expected1 = "X";
            string expected2 = "XX";
            Assert.AreEqual(expected1, (rn2).ToString());
            Assert.AreEqual(expected2, (rn1).ToString());
        }

        [TestMethod]
        public void CompareTest()
        {
            int a = 10, b = 38;
            RomanNumber rn1 = new RomanNumber((ushort)a);
            RomanNumber rn2 = new RomanNumber((ushort)b);

            Assert.AreEqual(a - b, rn1.CompareTo(rn2));
            Assert.AreEqual(b - a, rn2.CompareTo(rn1));
            Assert.AreEqual(0, rn1.CompareTo(rn1));
        }
    }
}