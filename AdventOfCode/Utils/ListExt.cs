public static class ListExt{

    public static bool NextValidator<T>(this List<T> numbers, Func<T,T,bool> nextValidator){
        bool result = true;
        for(var i = 0 ; i < numbers.Count - 1; i++){
            T current = numbers[i];
            T next = numbers[i + 1];
            result &= nextValidator(current, next);
            if(!result)
                return result;
        }
        return result;
    }

    public static List<T> WithoutIndex<T>(this List<T> list, int idx){
        return [.. list[..idx].Concat(list[(idx + 1)..])];
    }

    public static IEnumerable<List<T>> VariationsWithoutOneElement<T>(this List<T> list){
        return Enumerable.Range(0,list.Count).Select(list.WithoutIndex);
    }

    public static bool IsOrdered<T>(this List<T> list){
        return list.Order().SequenceEqual(list) || list.Order().Reverse().SequenceEqual(list);
    }
}