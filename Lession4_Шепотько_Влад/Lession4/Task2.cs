using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lession4
{
    class Task2
    {
        static List<int> list = new List<int>();
        static Random rng = new Random();

        public static void Start()
        {
            Console.WriteLine("\nВыберете способ нажав на нужную цифру:\n1 - Целые числа\n2 - Коллекция\n3 - Linq\n4 - Вернуться в меню\n");
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    Method1();
                    break;
                case '2':
                    Method2();
                    break;
                case '3':
                    Method3();
                    break;
                case '4':
                    Menu.Main();
                    break;
                default:
                    Console.WriteLine("\nНеверна нажата клавиша!");
                    Start();
                    break;
            }
        }

        public static void CreateList()
        {
            list.Clear();
            for (int i = 0; i < 30; i++) list.Add(rng.Next(1, 10));
            Console.WriteLine("\nКоллекция:\n");

            for (int i = 0; i < 30; i++)
            {
                Console.Write(list[i] + " ");
                if ((i + 1) % 10 == 0) Console.Write("\n");
                if (i == 29) Console.Write("\n");
            }
        }
        public static void Method1()
        {
            CreateList();
            for (int n = 1; n < 10; n++)
            {
                int i;
                int r = 0;
                for (i = 0; i < 30; i++)
                {
                    if (n == list[i]) r++;
                }
                if (r != 0) Console.WriteLine($"{n} - {r} раз!");
            }
            Start();
        }
        public static void Method2()
        {
            CreateList();
            list.Sort();
            int r = 1;
            for (int i = 0; i < list.Count; i++)
            {
                if ((i == list.Count - 1) || list[i + 1] != list[i])
                {
                    Console.WriteLine($"{list[i]} - {r} раз!");
                    r = 1;
                }
                else r++;
            }
            Start();
        }
        public static void Method3()
        {
            CreateList();
            foreach (int n in list.Distinct()) Console.WriteLine(n + " - " + list.Where(x => x == n).Count() + " раз!");
            Start();
        }
    }
}
