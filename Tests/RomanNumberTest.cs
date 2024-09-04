using App;

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
            };
            foreach (var testCase in testCases)
            {
                Assert.AreEqual(testCase.Value, RomanNumber.DigitalValue(testCase.Key), $"DigitalValue('{testCase.Value}')->{testCase.Value}");
            }
            Assert.AreEqual(1, RomanNumber.DigitalValue('I'), "DigitalValue('I')-> 1");
        }
    }
}