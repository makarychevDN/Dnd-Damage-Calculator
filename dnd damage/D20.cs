using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd_damage
{
    public static class D20
    {
        static int value;
        static Random rnd = new Random();

        public static int roll()
        {
            value = (rnd.Next(1, 21));
            return value;
        }

        public static int rollWithAdvantage()
        {
            int rerollValue;
            value = (rnd.Next(1, 21));
            rerollValue = (rnd.Next(1, 21));

            if (value < rerollValue)
                value = rerollValue;

            return value;
        }

        public static int rollWithHindrance()
        {
            int rerollValue;
            value = (rnd.Next(1, 21));
            rerollValue = (rnd.Next(1, 21));

            if (value > rerollValue)
                value = rerollValue;

            return value;
        }
    }
}
