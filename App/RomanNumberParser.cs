using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class RomanNumberParser
    {
        public static RomanNumber Parse(string input)
        {
            CheckSymbols(input);


            String argValue = input;
            input = input.Trim();

            CheckFormat(input);
            CheckSequence(input);

            int prevValue = 0;
            int result = 0;
            int position = 0;
            foreach (char c in input)
            {
                int currentValue = DigitalValue(c);
                //validate number or thorw excption

                if (currentValue > prevValue)
                {
                    result += currentValue - 2 * prevValue;
                }
                else
                {
                    result += currentValue;
                }
                if (prevValue != 0 && prevValue < currentValue)
                {
                    //проверка на правильность последовательности символов в парах (например, как в II(правильно) и в IL(не правильно))
                    if (currentValue / prevValue > 10 || prevValue.ToString()[0] == '5')
                    {
                        throw new FormatException($"RomanNumber.Parse('{argValue}') error: '{input[position - 1]} before {c}' in position position {position - 1}");
                    }
                }

                if (currentValue == 0 && input.Length > 0)
                {
                    throw new FormatException($"RomanNumber.Parse('{argValue}') error: 'digit 'N' must not be in number' in position {position}");
                }

                prevValue = currentValue;
                position++;
            }
            return new RomanNumber(result);
        }

        public static int DigitalValue(char digit) => digit switch
        {
            'N' => 0,
            'I' => 1,
            'V' => 5,
            'X' => 10,
            'L' => 50,
            'C' => 100,
            'D' => 500,
            'M' => 1000,
            _ => throw new ArgumentException($"'RomanNumberParser.DigitalValue': argument 'digit' has invalid value '{digit}'") { Source = "RomanNumberParser.DigitalValue" }
        };

        private static void CheckFormat(String input)
        {
            int maxDigit = 0;
            bool wasMaxRepeat = false;
            int pos = input.Length;
            // foreach(char c in input.Reverse())
            for (int i = input.Length - 1; i >= 0; i--)
            {
                char c = input[i];
                int digit = DigitalValue(c);
                if (digit > maxDigit)
                {
                    maxDigit = digit;
                    wasMaxRepeat = false;
                }
                else if (digit == maxDigit)
                {
                    wasMaxRepeat = true;
                }
                else if (wasMaxRepeat)
                {
                    throw new FormatException($"RomanNumber.Parse('{input}') error invalid format: '{c}' misplaced at position {i} ");
                }

            }
        }

        private static void CheckSequence(string input)
        {
            List<char> repetableSymbols = new List<char>() { 'I', 'X', 'C', 'M' };
            List<char> unrepeatableSymbols = new List<char>() { 'V', 'L', 'D' };
            int lessCount = 0;

            for (int j = 0; j < input.Length; j++)
            {
                int position = j;
                int currentValue = DigitalValue(input[j]);
                for (int i = 0; i < position; i++)
                {
                    if (DigitalValue(input[i]) < currentValue)
                    {
                        lessCount++;
                    }
                    else break;
                    if (lessCount > 1)
                    {
                        throw new FormatException($"RomanNumber.Parse({input}) error: more than one less digit before {input[position]} at position {position}. Invalid input {input}");
                    }
                }
                lessCount = 0;
            }
        }

        private static void CheckSymbols(String input)
        {
            String argValue = input;
            input = input.Trim();
            for (int i = 0; i < input.Length; i++)
            {
                try
                {
                    DigitalValue(input[i]);
                }
                catch
                {
                    throw new FormatException($"RomanNumber.Parse: invalid input '{argValue}' has illegal char '{input[i]}' at position {i}");
                }
            }
        }
    }
}
