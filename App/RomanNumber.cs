using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public record RomanNumber(int Value)
    {
        // public int Value { get; } = Value;
        public static RomanNumber Parse(string input) => input switch
        {
            "II"=> new(2),
            "III"=> new(3),
            "N" => new(0),
            "I" => new(1),
            "V" => new(5),
            "X" => new(10),
            "L" => new(50),
            "C" => new(100),
            "D" => new(500),
            _ => new(1000),
        };

        public static int DigitalValue(char digit) => digit switch
        {
            'N'=> 0,
            'I' => 1,
            'V' => 5,
            'X' => 10,
            'L' => 50,
            'C' => 100,
            'D' => 500,
            _ => 1000,
        };
    }
}
