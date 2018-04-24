using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot1.Core.UserAccounts;
using DiscordBot1.Core.BankAccounts;
using DiscordBot1.Core.Store;
using DiscordBot1.Modules.miscCore;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using Discord.Rest;
using Discord.Net;
using System.Drawing;
using System.Drawing.Imaging;


namespace DiscordBot1.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        //command to list commands
        [Command("Commands")]
        public async Task Commands()
        {
            var embed = new EmbedBuilder();//create embed builder
            string message;

            message = "!Commands = Show available commands" + "\r" +
                      "!Roulette = Spin the barrel and pull the trigger" + "\r" +
                      "!Echo [Message] = Bot repeats what you said" + "\r" +
                      "!8Ball [Question] = Ask the magic 8Ball a question" + "\r" +
                      "!Pick [thing1]|[thing2]... = Input options seperated by | and it chooses one." + "\r" +
                      "!Draw = Selects a random drawing prompt" + "\r" +
                      "!Slap [Person's Name] = Show them what for" + "\r" +
                      "!Timer [Time in minutes] = Sets a timer and will alert when timer goes off" + "\r" +
                      "!MyStats = Show your stats" + "\r" +
                      "!PayDay = Get some money" + "\r" +
                      "!GetGBP = Get your daily allotment of Good Boy Points" + "\r" +
                      "!LMGTFY [search query] = Let me google that for you" + "\r" +
                      "!GBPStore [item#] [Quantity] = Spend your GBP at the store. Enter no values to just see store menu.***broke an idk what's wrong :(***" + "\r" +
                      "!LBStore [item#] [Quantity] = Spend your Lennybucks at the store. Enter no values to just see store menu" + "\r" +
                      "!Inventory = Show what items and how many of them you have." + "\r" +
                      "!MyD = Measure the length of your d" + "\r" +
                      "!YoMama = Random \"Yo Mamma\" joke" + "\r" +
                      "!RPS [Choice] [Bet] = Play a game of Rock Paper Scissors and you can place a bet if you want" + "\r" +
                      "!TTT [x] [y] = Play tic tak toe on a 3x3 grid inputing values into x and y accordingly. You will always be X";

            //Age is just a number and jail is just a place.

            embed.WithTitle("Bot Commands");//adds title to the embed
            embed.WithDescription(message);//insert the message into the embed
            embed.WithColor(new Discord.Color(125, 115, 30));//add a color to the left bar of the embed

            await Context.Channel.SendMessageAsync("", false, embed);//print message into discord
        }
        //command to have bot echo what was said by the user who invoked the command
        [Command("Echo")]
        public async Task Echo([Remainder]string message)//remainder collects all text entered after the command and puts it into the string message
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("Echoed message");
            embed.WithDescription(message);
            embed.WithColor(new Discord.Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed);
        }
        //Sends a dm to the user with the secret
        [Command("Secret")]
        public async Task RevealSecret([Remainder]string arg = "")
        {
            //if (!UserIsSecretOwner((SocketGuildUser)Context.User))
            //{
            //    await Context.Channel.SendMessageAsync(":x: You need the SecretOwner role to do that. " + Context.User.Username);
            //    return;
            //}
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("SECRET"));
        }
        //spin the barrel and pull the trigger
        [Command("Roulette")]
        public async Task Roulette()
        {
            Random rnd = new Random();
            string message;
            int bullet;
            bullet = rnd.Next(1, 7); //generate random number between 1 and 6

            var embed = new EmbedBuilder();

            if (bullet == 6) //the bullet in the chamber is in the 6th chamber, so if the number rolls 6 the person dies
            {
                message = "has shot themselves in the head. :gun:";

                embed.WithDescription(Context.User.Username + " " + message);//context.user.username grabs the username of the person who asked the question and places it in the description
                embed.WithColor(new Discord.Color(255, 0, 0));

                await Context.Channel.SendMessageAsync("", false, embed);
            }
            else
            {
                message = "will live to see another day.";

                embed.WithDescription(Context.User.Username + " " + message);
                embed.WithColor(new Discord.Color(0, 255, 0));

                await Context.Channel.SendMessageAsync("", false, embed);
            }
            //make botbot play
            //await Context.Channel.SendMessageAsync("My turn.");
            //await Context.Channel.SendMessageAsync("!roulette");
        }
        //ask the magic 8ball a question and it will answer
        [Command("8ball")]
        public async Task EightBall([Remainder]string message)
        {
            Random rnd = new Random();
            int roll;
            int reply;
            string msg;
            roll = rnd.Next(1, 21); //generate random number between 1 and 20
            string[] yes = { "It is certain.", "It is decidedly so.", "Without a doubt.", "Without a doubt.", "Yes, definitely.", "You may rely on it", "As I see it, yes.",
                             "Most likely.", "Outlook good.", "Yes.", "Signs point to yes." };//responses for yes
            string[] maybe = { "Reply hazy try again.", "Ask again later.", "Better not tell you now.", "Cannot predict now.", "Concentrate and ask again." };//responses for maybe
            string[] no = { "Don't count on it.", "My reply is no.", "Outlook not so good.", "Outlook not so good.", "Very doubtful." };//responses for no

            var embed = new EmbedBuilder();

            if (roll == 1 || roll == 2 || roll == 3 || roll == 4 || roll == 5 || roll == 6 || roll == 7 || roll == 8 || roll == 9 || roll == 10)
            {
                reply = rnd.Next(1, yes.Length);
        
                msg = yes[reply - 1];

                embed.WithTitle("8 Ball response for" + " " + Context.User.Username);
                embed.WithDescription("The answer to " + "\"" + message + "\"" + " " + "is..." + "\r" + "\r" + msg);
                embed.WithColor(new Discord.Color(0, 255, 0));

                await Context.Channel.SendMessageAsync("", false, embed);
            }
            if (roll == 11 || roll == 12 || roll == 13 || roll == 14 || roll == 15)
            {
                reply = rnd.Next(1, maybe.Length);

                msg = maybe[reply - 1];

                embed.WithTitle("8 Ball response for" + " " + Context.User.Username);
                embed.WithDescription("The answer to " + "\"" + message + "\"" + " " + "is..." + "\r" + "\r" + msg);
                embed.WithColor(new Discord.Color(255, 255, 0));

                await Context.Channel.SendMessageAsync("", false, embed);
            }
            if (roll == 16 || roll == 17 || roll == 18 || roll == 19 || roll == 20)
            {
                reply = rnd.Next(1, no.Length);

                msg = no[reply - 1];

                embed.WithTitle("8 Ball response for" + " " + Context.User.Username);
                embed.WithDescription("The answer to " + "\"" + message + "\"" + " " + "is..." + "\r" + "\r" + msg);
                embed.WithColor(new Discord.Color(255, 0, 0));

                await Context.Channel.SendMessageAsync("", false, embed);
            }
        }
        //give some options to the bot and it will decide for you
        [Command("Pick")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);//creates an array of string and splits options based on | and removes empty entries

            Random rnd = new Random();
            string selection = options[rnd.Next(0, options.Length)];//select random entry from the options array

            var embed = new EmbedBuilder();
            embed.WithTitle("Choice for " + Context.User.Username);
            embed.WithDescription(selection);
            embed.WithColor(new Discord.Color(25, 125, 75));
            embed.WithThumbnailUrl("");

            await Context.Channel.SendMessageAsync("", false, embed);
        }
        //watch your language
        [Command("Fuckyou")]
        public async Task Fuckyou()
        {
            await Context.Channel.SendMessageAsync("No, fuck you!");
        }
        //the bot will give you a random drawing topic
        [Command("Draw")]
        public async Task Draw()
        {
            var embed = new EmbedBuilder();

            string message;

            Random rnd = new Random();

            //read all lines from a given text file and place them into an array
            string[] lnsWho = File.ReadAllLines(@"C:\Users\David\Documents\Visual Studio 2017\Projects\DiscordBot1\DiscordBot1\bin\Debug\Resources\who.txt");
            string[] lnsWhat = File.ReadAllLines(@"C:\Users\David\Documents\Visual Studio 2017\Projects\DiscordBot1\DiscordBot1\bin\Debug\Resources\what.txt");

            //get random element from an array
            var indexWho = rnd.Next(0, lnsWho.Length);
            var indexWhat = rnd.Next(0, lnsWhat.Length);

            //Console.WriteLine(lnsWho[indexWho] + " " + lnsWhat[indexWhat]);

            message = lnsWho[indexWho] + " " + lnsWhat[indexWhat];

            embed.WithTitle("Drawing choice for " + Context.User.Username);
            embed.WithDescription(message);
            embed.WithColor(new Discord.Color(125, 55, 165));

            await Context.Channel.SendMessageAsync("", false, embed);
        }
        //Slap someone
        [Command("Slap")]
        public async Task Slap([Remainder]string message)
        {
            await Context.Channel.SendMessageAsync(Context.User.Username + " slaps " + message);
        }
        //Calculate the size of your D
        [Command("myd")]
        public async Task MyD()
        {
            var account = UserAccounts.GetAccount(Context.User);
            Random rnd = new Random();
            double d1;
            double d2;
            double length = 20;
            int c = 0;
            double oldLength = account.length;
            
            
            //if this is the first time
            if (account.length == 0)
            {
                d1 = rnd.Next(1, 11);

                d2 = rnd.NextDouble();

                length = d1 + d2;

                length = Math.Round(length, 1);

                account.length = length;

                await Context.Channel.SendMessageAsync("Your D is " + length + " cm long.");

                UserAccounts.SaveAccounts();

                c = 1;
            }

            if(c != 1)
            {
                while(length > oldLength)
                {
                    d1 = rnd.Next(1, 11);

                    d2 = rnd.NextDouble();

                    length = d1 + d2;

                    length = Math.Round(length, 1);

                }
                await Context.Channel.SendMessageAsync("Your D is " + length + " cm long.");

                account.length = length;

                UserAccounts.SaveAccounts();
            }
        }
        //Let me google that for you
        [Command("lmgtfy")]
        public async Task LMGTFY([Remainder]string message)
        {
            message = WebUtility.UrlEncode(message);

            string URL = "http://lmgtfy.com/?q=" + message;//append url

            await Context.Channel.SendMessageAsync(URL);
        }
        [Command("YoMama")]
        public async Task YoMama()
        {
            Random rnd = new Random();
            int index = rnd.Next(1, 1041);

            string joke = index.ToString();

            await Context.Channel.SendMessageAsync(yoMama.GetJoke(joke));
        }
        //Creates a countdown timer and alerts you when its done
        [Command("Timer")]
        public async Task CountdownTimer([Remainder]string inputTime)
        {
            Random rndPic = new Random();

            string pic = "";
            int value = rndPic.Next(1, 6);

            if (value == 1)
            {
                pic = "C:\\Users\\David\\Desktop\\Stuff\\Pictures\\Discord Pictures\\Baka\\baka1.jpg";
            }
            if (value == 2)
            {
                pic = "C:\\Users\\David\\Desktop\\Stuff\\Pictures\\Discord Pictures\\Baka\\baka2.jpg";
            }
            if (value == 3)
            {
                pic = "C:\\Users\\David\\Desktop\\Stuff\\Pictures\\Discord Pictures\\Baka\\baka3.jpg";
            }
            if (value == 4)
            {
                pic = "C:\\Users\\David\\Desktop\\Stuff\\Pictures\\Discord Pictures\\Baka\\baka4.gif";
            }
            if (value == 5)
            {
                pic = "C:\\Users\\David\\Desktop\\Stuff\\Pictures\\Discord Pictures\\Baka\\baka5.jpg";
            }

            double time;
            //checks to see if the value entered is a number
            if (double.TryParse(inputTime, out time))
            {
                time = time * 60;//converts time input to minutes

                if (time > 600)//if timer is longer than 10min (600sec) it will throw an error. This can be changed if you want to allow a longer timer
                {
                    await Context.Channel.SendMessageAsync("Enter a numeric value of 10 or less, baka.");
                    await Context.Channel.SendFileAsync(pic);
                    return;
                }

                for (double a = 5; a >= 0; a--)
                {
                    System.Threading.Thread.Sleep(1000);
                    string timerAsString = Convert.ToString(a);//converts time remaining to string so it can be posted in discord

                    if (timerAsString == "0")
                    {
                        await Context.Channel.SendMessageAsync("Timer has started!");
                        continue;//breaks out of the for loop so it does not display a 0 in the countdown
                    }
                    await Context.Channel.SendMessageAsync(timerAsString);//displays countdown in discord
                }

                Console.WriteLine("Timer set for " + time);

                int ctr = 0;
                int ctr1 = 0;
                int ctr2 = 0;
                double timeRemaining = time;

                for (double a = time; a >= 0; a--)
                {
                    System.Threading.Thread.Sleep(1000);

                    ctr++;

                    if (ctr == 60 && timeRemaining != 0)
                    {
                        ctr1 = ctr1 + 1;

                        ctr2 = ctr1 * 60;

                        timeRemaining = time - ctr2;

                        timeRemaining = timeRemaining / 60;

                        if (timeRemaining == 0)
                        {
                            continue;
                        }

                        ctr = 0;

                        await Context.Channel.SendMessageAsync($"Time remaining: {timeRemaining} minutes.");
                    }
                }

                await Context.Channel.SendMessageAsync("Time's up!");
                await Context.Channel.SendMessageAsync("Time's up!");
                await Context.Channel.SendMessageAsync("Time's up!");
                await Context.Channel.SendMessageAsync("Time's up!");
                await Context.Channel.SendMessageAsync("Time's up!");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Enter a numeric value, baka.");//if they do not enter a number it throws an error and quits the command
                await Context.Channel.SendFileAsync(pic);
                return;
            }
        }
        //Display Stats
        [Command("myStats")]
        public async Task MyStats()
        {
            var account = UserAccounts.GetAccount(Context.User);
            await Context.Channel.SendMessageAsync($"You have {Math.Floor(account.GBP)} good boy points, and {Math.Floor(account.Money)} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅].");
        }
        //Payday
        [Command("payDay")]
        public async Task PayDay()
        {
            var account = UserAccounts.GetAccount(Context.User);
            string currencyType = "Lennybucks";
            double pay = 5000;//lennybucks earned per pay period
            int timeToWait = 168;//7 days worth of hours

            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;//create value equal to current datetime

            var hours = (dateTime - account.mdtPaid).TotalHours; //calculate time since last paid

            double hoursDiff = timeToWait - hours; //take timeToWait - hours to get value for how many hours until they can get paid again
            hoursDiff = Math.Round(hoursDiff, 2); //round hours to 2 decimal places

            double daysDiff = hoursDiff / 24; //convert hours diff to days
            daysDiff = Math.Round(daysDiff, 2); //round days to 2 decimal places

            if (account.mPaid == "yes" & hours < timeToWait) //check to see if user has been paid in the last 8 hours
            {
                await Context.Channel.SendMessageAsync($"You only get paid once a week. Please wait {daysDiff} more day(s) to recieve another pay check."); //print out
            }
            else //if not, then they get paid
            {
                //update account to reflect they have been paid and update account balance
                account.mPaid = "yes";
                account.mdtPaid = DateTime.Now;
                account.Money += pay;
                UserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"You have been paid 5000 [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅].");

                //update economey so it accurately represents the total lennybucks in the entire economey
                var bankAccount = BankAccounts.GetAccount(currencyType);
                bankAccount.totalFunds += pay;
                BankAccounts.SaveBankAccounts();
            }
        }
        //Get GBP
        [Command("getGBP")]
        public async Task GetGBP()
        {
            var account = UserAccounts.GetAccount(Context.User);
            string currencyType = "Goodboypoints";
            double pay = 2100;
            int timeToWait = 168;//7 days worth of hours

            if (Context.User.Id == 370772796323790848)//if user is saltbot
            {
                await Context.Channel.SendMessageAsync("Saltbot, you have not been a good boy, so you dont get any goodboypoints.");
                return;
            }

            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;//create value equal to current datetime

            var hours = (dateTime - account.gbpdtPaid).TotalHours; //calculate time since last paid

            double hoursDiff = timeToWait - hours; //take timeToWait - hours to get value for how many hours until they can get paid again
            hoursDiff = Math.Round(hoursDiff, 2); //round hours to 2 decimal places

            double daysDiff = hoursDiff / 24; //convert hours diff to days
            daysDiff = Math.Round(daysDiff, 2); //round days to 2 decimal places

            //hoursDiff = Math.Round(hoursDiff, 2); //round hours to 2 decimal places

            if (account.gbpPaid == "yes" & hours < timeToWait) //check to see if user has been paid in the last 8 hours
            {
                await Context.Channel.SendMessageAsync($"Please wait {daysDiff} more day(s) to recieve more Good Boy Points.");
            }
            else //if not, then they get paid
            {
                account.gbpPaid = "yes";
                account.gbpdtPaid = DateTime.Now;
                account.GBP += pay;

                UserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"You gained {pay} Good Boy Points.");

                var bankAccount = BankAccounts.GetAccount(currencyType);
                bankAccount.totalFunds += pay;
                BankAccounts.SaveBankAccounts();
            }
        }
        //Play rock paper scisors against Botbot (maybe againt player soon)
        [Command("rps")]
        public async Task RockPaperScisors(string pChoice, string bet = "0")
        {
            double betValue = 0;
            string currancyType = "Lennybucks";
            Random rndAIChoice = new Random();

            //check choice input
            if (pChoice != "Rock" && pChoice != "Paper" && pChoice != "Scissors" && pChoice != "scissors" && pChoice != "rock" && pChoice != "paper")
            {
                await Context.Channel.SendMessageAsync("Please choose either rock, paper, or scissors along with an optional bet as follows \"!rpc rock 5\"");
                return;
            }

            //check to see if bet is a positive number
            if (!double.TryParse(bet, out betValue))
            {
                await Context.Channel.SendMessageAsync("Please enter a non-decimal positive value for you bet.");
                return;
            }

            //check to see if they have enough money for the bet

            var user = UserAccounts.GetAccount(Context.User);

            double accbal = user.Money;

            if (betValue > accbal)
            {
                await Context.Channel.SendMessageAsync($"You don't have that much money. You currently have {Math.Floor(accbal)} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅]");
                return;
            }

            //AI makes choice
            int decision = rndAIChoice.Next(1, 4);

            //Game Logic
            if (decision == 1)//rock
            {
                if (pChoice == "Rock" || pChoice == "rock")
                {
                    await Context.Channel.SendMessageAsync("I chose rock too, the game is a tie.");

                    if (betValue > 0)
                    {
                        await Context.Channel.SendMessageAsync("Your bet has been returned to you.");
                    }
                }
                else if (pChoice == "Paper" || pChoice == "paper")
                {
                    await Context.Channel.SendMessageAsync("I chose rock, you win!");

                    if (betValue > 0)//award winnings
                    {
                        betValue = betValue * 2;

                        var account = UserAccounts.GetAccount(Context.User);
                        account.Money += betValue;
                        UserAccounts.SaveAccounts();

                        await Context.Channel.SendMessageAsync($"You have been awarded {Math.Floor(betValue)} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅].");

                        var bankAccount = BankAccounts.GetAccount(currancyType);
                        bankAccount.totalFunds += betValue;
                        BankAccounts.SaveBankAccounts();
                    }
                }
                else if (pChoice == "Scissors" || pChoice == "scissors")
                {
                    await Context.Channel.SendMessageAsync("I chose rock, you lose...");

                    if (betValue > 0)
                    {
                        var account = UserAccounts.GetAccount(Context.User);
                        account.Money -= betValue;
                        UserAccounts.SaveAccounts();

                        await Context.Channel.SendMessageAsync($"You have lost {Math.Floor(betValue)} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅]. Better luck next time!");

                        var bankAccount = BankAccounts.GetAccount(currancyType);
                        bankAccount.totalFunds -= betValue;
                        BankAccounts.SaveBankAccounts();
                    }
                }
            }
            else if (decision == 2)//paper
            {
                if (pChoice == "Paper" || pChoice == "paper")
                {
                    await Context.Channel.SendMessageAsync("I chose paper too, the game is a tie.");

                    if (betValue > 0)
                    {
                        await Context.Channel.SendMessageAsync("Your bet has been returned to you.");
                    }
                }
                else if (pChoice == "Scissors" || pChoice == "scissors")
                {
                    await Context.Channel.SendMessageAsync("I chose paper, you win!");

                    if (betValue > 0)//award winnings
                    {
                        betValue = betValue * 2;

                        var account = UserAccounts.GetAccount(Context.User);
                        account.Money += betValue;
                        UserAccounts.SaveAccounts();

                        await Context.Channel.SendMessageAsync($"You have been awarded {Math.Floor(betValue)} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅].");

                        var bankAccount = BankAccounts.GetAccount(currancyType);
                        bankAccount.totalFunds += betValue;
                        BankAccounts.SaveBankAccounts();
                    }
                }
                else if (pChoice == "Rock" || pChoice == "rock")
                {
                    await Context.Channel.SendMessageAsync("I chose paper, you lose...");

                    if (betValue > 0)
                    {
                        var account = UserAccounts.GetAccount(Context.User);
                        account.Money -= betValue;
                        UserAccounts.SaveAccounts();

                        await Context.Channel.SendMessageAsync($"You have lost {Math.Floor(betValue)} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅]. Better luck next time!");

                        var bankAccount = BankAccounts.GetAccount(currancyType);
                        bankAccount.totalFunds -= betValue;
                        BankAccounts.SaveBankAccounts();
                    }
                }
            }
            else if (decision == 3)//scissors
            {
                if (pChoice == "Scissors" || pChoice == "scissors")
                {
                    await Context.Channel.SendMessageAsync("I chose scissors too, the game is a tie.");

                    if (betValue > 0)
                    {
                        await Context.Channel.SendMessageAsync("Your bet has been returned to you.");
                    }
                }
                else if (pChoice == "Rock" || pChoice == "rock")
                {
                    await Context.Channel.SendMessageAsync("I chose scissors, you win!");

                    if (betValue > 0)//award winnings
                    {
                        betValue = betValue * 2;

                        var account = UserAccounts.GetAccount(Context.User);
                        account.Money += betValue;
                        UserAccounts.SaveAccounts();

                        await Context.Channel.SendMessageAsync($"You have been awarded {Math.Floor(betValue)} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅].");

                        var bankAccount = BankAccounts.GetAccount(currancyType);
                        bankAccount.totalFunds += betValue;
                        BankAccounts.SaveBankAccounts();
                    }
                }
                else if (pChoice == "Paper" || pChoice == "paper")
                {
                    await Context.Channel.SendMessageAsync("I chose scissors, you lose...");

                    if (betValue > 0)
                    {
                        var account = UserAccounts.GetAccount(Context.User);
                        account.Money -= betValue;
                        UserAccounts.SaveAccounts();

                        await Context.Channel.SendMessageAsync($"You have lost {Math.Floor(betValue)} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅]. Better luck next time!");

                        var bankAccount = BankAccounts.GetAccount(currancyType);
                        bankAccount.totalFunds -= betValue;
                        BankAccounts.SaveBankAccounts();
                    }
                }
            }
        }
        [Command("ttt")]
        public async Task TicTakToe(string x, string y)
        {
            //get user 
            var account = UserAccounts.GetAccount(Context.User);

            //get user save data and assign it to variables
            var sA = account.tttA;
            var sB = account.tttB;
            var sC = account.tttC;
            var sD = account.tttD;
            var sE = account.tttE;
            var sF = account.tttF;
            var sG = account.tttG;
            var sH = account.tttH;
            var sI = account.tttI;

            //validate input
            if (!int.TryParse(x, out int x1))
            {
                await Context.Channel.SendMessageAsync("Please enter a value between 1 and 3 for x");
                return;
            }
            if (!int.TryParse(y, out int y1))
            {
                await Context.Channel.SendMessageAsync("Please enter a value between 1 and 3 for y");
                return;
            }
            if (x1 > 4 || y1 > 4)
            {
                await Context.Channel.SendMessageAsync("Please enter a value between 1 and 3 for x and y");
                return;
            }

            //assign initial values to grid
            string a = "_";
            string b = "_";
            string c = "_";
            string d = "_";
            string e = "_";
            string f = "_";
            string g = "_";
            string h = "_";
            string i = "_";

            string reset = "_"; //used to reset grid when game ends
            int played = 0; //if value = 1 then it knows the bot played

            //assign saved values to grid
            a = sA;
            b = sB;
            c = sC;
            d = sD;
            e = sE;
            f = sF;
            g = sG;
            h = sH;
            i = sI;

            string gameboard; //gameboard
            Random rnd = new Random();//for when the bot makes its first move

            int num; //used to store random number

            //assigns player selection on grid and also makes sure player can't overwrite a space in the grid
            if (x == "1" && y == "1")
            {
                if (a == "_")
                {
                    a = "x";
                }
                else
                {
                    await Context.Channel.SendMessageAsync("This space is occupied, please select a different location");
                }
            }
            if (x == "1" && y == "2")
            {
                if (b == "_")
                {
                    b = "x";
                }
                else
                {
                    await Context.Channel.SendMessageAsync("This space is occupied, please select a different location");
                }
            }
            if (x == "1" && y == "3")
            {
                if (c == "_")
                {
                    c = "x";
                }
                else
                {
                    await Context.Channel.SendMessageAsync("This space is occupied, please select a different location");
                }
            }
            if (x == "2" && y == "1")
            {
                if (d == "_")
                {
                    d = "x";
                }
                else
                {
                    await Context.Channel.SendMessageAsync("This space is occupied, please select a different location");
                }
            }
            if (x == "2" && y == "2")
            {
                if (e == "_")
                {
                    e = "x";
                }
                else
                {
                    await Context.Channel.SendMessageAsync("This space is occupied, please select a different location");
                }

            }
            if (x == "2" && y == "3")
            {
                if (f == "_")
                {
                    f = "x";
                }
                else
                {
                    await Context.Channel.SendMessageAsync("This space is occupied, please select a different location");
                }
            }
            if (x == "3" && y == "1")
            {
                if (g == "_")
                {
                    g = "x";
                }
                else
                {
                    await Context.Channel.SendMessageAsync("This space is occupied, please select a different location");
                }
            }
            if (x == "3" && y == "2")
            {
                if (h == "_")
                {
                    h = "x";
                }
                else
                {
                    await Context.Channel.SendMessageAsync("This space is occupied, please select a different location");
                }
            }
            if (x == "3" && y == "3")
            {
                if (i == "_")
                {
                    i = "x";
                }
                else
                {
                    await Context.Channel.SendMessageAsync("This space is occupied, please select a different location");
                }
            }
            //comp plays
            //first move
            //react to player input
            if (a == "x" && b == "_" && c == "_" && d == "_" && e == "_" && f == "_" && g == "_" && h == "_" && i == "_")
            {
                num = rnd.Next(1, 4);

                if (num == 1)
                {
                    b = "o";
                }
                if (num == 2)
                {
                    e = "o";
                }
                if (num == 3)
                {
                    d = "o";
                }
                played = 1;
            }
            if (a == "_" && b == "x" && c == "_" && d == "_" && e == "_" && f == "_" && g == "_" && h == "_" && i == "_" && played != 1)
            {
                num = rnd.Next(1, 4);

                if (num == 1)
                {
                    a = "o";
                }
                if (num == 2)
                {
                    e = "o";
                }
                if (num == 3)
                {
                    c = "o";
                }
                played = 1;
            }
            if (a == "_" && b == "_" && c == "x" && d == "_" && e == "_" && f == "_" && g == "_" && h == "_" && i == "_" && played != 1)
            {
                num = rnd.Next(1, 4);

                if (num == 1)
                {
                    b = "o";
                }
                if (num == 2)
                {
                    e = "o";
                }
                if (num == 3)
                {
                    f = "o";
                }
                played = 1;
            }
            if (a == "_" && b == "_" && c == "_" && d == "x" && e == "_" && f == "_" && g == "_" && h == "_" && i == "_" && played != 1)
            {
                num = rnd.Next(1, 4);

                if (num == 1)
                {
                    a = "o";
                }
                if (num == 2)
                {
                    e = "o";
                }
                if (num == 3)
                {
                    g = "o";
                }
                played = 1;
            }
            if (a == "_" && b == "_" && c == "_" && d == "_" && e == "x" && f == "_" && g == "_" && h == "_" && i == "_" && played != 1)
            {
                num = rnd.Next(1, 9);

                if (num == 1)
                {
                    a = "o";
                }
                if (num == 2)
                {
                    b = "o";
                }
                if (num == 3)
                {
                    c = "o";
                }
                if (num == 4)
                {
                    d = "o";
                }
                if (num == 5)
                {
                    f = "o";
                }
                if (num == 6)
                {
                    g = "o";
                }
                if (num == 7)
                {
                    h = "o";
                }
                if (num == 8)
                {
                    i = "o";
                }
                played = 1;
            }
            if (a == "_" && b == "_" && c == "_" && d == "_" && e == "_" && f == "x" && g == "_" && h == "_" && i == "_" && played != 1)
            {
                num = rnd.Next(1, 4);

                if (num == 1)
                {
                    i = "o";
                }
                if (num == 2)
                {
                    e = "o";
                }
                if (num == 3)
                {
                    c = "o";
                }
                played = 1;
            }
            if (a == "_" && b == "_" && c == "_" && d == "_" && e == "_" && f == "_" && g == "x" && h == "_" && i == "_" && played != 1)
            {
                num = rnd.Next(1, 4);

                if (num == 1)
                {
                    d = "o";
                }
                if (num == 2)
                {
                    e = "o";
                }
                if (num == 3)
                {
                    h = "o";
                }
                played = 1;
            }
            if (a == "_" && b == "_" && c == "_" && d == "_" && e == "_" && f == "_" && g == "_" && h == "x" && i == "_" && played != 1)
            {
                num = rnd.Next(1, 4);

                if (num == 1)
                {
                    g = "o";
                }
                if (num == 2)
                {
                    e = "o";
                }
                if (num == 3)
                {
                    i = "o";
                }
                played = 1;
            }
            if (a == "_" && b == "_" && c == "_" && d == "_" && e == "_" && f == "_" && g == "_" && h == "_" && i == "x" && played != 1)
            {
                num = rnd.Next(1, 4);

                if (num == 1)
                {
                    f = "o";
                }
                if (num == 2)
                {
                    e = "o";
                }
                if (num == 3)
                {
                    h = "o";
                }
                played = 1;
            }
            //moves after first move
            //counter abc
            if (a == "x" && b == "x" && played != 1)
            {
                while (c != "x" && c != "o")
                {
                    c = "o";
                    played = 1;
                }
            }
            if (b == "x" && c == "x" && played != 1)
            {
                while (a != "x" && a != "o")
                {
                    a = "o";
                    played = 1;
                }
            }
            if (a == "x" && c == "x" && played != 1)
            {
                while (b != "x" && b != "o")
                {
                    b = "o";
                    played = 1;
                }
            }
            //win abc
            if (a == "o" && b == "o" && played != 1)
            {
                while (c != "x" && c != "o")
                {
                    c = "o";
                    played = 1;
                }
            }
            if (b == "o" && c == "o" && played != 1)
            {
                while (a != "x" && a != "o")
                {
                    a = "o";
                    played = 1;
                }
            }
            if (a == "o" && c == "o" && played != 1)
            {
                while (b != "x" && b != "o")
                {
                    b = "o";
                    played = 1;
                }
            }
            //counter def
            if (d == "x" && e == "x" && played != 1)
            {
                while (f != "x" && f != "o")
                {
                    f = "o";
                    played = 1;
                }
            }
            if (d == "x" && f == "x" && played != 1)
            {
                while (e != "x" && e != "o")
                {
                    e = "o";
                    played = 1;
                }
            }
            if (e == "x" && f == "x" && played != 1)
            {
                while (d != "x" && d != "o")
                {
                    d = "o";
                    played = 1;
                }
            }
            //win def
            if (d == "o" && e == "o" && played != 1)
            {
                while (f != "x" && f != "o")
                {
                    f = "o";
                    played = 1;
                }
            }
            if (d == "o" && f == "o" && played != 1)
            {
                while (e != "x" && e != "o")
                {
                    e = "o";
                    played = 1;
                }
            }
            if (e == "o" && f == "o" && played != 1)
            {
                while (d != "x" && d != "o")
                {
                    d = "o";
                    played = 1;
                }
            }
            //counter ghi
            if (g == "x" && h == "x" && played != 1)
            {
                while (i != "x" && i != "o")
                {
                    i = "o";
                    played = 1;
                }
            }
            if (h == "x" && i == "x" && played != 1)
            {
                while (g != "x" && g != "o")
                {
                    g = "o";
                    played = 1;
                }
            }
            if (g == "x" && i == "x" && played != 1)
            {
                while (h != "x" && h != "o")
                {
                    h = "o";
                    played = 1;
                }
            }
            //win ghi
            if (g == "o" && h == "o" && played != 1)
            {
                while (i != "x" && i != "o")
                {
                    i = "o";
                    played = 1;
                }
            }
            if (h == "o" && i == "o" && played != 1)
            {
                while (g != "x" && g != "o")
                {
                    g = "o";
                    played = 1;
                }
            }
            if (g == "o" && i == "o" && played != 1)
            {
                while (h != "x" && h != "o")
                {
                    h = "o";
                    played = 1;
                }
            }
            //counter adg
            if (a == "x" && d == "x" && played != 1)
            {
                while (g != "x" && g != "o")
                {
                    g = "o";
                    played = 1;
                }
            }
            if (d == "x" && g == "x" && played != 1)
            {
                while (a != "x" && a != "o")
                {
                    a = "o";
                    played = 1;
                }
            }
            if (a == "x" && g == "x" && played != 1)
            {
                while (d != "x" && d != "o")
                {
                    d = "o";
                    played = 1;
                }
            }
            //win adg
            if (a == "o" && d == "o" && played != 1)
            {
                while (g != "x" && g != "o")
                {
                    g = "o";
                    played = 1;
                }
            }
            if (d == "o" && g == "o" && played != 1)
            {
                while (a != "x" && a != "o")
                {
                    a = "o";
                    played = 1;
                }
            }
            if (a == "o" && g == "o" && played != 1)
            {
                while (d != "x" && d != "o")
                {
                    d = "o";
                    played = 1;
                }
            }
            //counter beh
            if (b == "x" && e == "x" && played != 1)
            {
                while (h != "x" && h != "o")
                {
                    h = "o";
                    played = 1;
                }
            }
            if (e == "x" && h == "x" && played != 1)
            {
                while (b != "x" && b != "o")
                {
                    b = "o";
                    played = 1;
                }
            }
            if (b == "x" && h == "x" && played != 1)
            {
                while (e != "x" && e != "o")
                {
                    e = "o";
                    played = 1;
                }
            }
            //win beh
            if (b == "o" && e == "o" && played != 1)
            {
                while (h != "x" && h != "o")
                {
                    h = "o";
                    played = 1;
                }
            }
            if (e == "o" && h == "o" && played != 1)
            {
                while (b != "x" && b != "o")
                {
                    b = "o";
                    played = 1;
                }
            }
            if (b == "o" && h == "o" && played != 1)
            {
                while (e != "x" && e != "o")
                {
                    e = "o";
                    played = 1;
                }
            }
            //counter cfi
            if (c == "x" && f == "x" && played != 1)
            {
                while (i != "x" && i != "o")
                {
                    i = "o";
                    played = 1;
                }
            }
            if (c == "x" && i == "x" && played != 1)
            {
                while (f != "x" && f != "o")
                {
                    f = "o";
                    played = 1;
                }
            }
            if (i == "x" && f == "x" && played != 1)
            {
                while (c != "x" && c != "o")
                {
                    c = "o";
                    played = 1;
                }
            }
            //win cfi
            if (c == "o" && f == "o" && played != 1)
            {
                while (i != "x" && i != "o")
                {
                    i = "o";
                    played = 1;
                }
            }
            if (c == "o" && i == "o" && played != 1)
            {
                while (f != "x" && f != "o")
                {
                    f = "o";
                    played = 1;
                }
            }
            if (i == "o" && f == "o" && played != 1)
            {
                while (c != "x" && c != "o")
                {
                    c = "o";
                    played = 1;
                }
            }
            //counter aei
            if (a == "x" && e == "x" && played != 1)
            {
                while (i != "x" && i != "o")
                {
                    i = "o";
                    played = 1;
                }
            }
            if (e == "x" && i == "x" && played != 1)
            {
                while (a != "x" && a != "o")
                {
                    a = "o";
                    played = 1;
                }
            }
            if (a == "x" && i == "x" && played != 1)
            {
                while (e != "x" && e != "o")
                {
                    e = "o";
                    played = 1;
                }
            }
            //win aei
            if (a == "o" && e == "o" && played != 1)
            {
                while (i != "x" && i != "o")
                {
                    i = "o";
                    played = 1;
                }
            }
            if (e == "o" && i == "o" && played != 1)
            {
                while (a != "x" && a != "o")
                {
                    a = "o";
                    played = 1;
                }
            }
            if (a == "o" && i == "o" && played != 1)
            {
                while (e != "x" && e != "o")
                {
                    e = "o";
                    played = 1;
                }
            }
            //counter ceg
            if (c == "x" && e == "x" && played != 1)
            {
                while (g != "x" && g != "o")
                {
                    g = "o";
                    played = 1;
                }
            }
            if (c == "x" && g == "x" && played != 1)
            {
                while (e != "x" && e != "o")
                {
                    e = "o";
                    played = 1;
                }
            }
            if (e == "x" && g == "x" && played != 1)
            {
                while (c != "x" && c != "o")
                {
                    c = "o";
                    played = 1;
                }
            }
            //win ceg
            if (c == "o" && e == "o" && played != 1)
            {
                while (g != "x" && g != "o")
                {
                    g = "o";
                    played = 1;
                }
            }
            if (c == "o" && g == "o" && played != 1)
            {
                while (e != "x" && e != "o")
                {
                    e = "o";
                    played = 1;
                }
            }
            if (e == "o" && g == "o" && played != 1)
            {
                while (c != "x" && c != "o")
                {
                    c = "o";
                    played = 1;
                }
            }

            //assign played marks to grid
            //gameboard = $"`_{a}_|_{b}_|_{c}_`" + "\r" +
            //             $"`_{d}_|_{e}_|_{f}_`" + "\r" +
            //             $"`_{g}_|_{h}_|_{i}_`";

            gameboard = $"```_{a}_|_{b}_|_{c}_" + "\r" +
                           $"_{d}_|_{e}_|_{f}_" + "\r" +
                           $"_{g}_|_{h}_|_{i}_```";


            await Context.Channel.SendMessageAsync(gameboard);//print gameboard to channel

            //win conditions
            //player wins
            if (a == "x" && b == "x" && c == "x")
            {
                await Context.Channel.SendMessageAsync("You win! Congrats!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (d == "x" && e == "x" && f == "x")
            {
                await Context.Channel.SendMessageAsync("You win! Congrats!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (g == "x" && h == "x" && i == "x")
            {
                await Context.Channel.SendMessageAsync("You win! Congrats!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (a == "x" && e == "x" && i == "x")
            {
                await Context.Channel.SendMessageAsync("You win! Congrats!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (c == "x" && e == "x" && g == "x")
            {
                await Context.Channel.SendMessageAsync("You win! Congrats!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (a == "x" && d == "x" && g == "x")
            {
                await Context.Channel.SendMessageAsync("You win! Congrats!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (b == "x" && e == "x" && h == "x")
            {
                await Context.Channel.SendMessageAsync("You win! Congrats!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (c == "x" && f == "x" && i == "x")
            {
                await Context.Channel.SendMessageAsync("You win! Congrats!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            //bot wins
            else if (a == "o" && b == "o" && c == "o")
            {
                await Context.Channel.SendMessageAsync("I win! Better luck next time!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (d == "o" && e == "o" && f == "o")
            {
                await Context.Channel.SendMessageAsync("I win! Better luck next time!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (g == "o" && h == "o" && i == "o")
            {
                await Context.Channel.SendMessageAsync("I win! Better luck next time!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (a == "o" && e == "o" && i == "o")
            {
                await Context.Channel.SendMessageAsync("I win! Better luck next time!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (c == "o" && e == "o" && g == "o")
            {
                await Context.Channel.SendMessageAsync("I win! Better luck next time!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (a == "o" && d == "o" && g == "o")
            {
                await Context.Channel.SendMessageAsync("I win! Better luck next time!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (b == "o" && e == "o" && h == "o")
            {
                await Context.Channel.SendMessageAsync("I win! Better luck next time!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            else if (c == "o" && f == "o" && i == "o")
            {
                await Context.Channel.SendMessageAsync("I win! Better luck next time!");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            //draw
            else if (a != "_" && b != "_" && c != "_" && d != "_" && e != "_" && f != "_" && g != "_" && h != "_" && i != "_")
            {
                await Context.Channel.SendMessageAsync("The game was a draw.");

                //reset game
                account.tttA = reset;
                account.tttB = reset;
                account.tttC = reset;
                account.tttD = reset;
                account.tttE = reset;
                account.tttF = reset;
                account.tttG = reset;
                account.tttH = reset;
                account.tttI = reset;

                UserAccounts.SaveAccounts();
            }
            //game still going and it saves progress
            else
            {
                account.tttA = a;
                account.tttB = b;
                account.tttC = c;
                account.tttD = d;
                account.tttE = e;
                account.tttF = f;
                account.tttG = g;
                account.tttH = h;
                account.tttI = i;

                UserAccounts.SaveAccounts();
            }
        }
        //Add GBP
        [Command("addGBP")]
        public async Task AddGoodBoyPoints(IGuildUser user, double pay)
        {
            //make sure only I can use this command
            var account = UserAccounts.GetAccount(Context.User);

            //verify user
            if (Context.User.Id != 226773963160682496)//me
            {
                await Context.Channel.SendMessageAsync($"You are not permitted to use this command. Politely fuck off.");
                return;
            }

            string currencyType = "Goodboypoints";

            //get userID of user
            ulong userID = user.Id;

            //change user to mentioned user
            account = UserAccounts.GetAccount(userID);//targets account

            //set up directmessage
            var dmChannel = await user.GetOrCreateDMChannelAsync();

            
            //update account
            account.GBP += pay;
            UserAccounts.SaveAccounts();
            await dmChannel.SendMessageAsync($"You have been given {pay} Good Boy Point by the generous and omnipotent MegaCereal, Inter-Galactic Panhandler, for being a very good boy.");

            //update economey so it accurately represents the total gbp in the entire economey
            var bankAccount = BankAccounts.GetAccount(currencyType);
            bankAccount.totalFunds += pay;
            BankAccounts.SaveBankAccounts();
        }
        //Add Money
        [Command("addMoney")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        public async Task AddMoney(IGuildUser user, double money)
        {
            //make sure only I can use this command
            var account = UserAccounts.GetAccount(Context.User);

            //verify user
            if (Context.User.Id != 226773963160682496)//me
            {
                await Context.Channel.SendMessageAsync($"You are not permitted to use this command. Politely fuck off.");
                return;
            }

            string currencyType = "Lennybucks";

            //get userID of user
            ulong userID = user.Id;

            //change user to mentioned user
            account = UserAccounts.GetAccount(userID);//targets account

            //set up directmessage
            var dmChannel = await user.GetOrCreateDMChannelAsync();

            //update account
            account.Money += money;
            UserAccounts.SaveAccounts();
            await dmChannel.SendMessageAsync($"You have been given {money} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅] by the generous and omnipotent MegaCereal, Inter-Galactic Panhandler, for he has taken pity on your poorness.");

            //update economey so it accurately represents the total gbp in the entire economey
            var bankAccount = BankAccounts.GetAccount(currencyType);
            bankAccount.totalFunds += money;
            BankAccounts.SaveBankAccounts();
        }
        //store to spend goodpoints
        [Command("gbpStore")]
        public async Task GoodBoyPointsStore(string iNum = "0", string qty = "0")//item number, quantity
        {
            string pic = "C:\\Temp\\broke.png";

            await Context.Channel.SendFileAsync(pic);

            ////get user account and account balance of lennybucks
            //var account = UserAccounts.GetAccount(Context.User);
            //double accbal = account.Money;

            ////get GBP store attributes
            //string storeType = "GBP";
            //var store = GBPstores.GetAccount(storeType);

            ////set variables
            //string currencyType = "Goodboypoints";
            //string lnItem = "";
            //string message = "";
            //uint iNum2;
            //uint qty2;
            //string storedInventory = "";
            //Random rnd = new Random();
            //int[] shopItems;
            //int i;
            //int itmNum = 1;
            //double price = 0;
            //int c = 0;
            //int a = 0;
            //List<string> myItems = account.gbpitems;

            ////store formatting
            //string start = "```--------------------------------------------------" + "\r";
            //string title = String.Format("{0,-10} | {1,-20} | {2,6}", "Item #", "Item", "Cost") + "\r";
            //string line = "--------------------------------------------------" + "\r";
            //string end = "--------------------------------------------------```";

            ////read txt file into array
            //string[] gbpStore = File.ReadAllLines(@"C:\Users\David\Documents\Visual Studio 2017\Projects\DiscordBot1\DiscordBot1\bin\Debug\Resources\gbpStoreItems.txt");
            //string[] gbpPrice = File.ReadAllLines(@"C:\Users\David\Documents\Visual Studio 2017\Projects\DiscordBot1\DiscordBot1\bin\Debug\Resources\gbpStoreITemsPrices.txt");

            ////parse user input
            //if (!uint.TryParse(iNum, out iNum2))
            //{
            //    await Context.Channel.SendMessageAsync("Please enter a positive, non decimal number");
            //    return;
            //}
            //if (!uint.TryParse(qty, out qty2))
            //{
            //    await Context.Channel.SendMessageAsync("Please enter a positive, non decimal number");
            //    return;
            //}

            ////display store
            //if (iNum2 == 0 && qty2 == 0)
            //{
            //    //if timestamp is created yesterday run loop, if not, then display message and skip generation of new list
            //    if (store.time == DateTime.Today)
            //    {
            //        message = store.inventory;
            //        await Context.Channel.SendMessageAsync(message);
            //        await Context.Channel.SendMessageAsync("Store updates every morning at 12am EST.");
            //        return;
            //    }

            //    //generate random values for store items
            //    List<int> listNumbers = new List<int>();
            //    int number;
            //    for (i = 0; i < 5; i++)
            //    {
            //        do
            //        {
            //            number = rnd.Next(0, gbpStore.Length);

            //        } while (listNumbers.Contains(number));
            //        listNumbers.Add(number);//add number to list
            //        message = message + String.Format("{0,-10} | {1,-20} | {2,6}", itmNum, gbpStore[number], gbpPrice[number]).Trim() + "\r";

            //        if (i < 5)
            //        {
            //            storedInventory = storedInventory + String.Format("{0,-10} | {1,-20} | {2,6}", itmNum, gbpStore[number], gbpPrice[number]).Trim() + "\r";
            //            itmNum++;
            //        }
            //    }

            //    //save list as an array in the json
            //    store.list = listNumbers.ToArray();
            //    //format store
            //    message = start + title + line + message + end;
            //    //print store
            //    await Context.Channel.SendMessageAsync(message);
            //    await Context.Channel.SendMessageAsync("Store updates every morning at 12am EST.");

            //    //save message to file
            //    message = start + title + line + storedInventory + end;
            //    store.inventory = message;
            //    //timestamp file save
            //    store.time = DateTime.Today;
            //    //save all
            //    GBPstores.SaveGBPStoreAccounts();
            //    return;
            //}

            ////if store isn't updated for today ask them to update it. will try to automate later
            //if (store.time != DateTime.Today)
            //{
            //    await Context.Channel.SendMessageAsync("Please refresh store by inputing !lbstore with no parameters. I will attempt to automate the update later. Thanks.");
            //    return;
            //}

            ////check for valid input
            //if (iNum2 > 5 || qty2 <= 0 || iNum2 <= 0)
            //{
            //    await Context.Channel.SendMessageAsync("If you plan on buying something you need to provide a valid item number and quantity greater than 1.");
            //    return;
            //}

            ////import item and price
            //shopItems = store.list;

            //int val = shopItems[iNum2 - 1];//adjust value for array bounds

            //lnItem = gbpStore[val];
            //price = double.Parse(gbpPrice[val]);

            //price = qty2 * price;//get total cost

            ////check to see if they can afford their purchase
            //if (price > accbal)
            //{
            //    await Context.Channel.SendMessageAsync($"You don't have enough [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅] for this transaction totaling `{Math.Floor(price)}  [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅].`. You currently have `{Math.Floor(accbal)}  [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅].`");
            //    return;
            //}

            ////update account
            //account.Money -= price;
            //UserAccounts.SaveAccounts();
            //await Context.User.SendMessageAsync($"You have purchased {qty2} {lnItem} for {price} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅]. Thank you for your patronage.");

            ////update economey so it accurately represents the total gbp in the entire economey
            //var bankAccount = BankAccounts.GetAccount(currencyType);
            //bankAccount.totalFunds -= price;
            //BankAccounts.SaveBankAccounts();

            ////add to inventory
            //while (!myItems.Contains(lnItem))
            //{
            //    account.gbpitems.Add(lnItem);
            //    account.gbpamount.Add((int)qty2);

            //    c = 1;
            //}

            //if (c != 1 && myItems.Contains(lnItem))
            //{
            //    while (account.gbpitems[a] != lnItem)
            //    {
            //        a++;
            //    }
            //    account.gbpamount[a] += (int)qty2;
            //}

            //UserAccounts.SaveAccounts();
        }
        [Command("lbstore")]
        public async Task LennybucksStore(string iNum = "0", string qty = "0")//item number, quantity
        {
            //get user account and account balance of lennybucks
            var account = UserAccounts.GetAccount(Context.User);
            double accbal = account.Money;

            //get LB store attributes
            string storeType = "LB";
            var store = LBstores.GetAccount(storeType);

            //set variables
            string currencyType = "Lennybucks";
            string lnItem = "";
            string message = "";
            uint iNum2;
            uint qty2;
            string storedInventory = "";
            Random rnd = new Random();
            int[] shopItems;
            int itmNum = 1;
            double price = 0;
            int i;
            int c = 0;
            int a = 0;
            List<string> myItems = account.lbitems;

            //store formatting
            string start = "```--------------------------------------------------" + "\r";
            string title = String.Format("{0,-10} | {1,-20} | {2,6}", "Item #", "Item", "Cost") + "\r";
            string line = "--------------------------------------------------" + "\r";
            string end = "--------------------------------------------------```";

            //read txt file into array
            string[] lbStore= File.ReadAllLines(@"C:\Users\David\Documents\Visual Studio 2017\Projects\DiscordBot1\DiscordBot1\bin\Debug\Resources\lbStoreItems.txt");
            string[] lbPrice = File.ReadAllLines(@"C:\Users\David\Documents\Visual Studio 2017\Projects\DiscordBot1\DiscordBot1\bin\Debug\Resources\lbStoreITemsPrices.txt");

            //parse user input
            if (!uint.TryParse(iNum, out iNum2))
            {
                await Context.Channel.SendMessageAsync("Please enter a positive, non decimal number");
                return;
            }
            if (!uint.TryParse(qty, out qty2))
            {
                await Context.Channel.SendMessageAsync("Please enter a positive, non decimal number");
                return;
            }

            //display store
            if (iNum2 == 0 && qty2 == 0)
            {
                //if timestamp is created yesterday run loop, if not, then display message and skip generation of new list
                if (store.time == DateTime.Today)
                {
                    message = store.inventory;
                    await Context.Channel.SendMessageAsync(message);
                    await Context.Channel.SendMessageAsync("Store updates every morning at 12am EST.");
                    return;
                }

                //generate random values for store items
                List<int> listNumbers = new List<int>();
                int number;
                for (i = 0; i < 5; i++)
                {
                    do
                    {
                        number = rnd.Next(0, lbStore.Length);
                        
                    } while (listNumbers.Contains(number));
                    listNumbers.Add(number);//add number to list
                    message = message + String.Format("{0,-10} | {1,-20} | {2,6}", itmNum, lbStore[number], lbPrice[number]).Trim() + "\r";

                    if(i < 5)
                    {
                        storedInventory = storedInventory + String.Format("{0,-10} | {1,-20} | {2,6}", itmNum, lbStore[number], lbPrice[number]).Trim() + "\r";
                        itmNum++;
                    }                    
                }

                //save list as an array in the json
                store.list = listNumbers.ToArray();
                //format store
                message = start + title + line + message + end;
                //print store
                await Context.Channel.SendMessageAsync(message);
                await Context.Channel.SendMessageAsync("Store updates every morning at 12am EST.");

                //save message to file
                message = start  + title + line + storedInventory + end;
                store.inventory = message;
                //timestamp file save
                store.time = DateTime.Today;
                //save all
                LBstores.SaveLBStoreAccounts();
                return;
            }

            //if store isn't updated for today ask them to update it. will try to automate later
            if(store.time != DateTime.Today)
            {
                await Context.Channel.SendMessageAsync("Please refresh store by inputing !lbstore with no parameters. I will attempt to automate the update later. Thanks.");
                return;
            }

            //check for valid input
            if (iNum2 > 5 || qty2 <= 0 || iNum2 <= 0)
            {
                await Context.Channel.SendMessageAsync("If you plan on buying something you need to provide a valid item number and quantity greater than 1.");
                return;
            }

            //import item and price
            shopItems = store.list;

            int val = shopItems[iNum2 - 1];//adjust value for array bounds

            lnItem = lbStore[val];
            price = double.Parse(lbPrice[val]);

            price = qty2 * price;//get total cost

            //check to see if they can afford their purchase
            if (price > accbal)
            {
                await Context.Channel.SendMessageAsync($"You don't have enough [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅] for this transaction totaling `{Math.Floor(price)}  [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅].`. You currently have `{Math.Floor(accbal)}  [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅].`");
                return;
            }

            //update account
            account.Money -= price;
            UserAccounts.SaveAccounts();
            await Context.User.SendMessageAsync($"You have purchased {qty2} {lnItem} for {price} [̲̅$̲̅(̲̅ ͡° ͜ʖ ͡°̲̅)̲̅$̲̅]. Thank you for your patronage.");

            //update economey so it accurately represents the total lb in the entire economey
            var bankAccount = BankAccounts.GetAccount(currencyType);
            bankAccount.totalFunds -= price;
            BankAccounts.SaveBankAccounts();

            //add item and amount purchased to inventory
            while (!myItems.Contains(lnItem))
            {
                account.lbitems.Add(lnItem);
                account.lbamount.Add((int)qty2);

                c = 1;
            }

            //update inventory amount if you bought another of the same item
            if (c != 1 && myItems.Contains(lnItem))
            {
                while (account.lbitems[a] != lnItem)
                {
                    a++;
                }
                account.lbamount[a] += (int)qty2;
            }
            //save account
            UserAccounts.SaveAccounts();
        }
        [Command("Inventory")]
        public async Task Inventory()
        {
            //get user account
            var account = UserAccounts.GetAccount(Context.User);

            //get items from json
            string[] myItems;
            int[] numItems;

            //assign to values
            myItems = account.lbitems.ToArray();
            numItems = account.lbamount.ToArray();

            //declare variables
            string message = "";
            int i = 0;
            
            while(i < myItems.Length)
            {
                message = message + myItems[i] + " - " + numItems[i] + "\r";
                i++;
            }

            await Context.Channel.SendMessageAsync($"```{Context.User.Username}'s Inventory" + "\r" + "\r" + message +"```");
            //await Context.Channel.SendMessageAsync(message);
        }
        //request transfer and sees if you have enough currency
        [Command("request")]
        public async Task Request(ulong userID, string amount, string currencyType, string transactionID)
        {
            //get user 
            var account = UserAccounts.GetAccount(Context.User);

            //verify user
            if (Context.User.Id != 415944041419505664)//bankbot
            {
                await Context.Channel.SendMessageAsync($"You are not permitted to use this command. Politely fuck off.");
                await Context.Channel.SendMessageAsync("deny withdraw " + transactionID + ".");
                return;
            }

            //declare variable
            double accbal;
            double amount1;

            if (!double.TryParse(amount, out amount1))
            {
                await Context.Channel.SendMessageAsync("deny withdraw " + transactionID + ". Please enter a valid number.");
                return;
            }
            if (amount1 < 0)
            {
                await Context.Channel.SendMessageAsync("deny withdraw " + transactionID + ". Please enter a valid number.");
                return;
            }

            //check to see if account exists
            var user = UserAccounts.GetAccount(userID);

            //chk currency type is either Lennybucks or Goodboypoints
            if (currencyType == "Lennybucks")
            {
                //check to see if withdraw amt is greater than acc bal
                accbal = user.Money;

                if (amount1 > accbal)
                {
                    await Context.Channel.SendMessageAsync("deny withdraw " + transactionID);
                    return;
                }
            }

            else if (currencyType == "Goodboypoints")
            {
                //check to see if withdraw amt is greater than acc bal
                accbal = user.GBP;

                if (amount1 > accbal)
                {
                    await Context.Channel.SendMessageAsync("deny withdraw " + transactionID);
                    return;
                }
            }
            else
            {
                return;
            }

            //report confirm msg
            await Context.Channel.SendMessageAsync("confirm withdraw " + transactionID);
        }
        //deposit money into correct account
        [Command("deposit")]
        public async Task Deposit(ulong userID, string amount, string currencyType, string transactionID)
        {
            //get user 
            var account = UserAccounts.GetAccount(Context.User);

            //verify user
            if (Context.User.Id != 415944041419505664 && Context.User.Id != 370772796323790848)//bankbot, saltbot
            {
                await Context.Channel.SendMessageAsync($"You are not permitted to use this command. Politely fuck off.");
                await Context.Channel.SendMessageAsync("deny deposit " + transactionID + ".");
                return;
            }

            //check to see if account exists
            var user = UserAccounts.GetAccount(userID);

            double amount1;
            //string id;

            if (!double.TryParse(amount, out amount1))
            {
                await Context.Channel.SendMessageAsync("deny deposit " + transactionID + ". Please enter a valid number.");
                return;
            }
            if (amount1 < 0)
            {
                await Context.Channel.SendMessageAsync("deny deposit " + transactionID + ". Please enter a valid number.");
                return;
            }

            //chk currency type is either Lennybucks or Goodboypoints
            if (currencyType == "Lennybucks")
            {

                //update user accounts
                user.Money += amount1;
                UserAccounts.SaveAccounts();

                //updae bank total
                var bankAccount = BankAccounts.GetAccount(currencyType);
                bankAccount.totalFunds += amount1;
                BankAccounts.SaveBankAccounts();

                //report confirm msg
                await Context.Channel.SendMessageAsync("confirm deposit " + transactionID);
            }

            else if (currencyType == "Goodboypoints")
            {
                //id = "2";

                //update user accounts
                user.GBP += amount1;
                UserAccounts.SaveAccounts();

                //update accounts
                var bankAccount = BankAccounts.GetAccount(currencyType);
                bankAccount.totalFunds += amount1;
                BankAccounts.SaveBankAccounts();

                //report confirm msg
                await Context.Channel.SendMessageAsync("confirm deposit " + transactionID);
            }
            else
            {
                //if currency does not exist
                return;
            }

        }
        //complete withdraw transfer
        [Command("complete")]
        public async Task Complete(ulong userID, string amount, string currencyType, string transactionID)
        {
            //get user 
            var account = UserAccounts.GetAccount(Context.User);

            //verify user
            if (Context.User.Id != 415944041419505664)//bankbot
            {
                await Context.Channel.SendMessageAsync($"You are not permitted to use this command. Politely fuck off.");
                await Context.Channel.SendMessageAsync("deny withdraw " + transactionID + ".");
                return;
            }

            //check to see if account exists
            var user = UserAccounts.GetAccount(userID);

            double amount1;

            if (!double.TryParse(amount, out amount1))
            {
                await Context.Channel.SendMessageAsync("deny withdraw " + transactionID + ". Please enter a valid number.");
                return;
            }
            if (amount1 < 0)
            {
                await Context.Channel.SendMessageAsync("deny withdraw " + transactionID + ". Please enter a valid number.");
                return;
            }

            //chk currency type is either Lennybucks or Goodboypoints
            if (currencyType == "Lennybucks")
            {
                //update user accounts
                user.Money -= amount1;
                UserAccounts.SaveAccounts();

                //update accounts
                var bankAccount = BankAccounts.GetAccount(currencyType);
                bankAccount.totalFunds -= amount1;
                BankAccounts.SaveBankAccounts();

                //report confirm msg
                await Context.Channel.SendMessageAsync("completed transaction " + transactionID);
            }

            else if (currencyType == "Goodboypoints")
            {
                //update user accounts
                user.GBP -= amount1;
                UserAccounts.SaveAccounts();

                //update accounts
                var bankAccount = BankAccounts.GetAccount(currencyType);
                bankAccount.totalFunds -= amount1;
                BankAccounts.SaveBankAccounts();

                //report confirm msg
                await Context.Channel.SendMessageAsync("completed transaction " + transactionID);
            }
            else
            {
                //if currency does not exist
                return;
            }
        }
        //display how much currency is in circulation
        [Command("currency")]
        public async Task Currency(string currencyType)
        {
            //get user 
            var account = UserAccounts.GetAccount(Context.User);

            //verify user
            if (Context.User.Id != 226773963160682496 && Context.User.Id != 415944041419505664 && Context.User.Id != 163129531278819328)//me, bankbot, connor
            {
                await Context.Channel.SendMessageAsync($"You are not permitted to use this command. Politely fuck off.");
                return;
            }

            //declare variable
            double amount = 0;

            //chk currency type is either Lennybucks or Goodboypoints
            if (currencyType == "Lennybucks")
            {
                //get total amount of currency in circulation
                var bankAccount = BankAccounts.GetAccount(currencyType);
                amount = bankAccount.totalFunds;

                //print out total currency in circulation
                await Context.Channel.SendMessageAsync($"{amount} {currencyType} in circulation.");
            }

            else if (currencyType == "Goodboypoints")
            {
                //get total amount of currency in circulation
                var bankAccount = BankAccounts.GetAccount(currencyType);
                amount = bankAccount.totalFunds;

                //print out total currency in circulation
                await Context.Channel.SendMessageAsync($"{amount} {currencyType} in circulation.");
            }
            else
            {
                return;
            }
        }
        [Command("setgame")]
        public async Task SetBotGame([Remainder]string game)
        {
            //make sure only I can use this command
            var account = UserAccounts.GetAccount(Context.User);

            //verify user
            if (Context.User.Id != 226773963160682496)//me
            {
                await Context.Channel.SendMessageAsync($"You are not permitted to use this command. Politely fuck off.");
                return;
            }

            DiscordSocketClient _client = new DiscordSocketClient();
            //Game g = new Game(game, "", StreamType.NotStreaming);
            await _client.SetGameAsync(game);
        }
        [Command("Test")]
        public async Task Pokemon(string Pokemon)
        {
            //make sure images exists
            //load list of available pokemon into array from txt or json
            //cycle though array for user input.
            //if pokemon does not exist then return with a message
            
            //string BattleStage = @"C:\Users\David\Documents\Visual Studio 2017\Projects\DiscordBot1\DiscordBot1\bin\Debug\Resources\Pictures\BattleImage\Battle.jpg";
            //string Pokemon1 = $@"C:\Users\David\Documents\Visual Studio 2017\Projects\DiscordBot1\DiscordBot1\bin\Debug\Resources\Pictures\Pokemon\{Pokemon.ToLower().Trim()}.jpg";

            //System.Drawing.Image bgImg = System.Drawing.Image.FromFile("C:\\Users\\David\\Documents\\Visual Studio 2017\\Projects\\DiscordBot1\\DiscordBot1\\bin\\Debug\\Resources\\Pictures\\BattleImage\\Stage.jpg");
            //System.Drawing.Image Pokemon1 = System.Drawing.Image.FromFile($"C:\\Users\\David\\Documents\\Visual Studio 2017\\Projects\\DiscordBot1\\DiscordBot1\\bin\\Debug\\Resources\\Pictures\\Pokemon\\{Pokemon.ToLower().Trim()}.jpg");
            //Graphics grImage = Graphics.FromImage(bgImg);
            //grImage.DrawImage(Pokemon1, bgImg.Width / 2, 10);
            //bgImg.Save("C:\\Users\\David\\Documents\\Visual Studio 2017\\Projects\\DiscordBot1\\DiscordBot1\\bin\\Debug\\Resources\\Pictures\\Completed\\Test.jpg");

            //await Context.Channel.SendFileAsync("C:\\Users\\David\\Documents\\Visual Studio 2017\\Projects\\DiscordBot1\\DiscordBot1\\bin\\Debug\\Resources\\Pictures\\Completed\\Test.jpg");

            
        }
    }
}
