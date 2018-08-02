namespace MyList
{
    using System.Collections.Generic;

    public interface IRecursion<T> : IEnumerable<IRecursion<T>>, IEnumerable<T>
    {
        T Item { get; }
        //void ListRecursively(Action<T> action, Action<Exception> enqueueException);
        //void ListRecursively(Action<T> action);
    }
}
