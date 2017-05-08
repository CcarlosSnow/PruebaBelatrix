using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace PruebaBelatrix
{
    public class JobLoggerJuanBazan
    {
        public JobLoggerJuanBazan()
        {
        }

        public static TypesEnum.LogTo LogMessage(string message, TypesEnum.LogTo LogTo, TypesEnum.TypeMessage TypeMessage)
        {
            message.Trim();
            if (message == null || message.Length == 0)
            {
                throw new Exception("Message can't be null");
            }

            switch (LogTo)
            {
                case TypesEnum.LogTo.Database:
                    LogInsert Ins = new LogInsert();
                    if (Ins.Insert(message, (int)TypeMessage))
                    {
                        return TypesEnum.LogTo.Database;
                    }
                    else
                    {
                        return TypesEnum.LogTo.Nothing;
                    }
                case TypesEnum.LogTo.File:
                    string LogFileName = ConfigurationManager.AppSettings["LogFileName"] + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                    string ReadedText = "";
                    if (File.Exists(LogFileName))
                    {
                        ReadedText = File.ReadAllText(LogFileName);
                    }
                    ReadedText += message;
                    File.WriteAllText(LogFileName, ReadedText);
                    return TypesEnum.LogTo.File;
                case TypesEnum.LogTo.Console:
                    switch (TypeMessage)
                    {
                        case TypesEnum.TypeMessage.Info:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case TypesEnum.TypeMessage.Warning:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case TypesEnum.TypeMessage.Error:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(DateTime.Now.ToString() + ":" + message);
                    return TypesEnum.LogTo.Console;
                default:
                    return TypesEnum.LogTo.Nothing;
                    //break;
            }
        }
    }
}
