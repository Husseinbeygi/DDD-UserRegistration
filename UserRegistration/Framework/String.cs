namespace Framework
{
	public static class String
	{
		public static string Fix(this string text)
		{
			if (text is null)
			{
				return null;
			}

			text =
				text.Trim();

			if (text == string.Empty)
			{
				return null;
			}

			while (text.Contains("  "))
			{
				text =
					text.Replace("  ", " ");
			}

			return text;
		}
	}
}