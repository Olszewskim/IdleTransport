using System;
using System.Globalization;
using IdleTransport.Utilities;
using UnityEngine;

namespace IdleTransport.ExtensionsMethods {
    public static class NumericExtensions {
        public static string ToSecondsWithTwoDecimalPlaces(this double seconds) {
            return $"{(seconds):0.##}s";
        }

        public static string ToTimePerSecond(this double seconds) {
            return $"{(1 / seconds):0.##}";
        }

        public static string ToPercentString(this double number) {
            return ((float) number).ToPercentString();
        }

        public static string ToPercentString(this float number) {
            return $"{number * 100}%";
        }

        public static string ConvertToTimeStringAuto(this uint seconds) {
            return seconds < Constants.SECONDS_IN_HOUR
                ? seconds.ConvertToTimeStringMSFormat()
                : seconds.ConvertToTimeStringHMSFormat();
        }

        public static string ConvertToTimeStringMSFormat(this uint seconds) {
            var min = Mathf.FloorToInt(seconds / 60F);
            var sec = Mathf.FloorToInt(seconds - min * 60);
            return string.Format("{0:00}:{1:00}", min, sec);
        }

        public static string ConvertToTimeStringMSMSFormat(this float seconds) {
            var min = Mathf.FloorToInt(seconds / 60F);
            var sec = Mathf.FloorToInt(seconds - min * 60);
            var milliSec = Mathf.FloorToInt(seconds * 1000) % 1000;
            return string.Format("{0:00}:{1:00}:{2:000}", min, sec, milliSec);
        }

        public static string ConvertToTimeStringHMSFormat(this uint seconds) {
            int hours = Mathf.FloorToInt(seconds / 3600F);
            int min = Mathf.FloorToInt((seconds - hours * 3600) / 60F);
            int sec = Mathf.FloorToInt(seconds - hours * 3600 - min * 60);
            return string.Format("{0:00}:{1:00}:{2:00}", hours, min, sec);
        }

        public static string ConvertToString(this float number) {
            return number.ToString("0.#", CultureInfo.InvariantCulture);
        }

        public static string ConvertToThousandsFormat<T>(this T number) where T : struct, IFormattable {
            var numberFormatInfo = (NumberFormatInfo) CultureInfo.InvariantCulture.NumberFormat.Clone();
            numberFormatInfo.NumberGroupSeparator = " ";
            return number.ToString("#,0", numberFormatInfo);
        }

        public static BigInteger MultipleByDouble(this BigInteger number, double? multiplier, int decimalDigits = 3) {
            multiplier = multiplier ?? 0;
            var decimalFactor = (int) Math.Pow(10, decimalDigits);
            var intMultiplier = (int) (multiplier * decimalFactor);
            return BigInteger.Divide(BigInteger.Multiply(number, intMultiplier), decimalFactor);
        }
    }
}
