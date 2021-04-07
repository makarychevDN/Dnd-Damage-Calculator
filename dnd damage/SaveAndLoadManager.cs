using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd_damage
{
    static class SaveAndLoadManager
    {

        private static string fileName = "dnd_data.txt";

        public static void Load()
        {
            StreamReader sr = new StreamReader(fileName);
            string[] strings = new string[File.ReadAllLines(fileName).Length];
            int count = strings.Length;

            Character.CurrentStatIndex = Convert.ToInt32(sr.ReadLine());
            Character.PB = Convert.ToInt32(sr.ReadLine());
            Character.STR = Convert.ToInt32(sr.ReadLine());
            Character.DEX = Convert.ToInt32(sr.ReadLine());
            Character.CON = Convert.ToInt32(sr.ReadLine());
            Character.INT = Convert.ToInt32(sr.ReadLine());
            Character.WIS = Convert.ToInt32(sr.ReadLine());
            Character.CHA = Convert.ToInt32(sr.ReadLine());

            for (int i = 8; i < count; i++)
            {
                strings[i] = sr.ReadLine();
            }

            AbilitiesManager.Abilites = new List<Ability>();

            string[] splitVar = new string[5];

            for (int i = 8; i < count; i++)
            {
                splitVar = strings[i].Split('~');

                AbilitiesManager.Abilites.Add(new Ability(splitVar[0],
                    (DamageTypes)Enum.Parse(typeof(DamageTypes),
                    splitVar[1]), Convert.ToInt32(splitVar[2]),
                    Convert.ToInt32(splitVar[3]),
                    Convert.ToInt32(splitVar[4])));
            }

            sr.Close();
        }

        public static void Save()
        {
            StreamWriter sw = new StreamWriter(fileName);

            sw.WriteLine(Character.CurrentStatIndex);
            sw.WriteLine(Character.PB);
            sw.WriteLine(Character.STR);
            sw.WriteLine(Character.DEX);
            sw.WriteLine(Character.CON);
            sw.WriteLine(Character.INT);
            sw.WriteLine(Character.WIS);
            sw.WriteLine(Character.CHA);

            for (int i = 0; i < AbilitiesManager.Abilites.Count; i++)
            {
                sw.WriteLine(AbilitiesManager.Abilites[i].Name + "~" 
                    + AbilitiesManager.Abilites[i].DamageType + "~" 
                    + AbilitiesManager.Abilites[i].DiceQuantity + "~" 
                    + AbilitiesManager.Abilites[i].DiceDamage + "~" 
                    + AbilitiesManager.Abilites[i].PlusDamage);
            }

            sw.Close();
        }

        public static void CreateFile()
        {
            StreamWriter sw = new StreamWriter(fileName); 

            for(int i = 0; i < 8; i++)
                sw.WriteLine("0");

            sw.Close();
        }
    }
}
