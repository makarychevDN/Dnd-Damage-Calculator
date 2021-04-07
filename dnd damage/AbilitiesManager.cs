using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd_damage
{
    public static class AbilitiesManager
    {
        private static List<Ability> abilities = new List<Ability>();

        public static List<Ability> Abilites
        {
            get { return abilities; }
            set { abilities = value; }
        }

        public static void loadAbilitiesFromFile()
        {
            string fileName = "abilities test.txt";

            StreamReader sr = new StreamReader(fileName);
            string[] strings = new string[File.ReadAllLines(fileName).Length];
            int count = strings.Length;

            for(int i = 0; i < count; i++)
            {
                strings[i] = sr.ReadLine();
            }

            abilities = new List<Ability>();

            string[] splitVar = new string[5];

            for (int i = 0; i < count; i++)
            {
                splitVar = strings[i].Split('~');

                abilities.Add(new Ability(splitVar[0], 
                    (DamageTypes)Enum.Parse(typeof(DamageTypes), 
                    splitVar[1]), Convert.ToInt32(splitVar[2]), 
                    Convert.ToInt32(splitVar[3]), 
                    Convert.ToInt32(splitVar[4])));
            }

            sr.Close();
        }

        public static void saveAbilitiesToFile()
        {
            string fileName = "abilities test.txt";

            StreamWriter sw = new StreamWriter(fileName);            

            for (int i = 0; i < abilities.Count; i++)
            {
                sw.WriteLine(abilities[i].Name + "~" + abilities[i].DamageType + "~" + abilities[i].DiceQuantity + "~" + abilities[i].DiceDamage + "~" + abilities[i].PlusDamage);
            }

            sw.Close();
        }
    }
}
