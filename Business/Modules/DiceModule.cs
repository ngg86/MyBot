using Business.Logic;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Modules
{
	public class DiceModule : ModuleBase<SocketCommandContext>
	{
		[Command("roll")]
		public Task Roll(string args)
		{
			var diceArguments = DiceRoller.SplitArgs(args);

			var diceRolls = string.IsNullOrEmpty(diceArguments.ErrorMessage) ? DiceRoller.RollDice(diceArguments) : diceArguments.ErrorMessage;
			var user = Context.Message.Author.Mention;
			var result = $"{user} {diceRolls}";

			return ReplyAsync(result);
		}

		[Command("insult")]
		public Task Insult()
		{
			return ReplyAsync(TextRoller.GetRandomInsult());
		}

		[Command("quote")]
		public Task Quote()
		{
			return ReplyAsync(TextRoller.GetRandomQuote());
		}

		//[Command("existentialcrisis")]
		//public Task Crisis()
		//{

		//	var result = "Do I am because I think or do I thunk 'cos I be?";
		//	return ReplyAsync(result);
		//}
	}
}
