using NumbersConverter.DigitsConverters;

namespace NumbersConverter.UnitTests
{
    public class ConverterTests
    {
        private Converter _converter = null!;

        [SetUp]
        public void Setup()
        {
            // Arrange
            var oneDigitConverter = new OneDigitConverter();
            var twoDigitsConverter = new TwoDigitsConverter(oneDigitConverter);
            var threeDigitsConverter = new ThreeDigitsConverter(oneDigitConverter,
                twoDigitsConverter);

            _converter = new Converter(threeDigitsConverter, twoDigitsConverter);
        }

        [Test]
        public void Convert_ValidInput_ConvertedAsExpected()
        {
            // Arrange
            var samples = new (string, string)[8];
            samples[0] = ("4", "FOUR DOLLARS");
            samples[1] = ("19.40", "NINETEEN DOLLARS AND FORTY CENTS");
            samples[2] = ("123087", "ONE HUNDRED AND TWENTY-THREE THOUSAND AND EIGHTY-SEVEN DOLLARS");
            samples[3] = ("774.15", "SEVEN HUNDRED AND SEVENTY-FOUR DOLLARS AND FIFTEEN CENTS");
            samples[4] = ("3120000100", "THREE BILLION AND ONE HUNDRED AND TWENTY MILLION AND ONE HUNDRED DOLLARS");
            samples[5] = ("1.01", "ONE DOLLAR AND ONE CENT");
            samples[6] = ("71681859077205028382502578945417750657811381671000000000000000000.45", "SEVENTY-ONE VIGINTILLION AND SIX HUNDRED AND EIGHTY-ONE NOVEMDECILLION AND EIGHT HUNDRED AND FIFTY-NINE OCTODECILLION AND SEVENTY-SEVEN SEPTENDECILLION AND TWO HUNDRED AND FIVE SEXDECILLION AND TWENTY-EIGHT QUINDECILLION AND THREE HUNDRED AND EIGHTY-TWO QUATTUORDECILLION AND FIVE HUNDRED AND TWO TREDECILLION AND FIVE HUNDRED AND SEVENTY-EIGHT DUODECILLION AND NINE HUNDRED AND FORTY-FIVE UNDECILLION AND FOUR HUNDRED AND SEVENTEEN DECILLION AND SEVEN HUNDRED AND FIFTY NONILLION AND SIX HUNDRED AND FIFTY-SEVEN OCTILLION AND EIGHT HUNDRED AND ELEVEN SEPTILLION AND THREE HUNDRED AND EIGHTY-ONE SEXTILLION AND SIX HUNDRED AND SEVENTY-ONE QUINTILLION DOLLARS AND FORTY-FIVE CENTS");
            samples[7] = ("0", "ZERO DOLLARS");
            samples[7] = ("0.50", "ZERO DOLLAR AND FIFTY CENTS");
 
            foreach (var (number, expected) in samples)
            {
                // Act
                var result = _converter.Convert(number);

                // Assert
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [Test]
        public void Convert_InvalidInput_ThrowsException()
        {
            // Arrange
            var samples = new string[7];
            samples[0] = "4.";
            samples[1] = "19A";
            samples[2] = "87.456";
            samples[3] = "774.A";
            samples[4] = "312.42A";
            samples[5] = "A560.10";
            samples[6] = "560.10.14";

            foreach (var number in samples)
            {
                // Assert
                Assert.Throws<ArgumentException>(() => _converter.Convert(number));
            }
        }

        [Test]
        public void Convert_SixtyFiveDigitsNumber_ConvertedSuccess()
        {
            // Arrange
            var samples = new string[6];
            for (var i = 0; i < 6; i++)
            {
                samples[i]
                    = $"{Random.Shared.NextInt64(long.MaxValue)}{Random.Shared.NextInt64(long.MaxValue)}{Random.Shared.Next(int.MaxValue)}"
                        .PadRight(126, '0') + $".{Random.Shared.Next(0,99)}";
            }

            foreach (var number in samples)
            {
                // Act
                var output = _converter.Convert(number);

                // Assert
                Assert.That(output, Is.TypeOf(typeof(string)));
            }
        }

        [Test]
        public void Convert_MoreThanSixtyFiveDigitsNumber_ThrowsException()
        {
            // Arrange
            var samples = new string[6];
            for (var i = 0; i < 6; i++)
            {
                samples[i] = $"{Random.Shared.NextInt64(long.MaxValue)}{Random.Shared.NextInt64(long.MaxValue)}{Random.Shared.NextInt64(long.MaxValue)}"
                    .PadRight(Random.Shared.Next(127, 150), '0') + $".{Random.Shared.Next(0,99)}";
            }

            foreach (var number in samples)
            {
                // Assert
                Assert.Throws<NotSupportedException>(() => _converter.Convert(number));
            }
        }
    }
}