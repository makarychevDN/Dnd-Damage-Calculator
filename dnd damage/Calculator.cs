using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd_damage
{
    static class Calculator
    {
        private static Damage
            slashingDamage = new Damage(DamageTypes.slashing),
            piercingDamage = new Damage(DamageTypes.piercing),
            bludgeoningDamage = new Damage(DamageTypes.bludgeoning),
            acidDamage = new Damage(DamageTypes.acid),
            coldDamage = new Damage(DamageTypes.cold),
            fireDamage = new Damage(DamageTypes.fire),
            forceDamage = new Damage(DamageTypes.force),
            lightningDamage = new Damage(DamageTypes.lightning),
            necroticDamage = new Damage(DamageTypes.necrotic),
            poisonDamage = new Damage(DamageTypes.poison),
            psychisDamage = new Damage(DamageTypes.psychiс),
            radiantDamage = new Damage(DamageTypes.radiant),
            thunderDamage = new Damage(DamageTypes.thunder),
            healing = new Damage(DamageTypes.healing);

        private static Damage[] allDamage = new Damage[]
        {
            slashingDamage, piercingDamage, bludgeoningDamage,
            acidDamage, coldDamage, fireDamage,
            forceDamage, lightningDamage, necroticDamage,
            poisonDamage, psychisDamage, radiantDamage, 
            thunderDamage, healing
        };

        static int[] lastAbilityDamage;
        static int sumOfLastAbilityDamage;

        public static void Add(Ability ability)
        {
            lastAbilityDamage = ability.LastDamage;
            sumOfLastAbilityDamage = 0;

            for(int i = 0; i < lastAbilityDamage.Length; i++)
            {
                sumOfLastAbilityDamage += lastAbilityDamage[i];
            }

            sumOfLastAbilityDamage += ability.PlusDamage;

            switch (ability.DamageType)
            {
                case DamageTypes.acid: acidDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.bludgeoning: bludgeoningDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.cold: coldDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.fire: fireDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.force: forceDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.lightning: lightningDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.necrotic: necroticDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.piercing: piercingDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.poison: poisonDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.psychiс: psychisDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.radiant: radiantDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.slashing: slashingDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.thunder: thunderDamage.DamageValue += sumOfLastAbilityDamage; break;
                case DamageTypes.healing: healing.DamageValue += sumOfLastAbilityDamage; break;
            }
        }

        public static void Reset()
        {
            for (int i = 0; i < allDamage.Length; i++)
                allDamage[i].DamageValue = 0;
        }

        public static Damage[] getSumInDetail()
        {
            Damage[] notEmptyValuesSum;
            int count = 0;

            for (int i = 0; i < allDamage.Length; i++)
                if (allDamage[i].DamageValue != 0)
                    count++;

            notEmptyValuesSum = new Damage[count];
            count = 0;

            for (int i = 0; i < allDamage.Length; i++)
                if (allDamage[i].DamageValue != 0)
                {
                    notEmptyValuesSum[count] = allDamage[i];
                    count++;
                }

            return notEmptyValuesSum;
        }

        public static int getSum()
        {
            int sum = 0;

            for (int i = 0; i < allDamage.Length; i++)
                sum += allDamage[i].DamageValue;

            return sum;
        }
    }

    public class Damage
    {
        private int damageValue;
        private DamageTypes type;

        public Damage(DamageTypes type)
        {
            this.type = type;
            damageValue = 0;
        }

        public int DamageValue
        {
            get { return damageValue; }
            set { damageValue = value; }
        }

        public DamageTypes Type
        {
            get { return type; }
        }
    }

}
