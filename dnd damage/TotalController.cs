using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd_damage
{
    public static class TotalController
    {
        public static int[] rollD20()
        {
            return new int[]
            {
                D20.roll(),
                Character.CurrentStat,
                Character.PB
            };
        }

        public static int[] rollD20withAdvantage()
        {
            return new int[]
            {
                D20.rollWithAdvantage(),
                Character.CurrentStat,
                Character.PB
            };
        }

        public static int[] rollD20withHindrance()
        {
            return new int[]
            {
                D20.rollWithHindrance(),
                Character.CurrentStat,
                Character.PB
            };
        }

        public static void useAbility(int index)
        {
            AbilitiesManager.Abilites[index].roll();
            Calculator.Add(AbilitiesManager.Abilites[index]);
        }

        public static int[] getDamageInDetail(int index)
        {
            return AbilitiesManager.Abilites[index].LastDamage;
        }

        public static Damage[] getDamageSumInDetail(int index)
        {
            return Calculator.getSumInDetail();
        }

        public static int getDamageSum()
        {
            return Calculator.getSum();
        }

        public static int getAbilityPlusDamage(int index)
        {
            return AbilitiesManager.Abilites[index].PlusDamage;
        }

        public static DamageTypes getAbilityDamageType(int index)
        {
            return AbilitiesManager.Abilites[index].DamageType;
        }

        public static string getAbilityName(int index)
        {
            return AbilitiesManager.Abilites[index].Name;
        }

        public static void resetCalculator()
        {
            Calculator.Reset();
        }

        public static void changeCharacterCurrentStat(int index)
        {
            switch (index)
            {
                case 0: Character.CurrentStat = Character.STR; Character.CurrentStatIndex = 0; break;
                case 1: Character.CurrentStat = Character.DEX; Character.CurrentStatIndex = 1; break;
                case 2: Character.CurrentStat = Character.CON; Character.CurrentStatIndex = 2; break;
                case 3: Character.CurrentStat = Character.INT; Character.CurrentStatIndex = 3; break;
                case 4: Character.CurrentStat = Character.WIS; Character.CurrentStatIndex = 4; break;
                case 5: Character.CurrentStat = Character.CHA; Character.CurrentStatIndex = 5; break;
            }
        }

        public static void loadDataFromFile()
        {
            try
            {
                SaveAndLoadManager.Load();
            }
            catch
            {
                SaveAndLoadManager.CreateFile();
                SaveAndLoadManager.Load();
            }
        }

        public static void saveDataToFile()
        {
            SaveAndLoadManager.Save();
        }

        public static Ability getAbilityByIndex(int index)
        {
            return AbilitiesManager.Abilites[index];
        }

        public static List<Ability> getAbilities()
        {
            return AbilitiesManager.Abilites;
        }

        public static void addAbility(Ability ability)
        {
            AbilitiesManager.Abilites.Add(ability);
        }

        public static void removeAbilityByIndex(int index)
        {
            AbilitiesManager.Abilites.RemoveAt(index);
        }
        public static int getCurrentAbilityIndex()
        {
            return Character.CurrentStatIndex;
        }

        public static void setCharacterStats(int[] stats)
        {
            Character.PB = stats[0];
            Character.STR = stats[1];
            Character.DEX = stats[2];
            Character.CON = stats[3];
            Character.INT = stats[4];
            Character.WIS = stats[5];
            Character.CHA = stats[6];
        }

        public static int[] getCharacterStats()
        {
            return new int[]
            {
                Character.PB,
                Character.STR,
                Character.DEX,
                Character.CON,
                Character.INT,
                Character.WIS,
                Character.CHA
            };
        }
    }
}
