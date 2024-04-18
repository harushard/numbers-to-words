namespace NumbersConverter.DigitsConverters;

internal class ThreeDigitsConverter : BaseDigitsConverter<ushort>
{
    private readonly OneDigitConverter _oneDigitConverter;
    private readonly TwoDigitsConverter _twoDigitsConverter;

    public ThreeDigitsConverter(
        OneDigitConverter oneDigitConverter,
        TwoDigitsConverter twoDigitsConverter)
    {
        _oneDigitConverter = oneDigitConverter;
        _twoDigitsConverter = twoDigitsConverter;
    }

    protected override string DoConversion(ushort input)
    {
        // is 2 digits or less?
        if (input <= byte.MaxValue && _twoDigitsConverter.CanConvert((byte)input))
            return _twoDigitsConverter.Convert((byte)input);

        // find last 2 digits (remainder) from 3 digits number by using MOD operator
        var remainder = input % 100;

        // get first digit by minus the remainder then divide by 100
        var hundred = $"{_oneDigitConverter.Convert((byte)((input - remainder) / 100))} HUNDRED";

        // remainder should be less than 100 as this is only for 2 digits number
        // use two digits converter to get converted words
        return remainder > 0
            ? $"{hundred} AND {_twoDigitsConverter.Convert((byte)remainder)}"
            : hundred;
    }

    protected override string UnableToConvertError => "Input must be less than 1000";

    public override bool CanConvert(ushort input) => input < 1000;
}