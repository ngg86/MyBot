using Business;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace MyBot
{
	public class Program
	{
		private DiscordSocketClient _client;
		private static readonly string _token = ConfigurationManager.AppSettings["token"];
		public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
			_client = new DiscordSocketClient();
			_client.Log += Log;
			var services = ConfigureServices();

			await services.GetRequiredService<CommandHandler>().InitializeCommandsAsync(services);
			await _client.LoginAsync(TokenType.Bot, _token);
			await _client.StartAsync();

			await Task.Delay(-1);
		}

		#region private methods
		private IServiceProvider ConfigureServices()
		{
			return new ServiceCollection()
				.AddSingleton(_client)
				.AddSingleton<CommandService>().
				AddSingleton<CommandHandler>().
				BuildServiceProvider();
		}

		private Task Log(LogMessage message)
		{
			Console.WriteLine(message.ToString());
			return Task.CompletedTask;
		}
		#endregion
	}
}
