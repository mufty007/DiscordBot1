//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DiscordBot1
//{
//    public class Timer
//    {
//        public Timer (string inputTime)
//        {
//            double time;
//            string message;
//            //checks to see if the value entered is a number
//            if (double.TryParse(inputTime, out time))
//            { 
//                time = time * 60;//converts time input to minutes

//                if (time > 600)//if timer is longer than 10min (600sec) it will throw an error. This can be changed if you want to allow a longer timer
//                {
//                    message = "Enter a numeric value of 10 or less, baka.";
//                    return(message);
//                }              

//                for (double a = 5; a >= 0; a--)
//                {
//                    System.Threading.Thread.Sleep(1000);
//                    string timerAsString = Convert.ToString(a);//converts time remaining to string so it can be posted in discord

//                    if(timerAsString == "0")
//                    {
//                        message = "Timer has started!";
//                        message = (message + " " + timerAsString);//displays countdown in discord
//                        continue;//breaks out of the for loop so it does not display a 0 in the countdown
//                    }
                    
//                }

//                Console.WriteLine("Timer set for " + time);

//                for (double a = time; a >= 0; a--)
//                {
//                    System.Threading.Thread.Sleep(1000);
//                }

//                message =  "Time's up!";
//                //return (message);
//            }
//            else
//            {
//                message = ("Enter a numeric value, baka.");//if they do not enter a number it throws an error and quits the command
//                //return(message);
//            }
//        }
//    }
//}
