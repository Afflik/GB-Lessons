using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lession4
{
    class Task1
    {
        static List<int> list = new List<int> ();
        static int count = 20;
        public static void Start()
        {
            list.Clear();
            for (int i = 0; i < count; i++) list.Add(i);
            Add();
        }
        public static void Add()
        {
            Console.WriteLine($"\nВ коллекции {list.Count} объектов. Пересоздать объекты в ней? Нажать Y/N");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Y:
                    for (int i = count-1; i >= 0; i--) list.RemoveAt(i);
                    count++;
                    for (int i = 0; i < count; i++) list.Add(i);
                    Add();
                    break;
                case ConsoleKey.N:
                    Menu.Main();
                    break;
                default:
                    Console.WriteLine("\nНеверна нажата клавиша!");
                    Add();
                    break;
            }
        }
    }
}
