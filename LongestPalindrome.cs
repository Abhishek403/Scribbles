using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace palindorme
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Console.ReadLine();
            var len = data.Length;
            LongestPalindrome(ref data, len);
        }

        static void LongestPalindrome(ref string data, int len)
        {
            int [,] storage = new int[len, len];
            var maxLength = 0;
            int[] dim = new int[2];
            for (int i = 0; i < len; i++)
            {
                storage[i, i] = 1;
            }

            for (int i = len - 2; i >= 0; i--)
            {
                for (int j = i+1; j<len; j++)
                {
                    if (i - j == 1)
                    {
                        storage[i, j] = (data[i] == data[j]) ? 2 : 0;
                    }

                    else if (data[i] == data[j])
                    {
                        storage[i, j] = 2 + storage[i + 1, j - 1];
                        if (maxLength < storage[i, j])
                        {
                            maxLength = storage[i, j];
                            dim[0] = i;
                            dim[1] = j;
                        }
                    }

                    else
                    {
                        storage[i, j] = Math.Max(storage[i + 1, j], storage[i, j - 1]);
                    }
                }
            }

           
                Console.WriteLine(data.Substring(dim[0], maxLength));
                Console.WriteLine(storage[0,len-1]);
                Console.Read();
           
        }
    }
}
