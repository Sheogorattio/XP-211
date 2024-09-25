using App;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.ComponentModel;

namespace Tests
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void ParseTest()
        {
            const int maxValue = 4000;
            Dictionary<String, int> validCases = new()
            {
                {"IV", 4},
                {"IIII", 4},
                {"IX", 9},      
                {"XL", 40},     
                {"XC", 90},     
                {"CD", 400},   
                {"CM", 900},   
                {"XVI", 16},    
                {"LXV", 65},   
                {"XCIX", 99},  
                {"DCCC", 800},
                {"MMMCMXCIX", 3999 },
                {"MMXXIV", 2024} ,
                {"MMMM", 4000},
            };
            foreach (var validCase in validCases)
            {
                Assert.AreEqual(validCase.Value, RomanNumber.Parse(validCase.Key).Value, $"Valid Parser Test '{validCase.Key}' => {validCase.Value}");
            }

            String [][]testCases =
            [
                ["IW", "W", "1"],
                ["IS", "S", "1"],
                ["IXW", "W", "2"],
                ["IEX", "E", "1"],
                ["CDX1", "1", "3"]
            ];

            foreach(var testCase in testCases)
            {
                var ex = Assert.ThrowsException<FormatException>(() => { RomanNumber.Parse(testCase[0]); }, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
                Assert.IsTrue(ex.Message.Contains("RomanNumber.Parse"), $"ex.Message must contain origin (class and method):" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains($"{testCase[0]}"), $"ex.Message must contain input value '{testCase[0]}':" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains($"{testCase[1]}") && ex.Message.Contains($"position {testCase[2]}"),
                    $"ex.Message must contain error char '{testCase[1]}' and its position {testCase[2]}: " + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
            }

            String[][] testCases2 = //парні символи(недозволені)
            [
                ["IL", "I", "L", "0"],
                ["IC", "I", "C", "0"],
                ["ID", "I", "D", "0"],
                ["IM", "I", "M", "0"],
                ["XM", "X", "M", "0"],
                ["XD", "X", "D", "0"],
                ["VX", "V", "X", "0"],
                ["VM", "V", "M", "0"],

            ];
            foreach (var testCase in testCases2)
            {
                var ex = Assert.ThrowsException<FormatException>(() => { RomanNumber.Parse(testCase[0]); }, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
                Assert.IsTrue(ex.Message.Contains("RomanNumber.Parse"), $"ex.Message must contain origin (class and method):" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains($"{testCase[0]}"), $"ex.Message must contain input value '{testCase[0]}':" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains($"{testCase[1]} before {testCase[2]}") && ex.Message.Contains($"position {testCase[3]}"),
                    $"ex.Message must contain <'{testCase[1]}'> before <'{testCase[2]}> and error position {testCase[3]}: " + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
            }

            String[][] testCases3 = //неприпустимість цифри  0  у числах
            [
                ["VN","1"],
                ["NV","0"]
            ];
            foreach (var testCase in testCases3)
            {
                var ex = Assert.ThrowsException<FormatException>(() => { RomanNumber.Parse(testCase[0]); }, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
                Assert.IsTrue(ex.Message.Contains("RomanNumber.Parse"), $"ex.Message must contain origin (class and method):" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains($"{testCase[0]}"), $"ex.Message must contain input value '{testCase[0]}':" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains($"digit 'N' must not be in number") && ex.Message.Contains($"position {testCase[1]}"),
                    $"ex.Message must contain <digit 'N' must not be in number> and error position {testCase[1]}: " + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
            }


            String[][] testCases4 = //неприпустимість пробілів (не дозволяємо всередині числа)
           [
                ["V I","1"],
                ["X\tX","1"],
                ["X\nX","1"],
                ["X\0X","1"],
           ];
            foreach (var testCase in testCases4)
            {
                var ex = Assert.ThrowsException<FormatException>(() => { RomanNumber.Parse(testCase[0]); }, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
              
            }


            String[] testCases5 = //неприпустимість пробілів (не дозволяємо всередині числа)
           [
                "XX", "CC", "  CC", "    XX", "\tIV", "XI\t\t", "CD\n"
           ];
            foreach (var testCase in testCases5)
            {
                Assert.IsNotNull(RomanNumber.Parse(testCase),
                    "Ceiling and trailing spaces are allowed");
            }

            String[][] testCases6 = //контроль позицій з пробілами
           [
                ["    XSV", "S","1"],
                [" VC ","V", "0"],
           ];
            foreach (var testCase in testCases6)
            {
                var ex = Assert.ThrowsException<FormatException>(() => { RomanNumber.Parse(testCase[0]); }, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
                Assert.IsTrue(ex.Message.Contains($"{testCase[1]}") && ex.Message.Contains($"position {testCase[2]}"),
                                    $"ex.Message must contain error char '{testCase[1]}' and its position {testCase[2]}: " + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains("RomanNumber.Parse"), $"ex.Message must contain origin (class and method):" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains($"{testCase[0]}"), $"ex.Message must contain input value '{testCase[0]}':" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
            }

            String[][] testCases7 = //декілька менших цифр ліворуч від більшої
            [
                ["IIX", "X","2"], // при цьому кожна ПАРА цифр  - нормальна
                ["IVIX","X", "3"],
                ["IXCM", "C","2"],
                ["XLXC", "C","3"],
                ["IIV", "V","2"],
            ];
            foreach (var testCase in testCases7)
            {
                var ex = Assert.ThrowsException<FormatException>(() => { RomanNumber.Parse(testCase[0]); }, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
                Assert.IsTrue(ex.Message.Contains($"{testCase[1]}") && ex.Message.Contains($"position {testCase[2]}"),
                                    $"ex.Message must contain error char '{testCase[1]}' and its position {testCase[2]}: " + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains("RomanNumber.Parse"), $"ex.Message must contain origin (class and method):" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains($"{testCase[0]}"), $"ex.Message must contain input value '{testCase[0]}':" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains($"more than one less digit before {testCase[1]}"), $"ex.Message must contain input valeue 'more than one less digit before {testCase[1]}':" + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
            }
        }

        [TestMethod]
        public void DigitalValueTest()
        {
            Dictionary<char, int> testCases = new()
            {
                { 'N', 0},
                { 'I', 1},
                { 'V', 5},
                { 'X', 10},
                { 'L', 50},
                { 'C', 100},
                { 'D', 500},
                { 'M', 1000},
            };
            foreach (var _testCase in testCases)
            {
                Assert.AreEqual(_testCase.Value, RomanNumber.DigitalValue(_testCase.Key), $"DigitalValue('{_testCase.Value}')->{_testCase.Value}");
            }

            var ex_source = new List<string>();
            char[] ExcludedSymbols = { 'I', 'N', 'V', 'X', 'L', 'C', 'D', 'M' };
            char[] AllowedSymbols = Enumerable.Range(0,255)
                                   .Select(c => (char)c)
                                   .Where(c => !ExcludedSymbols.Contains(c))
                                   .ToArray();
            var rand = new Random();
            var ex_cases = new List<char>();
            for(int i =0; i < 100; i++)
            {
                ex_cases.Add(AllowedSymbols[rand.Next(AllowedSymbols.Length)]);
            }
            foreach(var testCase in ex_cases)
            {
                var ex = Assert.ThrowsException<ArgumentException>(() => RomanNumber.DigitalValue(testCase), $"DigitalValue('{testCase}') must throw ArgumentException");
                Assert.IsFalse(String.IsNullOrEmpty(ex.Message), "ex.Message must not be empty");
                Assert.IsTrue(ex.Message.Contains($"invalid value '{testCase}'")&& ex.Message.Contains("argument 'digit'"), $"ex.Message must contain param name ('digit') and its value '{testCase}'. ex.Messaage: '{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains("'RomanNumber.DigitalValue'"), $"ex.Message must contain origin (class and method): '{ex.Message}'");
                Assert.IsTrue(ex.Source?.Contains("RomanNumber.DigitalValue"), $"ex.Source must contain origin (class and method): '{ex.Source}'");
            }
        }
    }
}