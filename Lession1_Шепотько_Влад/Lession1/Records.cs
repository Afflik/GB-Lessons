using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;

class Records
{
    // data1 - data4 / score1 - score4 основные переменные для вывода рекордов
    public static string date1;
    public static string score1;

    public static string date2;
    public static string score2;

    public static string date3;
    public static string score3;

    public static string date4;
    public static string score4;

    public static void AddRecord(string file, int score) // Записывает в файл заработанные очки после игры
    {
        using (StreamWriter records = File.AppendText(file))
        {
            string line = $"{DateTime.Now}|Набрано {score} очков";
            records.WriteLine(line);
        }
    }
    public static void WriteRecords(string file) // Читает все записанные игры и сортирует их для вывода в меню "Рекорды"
    {
        List<int> scores = new List<int>();
        List<int> scoresPos = new List<int>();
        List<string> rightSides = new List<string>();
        List<string> leftSides = new List<string>();
        List<string> completeLeft = new List<string>();
        List<string> completeRight = new List<string>();

        string line;

        StreamReader records = new StreamReader(file);
        while ((line = records.ReadLine()) != null) // Проверка каждой строчки
        {
            int num = 0;
            int index = line.IndexOf('|');

            string tempDate = "";
            string gameInfo = "";
            string tempScore = "";
            foreach (char ch in line)
            {
                if (num < index) tempDate += ch; // Отделяет левую часть строчки по знаку '|'
                else if (num > index) // Отделяет правую часть строчки по знаку '|'
                {
                    if (char.IsDigit(ch)) tempScore += ch; // Записывает отдельно заработанные за игру очки
                    gameInfo += ch;
                }

                if (ch == line[line.Length - 1])
                {
                    scores.Add(Convert.ToInt32(tempScore));
                    scoresPos.Add(Convert.ToInt32(tempScore));
                    leftSides.Add(tempDate);
                    rightSides.Add(gameInfo);
                }
                num++;
            }
        }
        scores.Sort((x, y) => y.CompareTo(x)); // Сортирует числа в scores по убыванию 
        for (int i = 4; i < scores.Count; i++) scores.RemoveAt(i); // Удаляет все элементы кроме первой четверки, которая выводится
        int pos = 0;
        for (int i = 0; i < scores.Count; i++) 
        {
            for (int j = 0; j < scoresPos.Count; j++)
            {
                if (scores[i] == scoresPos[j]) // Теперь сортирует полностью позиции строчки по колчеству очков
                {
                    completeLeft.Add(leftSides[scoresPos.IndexOf(scoresPos[j])]);
                    completeRight.Add(rightSides[scoresPos.IndexOf(scoresPos[j])]);
                    pos++;
                }
            }
        }

        date1 = completeLeft[0];
        date2 = completeLeft[1];
        date3 = completeLeft[2];
        date4 = completeLeft[3];

        score1 = completeRight[0];
        score2 = completeRight[1];
        score3 = completeRight[2];
        score4 = completeRight[3];

    }
}   