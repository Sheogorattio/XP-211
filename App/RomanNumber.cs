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
        public static RomanNumber Parse(string input) => RomanNumberParser.Parse(input);
    }
}