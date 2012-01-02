using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MerboGrease
{
    internal class ProgramFunction
    {
        public static void Run()
        {
            Properties.Settings.Default.OwnerHost = "MerbosMagic[/.].*";
            LogLine("Using default ownerhost, as this is beta.");

        Server:
            Log("Type your desired IRC server: ", 2);
            string s = Console.ReadLine();
            if (s != "")
                Properties.Settings.Default.Server = s;
            else
            {
                LogLine("You cannot do that!", 6);
                goto Server;
            }

        Port:
            Log("Type your desired port: ", 2);
            string sp = Console.ReadLine();
            int p;
            if (int.TryParse(sp, out p))
            {
                Properties.Settings.Default.Port = p;
            }
            else
            {
                LogLine("You cannot do that!", 6);
                goto Port;
            }

        Nick:
            Log("Type your desired bot nickname: ", 2);
            string n = Console.ReadLine();
            if (n != "")
                Properties.Settings.Default.User = n;
            else
            {
                LogLine("You cannot do that!", 6);
                goto Nick;
            }

        RealName:
            Log("Type your desired bot realname: ", 2);
            string rn = Console.ReadLine();
            if (rn != "")
                Properties.Settings.Default.RealName = rn;
            else
            {
                LogLine("You cannot do that!", 6);
                goto RealName;
            }

        Channel:
            Log("Type your desired bot channel: ", 2);
            string c = Console.ReadLine();
            if (c != "" && c.StartsWith("#"))
                Properties.Settings.Default.Channel = c;
            else
            {
                LogLine("You cannot do that!", 6);
                goto Channel;
            }

            Log("Type your desired nickserv user: ", 2);
            Properties.Settings.Default.NSUser = Console.ReadLine();
            Log("Now type your desired NickServ pass: ", 2);
            Properties.Settings.Default.NSPass = Console.ReadLine();
            IRCFunction.Run();
        }

        public static void LogLine(string data, int level, string color = "white")
        {
            ConsoleColor c = ConsoleColor.White;
            string leveltext = "FINEST: ";

            #region ColorPicker

            switch (color.ToLower())
            {
                case "white":
                    c = ConsoleColor.White;
                    break;

                case "black":
                    c = ConsoleColor.Black;
                    break;

                case "red":
                    c = ConsoleColor.Red;
                    break;

                case "blue":
                    c = ConsoleColor.Blue;
                    break;

                case "cyan":
                    c = ConsoleColor.Cyan;
                    break;

                case "gray":
                    c = ConsoleColor.Gray;
                    break;

                case "green":
                    c = ConsoleColor.Green;
                    break;

                case "magenta":
                    c = ConsoleColor.Magenta;
                    break;

                case "yellow":
                    c = ConsoleColor.Yellow;
                    break;

                case "darkblue":
                    c = ConsoleColor.DarkBlue;
                    break;

                case "darkcyan":
                    c = ConsoleColor.DarkCyan;
                    break;

                case "darkgray":
                    c = ConsoleColor.DarkGray;
                    break;

                case "darkgreen":
                    c = ConsoleColor.DarkGreen;
                    break;

                case "darkmagenta":
                    c = ConsoleColor.DarkMagenta;
                    break;

                case "darkyellow":
                    c = ConsoleColor.DarkYellow;
                    break;

                default:
                    c = ConsoleColor.White;
                    break;
            }

            #endregion ColorPicker

            #region LevelPicker

            switch (level)
            {
                case 0:
                    leveltext = "FINEST: ";
                    break;
                case 1:
                    leveltext = "FINER: ";
                    break;
                case 2:
                    leveltext = "FINE: ";
                    break;

                case 3:
                    leveltext = "LOG: ";
                    break;
                case 4:
                    leveltext = "WARNING: ";
                    break;

                case 5:
                    leveltext = "ERRORDATA: ";
                    break;

                case 6:
                    leveltext = "ERROR: ";
                    break;
                default:
                    leveltext = "UNKNOWN: ";
                    break;
            }

            #endregion LevelPicker

            Console.ForegroundColor = c;
            if (leveltext != "BLOOF")
                Console.WriteLine(leveltext + data);
            Console.ResetColor();
        }

        public static void LogLine(string data, int level = 3)
        {
            ConsoleColor c = ConsoleColor.White;
            string leveltext = "FINEST: ";

            #region LevelPicker

            switch (level) //color our items the fun way :D
            {
                case 0:
                    leveltext = "FINEST: ";
                    c = ConsoleColor.DarkBlue;
                    break;
                case 1:
                    leveltext = "FINER: ";
                    c = ConsoleColor.Blue;
                    break;
                case 2:
                    leveltext = "FINE: ";
                    c = ConsoleColor.Cyan;
                    break;

                case 3:
                    leveltext = "LOG: ";
                    c = ConsoleColor.Green;
                    break;
                case 4:
                    leveltext = "WARNING: ";
                    c = ConsoleColor.Yellow;
                    break;

                case 5:
                    leveltext = "ERRORDATA: ";
                    c = ConsoleColor.Magenta;
                    break;

                case 6:
                    leveltext = "ERROR: ";
                    c = ConsoleColor.Red;
                    break;
                default:
                    leveltext = "BLOOF";
                    c = ConsoleColor.White;
                    break;
            }

            #endregion LevelPicker

            Console.ForegroundColor = c;
            if (leveltext != "BLOOF")
                Console.WriteLine(leveltext + data);
            Console.ResetColor();
        }

        public static void Log(string data, int level, string color = "white")
        {
            #region ColorPicker

            ConsoleColor c = ConsoleColor.White;
            switch (color.ToLower())
            {
                case "white":
                    c = ConsoleColor.White;
                    break;

                case "black":
                    c = ConsoleColor.Black;
                    break;

                case "red":
                    c = ConsoleColor.Red;
                    break;

                case "blue":
                    c = ConsoleColor.Blue;
                    break;

                case "cyan":
                    c = ConsoleColor.Cyan;
                    break;

                case "gray":
                    c = ConsoleColor.Gray;
                    break;

                case "green":
                    c = ConsoleColor.Green;
                    break;

                case "magenta":
                    c = ConsoleColor.Magenta;
                    break;

                case "yellow":
                    c = ConsoleColor.Yellow;
                    break;

                case "darkblue":
                    c = ConsoleColor.DarkBlue;
                    break;

                case "darkcyan":
                    c = ConsoleColor.DarkCyan;
                    break;

                case "darkgray":
                    c = ConsoleColor.DarkGray;
                    break;

                case "darkgreen":
                    c = ConsoleColor.DarkGreen;
                    break;

                case "darkmagenta":
                    c = ConsoleColor.DarkMagenta;
                    break;

                case "darkyellow":
                    c = ConsoleColor.DarkYellow;
                    break;

                default:
                    c = ConsoleColor.White;
                    break;
            }

            #endregion ColorPicker

            #region LevelPicker

            string leveltext;
            switch (level)
            {
                case 0:
                    leveltext = "FINEST: ";
                    break;
                case 1:
                    leveltext = "FINER: ";
                    break;
                case 2:
                    leveltext = "FINE: ";
                    break;

                case 3:
                    leveltext = "LOG: ";
                    break;
                case 4:
                    leveltext = "WARNING: ";
                    break;

                case 5:
                    leveltext = "ERRORDATA: ";
                    break;

                case 6:
                    leveltext = "ERROR: ";
                    break;
                default:
                    leveltext = "UNKNOWN: ";
                    break;
            }

            #endregion LevelPicker

            Console.ForegroundColor = c;
            Console.Write(data);
            Console.ResetColor();
        }

        public static void Log(string data, int level = 3)
        {
            #region LevelPicker

            ConsoleColor c = ConsoleColor.White;
            string leveltext;
            switch (level)
            {
                case 0:
                    leveltext = "FINEST: ";
                    c = ConsoleColor.DarkBlue;
                    break;
                case 1:
                    leveltext = "FINER: ";
                    c = ConsoleColor.Blue;
                    break;
                case 2:
                    leveltext = "FINE: ";
                    c = ConsoleColor.Cyan;
                    break;

                case 3:
                    leveltext = "LOG: ";
                    c = ConsoleColor.Green;
                    break;
                case 4:
                    leveltext = "WARNING: ";
                    c = ConsoleColor.Yellow;
                    break;

                case 5:
                    leveltext = "ERRORDATA: ";
                    c = ConsoleColor.Magenta;
                    break;

                case 6:
                    leveltext = "ERROR: ";
                    c = ConsoleColor.Red;
                    break;
                default:
                    leveltext = "UNKNOWN: ";
                    c = ConsoleColor.White;
                    break;
            }

            #endregion LevelPicker

            Console.ForegroundColor = c;
            Console.Write(data);
            Console.ResetColor();
        }

        public static void BlankLine()
        {
            Console.WriteLine();
        }
    }
}