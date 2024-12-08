public static class ListExt
{

    public static bool NextValidator<T>(this List<T> numbers, Func<T, T, bool> nextValidator)
    {
        bool result = true;
        for (var i = 0; i < numbers.Count - 1; i++)
        {
            T current = numbers[i];
            T next = numbers[i + 1];
            result &= nextValidator(current, next);
            if (!result)
                return result;
        }
        return result;
    }

    public static List<T> WithoutIndex<T>(this List<T> list, int idx)
    {
        return [.. list[..idx].Concat(list[(idx + 1)..])];
    }

    public static IEnumerable<List<T>> VariationsWithoutOneElement<T>(this List<T> list)
    {
        return Enumerable.Range(0, list.Count).Select(list.WithoutIndex);
    }

    public static IEnumerable<List<T>> OrderingVariations<T>(this List<T> list)
    {
        return Enumerable.Range(0, list.Count).Select(list.WithoutIndex);
    }

    public static bool IsOrdered<T>(this List<T> list)
    {
        return list.Order().SequenceEqual(list) || list.Order().Reverse().SequenceEqual(list);
    }

    public static void Swap(this List<int> toSwap, int idx1, int idx2)
    {
        var toInsert = toSwap[idx2];
        toSwap.RemoveAt(idx2);
        toSwap.Insert(idx1, toInsert);
    }

    public static void PrintMatrix(this List<string> lines)
    {
        foreach (var line in lines)
        {
            Console.WriteLine(string.Join("", line.Select(c => c + " ")));
        }
    }

    public static ICollection<ICollection<T>> Permutations<T>(this ICollection<T> list)
    {
        var result = new List<ICollection<T>>();
        if (list.Count == 1)
        { // If only one possible permutation
            result.Add(list); // Add it and return it
            return result;
        }
        foreach (var element in list)
        { // For each element in that list
            var remainingList = new List<T>(list);
            remainingList.Remove(element); // Get a list containing everything except of chosen element
            foreach (var permutation in Permutations<T>(remainingList))
            { // Get all possible sub-permutations
                permutation.Add(element); // Add that element
                result.Add(permutation);
            }
        }
        return result;
    }



    public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IList<T> list, int length)
    {
        var numberOfCombinations = (long)Math.Pow(list.Count, length);
        for (long i = 0; i < numberOfCombinations; i++)
        {
            yield return BuildCombination(list, length, i);
        }
    }
    private static IEnumerable<T> BuildCombination<T>(
        IList<T> list,
        int length,
        long combinationNumber)
    {
        var temp = combinationNumber;
        for (int j = 0; j < length; j++)
        {
            yield return list[(int)(temp % list.Count)];
            temp /= list.Count;
        }
    }
}