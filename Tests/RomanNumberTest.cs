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
            Dictionary<String, int> testCases = new()
            {
                { "I", 1 },
                { "II", 2 },
                { "III", 3 },
                { "N", 0},
                { "V", 5},
                { "X", 10},
                { "L", 50},
                { "C", 100},
                { "M", 1000 },
            };
            foreach (var testCase in testCases) {
                Assert.AreEqual(testCase.Value, RomanNumber.Parse(testCase.Key).Value, $"Roman value {testCase.Value} -> {testCase.Key}");
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