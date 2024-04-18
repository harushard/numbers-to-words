namespace NumbersConverter.DigitsConverters;

internal class TwoDigitsConverter : BaseDigitsConverter<byte>
{
    private readonly OneDigitConverter _oneDigitConverter;

    public TwoDigitsConverter(OneDigitConverter oneDigitConverter)
    {
        _oneDigitConverter = oneDigitConverter;
    }

    protected override string DoConversion(byte input)
    {
        // is it a 1 digit?
        if (_oneDigitConverter.CanConvert(input))
            return _oneDigitConverter.Convert(input);

        // is it 2 digits and less than 20
        // get wording for number from 10 until 19
        if (input < 20)
            return input switch
            {
                10 => "TEN",
                11 => "ELEVEN",
                12 => "TWELVE",
                13 => "THIRTEEN",
                14 => "FOURTEEN",
                15 => "FIFTEEN",
                16 => "SIXTEEN",
                17 => "SEVENTEEN",
                18 => "EIGHTEEN",
                19 => "NINETEEN",
                _ => throw new NotSupportedException()
            };

        // get the second digit (remainder) of a 2 digits number by using MOD operator
        var remainder = input % 10;

        // get wording for ten numbers
        var ten = (input - remainder) switch
        {
            20 => "TWENTY",
            30 => "THIRTY",
            40 => "FORTY",
            50 => "FIFTY",
            60 => "SIXTY",
            70 => "SEVENTY",
            80 => "EIGHTY",
            90 => "NINETY",
            _ => throw new NotSupportedException()
        };

        // remainder should be less than 10 as this is only for 1 digit number
        // use one digit converter to get wording for remainder if it more than 0
        return remainder > 0
            ? $"{ten}-{_oneDigitConverter.Convert((byte)remainder)}"
            : ten;
    }

    protected override string UnableToConvertError => "Input must be less than 100";

    public override bool CanConvert(byte input) => input < 100;
}