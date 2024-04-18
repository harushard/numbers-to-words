namespace NumbersConverter;

public interface IConverter
{
    /// <summary>
    /// Converts number input to words
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    string Convert(string input);
}