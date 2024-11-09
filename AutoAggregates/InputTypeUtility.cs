using System;
using System.Linq;
using System.Text;

namespace AutoAggregates;
internal static class InputTypeUtility
{

	public static string EnglishOnly()
	{
		Console.OutputEncoding = Encoding.UTF8;
		Console.WriteLine("Enter Aggregate Name (English only and press Enter):");
		StringBuilder input = new StringBuilder();

		while (true)
		{
			ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

			// Check if the key is a valid English character or common symbol
			if (IsEnglish(keyInfo.KeyChar))
			{
				// Append the character to input and write to console
				input.Append(keyInfo.KeyChar);
				Console.Write(keyInfo.KeyChar);
			}
			else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
			{
				// Handle backspace
				input.Remove(input.Length - 1, 1);
				Console.Write("\b \b");
			}
			else if (keyInfo.Key == ConsoleKey.Enter)
			{
				// Handle enter key to end input
				return FixAggregateName.SafeName(input.ToString());
			}
		}
	}

	private static bool IsEnglish(char c)
	{
		// Check if the character is an English letter or common English symbol
		return c >= ' ' && c <= '~';
	}
}


