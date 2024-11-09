
using AutoAggregates;


string workingDirectory = Directory.GetCurrentDirectory();
string startDirectory = workingDirectory;
var userInput = InputTypeUtility.EnglishOnly();
string targetFile = userInput; // نام فایل مورد نظر را وارد کنید
Search.FileInDirectories(startDirectory, targetFile);
Search.TraverseDirectories(startDirectory, targetFile);
Console.WriteLine("Press any key to exit. :)");
Console.ReadKey();
