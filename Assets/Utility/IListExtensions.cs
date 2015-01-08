namespace TheGoldenMule.Geo
{
    /// <summary>
    /// Extensions for IList.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Fills an array and returns it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="element"></param>
        public static T[] Fill<T>(this T[] @this, T element)
        {
            for (int i = 0, len = @this.Length; i < len; i++)
            {
                @this[i] = element;
            }
            return @this;
        }
    }
}