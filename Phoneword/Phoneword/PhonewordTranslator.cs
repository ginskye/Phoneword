using System;
using System.Collections.Generic;
using System.Text;

namespace Core  
{
    public static class PhonewordTranslator
    {
        public static string ToNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) //condition for no or empty string
                return null;

            raw = raw.ToUpperInvariant(); //capitalizes all characters in the passed string

            var newNumber = new StringBuilder();

            foreach (var c in raw) //loops through each character i nthe uppercase string
            {
                if(" -0123456789".Contains(c))
                    newNumber.Append(c); //checks if the character is already a number or separator and if so, adds to the newNumber array
                else
                {
                    var result = TranslateToNumber(c); //calls the number translation function
                    if (result != null)
                        newNumber.Append(result); //if there's something there, adds to newNumber (like l 21)
                    else
                        return null; //if it's empty, returns nothing
                }
            }
            return newNumber.ToString(); //takes the full list and returns it as a string
        }
        static bool Contains(this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }

        static readonly string[] digits =
        {
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        static int? TranslateToNumber(char c)
        {
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i].Contains(c))
                    return 2 + i;
            }
            return null;
        }
    }
}
