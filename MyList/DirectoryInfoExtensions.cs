//namespace MyList
//{
//    using System;
//    using System.IO;
//    using System.Threading.Tasks;

//    public static class DirectoryInfoExtension
//    {
//        public static void List(this DirectoryInfo directory, Action<DirectoryInfo> action)
//        {
//            action(directory);

//            MyList.List(directory.GetDirectories(), action);
//        }

//        public static void ListRecursively(this DirectoryInfo directory, Action<DirectoryInfo> action, Func<DirectoryInfo, bool> where = null)
//        {
//            action(directory);

//            void recursion(DirectoryInfo localDirectory) => localDirectory.ListRecursively(action, where);

//            //System.Threading.Tasks.Parallel.ForEach(directory.GetDirectories(), recursion);
//            MyList.List(directory.GetDirectories(), recursion);
//        }

//        public static void ListFiles(this DirectoryInfo directory, Action<FileInfo> action, Func<DirectoryInfo, bool> where)
//        {
//            try
//            {
//                Parallel.ForEach(directory.GetFiles(), action);
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }

//        public static void ListFilesRecursively(this DirectoryInfo directory, Action<FileInfo> action, Func<DirectoryInfo, bool> where)
//        {
//            void listFiles(DirectoryInfo localDirectory)
//            { localDirectory.ListFiles(action, where); }

//            //System.Threading.Tasks.Parallel.ForEach(directory.GetFiles(), recursion);
//            ListRecursively(directory, listFiles, where);
//        }
//    }
//}
