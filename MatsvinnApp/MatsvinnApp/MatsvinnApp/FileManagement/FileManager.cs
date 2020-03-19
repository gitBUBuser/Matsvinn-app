using System;
using System.Collections.Generic;
using System.Text;
using MatsvinnApp.Models;
using System.IO;


namespace MatsvinnApp.FileManagement
{
    class FileManager
    {
        string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        

        public List<Food> FoodsAtDate(DateTime date)
        {
            List<Food> foods = new List<Food>();

            string foodDocLocation = Path.Combine(saveFolder, "DateInfo.txt");
            string dateString = DateToString(date);

            if (File.Exists(foodDocLocation))
            {
                string[] foodStrings = File.ReadAllLines(foodDocLocation);

                for (int i = 0; i < foodStrings.Length; i++)
                {
                    string[] foodFacts = foodStrings[i].Split('|');

                    if(foodFacts[0] == dateString)
                    {
                        foods.Add(new Food(DateTime.Parse(foodFacts[0]), foodFacts[1], foodFacts[2]));
                    }
                }

            }

            return foods;
        }

        public void EnterFood(Food food)
        {
            string foodDocLocation = Path.Combine(saveFolder, "DateInfo.txt");

            if (File.Exists(foodDocLocation))
            {
                using(StreamWriter file = new StreamWriter(foodDocLocation, true))
                {
                    file.WriteLine(DateToString(food.Date) + '|' + food.Name + '|' + food.Caption);
                }
            }
        }

        public void DeleteFood(Food food)
        {
            string foodDocLocation = Path.Combine(saveFolder, "DateInfo.txt");
            if (File.Exists(foodDocLocation))
            {
                List<string> newLines = new List<string>();
                string[] lines = File.ReadAllLines(foodDocLocation);
                string line = DateToString(food.Date) + '|' + food.Name + '|' + food.Caption;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != line)
                    {
                        newLines.Add(lines[i]);
                    }
                }
                File.WriteAllLines(foodDocLocation, newLines.ToArray());
            }
        }

        public string DateToString(DateTime date)
        {
            return date.Month.ToString() + '/' + date.Day.ToString() + '/' + date.Year.ToString();
        }
    }
}
