using IdleTransport.Utilities;
using NUnit.Framework;

namespace Tests {

    public class HugeNumbersToLettersConverterTests {
        [TestCase("-666", "-666")]
        [TestCase("-66", "-66")]
        [TestCase("-1", "-1")]
        [TestCase("0", "0")]
        [TestCase("1", "1")]
        [TestCase("66", "66")]
        [TestCase("666", "666")]
        [TestCase("999", "999")]
        public void Test_Numbers_Lower_Than_1000_Should_Be_Strings_Without_Conversion(string numberString,
            string result) {
            var number = new BigInteger(numberString);
            Assert.AreEqual(result, number.FormatHugeNumber());
        }

        [TestCase("-999 999", "-999.9K")]
        [TestCase("-10 000", "-10K")]
        [TestCase("-1 000", "-1K")]
        [TestCase("1 000", "1K")]
        [TestCase("1 003", "1K")]
        [TestCase("1 033", "1.03K")]
        [TestCase("1 303", "1.3K")]
        [TestCase("10 000", "10K")]
        [TestCase("10 330", "10.33K")]
        [TestCase("999 999", "999.9K")]
        public void Test_Numbers_Between_1K_And_1M_Should_Have_K_Appended(string numberString, string result) {
            var number = new BigInteger(numberString.Replace(" ", string.Empty));
            Assert.AreEqual(result, number.FormatHugeNumber());
        }

        [TestCase("-999 999 999", "-999.9M")]
        [TestCase("-10 000 000", "-10M")]
        [TestCase("-1 000 000 ", "-1M")]
        [TestCase("1 000 000", "1M")]
        [TestCase("1 320 000", "1.32M")]
        [TestCase("10 056 300", "10.05M")]
        [TestCase("10 303 000", "10.3M")]
        [TestCase("999 999 999", "999.9M")]
        public void Test_Numbers_Between_1M_And_1B_Should_Have_M_Appended(string numberString, string result) {
            var number = new BigInteger(numberString.Replace(" ", string.Empty));
            Assert.AreEqual(result, number.FormatHugeNumber());
        }

        [TestCase("1 000 000 000", "1aa")]
        [TestCase("10 000 000 000", "10aa")]
        [TestCase("100 000 000 000", "100aa")]
        [TestCase("1 000 000 000 000", "1ab")]
        [TestCase("1 000 000 000 000 000", "1ac")]
        [TestCase("1 000 000 000 000 000 000", "1ad")]
        [TestCase("1 000 000 000 000 000 000 000", "1ae")]
        [TestCase("18 446 744 073 709 551 615", "18.44ad")]
        [TestCase("9 223 372 036 854 775 807", "9.22ad")]
        [TestCase("952 223 372 036 854 775 807", "952.2ad")]
        public void Test_Numbers_Above_1B_Should_Have_Double_Consecutive_Alphaber_Letters_Appended_Starting_With_aa(
            string numberString, string result) {
            var number = new BigInteger(numberString.Replace(" ", string.Empty));
            Assert.AreEqual(result, number.FormatHugeNumber());
        }

        [TestCase("666", "666")]
        [TestCase("1 033", "1K")]
        [TestCase("1 303", "1K")]
        [TestCase("999 999", "999K")]
        [TestCase("10 056 300", "10M")]
        [TestCase("10 303 000", "10M")]
        [TestCase("999 999 999", "999M")]
        [TestCase("18 446 744 073 709 551 615", "18ad")]
        [TestCase("9 223 372 036 854 775 807", "9ad")]
        [TestCase("952 223 372 036 854 775 807", "952ad")]
        public void Test_Numbers_Have_Correct_Appendix_Without_Decimals(string numberString, string result) {
            var number = new BigInteger(numberString.Replace(" ", string.Empty));
            Assert.AreEqual(result, number.FormatHugeNumber(false));
        }

        [TestCase(0, "")]
        [TestCase(3, "K")]
        [TestCase(6, "M")]
        [TestCase(9, "aa")]
        [TestCase(12, "ab")]
        [TestCase(15, "ac")]
        [TestCase(18, "ad")]
        [TestCase(21, "ae")]
        [TestCase(24, "af")]
        [TestCase(3 * 27, "ay")]
        [TestCase(3 * 28, "az")]
        [TestCase(3 * 29, "ba")]
        [TestCase(3 * 54, "bz")]
        [TestCase(3 * 55, "ca")]
        public void Test_GetSuffix_Should_Generate_Suffixes_Every_3_Zeros_Starting_With_aa(int zeros, string suffix) {
            Assert.AreEqual(suffix, HugeNumbersToLettersConverter.GetSuffix(zeros));
        }
    }
}
