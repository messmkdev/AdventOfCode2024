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
}