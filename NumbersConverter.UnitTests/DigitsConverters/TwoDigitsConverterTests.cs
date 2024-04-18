using NumbersConverter.DigitsConverters;

namespace NumbersConverter.UnitTests.DigitsConverters
{
    public class TwoDigitConverterTests
    {
        private TwoDigitsConverter _twoDigitsConverter = null!;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _twoDigitsConverter = new TwoDigitsConverter(new OneDigitConverter());
        }

        [Test]
        public void Convert_ValidNumbers_ConvertedAsExpected()
        {
            // Arrange
            var samples = new (byte, string)[4];
            samples[0] = (4, "FOUR");
            samples[1] = (19, "NINETEEN");
            samples[2] = (87, "EIGHTY-SEVEN");
            samples[3] = (50, "FIFTY");

            foreach (var (number, expected) in samples)
            {
                // Act
                var result = _twoDigitsConverter.Convert(number);

                // Assert
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [Test]
        public void Convert_InvalidNumbers_ThrowExceptions()
        {
            // Arrange
            var samples = new byte[3];
            samples[0] = 100;
            samples[1] = 255;
            samples[2] = 182;

            foreach (var number in samples)
            {
                // Assert
                Assert.Throws<NotSupportedException>(()=>_twoDigitsConverter.Convert(number));
            }
        }

        [Test]
        public void CanConvert_SmallerThan100_ReturnsTrue()
        {
            for (byte i = 0; i < 100; i += (byte)Random.Shared.Next(15))
            {
                // Act
                var result = _twoDigitsConverter.CanConvert(i);

                // Assert
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void CanConvert_BiggerThan100_ReturnsFalse()
        {
            for (byte i = 100; i < 200; i += (byte)Random.Shared.Next(15))
            {
                // Act
                var result = _twoDigitsConverter.CanConvert(i);

                // Assert
                Assert.IsFalse(result, $"Input {i}");
            }
        }
    }
}