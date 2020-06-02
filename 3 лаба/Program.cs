using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace _3_лаба
{
    class Program
    {
        static public bool check_pustota(string stroka)
        {

            if (stroka.Trim() != "") { return true; };
            return false;

        }
        public static bool checkInt(string stroka)
        {
            if (check_pustota(stroka)) { return true; }
            int n;
            bool ads = false;
            ads = int.TryParse(stroka, out n);
            if (!ads) { return false; }

            return true;
        }

        public static int sizeArray()
        {
            string n = null;
            do
            {
                Console.WriteLine("Enter the size of the array");
                n = Console.ReadLine();

            } while (!checkInt(n));
            int N = Convert.ToInt32(n);
            return N;
        }
        public static int[] createArray(int N)
        {
            Random rnd = new Random();
            int[] a = new int[N];
            for (int i = 0; i < N; i++)
            {
                a[i] = rnd.Next();
          
            }
            return a;
        }
        public static int parametr()
        {
            string n = null;
            do
            {
                Console.WriteLine("Enter difficulty setting"); //параметр сложности
                n = Console.ReadLine();
            } while (!checkInt(n));
            int K = Convert.ToInt32(n);
            return K;
        }
        public static void processArray(int K, int[] a)
        {


            double[] b = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < K; j++)
                    b[i] += Math.Pow(a[i], 1.789);
            }

        }

       
        public static void Mnogopotochnost(int K, int[] a)
        {
            Thread[] thread = new Thread[3];
            double[] b = new double[a.Length];
            var time = DateTime.Now;
            var sum = 0;
            thread[0] = new Thread(() =>
            {
                for (int i = 0; i < a.Length / 3; i++)
                {

                    for (int j = 0; j < K; j++)
                    {

                        b[i] += Math.Pow(a[i], 1.789);

                    }
                }
                
            });
            thread[0].Start();
            thread[0].Join();
            var dTime = DateTime.Now - time;
            Console.WriteLine("Time 1 {0}", dTime.ToString());
            time = DateTime.Now;
            thread[1] = new Thread(() =>
            {
                for (int i = a.Length / 3; i < (a.Length / 3) * 2; i++)
                {

                    for (int j = 0; j < K; j++)
                    {

                        b[i] += Math.Pow(a[i], 1.789);

                    }
                }
                sum += dTime.Milliseconds;
            });
            thread[1].Start();
            thread[1].Join();
            dTime = DateTime.Now - time;
            Console.WriteLine("Time 2 {0}", dTime.ToString());
            time = DateTime.Now;
            thread[2] = new Thread(() =>
            {
                for (int i = (a.Length / 3) * 2; i < a.Length; i++)
                {

                    for (int j = 0; j < K; j++)
                    {

                        b[i] += Math.Pow(a[i], 1.789);

                    }
                }
                sum += dTime.Milliseconds;
            });
            thread[2].Start();
            thread[2].Join();
            dTime = DateTime.Now - time;
            Console.WriteLine("Time 3 {0}", dTime.ToString());
            sum += dTime.Milliseconds;
            Console.WriteLine(sum.ToString());
        }
        public static string bezMnogopotochnosti(int K, int[] a)
        {

            var time = DateTime.Now;

            processArray(K, a);

            var dTime = DateTime.Now - time;
            return dTime.ToString();
        }
        static void Main(string[] args)
        {
            int N = sizeArray();
            int K = parametr();
            int[] a = createArray(N);
            string t1 = bezMnogopotochnosti(K, a);
            Mnogopotochnost(K, a);
            //Console.WriteLine("Using multithreading {0}", t1);
            Console.WriteLine("Without multithreading {0}", t1);
            Console.ReadLine();
        }
    }
}
 