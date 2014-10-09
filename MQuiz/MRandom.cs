using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQuiz
{
    class MRandom
    {
        private static Random random;
        public static int getRandomNumber(int from, int to)
        {
            if (random == null)
            {
                random = new Random();
            }
            int len = to - from; //if range is 2-5 then l = 3
            int n = random.Next(len); //len is 0-2
            n = n + from;
            return n;
        }
    }
}
