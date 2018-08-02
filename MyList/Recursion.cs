//namespace MyList
//{
//    using System;
//    using System.Collections;
//    using System.Collections.Generic;

//    public abstract class Recursion<T> : IRecursion<T>
//    {
//        //protected T _item { get; private set; }

//        //public Recursion(T item)
//        //{
//        //    _item = item;
//        //}

//        public abstract IEnumerator<T> GetEnumerator();
//        public virtual void ListRecursively(Action<T> action, Action<Exception> enqueueException)
//        {
//            void recursion(T localDirectory) => ((IRecursion<T>)localDirectory).ListRecursively(action, enqueueException);

//            //action(_item);
//            ((IEnumerable<T>)this).List(recursion, enqueueException);
//        }

//        //protected virtual void ListRecursively(T item, Action<T> action, Action<Exception> enqueueException)
//        //{
//        //    void recursion(T localDirectory) => ((IRecursion<T>)localDirectory).ListRecursively(action, enqueueException);

//        //    action(item);
//        //    ((IEnumerable<T>)this).List(recursion, enqueueException);
//        //}

//        IEnumerator<IRecursion<T>> IEnumerable<IRecursion<T>>.GetEnumerator()
//        {
//            foreach (var item in (IEnumerable<T>) this)
//                yield return (IRecursion<T>)item;
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }
//}
