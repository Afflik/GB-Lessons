using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lession4
{
    class Menu
    {
        public static void Main()
        {
            Console.WriteLine("\nВыбрать задание для проверки. Нажмите на цифру задания от одного до трех.\n");
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    Task1.Start();
                    break;
                case '2':
                    Task2.Start();
                    break;
                case '3':
                    Task3.Start();
                    break;
                default:
                    Console.WriteLine("\nНеверна нажата клавиша!");
                    Main();
                    break;
            }
        }
    }
}
