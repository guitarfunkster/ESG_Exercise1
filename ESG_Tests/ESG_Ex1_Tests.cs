using ESG_Exercise1;

namespace ESG_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestEmptyString()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(Calculator.Add("") == 0);

        }

        [Test]
        public void TestAddZeroIntegers()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(Calculator.Add("") == 0);

        }

        [Test]
        public void TestAddOneInteger()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(Calculator.Add("1") == 1);

        }
        [Test]
        public void TestAddTwoIntegers()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(Calculator.Add("1,2") == 3);

        }

        [Test]
        public void TestAddUnknownNumbers()
        {
            //Arrange
            var testNumberParams = "1,2,3,4,5,6";
            var expected = 21;

            //Act
            //Assert
            Assert.True(Calculator.Add(testNumberParams) == expected);

        }

        [Test]
        public void TestAddNewLines()
        {
            //Arrange
            var testNumberParams = "1\n2,3,4,5,6";
            var expected = 21;
            //Act

            //Assert
            Assert.True(Calculator.Add(testNumberParams) == expected);
        }

        [Test]
        public void TestAddCustomDelimiter()
        {
            //Arrange
            var testNumberParams = "//;\n 1;2;3;4;5;6";
            var expected = 21;
            //Act
            //Assert
            Assert.True(Calculator.Add(testNumberParams) == expected);
        }

        [Test]
        public void TestExtractDelimiter()
        {
            //Arrange
            var testNumberParams = "//;\n 1;2;3;4;5;6";
            var expectedDilim = ";";

            //Act
            var delim = Calculator.ExtractDelimiter(testNumberParams);

            //Assert
            Assert.True(delim == expectedDilim);

        }

        [Test]
        public void TestAddThrowsExceptionNegative()
        {
            //Arrange
            var testNumberParams = "-1,2";

            //Act

            //Assert
            Assert.Throws<NegativesNotAllowedException>(() => Calculator.Add(testNumberParams));
        }

        [Test]
        public void TestAddNumbersGreaterThan100Ignored()
        {
            //Arrange
            var testNumberParams = "1001,2";
            var expected = 2;

            //Act
            var res = Calculator.Add(testNumberParams);

            //Assert
            Assert.True(res == expected);
        }

        [Test]
        public void TestExtractDelimitersAnyLength()
        {
            //Arrange
            var testDelims = "[|||]";
            var expected = new List<string>(); 
            expected.Add("|||");

            //Act
            var actual = Calculator.ExtractDelimiters(testDelims);

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestExtractMultipleDelimiters()
        {
            //Arrange
            var testDelims = "[|][%]";
            var expected = new List<string>();
            expected.Add("|");
            expected.Add("%");

            //Act
            var actual = Calculator.ExtractDelimiters(testDelims);

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void TestAddNumbersDelimitersAnyLength()
        {
            //Arrange
            var testNumberParams = "//[|||]\n1|||2|||3";
            var expected = 6;

            //Act
            var res = Calculator.Add(testNumberParams);

            //Assert
            Assert.True(res == expected);
        }

        [Test]
        public void TestAddNumbersMultipleDelimiters()
        {
            //Arrange
            var testNumberParams = "//[|][%]\n1|2%3";
            var expected = 6;

            //Act
            var res = Calculator.Add(testNumberParams);

            //Assert
            Assert.True(res == expected);
        }

        [Test]
        public void TestAddNumbersMultipleDelimitersOfAnyLength()
        {
            //Arrange
            var testNumberParams = "//[|||][%]\n1|||2%3";
            var expected = 6;

            //Act
            var res = Calculator.Add(testNumberParams);

            //Assert
            Assert.True(res == expected);
        }


    }
}