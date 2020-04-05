using System;

namespace IdleTransport.Utilities
{
    public static class HugeNumbersToLettersConverter {

        public static string FormatHugeNumber(this BigInteger number, bool withDecimals = true) {
            var absNumber = BigInteger.Abs(number);
            if (absNumber < 1000) {
                return number.ToString();
            }

            var zeros = absNumber.ToString().Length - 1;
            var order = zeros - (zeros % 3);
            var suffix = GetSuffix(order);
            var decimalsModifier = withDecimals ? 2 : 0;
            var convertedNumber = (double)(BigInteger.ToInt32(BigInteger.Divide(number, BigInteger.Pow(10, order - decimalsModifier))));
            if (withDecimals) {
                convertedNumber /= 100d;
                convertedNumber = convertedNumber.Truncate(Math.Abs(convertedNumber) < 100 ? 2 : 1);
            }
            return $"{convertedNumber}{suffix}";
        }

        public static string FormatHugeNumber(this double number) {
            var absNumber = Math.Abs(number);
            if (absNumber < 1000) {
                return number.ToString("0.#");
            }

            var zeros = (int)Math.Floor(Math.Log10(absNumber));
            var order = zeros - (zeros % 3);
            var suffix = GetSuffix(order);
            var convertedNumber = Math.Truncate(number / Math.Pow(10, order));
            return $"{convertedNumber}{suffix}";
        }

        private static string GetSuffix(int zeros) {
            if (zeros < 3) { return ""; }

            if (zeros >= 3 && zeros < 6) { return "K"; }

            if (zeros >= 6 && zeros < 9) { return "M"; }
            return GenerateHugeSuffix(zeros);
        }

        private static string GenerateHugeSuffix(int zeros) {
            var startSequenceAtNumZeros = 6;
            var asciiRange = 26;
            var aCharCode = 97;
            var ordOffset = (zeros - startSequenceAtNumZeros) / 3 - 1;
            var firstCharCode = aCharCode + (ordOffset / asciiRange);
            var secondChar = aCharCode + (ordOffset % asciiRange);
            return $"{(char)firstCharCode}{(char)secondChar}";
        }

        private static double Truncate(this double d, double decimals) {
            return Math.Truncate(d * Math.Pow(10, decimals)) / Math.Pow(10, decimals);
        }
    }
}
