using NumbersConverter.DigitsConverters;

namespace NumbersConverter.UnitTests.DigitsConverters
{
    public class ThreeDigitsConverterTests
    {
        private ThreeDigitsConverter _threeDigitsConverter = null!;

        [SetUp]
        public void Setup()
        {
            // Arrange
            var oneDigitConverter = new OneDigitConverter();
            _threeDigitsConverter = new ThreeDigitsConverter(oneDigitConverter,
                new TwoDigitsConverter(oneDigitConverter));
        }

        [Test]
        public void Convert_NumberToWords_ConvertedAsExpected()
        {
            // Arrange
            var samples = new (ushort, string)[6];
            samples[0] = (4, "FOUR");
            samples[1] = (19, "NINETEEN");
            samples[2] = (87, "EIGHTY-SEVEN");
            samples[3] = (774, "SEVEN HUNDRED AND SEVENTY-FOUR");
            samples[4] = (312, "THREE HUNDRED AND TWELVE");
            samples[5] = (560, "FIVE HUNDRED AND SIXTY");

            foreach (var (number, expected) in samples)
            {
                // Act
                var result = _threeDigitsConverter.Convert(number);

                // Assert
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [Test]
        public void Convert_InvalidNumbers_ThrowExceptions()
        {
            // Arrange
            var samples = new ushort[3];
            samples[0] = 1000;
            samples[1] = 35550;
            samples[2] = 56820;

            foreach (var number in samples)
            {
                // Assert
                Assert.Throws<NotSupportedException>(() => _threeDigitsConverter.Convert(number));
            }
        }

        [Test]
        public void CanConvert_SmallerThan1000_ReturnsTrue()
        {
            for (ushort i = 200; i < 1000; i += (byte)Random.Shared.Next(15))
            {
                // Act
                var result = _threeDigitsConverter.CanConvert(i);

                // Assert
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void CanConvert_BiggerThan1000_ReturnsFalse()
        {
            for (ushort i = 1000; i < 1500; i += (byte)Random.Shared.Next(15))
            {
                // Act
                var result = _threeDigitsConverter.CanConvert(i);

                // Assert
                Assert.IsFalse(result, $"Input {i}");
            }
        }
    }
}