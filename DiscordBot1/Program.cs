using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace DiscordBot1
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;

        //starts program
        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        //connect bot
        public async Task StartAsync()
        { 
            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            _handler = new CommandHandler();
            await _client.SetGameAsync("with salt-bot");
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }

        //Prints out log in the console explaining what it is doing
        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
