using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoDispatcher
{
    class Logger
    {
        static private StreamWriter writer;
        

        /// <summary>
        /// Запись сообщения в лог
        /// </summary>
        /// <param name="text">Сообщение</param>
        static private void Write(string text)
        {
            string fileName = "log.txt";
            string directoryName = "log";

            if (Directory.Exists(directoryName))
            {
                writer = new StreamWriter($@"{directoryName}/{fileName}", append: true);
            }
            else
            {
                Directory.CreateDirectory(directoryName);
                File.Create($@"{directoryName}/{fileName}");
            }

            writer.WriteLine(text);
            writer.Close();
        }

        /// <summary>
        /// Запись в лог о том, что самолёт разбился
        /// </summary>
        /// <param name="Company">Фирма-производитель</param>
        /// <param name="Number">Бортовой номер</param>
        static public void Crash(string Company, int Number)
        {
            string mes = $@"Разбился самолёт '{Company}', бортовой номер {Number}, Время {DateTime.Now}";
            Logger.Write(mes);
        }

        /// <summary>
        /// Запись в лог сообщения о том, что самолёт приземлился
        /// </summary>
        /// <param name="Company">Фирма-производитель</param>
        /// <param name="Number">Бортовой номер</param>
        /// <param name="Strip">Полоса</param>
        static public void Landing(string Company, int Number, int Strip)
        {
            string mes = $@"Сел самолёт '{Company}', полоса {Strip}, бортовой номер {Number}, Время {DateTime.Now}";
            Logger.Write(mes);
        }
    }
}
