using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MerboGrease
{
    internal class IRCFunction : ProgramFunction
    {
        public static TcpClient irc = new TcpClient(Properties.Settings.Default.Server, Properties.Settings.Default.Port);
        private static string NICK = Properties.Settings.Default.User;
        private static string USER = "USER " + NICK + " 0 * :" + Properties.Settings.Default.RealName;

        public static NetworkStream stream = irc.GetStream();
        public static StreamWriter writer = new StreamWriter(stream);
        public static StreamReader reader = new StreamReader(stream);

        public static void IRCSend(string data)
        {
            if (irc.Connected)
            {
#if DEBUG
                LogLine(data, 0);
#endif
                writer.WriteLine(data);
                writer.Flush();
            }
        }

        public static string IRCRecieve()
        {
            string r = reader.ReadLine();
            if (r != null)
            {
#if DEBUG
                LogLine(r, 0);
#endif
                return r;
            }
            else
                return "";
        }

        public static void Run()
        {
            string inputLine;
            try
            {
                LogLine("Logging in!", 3);
                IRCSend(USER);
                IRCSend("NICK " + NICK);
                while (irc.Connected)
                {
                    while ((inputLine = IRCRecieve()) != "")
                    {
                        string[] data = inputLine.Split(' ');

                        string hostmask = data[0];

                        if (data[0] == "PING")
                        {
                            string pong = data[1].Substring(1, data[1].Length - 1);
                            IRCSend("PONG " + data[1]);
                            LogLine("Sent pong!", 2);
                        }
                        else
                        {
                            string runcmd = data[1];

                            string Hostmask = hostmask.Replace(":", "");

                            string nick = "", user = "", host = "", args = "", target = "", cmd = "";
                            if (Hostmask.Contains('!') && Hostmask.Contains('@'))
                            {
                                string[] EXM = Hostmask.Split('!');
                                string[] ATS = Hostmask.Split('@');

                                nick = EXM[0];
                                host = ATS[1];
                                user = EXM[1].Replace(host, "").Replace("@", "");
                            }

                            if (data.Length > 2)
                            {
                                target = data[2];
                                if (data.Length > 3)
                                {
                                    cmd = data[3];
                                    if (data.Length > 4)
                                    {
                                        args = string.Join<string>(" ", data.Skip<string>(4));
                                    }
                                }
                            }
                            switch (runcmd)
                            {
                                case "PRIVMSG":
                                    if (target != "" && cmd != "" && Regex.IsMatch(host, Properties.Settings.Default.OwnerHost))
                                    {
                                        string command = cmd.Substring(1, cmd.Length - 1);
                                        bool didcommand = true;
                                        switch (command.ToLower())
                                        {
                                            case "#test":
                                                IRCSend("PRIVMSG " + target + " :I've been tested enough.");
                                                break;
                                            case "#say":
                                                IRCSend("PRIVMSG " + target + " :" + args);
                                                break;
                                            case "#quit":
                                                IRCSend("QUIT :" + args);
                                                break;
                                            case "#raw":
                                                IRCSend(args);
                                                break;
                                            default:
                                                didcommand = false;
                                                break;
                                        }
                                        if (didcommand)
                                        {
                                            if (args == "")
                                            {
                                                Log("COMMAND: ", 3);
                                                Log(nick, 3, "green");
                                                Log(" ", 3);
                                                Log("(", 3);
                                                Log(Hostmask, 3, "yellow");
                                                Log(")", 3);
                                                Log(" ", 3);
                                                Log("Executed command", 3);
                                                Log(" ", 3);
                                                Log(command, 3, "magenta");
                                                BlankLine();
                                            }
                                            else
                                            {
                                                Log("COMMAND: ", 3);
                                                Log(nick, 3, "green");
                                                Log(" ", 3);
                                                Log("(", 3);
                                                Log(Hostmask, 3, "yellow");
                                                Log(")", 3);
                                                Log(" ", 3);
                                                Log("Executed command", 3);
                                                Log(" ", 3);
                                                Log(command, 3, "magenta");
                                                Log(" ", 3);
                                                Log("with arguements", 3);
                                                Log(" ", 3);
                                                Log(args, 3, "red");
                                                BlankLine();
                                            }
                                        }
                                    }
                                    break;
                                case "376":
                                    LogLine("MOTD Complete.");
                                    if (Properties.Settings.Default.NSUser != "" && Properties.Settings.Default.NSPass != "")
                                    {
                                        LogLine("Identifying to account " + Properties.Settings.Default.NSUser + "...");
                                        IRCSend("PRIVMSG NickServ :IDENTIFY " + Properties.Settings.Default.NSUser + " " + Properties.Settings.Default.NSPass);
                                    }
                                    LogLine("Joining " + Properties.Settings.Default.Channel);
                                    IRCSend("JOIN " + Properties.Settings.Default.Channel);
                                    break;
                            }
                        }
                    }
                    writer.Close();
                    reader.Close();
                    irc.Close();
                }
            }
            catch (Exception e)
            {
                string sx254 = "\x254";
                char x254 = sx254[0];
                LogLine(e.Message, 6);
                string stacktrace = e.StackTrace.ToString().Replace("\x12", "").Replace("\x15", "\x254");
                string[] st = stacktrace.Split(x254);
                List<string> StackTrace = st.ToList<string>();
                foreach (string str in StackTrace)
                {
                    LogLine(str, 5);
                }
            }
        }
    }
}