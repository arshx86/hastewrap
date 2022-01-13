# hastewrap
Hastebin Wrapper Library

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
string UploadLink = Hastebin.CreateHaste("Hello from hastewrap!");
Console.WriteLine(UploadLink);
```

The `Hastebin.CreateHaste();` will return link of uploaded data.

> You can also read the whole content of text file then upload it with this code.

```csharp
using HasteWRAP;

var Hastebin = new HasteBIN();
string UploadLink = Hastebin.CreateHasteFromFile("C:\\somefile.txt");
Console.WriteLine(UploadLink);
```

## Reading text 

You can also use hastewrap to read text from hastebin links.

```csharp
using HasteWRAP;

var Hastebin = new HasteBIN();
string ReadedText = Hastebin.GetHaste("https://www.toptal.com/developers/hastebin/karonigili.py"); // or you can pass only the code
Console.WriteLine(ReadedText);
```

## Config Support

HasteWRAP also supports custom proxy & user agent properties. 

```csharp
var Hastebin = new HasteBIN(new Config() // Pass null or leave as empty to use default config.
{
  Proxy = "192.168.1.1:8080",
  UserAgent = "Custom User Agent"
});
```

## Packages

Library made with [Leaf.xNet](https://github.com/csharp-leaf/Leaf.xNet) library.
