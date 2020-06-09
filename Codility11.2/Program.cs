/*
A prime is a positive integer X that has exactly two distinct divisors: 1 and X. The first few prime integers are 2, 3, 5, 7, 11 and 13.

A semiprime is a natural number that is the product of two (not necessarily distinct) prime numbers. The first few semiprimes are 4, 6, 9, 10, 14, 15, 21, 22, 25, 26.

You are given two non-empty arrays P and Q, each consisting of M integers. These arrays represent queries about the number of semiprimes within specified ranges.

Query K requires you to find the number of semiprimes within the range (P[K], Q[K]), where 1 ≤ P[K] ≤ Q[K] ≤ N.

For example, consider an integer N = 26 and arrays P, Q such that:

    P[0] = 1    Q[0] = 26
    P[1] = 4    Q[1] = 10
    P[2] = 16   Q[2] = 20
The number of semiprimes within each of these ranges is as follows:

(1, 26) is 10,
(4, 10) is 4,
(16, 20) is 0.
Write a function:

class Solution { public int[] solution(int N, int[] P, int[] Q); }

that, given an integer N and two non-empty arrays P and Q consisting of M integers, returns an array consisting of M elements specifying the consecutive answers to all the queries.

For example, given an integer N = 26 and arrays P, Q such that:

    P[0] = 1    Q[0] = 26
    P[1] = 4    Q[1] = 10
    P[2] = 16   Q[2] = 20
the function should return the values [10, 4, 0], as explained above.

Write an efficient algorithm for the following assumptions:

N is an integer within the range [1..50,000];
M is an integer within the range [1..30,000];
each element of arrays P, Q is an integer within the range [1..N];
P[i] ≤ Q[i].
*/

using System;

namespace Codility11._2
{
    class Solution
    {
        public int[] solution(int N, int[] P, int[] Q)
        {
            int[] s = new int[P.Length];
            if (N < 4)
                return s;
                
            int[] primes = new int[N + 1];
            for (int i = 2; i * i <= N; i++)
            {
                if (primes[i] == 0)
                {
                    int k = i * i;
                    while (k <= N)
                    { 
                        if (primes[k] == 0)
                            primes[k] = i;
                        k += i;
                    }
                }
            }

            bool[] semiprimes = new bool[N + 1];
            for (int i = 4; i <= N; i++)
                if (primes[i] != 0)
                {
                    int k = i / primes[i];
                    semiprimes[i] = (primes[k] == 0);
                }

            int[] cumSum = new int[N + 1];
            for (int i = 4; i <= N; i++)
                if (semiprimes[i])
                    cumSum[i] = cumSum[i - 1] + 1;
                else
                    cumSum[i] = cumSum[i - 1];

            for (int i = 0; i < P.Length; i++)
                s[i] = cumSum[Q[i]] - cumSum[P[i] - 1];

            return s;
        }
    }

    class Program
    {
        static void Main()
        {
            Solution sol = new Solution();
            int N = 26;
            int[] P = { 1, 4, 16 };
            int[] Q = { 26, 10, 20 };
            int[] s = sol.solution(N, P, Q);
            //Console.WriteLine("Solution: " + s);
            Console.WriteLine("Solution: " + string.Join(", ", s));
        }
    }
}
