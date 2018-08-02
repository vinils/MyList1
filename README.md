# MyList (C#)

Directory is a tree that can list files faster in parallel.
This project was created to list items of a tree faster as possible.

It was able to list my 380888 directories in 16:53 seconds and 2093396 files in XXXX seconds

Was created as part of [MyComparer1](https://github.com/vinils/MyComparer1) project even though its can be used independently

## How to use:

### List all folders and subfolders in parallel

```csharp
using MyList;

public void Mehtod1()
{
    var dir = new System.IO.DirectoryInfo("U:\\");
    var directories = new ConcurrentBag<System.IO.DirectoryInfo>();
    var exceptions = new ConcurrentBag<Exception>();

    dir.ListAllParallel(directories.Add, exceptions.Add);
}
```

### List all files in folders and subfolders in parallel

```csharp
using MyList;
dir.ListAllFilesParallel(directories.Add, exceptions.Add);
```
 or
```csharp
MyList.SystemIoDirectory dir = new System.IO.DirectoryInfo("A:\\"); 
dir.ListAllFilesParallel(directories.Add, exceptions.Add);
```

### Looping folder tree below (or any inheritance of IRecursion)
```
A
└───B
│   └───D
│   │   └───H
│   │   └───I
│   └───E
│   │   └───J
│   │   └───L
└───C
│   └───F
│   │   └───M
│   │   └───N
│   └───G
│   │   └───O
│   │   └───P
```

 ```csharp
MyList.SystemIoDirectory dir = new System.IO.DirectoryInfo("A:\\"); 

foreach (var root in dir) // B, C
    foreach (var level1 in root) // D, E, F, G
        foreach (var level2 in level1) // H,I,J,L,M,N,O,P
        { }
```

