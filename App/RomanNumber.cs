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
            int prevValue = 0;
            int result = 0;
            int position = 0;
            foreach(char c in input)
            {
                try
                {
                   int currentValue = DigitalValue(c);
                    if (currentValue >prevValue)
                    {
                        result += currentValue - 2 * prevValue;
                    }
                    else
                    {
                        result += currentValue;
                    }
                    prevValue = currentValue;
                }
                catch
                {
                    throw new FormatException($"RomanNumber.Parse: invalid input {input} has illegal char '{c}' at position {position}");
                }
                position++;
            }
           return new RomanNumber(result);
        }

        public static int DigitalValue(char digit) => digit switch
        {
            'N'=> 0,
            'I' => 1,
            'V' => 5,
            'X' => 10,
            'L' => 50,
            'C' => 100,
            'D' => 500,
            'M' => 1000,
            _ => throw new ArgumentException($"'RomanNumber.DigitalValue': argument 'digit' has invalid value '{digit}'") { Source= "RomanNumber.DigitalValue" }
        };
    }
}
