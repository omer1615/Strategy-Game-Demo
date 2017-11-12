using System.Collections.Generic;

// extend for List Class to get next or previsous object of item
public class NavigationList<T> : List<T>
{

    public T GetNextByObject(T item)
    {
        int _index = IndexOf(item) >= Count - 1 ? 0 : IndexOf(item) + 1; 
        return this[_index];
    }
    
    public T GetPrevisousByObject(T item)
    {
        int _index = IndexOf(item) <= 0 ? Count - 1 : IndexOf(item) - 1;
        return this[_index];
    }

}