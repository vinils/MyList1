namespace MyList
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public static class MyListExtensions
    {
        public static void List<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
                return;

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in enumerable)
                action(item);
        }

        public static void List(this DirectoryInfo directoryInfo, Action<DirectoryInfo> action)
        {
            SystemIoDirectory dir = directoryInfo;
            dir.List(action);
        }

        public static void ListParallel<T>(
            this IEnumerable<T> enumerable, 
            Action<T> action, 
            Action<Exception> enqueueException)
        {
            if (enumerable == null)
                return;

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (enqueueException == null)
                throw new ArgumentNullException(nameof(enqueueException));

            Parallel.ForEach(enumerable, item =>
            {
                try
                {
                    action(item);
                }
                catch (Exception ex)
                {
                    enqueueException(ex);
                }
            });
        }

        public static void ListParallel(
            this DirectoryInfo directoryInfo, 
            Action<DirectoryInfo> action, 
            Action<Exception> enqueueException)
        {
            SystemIoDirectory dir = directoryInfo;
            dir.ListParallel(action, enqueueException);
        }

        public static void ListFilesParallel(
            this DirectoryInfo directoryInfo, 
            Action<FileInfo> action, 
            Action<Exception> enqueueException)
        {
            SystemIoDirectory dir = directoryInfo;
            dir.ListFilesParallel(action, enqueueException);
        }

        public static void ListAllParallel(
            this DirectoryInfo directoryInfo, 
            Action<DirectoryInfo> action, 
            Action<Exception> enqueueException)
        {
            SystemIoDirectory dir = directoryInfo;
            dir.ListAllParallel(action, enqueueException);
        }

        public static void ListParallelAggregatingExceptions<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            var exceptions = new ConcurrentBag<Exception>();

            enumerable.ListParallel(action, exceptions.Add);

            if (exceptions.Any())
                throw new AggregateException(exceptions);
        }

        public static Action<T> EnqueueExceptions<T>(this Action<T> action, Action<Exception> enqueueExceptions)
        {
            void actionEnqueuingExceptions(T item)
            {
                try
                {
                    action(item);
                }
                catch (Exception ex)
                {
                    enqueueExceptions(ex);
                }

            }

            return actionEnqueuingExceptions;
        }

        public static void ExecuteEnqueuingException<T>(this Action<T> action, T item, Action<Exception> enqueueException)
            => action.EnqueueExceptions(enqueueException)(item);

        public static void List<T>(this IEnumerable<T> enumerable, Action<T> action, Action<Exception> enqueueException)
        {
            foreach (var item in enumerable)
                action.ExecuteEnqueuingException(item, enqueueException);
        }

        public static void ListAggregatingExceptions<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            var exceptions = new ConcurrentBag<Exception>();

            enumerable.List(action, exceptions.Add);

            if (exceptions.Any())
                throw new AggregateException(exceptions);
        }

        public static void JoinEnqueueException<T>(this Action<Action<T>, Action<Exception>> thisAction, Action<T, Action<Exception>> paramAction, Action<Exception> enqueueException)
        {
            void joinEnqueueException(T item) => paramAction(item, enqueueException);
            thisAction(joinEnqueueException, enqueueException);
        }

        public static void ListAll<T>(this IRecursion<T> recursive, Action<T> action)
        {
            void recursion(T localDirectory) => ((IRecursion<T>)localDirectory).ListAll(action);

            action((T)recursive);

            IEnumerable<T> enumerable = recursive;

            enumerable.List(recursion);
        }

        public static void ListAll<T>(
            this IRecursion<T> recursive, 
            Action<T> action, 
            Action<Exception> enqueueException)
        {
            void recursion(T localDirectory) => ((IRecursion<T>)localDirectory).ListAll(action, enqueueException);

            action((T)recursive);

            IEnumerable<T> enumerable = recursive;

            enumerable.List(recursion, enqueueException);
        }

        public static void ListAllFilesParallel(
            this DirectoryInfo directories,
            Action<FileInfo> action,
            Action<Exception> enqueueException)
        {
            void listFiles(DirectoryInfo directory) => directory.ListFilesParallel(action, enqueueException);

            directories.ListAllParallel(listFiles, enqueueException);
        }

        public static void ListAllParallel<T>(
            this IRecursion<T> recursive, 
            Action<T> action, 
            Action<Exception> enqueueException)
        {
            void recursion(T localDirectory) => ((IRecursion<T>)localDirectory).ListAllParallel(action, enqueueException);

            action((T)recursive);

            IEnumerable<T> enumerable = recursive;

            enumerable.ListParallel(recursion, enqueueException);
        }
    }
}
