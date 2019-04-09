using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lesson5
{
    /// <summary>
    /// Логика взаимодействия для NewEmployee.xaml
    /// </summary>
    public partial class NewEmployee : Window
    {
        static MainWindow mw = new MainWindow();

        public static string Employee { get; set; }
        public static string Departament { get; set; }
        public static string Salary { get; set; }

        public NewEmployee()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void AddEmployeeButton(object sender, RoutedEventArgs e) // Добавляет новую строчку
        {
            MainWindow.isAdded = true;
            StreamWriter write = new StreamWriter("list.ini", true, Encoding.Default);
            write.WriteLine($"{Employee}|{Departament}|{Salary}");
            write.Close();
            Close();
        }
        private void NumericOnly(object sender, TextCompositionEventArgs e) // Следит чтобы в поле "Зарплата" писали только цифры
        {
            e.Handled = IsTextNumeric(e.Text);
        }
        public static bool IsTextNumeric(string str)
        {
            Regex reg = new Regex("[^0-9]");
            return reg.IsMatch(str);
        }
    }
}
