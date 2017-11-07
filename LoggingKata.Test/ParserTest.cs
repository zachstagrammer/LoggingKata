using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LoggingKata.Test
{
    [TestFixture]
    public class TacoParserTestFixture
    {
        [Test]
        public void ShouldNotParseLine()
        {
            //Arrange
            var tacoParser = new TacoParser();

            var lessThen3 = "1234, 1234";
            var longNotNumber = "abc, 1234, Testing";
            var latNotNumber = "123, abc, Testing";

            //Act

            var lessThen3Value = tacoParser.Parse(lessThen3);
            var longNotNumberValue = tacoParser.Parse(longNotNumber);
            var latNotNumberValue = tacoParser.Parse(latNotNumber);

            //Assert

            Assert.Null(lessThen3Value, lessThen3 + " should not parse");
            Assert.Null(longNotNumberValue, longNotNumber + " should not parse");
            Assert.Null(latNotNumberValue, latNotNumber + " should not parse");

        }
        
        [Test]
        public void ShouldParseLine()
        {
            //Arrange
            var tacoParser = new TacoParser();
            var correctString = "123.04, 456.07, Name of place";

            //Act

            var value = tacoParser.Parse(correctString);

            //Assert

            Assert.NotNull(value, correctString + " should parse into an ITrackable");
        }
    }
}
