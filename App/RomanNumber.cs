using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public record RomanNumber(int Value)
    {
        // public int Value { get; } = Value;
        public static RomanNumber Parse(string input)
        {
            CheckSymbols(input);


            String argValue = input;
            input = input.Trim();

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

                //!--------------------------------------------------------------------------------------------------------

                List<char> repetableSymbols = new List<char>() { 'I', 'X', 'C', 'M' };
                List<char> unrepeatableSymbols = new List<char>() { 'V', 'L', 'D' };
                int lessCount = 0;

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
            _ => throw new ArgumentException($"'RomanNumber.DigitalValue': argument 'digit' has invalid value '{digit}'") { Source = "RomanNumber.DigitalValue" }
        };

        public static void CheckSymbols(String input)
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