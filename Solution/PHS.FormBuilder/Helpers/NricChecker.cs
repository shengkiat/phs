using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.FormBuilder.Helpers
{
    public static class NricChecker
    {
        private static readonly int[] Multiples = { 2, 7, 6, 5, 4, 3, 2 };

        public static bool IsNRICValid(string nric)
        {
            if (string.IsNullOrEmpty(nric))
            {
                return false;
            }

            //	check length must be 9 digits
            if (nric.Length != 9)
            {
                return false;
            }

            int total = 0
                , count = 0
                , numericNric;
            char first = nric[0]
                , last = nric[nric.Length - 1];

            // first chat alwatas T or S
            if (first != 'S' && first != 'T')
            {
                return false;
            }

            // ensure first chars is char and last

            if (!int.TryParse(nric.Substring(1, nric.Length - 2), out numericNric))
            {
                return false;
            }

            while (numericNric != 0)
            {
                total += numericNric % 10 * Multiples[Multiples.Length - (1 + count++)];

                numericNric /= 10;
            }

            char[] outputs;
            // first S, pickup different array( read specification)
            if (first == 'S')
            {
                outputs = new char[] { 'J', 'Z', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A' };
            }
            // T pickup different arrary ( read specification)
            else
            {
                outputs = new char[] { 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'J', 'Z', 'I', 'H' };
            }

            return last == outputs[total % 11];

        }




    }
}
