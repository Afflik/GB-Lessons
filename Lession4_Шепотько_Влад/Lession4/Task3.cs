using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lession4
{
    class Task3
    {
        public static Dictionary<string, int> dict = new Dictionary<string, int>()
  {
    {"four",4 },
    {"two",2 },
    { "one",1 },
    {"three",3 },
  };
        public static void Start()
        {
            var d = dict.OrderBy(x => x.Value);
            foreach (var pair in d) Console.WriteLine($"{pair.Key} - {pair.Value}");
            Menu.Main();
        }
    }
}
