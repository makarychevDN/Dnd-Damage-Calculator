using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd_damage
{
    public static class Character
    {
        private static int proficiencyBonus = 3;
        private static int strengthMod;
        private static int dexterityMod;
        private static int constitutionMod;
        private static int intelligenceMod;
        private static int wisdomMod = 5;
        private static int charismaMod;

        private static int currentStat;
        private static int currentStatIndex;

        static public void setStats(int newstrengthMod, int newdexterityMod, int newconstitution, 
                                    int newintelligenceMod, int newwisdomMod, int newcharismaMod)
        {
            strengthMod = newstrengthMod;
            dexterityMod = newdexterityMod;
            constitutionMod = newconstitution;
            intelligenceMod = newintelligenceMod;
            wisdomMod = newwisdomMod;
            charismaMod = newcharismaMod;
        }

        public static int PB
        {
            get { return proficiencyBonus; }
            set { proficiencyBonus = value; }
        }

        public static int STR
        {
            get { return strengthMod; }
            set { strengthMod = value; }
        }

        public static int DEX
        {
            get { return dexterityMod; }
            set { dexterityMod = value; }
        }

        public static int CON
        {
            get { return constitutionMod; }
            set { constitutionMod = value; }
        }

        public static int INT
        {
            get { return intelligenceMod; }
            set { intelligenceMod = value; }
        }

        public static int WIS
        {
            get { return wisdomMod; }
            set { wisdomMod = value; }
        }

        public static int CHA
        {
            get { return charismaMod; }
            set { charismaMod = value; }
        }

        public static int CurrentStat
        {
            get { return currentStat; }
            set { currentStat = value; }
        }

        public static int CurrentStatIndex
        {
            get { return currentStatIndex; }
            set { currentStatIndex = value; }
        }

    }
}
