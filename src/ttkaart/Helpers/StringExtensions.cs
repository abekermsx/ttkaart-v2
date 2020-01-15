using System;
using System.Text;

namespace ttkaart.Helpers
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string str)
        {
            if (String.IsNullOrEmpty(str))
                return "";

            if (str.Length == 1)
                return str.ToUpper();

            return str[0].ToString().ToUpper() + str.Substring(1);
        }

        public static string ToUrlFriendlyString(this string str, char replaceChar = '-')
        {
            string normalized = str.Normalize(NormalizationForm.FormKD);
            Encoding removal = Encoding.GetEncoding(Encoding.ASCII.CodePage,
                                                    new EncoderReplacementFallback(""),
                                                    new DecoderReplacementFallback(""));
            byte[] arr = removal.GetBytes(normalized);

            StringBuilder sb = new StringBuilder();

            char prevChar = replaceChar;

            for (int i = 0; i < arr.Length; i++)
            {
                int ci = Convert.ToInt32(arr[i]);

                if (!((ci > 47 && ci < 58) || (ci > 64 && ci < 91) || (ci > 96 && ci < 123)))
                {
                    if (prevChar != replaceChar)
                    {
                        sb.Append(replaceChar);
                        prevChar = replaceChar;
                    }
                }
                else
                {
                    prevChar = (char)arr[i];
                    sb.Append(prevChar);
                }
            }

            string result = sb.ToString().TrimEnd(replaceChar);

            return result;
        }
    }
}