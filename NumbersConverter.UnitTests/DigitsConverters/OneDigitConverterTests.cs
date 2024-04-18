using NumbersConverter.DigitsConverters;

namespace NumbersConverter.UnitTests.DigitsConverters
{
    public class OneDigitConverterTests
    {
        private OneDigitConverter _oneDigitConverter = null!;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _oneDigitConverter = new OneDigitConverter();
        }

        [Test]
        public void Convert_ValidNumbers_ConvertedAsExpected()
        {
            // Arrange
            var samples = new (byte, string)[3];
            samples[0] = (4, "FOUR");
            samples[1] = (9, "NINE");
            samples[2] = (7, "SEVEN");

            foreach (var (number, expected) in samples)
            {
                // Act
                var result = _oneDigitConverter.Convert(number);

                // Assert
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [Test]
        public void Convert_InvalidNumbers_ThrowExceptions()
        {
            // Arrange
            var samples = new byte[3];
            samples[0] = 10;
            samples[1] = 25;
            samples[2] = 156;

            foreach (var number in samples)
            {
                // Assert
                Assert.Throws<NotSupportedException>(()=>_oneDigitConverter.Convert(number));
            }
        }

        [Test]
        public void CanConvert_SmallerThan9_ReturnsTrue()
        {
            for (byte i = 0; i < 10; i++)
            {
                // Act
                var result = _oneDigitConverter.CanConvert(i);

                // Assert
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void CanConvert_BiggerThan9_ReturnsFalse()
        {
            for (byte i = 10; i < 20; i++)
            {
                // Act
                var result = _oneDigitConverter.CanConvert(i);

                // Assert
                Assert.IsFalse(result);
            }
        }
    }
}