using System;
using System.Collections.Generic;
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

        FileSystemWatcher watcher = new FileSystemWatcher();  // Переменная для проверки изменения файла

        public static List<Row> EmployeeList = new List<Row>();

        string line;
        int column = 1; // Отвечает в какую колонну идет запись

        public MainWindow()
        {
            LoadList();

            InitializeComponent();
            employeeGrid.ItemsSource = EmployeeList;
            CreateFileWatcher();

            employeeGrid.CellEditEnding += employeeGrid_CellEditEnding;
        }
        public void LoadList()  // Создает коллекцию для отображения в таблице
        {
            EmployeeList.Clear();
            StreamReader readList = new StreamReader("list.ini", Encoding.Default);
            while ((line = readList.ReadLine()) != null)
            {
                string employee = "";
                string departament = "";
                string salary = "";

                foreach (char ch in line)
                {
                    if (ch == '|') column++; // Разделяет строку на переменные
                    else
                    {
                        if (column == 1) employee += ch;
                        else if (column == 2) departament += ch;
                        else if (column == 3) salary += ch;
                    }
                }
                    EmployeeList.Add(new Row { Employee = employee, Departament = departament, Salary = Convert.ToInt32(salary) });
                column = 1;
            }
            readList.Close();
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
            File.WriteAllText("list.ini", String.Empty);

            StreamWriter writeList = new StreamWriter("list.ini", true, Encoding.Default);

            var index = employeeGrid.SelectedIndex;
            for (int i = 0;i < EmployeeList.Count; i++ )
            {
                if(i != index) writeList.WriteLine($"{EmployeeList[i].Employee}|{EmployeeList[i].Departament}|{EmployeeList[i].Salary}");
            }
            writeList.Close();
            Refresh();
        }
        public void CreateFileWatcher()  // Проверяет изменения списка сотрудников
        {
            watcher.Path = Directory.GetCurrentDirectory();
            watcher.Filter = "list.ini";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (isAdded)
            {
                isAdded = false;
                Refresh();
            }
        }
        void employeeGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) // Редактирование строк
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                File.WriteAllText("list.ini", String.Empty);

                StreamWriter writeList = new StreamWriter("list.ini", true, Encoding.Default);

                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var _column = (column.Binding as Binding).Path.Path;
                    int rowIndex = e.Row.GetIndex();
                    var el = e.EditingElement as TextBox;
                    for (int i = 0; i < EmployeeList.Count; i++)
                    {
                        if(i == rowIndex)
                        {
                            if (_column == "Employee") writeList.WriteLine($"{el.Text}|{EmployeeList[i].Departament}|{EmployeeList[i].Salary}");
                            else if (_column == "Departament") writeList.WriteLine($"{EmployeeList[i].Employee}|{el.Text}|{EmployeeList[i].Salary}");
                            else if (_column == "Salary") writeList.WriteLine($"{EmployeeList[i].Employee}|{EmployeeList[i].Departament}|{el.Text}");
                        }
                            else
                        {
                            writeList.WriteLine($"{EmployeeList[i].Employee}|{EmployeeList[i].Departament}|{EmployeeList[i].Salary}");
                        }
                    }
                }
                writeList.Close();
                Refresh();
            }
        }
        public void Refresh() // Обновляет данные в таблице
        {
            LoadList();
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                employeeGrid.ItemsSource = null;
                employeeGrid.ItemsSource = EmployeeList;
            }));
        }
    }
}
