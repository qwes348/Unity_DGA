using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot {
  class Program {
    static ITelegramBotClient botClient;
    static void Main() {
        botClient = new TelegramBotClient("635435512:AAGFCHk0gb3ieFUZ5TTxTBEMuEmJD1htOn8");
        var me = botClient.GetMeAsync().Result;
        Console.WriteLine(
            $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
        );

        botClient.OnMessage += Bot_OnMessage;
        botClient.StartReceiving();
        Thread.Sleep(int.MaxValue);
    }

    static async void Bot_OnMessage(object sender, MessageEventArgs e)  // like Coroutine
    {
        if(e.Message.Text != null)
        {
            Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}");
            await botClient.SendTextMessageAsync(  // like yield return
                chatId: e.Message.Chat,
                text: "You said \n" + e.Message.Text
            );
        }
    }
  }
}
