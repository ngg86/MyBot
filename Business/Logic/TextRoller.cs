using Business.Texts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Business.Logic
{
	public static class TextRoller
	{
		public static string GetRandomErrorMessage()
		{
			var resources = Errors.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
			var errors = new List<string>();
			foreach (DictionaryEntry entry in resources)
			{
				errors.Add(entry.Value.ToString());
			}

			return errors.ElementAt(new Random().Next(0, errors.Count));
		}

		public static string GetRandomInsult()
		{
			var resources = Insults.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
			var errors = new List<string>();
			foreach (DictionaryEntry entry in resources)
			{
				errors.Add(entry.Value.ToString());
			}

			return errors.ElementAt(new Random().Next(0, errors.Count));
		}

		public static string GetRandomQuote()
		{
			var resources = Quotes.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
			var errors = new List<string>();
			foreach (DictionaryEntry entry in resources)
			{
				errors.Add(entry.Value.ToString());
			}

			return errors.ElementAt(new Random().Next(0, errors.Count));
		}
	}
}
