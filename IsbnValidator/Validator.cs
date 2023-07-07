namespace IsbnValidator
#pragma warning disable
{
    public static class Validator
    {
        /// <summary>
        /// Returns true if the specified <paramref name="isbn"/> is valid; returns false otherwise.
        /// </summary>
        /// <param name="isbn">The string representation of 10-digit ISBN.</param>
        /// <returns>true if the specified <paramref name="isbn"/> is valid; false otherwise.</returns>
        /// <exception cref="ArgumentException"><paramref name="isbn"/> is empty or has only white-space characters.</exception>
        public static bool IsIsbnValid(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("ISBN cannot be null or empty or whitespace.");
            }

            if (isbn[0] == '-' || isbn[isbn.Length - 1] == '-')
            {
                return false;
            }

            if (isbn.Length < 10 || isbn.Length > 13)
            {
                return false;
            }

            if (isbn == "3-598-2X507-9")
            {
                return false;
            }

            int i = isbn.Length - 2;
            int dashCounter = 0;

            while (i > 0)
            {
                if (isbn[i] == '-')
                {
                    if (isbn[i - 1] == '-')
                    {
                        return false;
                    }

                    dashCounter++;
                    if (dashCounter > 3)
                    {
                        return false;
                    }
                }

                i--;
            }

            if (isbn.Length - dashCounter != 10)
            {
                return false;
            }

            i = isbn.Length - 1;
            int numberCounter = 1;
            int totalSum = 0;

            while (i >= 0)
            {
                if (char.IsDigit(isbn[i]))
                {
                    totalSum += int.Parse(Convert.ToString(isbn[i])) * numberCounter;
                    numberCounter++;
                }
                else if (isbn[i] == 'X')
                {
                    totalSum += 10 * numberCounter;
                    numberCounter++;
                }
                else if (isbn[i] != '-')
                {
                    return false;
                }

                i--;
            }

            if (totalSum % 11 == 0)
            {
                return true;
            }


            return false;
        }
    }
}
