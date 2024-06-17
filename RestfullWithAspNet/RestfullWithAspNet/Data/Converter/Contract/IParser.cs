namespace RestfullWithAspNet.Data.Converter.Contract
{
    /// <summary>
    /// Represents a generic interface for parsing objects.
    /// </summary>
    /// <typeparam name="O">The type of the original object.</typeparam>
    /// <typeparam name="D">The type of the destination object.</typeparam>
    public interface IParser<O, D>
    {
        /// <summary>
        /// Parses the original object into a destination object.
        /// </summary>
        /// <param name="origin">The original object to be parsed.</param>
        /// <returns>The parsed destination object.</returns>
        D Parse(O origin);

        /// <summary>
        /// Parses a list of original objects into a list of destination objects.
        /// </summary>
        /// <param name="origin">The list of original objects to be parsed.</param>
        /// <returns>The list of parsed destination objects.</returns>
        List<D> Parse(List<O> origin);
    }
}
