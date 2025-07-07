namespace _20250707_DELEGATES
{
    public static class IEnumerableExtensions
    {
        public static T GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
        {
            if (collection == null || !collection.Any() || convertToNumber == null)
                throw new ArgumentNullException(nameof(collection));

            T result = collection.First();
            float max = convertToNumber(result);


            foreach (var item in collection)
            {
                float numericValue = convertToNumber(item);
                if (max < numericValue)
                {
                    max = numericValue;
                    result = item;
                }
            }
            return result;
        }
    }
}
