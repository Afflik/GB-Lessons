using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lesson5
{
    public struct Row
    {
        public string Employee { get; set; }
        public string Departament { get; set; }
        public int Salary { get; set; }
    }

    public partial class MainWindow : Window
    {
        public static bool isAdded = false;  // Переменная для разрешения записи в файл

        public static ObservableCollection<Row> EmployeeList = new ObservableCollection<Row>();

        static string location = Directory.GetCurrentDirectory();
        static string dbPath;

        static SqlConnection myDB;
        SqlCommand command;

        public MainWindow()
        {
            location = location.Remove(location.Length - 10);
            dbPath = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={location}\db.mdf;Integrated Security=True;";
            myDB = new SqlConnection(dbPath);

            LoadList();

            InitializeComponent();
            employeeGrid.ItemsSource = EmployeeList;
            employeeGrid.CellEditEnding += employeeGrid_CellEditEnding;
        }

        public void LoadList()  // Создает коллекцию для отображения в таблице
        {

            int i = 0;
            EmployeeList.Clear();
            

            string query = "SELECT * FROM List";
            command = new SqlCommand(query, myDB);
            myDB.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                EmployeeList.Add(new Row {
                                           Employee = reader[0].ToString(),
                                           Departament = reader[1].ToString(),
                                           Salary = Convert.ToInt32(reader[2].ToString())
                                         });
                i++;
            }
            reader.Close();
            myDB.Close();
            i = 0;
        }

        private void NewRowButton(object sender, RoutedEventArgs e) // Открываем окно добавление строчки
        {
            NewEmployee win = new NewEmployee();
            win.Show();
        }

        private void ExitButton(object sender, RoutedEventArgs e) // Выход из приложения
        {
            Application.Current.Shutdown();
        }

        private void DeleteRowButton(object sender, RoutedEventArgs e)  // Удаляет выбранную строчку
        {
            var index = employeeGrid.SelectedIndex;
            string query = $"DELETE from List where Сотрудник = @Employee";
            command = new SqlCommand(query, myDB);
            command.Parameters.AddWithValue("@Employee", EmployeeList[index].Employee);

            myDB.Open();
            command.ExecuteNonQuery();
            myDB.Close();


            Refresh();
        }
        void employeeGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) // Редактирование строк
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    string query = "";
                    var index = employeeGrid.SelectedIndex;
                    var _column = (column.Binding as Binding).Path.Path;
                    var el = e.EditingElement as TextBox;
                    if (_column == "Employee")
                    {
                        query = "update List set Сотрудник = @after where Сотрудник = @beforeEmp";
                    }
                    else if (_column == "Departament")
                    {
                        query = "update List set Должность = @after where Должность = @beforeDep and Сотрудник = @beforeEmp";
                    }
                    else if (_column == "Salary")
                    {
                        query = "update List set Зарплата = @after where Зарплата = @beforeSal and Сотрудник = @beforeEmp";
                    }
                    command = new SqlCommand(query, myDB);
                    command.Parameters.AddWithValue("@after", el.Text);
                    command.Parameters.AddWithValue("@beforeEmp", EmployeeList[index].Employee);
                    command.Parameters.AddWithValue("@beforeDep", EmployeeList[index].Departament);
                    command.Parameters.AddWithValue("@beforeSal", EmployeeList[index].Salary);
                    myDB.Open();
                    command.ExecuteNonQuery();
                    myDB.Close();
                }
                Refresh();
            }
        }

        public void Refresh() // Обновляет данные в таблице
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                LoadList();
                employeeGrid.ItemsSource = null;
                employeeGrid.ItemsSource = EmployeeList;
            }));
        }
        public void Refresh(object sender, RoutedEventArgs e) // Обновляет данные в таблице
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                LoadList();
                employeeGrid.ItemsSource = null;
                employeeGrid.ItemsSource = EmployeeList;
            }));
        }
    }
}
