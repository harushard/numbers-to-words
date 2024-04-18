using NumbersConverter.DigitsConverters;
using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;

namespace NumbersConverter;

internal class Converter
    : IConverter
{
    private readonly ThreeDigitsConverter _threeDigitsConverter;
    private readonly TwoDigitsConverter _twoDigitsConverter;
    private readonly ImmutableArray<string> _groupNames;
    private readonly Regex _validator;

    public Converter(ThreeDigitsConverter threeDigitsConverter, TwoDigitsConverter twoDigitsConverter)
    {
        _threeDigitsConverter = threeDigitsConverter;
        _twoDigitsConverter = twoDigitsConverter;
        _validator = new Regex(@"^\$?[0-9]+(\.[0-9]{1,2})?$");

        // groups name begin with THOUSAND for first 4-6 digits
        // subsequence group name is for +3 more digits number from previous group
        _groupNames = new[]
        {
            "THOUSAND", // 4-6
            "MILLION", // 7-9
            "BILLION", // 10-12
            "TRILLION",
            "QUADRILLION",
            "QUINTILLION",
            "SEXTILLION",
            "SEPTILLION",
            "OCTILLION",
            "NONILLION",
            "DECILLION",
            "UNDECILLION",
            "DUODECILLION",
            "TREDECILLION",
            "QUATTUORDECILLION",
            "QUINDECILLION",
            "SEXDECILLION",
            "SEPTENDECILLION",
            "OCTODECILLION",
            "NOVEMDECILLION",
            "VIGINTILLION"
        }.ToImmutableArray();
    }

    public string Convert(string input)
    {
        // validate the input against regex
        var match = _validator.Match(input.Trim());

        if (!match.Success)
            throw new ArgumentException("Invalid input");

        var wordBuilder = new StringBuilder();

        var splits = match.Value.Split('.');

        // dollars portion
        if (splits.Length > 0)
        {
            var dollarSpan = splits[0].AsSpan();
            var spanLength = dollarSpan.Length;

            // split number into groups, one group with max size of 3
            // enumerate from last digit to the first.
            // use Stack as we are using Last In First Out strategy
            var groups = new Stack<string>();
            var group = new Stack<char>(3);
            for (var i = 0; i < spanLength; i++)
            {
                group.Push(dollarSpan[spanLength - 1 - i]);

                if (group.Count == 3 || i == spanLength - 1)
                {
                    groups.Push(new string(group.ToArray()));
                    group.Clear();
                }
            }

            var groupsLength = groups.Count;

            if (groupsLength - 1 > _groupNames.Length)
                throw new NotSupportedException($"Number is too big, only supported up to {_groupNames.Length * 3 + 2} digits number.");

            var groupNames = GetGroupNames(groupsLength);

            byte groupIndex = 0;

            while (groups.TryPop(out var outGroup) && ushort.TryParse(outGroup, out var parseGroup))
            {
                var isLastGroup = groupIndex == groupsLength - 1 || groupsLength == 1;

                if (parseGroup > 0 || (isLastGroup && groupsLength == 1))
                {
                    if (groupIndex > 0)
                    {
                        wordBuilder.Append(" AND ");
                    }

                    wordBuilder.Append($"{_threeDigitsConverter.Convert(parseGroup)}");

                    // append group name to converted string, except for last one
                    if (!isLastGroup)
                    {
                        wordBuilder.Append($" {groupNames[groupIndex]}");
                    }
                }

                groupIndex++;
            }

            // check if only a dollar or less
            var isOneDollarOrLess = groupsLength <= 1 && ushort.TryParse(dollarSpan, out var dollarValue) &&
                                    dollarValue <= 1;

            if (wordBuilder.Length > 0)
                wordBuilder.Append($" {(isOneDollarOrLess ? "DOLLAR" : "DOLLARS")}");
        }

        // cents portion
        if (splits.Length > 1)
        {
            if (wordBuilder.Length > 0) wordBuilder.Append(" AND ");

            // pad 0 with total length of 2, to cater one digit cent input
            // regex already validate for cents portion to be max 2 digits
            // parse the input, then convert it to words using two digits converter
            if (byte.TryParse(splits[1].PadRight(2, '0'), out var outCents))
                wordBuilder.Append($"{_twoDigitsConverter.Convert(outCents)} {(outCents > 1 ? "CENTS" : "CENT")}");
        }

        return wordBuilder.ToString();
    }

    private ImmutableArray<string> GetGroupNames(int totalGroups)
    {
        // minus one from the totalGroups, as total groups include Hundred group as well.
        // revers the list,
        return _groupNames[..(totalGroups - 1)]
            .Reverse()
            .ToImmutableArray();
    }
}