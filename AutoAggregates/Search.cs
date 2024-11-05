namespace AutoAggregates;
internal static class Search
{
	public static void FileInDirectories(string currentDirectory, string targetFile)
	{
		// مشاهده تمام فایل‌ها در پوشه فعلی
		string[] files = Directory.GetFiles(currentDirectory);
		bool fileExists = false;

		foreach (string file in files)
		{
			if (Path.GetFileName(file) == targetFile)
			{
				Console.WriteLine($"file: {file} in {Path.GetDirectoryName(file)} exists.");
				fileExists = true; // فایل پیدا شد
				break; // خروج از حلقه
			}
		}

		// بررسی آیا پوشه‌ای هست که وارد نشده‌ای؟
		string[] directories = Directory.GetDirectories(currentDirectory);
		foreach (string directory in directories)
		{
			FileInDirectories(directory, targetFile);
		}

		// اگر فایل پیدا نشد، فایل‌های جدید ایجاد می‌شوند
		//if (!fileExists)
		//{
		//	bool success = CreateFiles.CreateAggregate(currentDirectory, targetFile);
		//	if (success)
		//	{
		//		Console.WriteLine($"file: {targetFile} created successfully in {currentDirectory}.");
		//	}
		//	else
		//	{
		//		Console.WriteLine($"failed to create file: {targetFile} in {currentDirectory}.");
		//	}
		//}
	}


	public static void TraverseDirectories(string path, string targetFile)
	{
		try
		{
			// نمایش پوشه فعلی
			//Console.WriteLine(path);
			// دریافت تمام فایل‌ها (اختیاری)
			//	string[] files = Directory.GetFiles(path);
			//foreach (var file in files)
			//{
			//	Console.WriteLine("  " + file); // نمایش فایل‌ها با یک فاصله اضافی برای تشخیص
			//}
			string[] files = Directory.GetFiles(path);
			if (!files.Contains(targetFile))
			{
				CreateFiles.CreateAggregate(path, targetFile);
			}
			// دریافت تمام زیرپوشه‌ها
			string[] directories = Directory.GetDirectories(path);
			foreach (var directory in directories)
			{
				TraverseDirectories(directory, targetFile); // پیمایش بازگشتی زیرپوشه‌ها
			}
		}
		catch (UnauthorizedAccessException)
		{
			Console.WriteLine("Access denied to: " + path); // در صورتی که دسترسی محدود باشد
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message); // مدیریت خطاهای دیگر
		}
	}
}

