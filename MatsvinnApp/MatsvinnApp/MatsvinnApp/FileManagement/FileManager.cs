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
            string dateString = date.Year.ToString() + ", " + date.Month.ToString() + ", " + date.Day.ToString();

            if (File.Exists(foodDocLocation))
            {
                string[] foodStrings = File.ReadAllLines(foodDocLocation);

                for (int i = 0; i < foodStrings.Length; i++)
                {
                    string[] foodFacts = foodStrings[i].Split('|');

                    if(foodFacts[0] == dateString)
                    {
                        foods.Add(new Food(foodFacts[0], foodFacts[1], foodFacts[2]));
                    }
                }

            }

            return foods;
        }
    }
}
