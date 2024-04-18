namespace NumbersConverter.DigitsConverters
{
    internal abstract class BaseDigitsConverter<T> where T : struct
    {
        /// <summary>
        /// Convert the input into string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public string Convert(T input)
        {
            // check if can convert
            if (CanConvert(input))
                return DoConversion(input);

            throw new NotSupportedException(UnableToConvertError);
        }

        protected abstract string UnableToConvertError { get; }

        /// <summary>
        /// Do conversion
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected abstract string DoConversion(T input);

        /// <summary>
        /// Can input be converted?
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public abstract bool CanConvert(T input);
    }
}