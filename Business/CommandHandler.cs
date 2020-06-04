using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Business
{
	public class CommandHandler
	{
		private readonly DiscordSocketClient _client;
		private readonly CommandService _service;
		private IServiceProvider _provider;

		public CommandHandler(DiscordSocketClient client, CommandService service, IServiceProvider provider)
		{
			_client = client;
			_service = service;
			_provider = provider;
		}

		public async Task InitializeCommandsAsync(IServiceProvider provider)
		{
			_provider = provider;
			await _service.AddModulesAsync(Assembly.GetExecutingAssembly(), _provider);
			_client.MessageReceived += MessageReceived;
		}

		private async Task MessageReceived(SocketMessage incomingMessage)
		{
			if (!(incomingMessage is SocketUserMessage message)) return;
			if (message.Source != Discord.MessageSource.User) return;

			var position = 0;
			if (!message.HasCharPrefix('!', ref position)) return;


			var context = new SocketCommandContext(_client, message);
			var result = await _service.ExecuteAsync(context, position, _provider);

			if (result.Error.HasValue && result.Error.Value != CommandError.UnknownCommand)
			{
				Console.WriteLine(result.ToString());
			}
		}
	}
}
