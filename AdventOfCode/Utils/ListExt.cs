public static class ListExt{

    public static bool NextValidator<T>(this List<T> numbers, Func<T,T,bool> nextValidator){
        bool result = true;
        for(var i = 0 ; i < numbers.Count - 1; i++){
            T current = numbers[i];
            T next = numbers[i + 1];
            result &= nextValidator(current, next);
        }
        return result;
    }

    public static List<T> WithoutIndex<T>(this List<T> list, int idx){
        return new List<T>(list.Slice(0,idx).Concat(list.Slice(idx + 1, list.Count -( idx + 1))));
    }

    public static List<List<T>> VariationsWithoutOneElement<T>(this List<T> list){
        List<List<T>> variations = new List<List<T>>();
        for (var i = 0; i < list.Count; i++)
        {            
            variations.Add(list.WithoutIndex(i));
        }
        return variations;
    }

    public static bool IsOrdered<T>(this List<T> list){
        return list.Order().SequenceEqual(list) || list.Order().Reverse().SequenceEqual(list);
    }
}