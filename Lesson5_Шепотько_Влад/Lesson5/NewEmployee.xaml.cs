using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public static string Employee { get; set; }
        public static string Departament { get; set; }
        public static string Salary { get; set; }
        
        static string location = Directory.GetCurrentDirectory();
        static string dbPath;

        static SqlConnection myDB;
        SqlCommand command;


        public NewEmployee()
        {
            location = location.Remove(location.Length - 10);
            dbPath = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={location}\db.mdf;Integrated Security=True;";
            myDB = new SqlConnection(dbPath);

            InitializeComponent();
            DataContext = this;
        }
        private void AddEmployeeButton(object sender, RoutedEventArgs e) // Добавляет новую строчку
        {
            MainWindow.isAdded = true;

            string query = "insert into List (Сотрудник, Должность, Зарплата) Values (@Employee,@Departament,@Salary);";
            command = new SqlCommand(query, myDB);
            command.Parameters.AddWithValue("@Employee", Employee);
            command.Parameters.AddWithValue("@Departament", Departament);
            command.Parameters.AddWithValue("@Salary", Salary);

            myDB.Open();
            command.ExecuteNonQuery();
            myDB.Close();
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
