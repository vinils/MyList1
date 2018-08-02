namespace MyList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    public class SystemIoDirectory : IRecursion<SystemIoDirectory>, IRecursion<DirectoryInfo>, IEnumerable<FileInfo>
    {
        private DirectoryInfo _directoryInfo = null;

        protected DirectoryInfo DirectoryInfo
        {
            get => _directoryInfo ?? new DirectoryInfo(Path);
            private set => _directoryInfo = value;
        }

        public string Path { get; private set; }

        SystemIoDirectory IRecursion<SystemIoDirectory>.Item
            => DirectoryInfo;

        DirectoryInfo IRecursion<DirectoryInfo>.Item
            => this;

        protected SystemIoDirectory(DirectoryInfo directoryInfo)
        {
            this.DirectoryInfo = directoryInfo;
            this.Path = directoryInfo.FullName;
        }

        public void ListAllParallel(Action<DirectoryInfo> action, Action<Exception> enqueueException)
        {
            void castAction(SystemIoDirectory sysIoDir) => action(sysIoDir);
            IRecursion<SystemIoDirectory> recursion = this;
            recursion.ListAllParallel(castAction, enqueueException);
        }

        public void ListFilesParallel(Action<FileInfo> action, Action<Exception> enqueueException)
        {
            IEnumerable<FileInfo> enumerable = this;
            enumerable.ListParallel(action, enqueueException);
        }

        IEnumerator<IRecursion<SystemIoDirectory>> IEnumerable<IRecursion<SystemIoDirectory>>.GetEnumerator()
        {
            foreach (var dir in (IEnumerable<SystemIoDirectory>)this)
                yield return dir;
        }

        public IEnumerator<SystemIoDirectory> GetEnumerator()
        {
            foreach (var dir in (IEnumerable<DirectoryInfo>)this)
                yield return dir;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        IEnumerator<IRecursion<DirectoryInfo>> IEnumerable<IRecursion<DirectoryInfo>>.GetEnumerator()
        {
            foreach (var dir in (IEnumerable<SystemIoDirectory>)this)
                yield return dir;
        }

        IEnumerator<DirectoryInfo> IEnumerable<DirectoryInfo>.GetEnumerator()
        {
            foreach (var dir in DirectoryInfo.GetDirectories())
                yield return dir;
        }

        IEnumerator<FileInfo> IEnumerable<FileInfo>.GetEnumerator()
        {
            foreach (var file in _directoryInfo.GetFiles())
                yield return file;
        }

        public static implicit operator SystemIoDirectory(DirectoryInfo directoryInfo)
            => new SystemIoDirectory(directoryInfo);

        public static implicit operator DirectoryInfo(SystemIoDirectory directory)
            => directory._directoryInfo;
    }
}