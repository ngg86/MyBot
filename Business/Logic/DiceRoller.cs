using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Logic
{
	public static class DiceRoller
	{
		public static DiceArguments SplitArgs(string args)
		{
			DiceArguments diceArgs = null;
			if (ContainsValidCharacters(args))
			{
				diceArgs = new DiceArguments
				{
					AmountOfDice = GetAmountOfDiceToRoll(args)
				};
				if (diceArgs.AmountOfDice != 0)
				{
					diceArgs.DiceValue = GetDiceValue(args);
					diceArgs.AbilityModifier = GetAbilityModifier(args, diceArgs);
				}
				else
				{
					diceArgs = null;
				}
			}

			if (diceArgs == null)
			{
				diceArgs = new DiceArguments() { ErrorMessage = TextRoller.GetRandomErrorMessage() };
			}
			return diceArgs;
		}

		public static string RollDice(DiceArguments diceArguments)
		{
			var diceResults = new List<int>();
			var total = 0;

			for (int i = 0; i < diceArguments.AmountOfDice; i++)
			{
				var randomGenerator = new Random();
				diceResults.Add(randomGenerator.Next(1, diceArguments.DiceValue));
			}

			foreach (var result in diceResults)
			{
				total += result;
			}
			total = diceArguments.ModifierIsNegative ? total - diceArguments.AbilityModifier : total + diceArguments.AbilityModifier;
			var message = StringifyResults(diceResults, total, diceArguments);

			return message;
		}

		#region private methods
		private static string StringifyResults(List<int> diceResults, int total, DiceArguments diceArgs)
		{
			var message = new StringBuilder();
			message.Append("rolled a ");
			message.Append($"{total}! ");
			message.Append($"({diceResults[0]}");

			if (diceResults.Count > 1)
			{
				for (int i = 1; i < diceResults.Count; i++)
				{
					message.Append($" + {diceResults[i]}");
				}
			}

			if (diceArgs.AbilityModifier != 0)
			{
				message.Append(diceArgs.ModifierIsNegative ? " -" : " +");
				message.Append($" {diceArgs.AbilityModifier}");
			}
			message.Append(")");
			return message.ToString();
		}

		private static int GetAbilityModifier(string args, DiceArguments diceArgs)
		{
			var index = 0;
			var modifier = 0;
			if (args.Contains("+"))
			{
				index = args.IndexOf("+") + 1;
			}
			else if (args.Contains("-"))
			{
				index = args.IndexOf("-") + 1;
				diceArgs.ModifierIsNegative = true;
			}

			var modifierString = args.Substring(index, args.Length - index);
			if (modifierString.Length <= 2)
			{
				int.TryParse(args.Substring(index, args.Length - index), out modifier);
			}
			return modifier;
		}

		private static int GetDiceValue(string args)
		{
			var startIndex = 0;
			var endIndex = args.Length;
			if (args.Contains("d"))
			{
				startIndex = args.IndexOf("d") + 1;
			}
			if (args.Contains("+"))
			{
				endIndex = args.IndexOf("+");
			}
			else if (args.Contains("-"))
			{
				endIndex = args.IndexOf("-");
			}

			int.TryParse(args.Substring(startIndex, endIndex - startIndex), out int diceValue);

			if (diceValue > 100)
			{
				diceValue = 0;
			}

			return diceValue;
		}

		private static bool ContainsValidCharacters(string text)
		{
			foreach (char character in text)
			{
				if (!char.IsDigit(character))
				{
					switch (character)
					{
						case 'd':
						case '+':
						case '-':
							break;
						default:
							return false;
					}
				}
			}
			return true;
		}

		private static int GetAmountOfDiceToRoll(string args)
		{
			var amountOfDice = 1;
			if (args.Contains("d"))
			{
				var index = args.IndexOf("d");
				if (index < 2 && index != 0)
				{
					int.TryParse(args.Substring(0, index), out amountOfDice);
				}
				if (index >= 2)
				{
					amountOfDice = 0;
				}
			}
			return amountOfDice;
		}
		#endregion
	}
}
