namespace NumbersConverter.DigitsConverters;

internal class OneDigitConverter : BaseDigitsConverter<byte>
{
    protected override string DoConversion(byte input)
    {
        // get wording for one digit number
        return input switch
        {
            0 => "ZERO",
            1 => "ONE",
            2 => "TWO",
            3 => "THREE",
            4 => "FOUR",
            5 => "FIVE",
            6 => "SIX",
            7 => "SEVEN",
            8 => "EIGHT",
            9 => "NINE",
            _ => throw new NotSupportedException()
        };
    }

    protected override string UnableToConvertError => "Input must be more than 0 and less than 10";

    public override bool CanConvert(byte input) => input is >= 0 and < 10;
}