﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousTasks
{
    public class Exercises
    {
      
        public static BigInteger CalculateFactorial(BigInteger num)
        {
            BigInteger result = BigInteger.One;
            for (BigInteger i = BigInteger.One; i.CompareTo(num) <= 0; i = BigInteger.Add(i, BigInteger.One))
            {
                result = BigInteger.Multiply(result, i);
            }
            return result;
        }

    }
}
