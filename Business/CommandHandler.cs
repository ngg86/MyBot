using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Business
{
	public class CommandHandler
	{
		private readonly DiscordClient _client;
#pragma warning disable IDE0052 // Remove unread private members
		private IServiceProvider _provider;
#pragma warning restore IDE0052 // Remove unread private members
		private List<string> _botCommands = new List<string>() { "insult", "roll", "quote", "event" };
		public CommandHandler(DiscordClient client, IServiceProvider provider)
		{
			_client = client;
			_provider = provider;
		}

		public void InitializeCommands(IServiceProvider provider)
		{
			_provider = provider;
			var message = string.Empty;
			_client.MessageCreated += async e =>
			{
				if (e.Message.Content.StartsWith("!"))
				{
					var messageArgument = e.Message.Content.Substring(1,e.Message.Content.Length-1);
					foreach(var argument in _botCommands)
					{
						if (messageArgument.StartsWith(argument))
						{
							switch (argument)
							{
								case "insult":
									message = HandleInsult();
									break;

								case "quote":
									message = HandleQuote();
									break;

								case "roll":
									message = HandleRoll(messageArgument.Substring(4, messageArgument.Length-4));
									break;

								case "event":
									message = HandleEvent(string.Empty);
									break;

								default:
									message = HandleError();
									break;
							}
							break;
						}
					}

					await e.Message.RespondAsync($"{e.Message.Author.Username}  {message}");
				}
			};
		}

		private string HandleRoll(string input)
		{
			var diceArgs = Logic.DiceRoller.SplitArgs(input);
			return Logic.DiceRoller.RollDice(diceArgs);
		}

		private string HandleQuote()
		{
			return Logic.TextRoller.GetRandomQuote();
		}
		private string HandleInsult()
		{
			return Logic.TextRoller.GetRandomInsult();
		}
		private string HandleEvent(string input)
		{
			//NYI
			return Logic.TextRoller.GetRandomErrorMessage();
		}

		private string HandleError()
		{
			return Logic.TextRoller.GetRandomErrorMessage();
		}
	}
}
