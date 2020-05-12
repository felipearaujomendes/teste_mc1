using System;

namespace FibonnaciTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            RetornaNesimo(10);
        }

        public static String RetornaNesimo(int n)
        {
            String sn_1 = "0";
            String Sn = "01";
            String tmp;
            for (int i = 2; i < n; i++)
            {
                tmp = Sn;
                Sn += sn_1;
                sn_1 = tmp;
                Console.WriteLine(Sn);
            }
            return Sn;
        }
    }
}
