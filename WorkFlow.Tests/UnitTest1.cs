using NUnit.Framework;

namespace WorkFlow.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalculateWorkingDayTest()
        {
            Assert.Pass();
        }

        [TestCase(3, 3, 2, ExpectedResult = true)]
        [TestCase(3, 4, 2, ExpectedResult = true)]
        [TestCase(3, 5, 2, ExpectedResult = false)]
        [TestCase(3, 6, 2, ExpectedResult = false)]
        public bool CalculateWorkingDay(int workingday, int today, int workdays)
        {
            bool result = true;
            return (today - workingday) % (workdays+2) <= workdays - 1;
            return result;
        }
    }
}