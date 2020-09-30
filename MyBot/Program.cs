using Business;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace MyBot
{
	public class Program
	{
		//private DiscordSocketClient _client;
		private static readonly string _token = ConfigurationManager.AppSettings["token"];
		private static DiscordClient _client;
		public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
			_client = new DiscordClient(new DiscordConfiguration
			{
				Token = _token,
				TokenType = TokenType.Bot
			});
			var services = ConfigureServices();

			services.GetRequiredService<CommandHandler>().InitializeCommands(services);
			await _client.ConnectAsync();

			await Task.Delay(-1);
		}

		#region private methods
		private IServiceProvider ConfigureServices()
		{
			return new ServiceCollection()
				.AddSingleton(_client).
				//.AddSingleton<CommandService>().
				AddSingleton<CommandHandler>().
				BuildServiceProvider();
		}
		#endregion
	}
}
