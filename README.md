# hastewrap
Hastebin Wrapper Library

# Cancelled

Hastebin now requires authentication token, which requires github account. It is pointless to use this library as now.

## Installation

Add "HasteWRAP.dll" to references.
```bash
Solution Explorer > References > Add Reference > Pick up "HasteWRAP.dll"
```

Add required usings.
```csharp
using HasteWRAP;
```

## Uploading text 

```csharp
using HasteWRAP;

var Hastebin = new HasteBIN();
string UploadLink = Hastebin.Create("Hello world!");
Console.WriteLine(UploadLink); // output: # https://hastebin.com/raw/xxx
```
> You can also read the whole content of text file then upload it with this code.

```csharp
using HasteWRAP;

var Hastebin = new HasteBIN();
string UploadLink = Hastebin.CreateFromFile("C:\\somefile.txt");
Console.WriteLine(UploadLink);
```

## Reading  

```csharp
using HasteWRAP;

var Hastebin = new HasteBIN();
string ReadedText = Hastebin.Get("https://hastebin.com/about.md"); // or Get("key")
Console.WriteLine(ReadedText);
```

## Proxy Support

```csharp
var Hastebin = new HasteBIN("192.168.1.1:8080");
```

## Packages

Library made with [Leaf.xNet](https://github.com/csharp-leaf/Leaf.xNet) library.
Referances to [here](https://hastebin.com/about.md)
