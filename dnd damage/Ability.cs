using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd_damage
{
    public class Ability
    {
        string name;
        DamageTypes damageType;
        int diceQuantity;
        int diceDamage;
        int plusDamage;


        Random rnd = new Random();
        int[] lastDamage;

        public Ability(string name, DamageTypes damageType, int diceQuantity, int diceDamage, int plusDamage)
        {
            this.diceQuantity = diceQuantity;
            this.damageType = damageType;
            this.diceDamage = diceDamage;
            this.plusDamage = plusDamage;
            this.name = name;
        }

        public int DiceQuantity
        {
            get { return diceQuantity; }
            set { diceQuantity = value; }
        }

        public DamageTypes DamageType
        {
            get { return damageType; }
            set { damageType = value; }
        }

        public int DiceDamage
        {
            get { return diceDamage; }
            set { diceDamage = value; }
        }

        public int PlusDamage
        {
            get { return plusDamage; }
            set { plusDamage = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int[] LastDamage
        {
            get { return lastDamage; }
            set { lastDamage = value; }
        }

        public int[] roll()
        {
            lastDamage = new int[diceQuantity];
            for(int i = 0; i < diceQuantity; i++)
            {
                lastDamage[i] = rnd.Next(1, diceDamage + 1);
            }

            return lastDamage;
        }
    }
}
