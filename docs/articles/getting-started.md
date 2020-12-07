# Getting Started

> [!NOTE]
> This guide assumes you have already created a new .NET Console project.

To begin, please install the [Konayuki package](https://nuget.org/packages/Konayuki) from Nuget.

Include the `Konayuki` namespace by entering this at the top of your file:

```cs
using Konayuki;
```

We'll now create a new `Konayuki` instance in our `Main` method.

```cs
var ids = new Konayuki.Konayuki(0);
```

The first parameter (0) is the generator ID. This should be unique for every generator, but we'll only be using one in this very basic example.

Since `IKonayuki` inherits from `IEnumerable`, we can use methods like `.First()` and `.Take(int)`.

> [!WARNING]
> Because an `IKonayuki` is a never-ending stream of IDs, be careful not to use LINQ methods like `.Select()` or `.ForEach()`!

Now generate a token using `IEnumerable#First()`. Even though this seems like a bad idea, it is guaranteed that `Konayuki` will return a unique ID every time it is called.

```cs
Console.WriteLine(ids.First());
```

> [!NOTE]
> If you wish to generate more than one ID, you can use `ids.Take(x)` to take *x* IDs.

Now run the program, and you should see an output similar to this:

```cs
2224083802521600
```

Overall, your program should look something like this:

```cs
using System;
using System.Linq;
using Konayuki;

class Program
{
    static void Main()
    {
        var ids = new Konayuki.Konayuki(0);
        Console.WriteLine(ids.First());
    }
}
```
