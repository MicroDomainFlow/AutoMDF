using Humanizer;

namespace AutoAggregates;

internal static class CreateFilesHelpers
{
	public static void CreateCsFileFromText(string aggregateName, AutoAggregates.TxtFileTypes txtFileName, string csFileName, string outPutPath, string? projectName)
	{
		string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
		string inputFilePath = Path.Combine(baseDirectory, "Files", txtFileName.ToString() + ".txt");

		string singularName = aggregateName.Singularize(inputIsKnownToBePlural: false);
		string pluralizeName = aggregateName.Pluralize(inputIsKnownToBeSingular: false);

		string outputFilePath = csFileName.EndsWith(".cs") ? csFileName : $"{csFileName}.cs";

		// خواندن محتوای فایل .txt
		string content = File.ReadAllText(inputFilePath);

		content = content.Replace("{Singular}", singularName);
		content = content.Replace("{Pluralize}", pluralizeName);
		if (!string.IsNullOrWhiteSpace(projectName))
		{
			content = content.Replace("{ProjectName}", projectName);
		}

		// ایجاد دایرکتوری خروجی در صورت عدم وجود
		if (!Directory.Exists(outPutPath))
		{
			Directory.CreateDirectory(outPutPath);
		}

		if (!File.Exists(Path.Combine(outPutPath, outputFilePath)))
		{
			// نوشتن محتوای تغییر یافته به فایل .cs
			File.WriteAllText(Path.Combine(outPutPath, outputFilePath), content);
		}
	}

	public static void CreateCsFileFromText(List<CreateCsFile> csFiles)
	{
		foreach (var item in csFiles)
		{
			CreateCsFileFromText(item.AggregateName, item.TxtFileName, item.CsFileName, item.OutPutPath, item.ProjectName);
		}
	}

	public static string CreateFolder(string folderName, string folderPath)
	{
		string pluralizeName = folderName.Pluralize(inputIsKnownToBeSingular: false);
		var path = Path.Combine(folderPath, pluralizeName);
		var directoryInfo = Directory.CreateDirectory(path);
		return directoryInfo.FullName;
	}

	public static List<string> CreateFolder(List<CreateFolder> folders)
	{
		List<string> result = new List<string>();
		foreach (var item in folders)
		{
			var path = CreateFolder(item.FolderName, item.FolderPath);
			result.Add(path);
		}
		return result;
	}

	public static string FindProjectNameFormDirectoryPath(string currentDirectory)
	{
		var names = currentDirectory.Split('\\').ToList();
		var projectName = names[5];
		return projectName;
	}
}

public record CreateCsFile(string AggregateName, AutoAggregates.TxtFileTypes TxtFileName, string CsFileName, string OutPutPath, string? ProjectName);

public record CreateFolder(string FolderName, string FolderPath);