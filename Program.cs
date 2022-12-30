using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace tBots {
    class Program {

        private static string[] users;
        static void Main(string[] args) {
            var client = new TelegramBotClient("5572130781:AAEBa_rpjv_HFdKa0KFZnRJKpZD3lqAvOM0");
            client.StartReceiving(UpdateM,Error);
            users = System.IO.File.ReadAllLines("userB.txt");
            for (int i = 0; i < users.Length; i++)
                Console.Write($" {users[i]} ");
            Console.WriteLine("\n Bot start!");
            Console.ReadLine();
        }

        async private static Task UpdateM(ITelegramBotClient botClient, Update update, CancellationToken token) {
            var message = update.Message;
            try
            {
                if (message.Chat.Username != null)
                {
                    if (message.Text != null)
                    {
                        if (message.Text == "/start" && message.Chat.Username != "Grichulevichvika")
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $" Привіт {message.Chat.Username}");
                            return;
                        }
                        else if (message.Text == "/start") {
                            await botClient.SendPhotoAsync(message.Chat.Id, "https://maikasoft.com.ua/image/cache/catalog/goods/i_love_Viky.reglan.M.black.1-250x294.jpg");
                            return;
                        }

                        if (message.Text.ToLower().Contains("підар"))
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $" А може {message.Chat.Username} підар?");
                            return;
                        }
                        if (message.Text.Contains("/add") && message.Chat.Username == "PriCepThyk") {
                            using (StreamWriter stream = new StreamWriter("userB.txt", true))
                                stream.WriteLine(message.Text.Substring(4));
                            users = System.IO.File.ReadAllLines("userB.txt");
                            await botClient.SendTextMessageAsync(message.Chat.Id, $" Користувача {message.Text.Substring(4)} додано.");
                            return;
                        }
                        if (message.Text.Contains("/rem") && message.Chat.Username == "PriCepThyk")
                        {
                            using (var fs = new FileStream("userB.txt", FileMode.Truncate)) { }
                            using (StreamWriter stream = new StreamWriter("userB.txt", true))
                                for (int i = 0; i < users.Length; i++)
                                    if (users[i]!= message.Text.Substring(4))
                                        stream.WriteLine(users[i]);
                            users = System.IO.File.ReadAllLines("userB.txt");
                            await botClient.SendTextMessageAsync(message.Chat.Id, $" Користувача {message.Text.Substring(4)} видалено.");
                            return;
                        }
                        foreach (string strPos in users)
                        {
                            if (message.Chat.Username.Contains(strPos))
                            {
                                Console.WriteLine($" IN - {message.Chat.Username} - {message.Text}");
                                message.Text = message.Text.Replace('A', 'А');
                                message.Text = message.Text.Replace('B', 'В');
                                message.Text = message.Text.Replace('C', 'С');
                                message.Text = message.Text.Replace('E', 'Е');
                                message.Text = message.Text.Replace('H', 'Н');
                                message.Text = message.Text.Replace('I', 'І');
                                message.Text = message.Text.Replace('K', 'К');
                                message.Text = message.Text.Replace('M', 'М');
                                message.Text = message.Text.Replace('O', 'О');
                                message.Text = message.Text.Replace('P', 'Р');
                                message.Text = message.Text.Replace('T', 'Т');
                                message.Text = message.Text.Replace('X', 'Х');
                                message.Text = message.Text.Replace('Y', 'У');
                                message.Text = message.Text.Replace('a', 'а');
                                message.Text = message.Text.Replace('c', 'с');
                                message.Text = message.Text.Replace('e', 'е');
                                message.Text = message.Text.Replace('i', 'і');
                                message.Text = message.Text.Replace('o', 'о');
                                message.Text = message.Text.Replace('p', 'р');
                                message.Text = message.Text.Replace('x', 'х');
                                message.Text = message.Text.Replace('y', 'у');
                                await botClient.SendTextMessageAsync(message.Chat.Id, "`"+message.Text+"`", ParseMode.MarkdownV2);
                                using (StreamWriter stream = new StreamWriter("logs.txt", true))
                                    stream.WriteLine($"{ message.Chat.Username} - { message.Text}");
                                Console.WriteLine($" OYT- {message.Chat.Username} - {message.Text}");
                                return;
                            }
                        }
                        Console.WriteLine($" ID - {message.Chat.Username} - {message.Text}");
                        return;
                    }
                    else
                    {
                        await botClient.SendPhotoAsync(message.Chat.Id, "https://memepedia.ru/wp-content/uploads/2019/08/day-2-grivni-2-768x578.jpg");
                        return;
                    }
                }
                else {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $" Для продовження роботи добавте псевдонімв профілі телеграму!");
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
           
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3) {
            throw new NotImplementedException();
        }
    }
}
