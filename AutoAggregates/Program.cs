
using AutoAggregates;

using System.Reflection;

string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
string workingDirectory = Directory.GetCurrentDirectory();
Console.WriteLine("Enter Aggregate Name (and press Enter):");
//string startDirectory = exePath; // مسیر شروع را تنظیم کنید
string startDirectory = workingDirectory;
var userInput = FixAggregateName.SafeName(Console.ReadLine());
string targetFile = userInput; // نام فایل مورد نظر را وارد کنید
							   //Search.FileInDirectories(startDirectory, targetFile);
Search.TraverseDirectories(startDirectory, targetFile);
Console.WriteLine("Press any key to exit. :)");
Console.ReadKey();
