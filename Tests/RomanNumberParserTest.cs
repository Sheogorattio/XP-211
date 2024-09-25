using App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class RomanNumberParserTest
    {
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
                Assert.AreEqual(_testCase.Value, RomanNumberParser.DigitalValue(_testCase.Key), $"DigitalValue('{_testCase.Value}')->{_testCase.Value}");
            }

            var ex_source = new List<string>();
            char[] ExcludedSymbols = { 'I', 'N', 'V', 'X', 'L', 'C', 'D', 'M' };
            char[] AllowedSymbols = Enumerable.Range(0, 255)
                                   .Select(c => (char)c)
                                   .Where(c => !ExcludedSymbols.Contains(c))
                                   .ToArray();
            var rand = new Random();
            var ex_cases = new List<char>();
            for (int i = 0; i < 100; i++)
            {
                ex_cases.Add(AllowedSymbols[rand.Next(AllowedSymbols.Length)]);
            }
            foreach (var testCase in ex_cases)
            {
                var ex = Assert.ThrowsException<ArgumentException>(() => RomanNumberParser.DigitalValue(testCase), $"DigitalValue('{testCase}') must throw ArgumentException");
                Assert.IsFalse(String.IsNullOrEmpty(ex.Message), "ex.Message must not be empty");
                Assert.IsTrue(ex.Message.Contains($"invalid value '{testCase}'") && ex.Message.Contains("argument 'digit'"), $"ex.Message must contain param name ('digit') and its value '{testCase}'. ex.Messaage: '{ex.Message}'");
                Assert.IsTrue(ex.Message.Contains("'RomanNumberParser.DigitalValue'"), $"ex.Message must contain origin (class and method): '{ex.Message}'");
                Assert.IsTrue(ex.Source?.Contains("RomanNumberParser.DigitalValue"), $"ex.Source must contain origin (class and method): '{ex.Source}'");
            }
        }

        [TestMethod]
        public void CheckSymbolTest()
        {
            //private method test (CheckSymbols)

            var method = typeof(RomanNumberParser).GetMethod("CheckSymbols",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            Assert.IsNotNull(method, "CheckSymbols method is not accessible");
            //Assert.NotThrows
            method!.Invoke(null, ["IX"]);
            String[][] testCases =
            [
                ["IQ", "Q", "1"],
                ["IA", "A", "1"],
                ["IXF", "F", "2"],
                ["IGX", "G", "1"],
                ["CDX2", "2", "3"]
            ];

            foreach (var testCase in testCases)
            {
                var ex = Assert.ThrowsException<TargetInvocationException>(() => { method!.Invoke(null, [testCase[0]]);}, $"TargetInvocationException expected from Reflect-Invoke");
                Assert.IsInstanceOfType<FormatException>(ex.InnerException, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
            }
        }

        [TestMethod]
        public static void CheckSequenceTest(string input)
        {
            var method = typeof(RomanNumberParser).GetMethod("CheckSequence",
                                                                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(method, "CheckSequence method is not accessible");

            String[][] testCases = //декілька менших цифр ліворуч від більшої
            [
                ["IIV", "V","2"], // при цьому кожна ПАРА цифр  - нормальна
                ["CLM","M", "3"],
                ["CCM", "M","2"],
            ];
            foreach (var testCase in testCases)
            {
                var ex = Assert.ThrowsException<TargetInvocationException>(() => { RomanNumber.Parse(testCase[0]); }, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
                Assert.IsInstanceOfType<FormatException>(ex.InnerException, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
            }
        }

        [TestMethod]
        public static void CheckFormatTest(string input) {
            var method = typeof(RomanNumberParser).GetMethod("CheckFormat",
                                                                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(method, "CheckFormat method is not accessible");

            String[][] testCases = [
                ["IVIV", "I", "0"],
                ["CXCXC", "X", "1"],// !!кожна пара символів - правильна
                ["XCC", "I", "0"],  //  а також немає двох менших зліва
                ["IXXX", "I", "0"],
            ];
            foreach (var testCase in testCases)
            {
                var ex = Assert.ThrowsException<TargetInvocationException>(() => { RomanNumber.Parse(testCase[0]); }, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
                Assert.IsInstanceOfType<FormatException>(ex.InnerException, $"RomanNumber.Parse('{testCase[0]}') must throw FormatException");
            }
        }
    }
}
