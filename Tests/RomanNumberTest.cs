using App;
using System;

namespace Tests
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void ParseTest()
        {
            //Dictionary<String, int> testCases = new()
            //{
            //   { "I", 1 },
            //   { "II", 2 },
            //   { "III", 3 },
            //    //{ "N", 0},
            //    //{ "V", 5},
            //    //{ "X", 10},
            //    //{ "L", 50},
            //    //{ "C", 100},
            //    //{ "M", 1000 },
            //};
            //foreach (var _testCase in testCases)
            //{
            //    Assert.AreEqual(_testCase.Value, RomanNumber.Parse(_testCase.Key).Value, $"Roman value {_testCase.Value} -> {_testCase.Key}");
            //}

            Dictionary<String, int> validCases = new()
            {
                {"IV", 4},    
                {"IX", 9},      
                {"XL", 40},     
                {"XC", 90},     
                {"CD", 400},   
                {"CM", 900},   
                {"XVI", 16},    
                {"LXV", 65},   
                {"XCIX", 99},  
                {"DCCC", 800},  
                {"MMXXIV", 2024} 
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
                Assert.IsTrue(ex.Message.Contains($"{testCase[1]}") && ex.Message.Contains($"position {testCase[2]}"), $"ex.Message must contain error char '{testCase[1]}' and its position {testCase[2]}: " + $"testCase='{testCase[0]}', ex.message='{ex.Message}'");
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