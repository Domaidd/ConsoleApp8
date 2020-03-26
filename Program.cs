using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        public static object locker = new object();

        static void Main(string[] args)
        {
            #region tr
            //Thread thread = new Thread(new ThreadStart(DoWork));
            //thread.Start();
            //Thread thread2 = new Thread(new ParameterizedThreadStart(DoWork2));
            //thread2.Start(int.MaxValue);
            //int j = 0;
            //for (int i = 0; i < int.MaxValue; i++)
            //{
            //    j++;

            //    if (j % 10000 == 0)
            //    {
            //        Console.WriteLine("Main");
            //    }
            //}
            #endregion

            #region async|await
            //DoWorkAsync();

            //int j = 0;
            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("Main");
            //}
            #endregion

            var result = SaveFileAsync("C://egr/test123.txt");
            var text = Console.ReadLine();
            Console.WriteLine(text);
            Console.WriteLine(result.Result);
            Console.ReadLine();
        }

        static async Task<bool> SaveFileAsync(string path)
        {
            var result = await Task.Run(() => SaveFile(path));
            return result;
        }

        static bool SaveFile(string path)
        {
            lock (locker)
            {
                var rnd = new Random();
                var text = "";
                for (int i = 0; i < 40000; i++)
                {
                    text += rnd.Next() + "\n";
                }

                using (var sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine(text);
                }
            }

            return true;
        }

        static async Task DoWorkAsync()
        {
            await Task.Run(() => DoWork());
            Console.WriteLine("DoWorkAsync");
        }

        static void DoWork()
        {
            int j = 0;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("DoWork");
            }
        }

        static void DoWork2(object max)
        {
            int j = 0;
            for (int i = 0; i < (int)max; i++)
            {
                j++;
                if(j % 10000 == 0)
                {
                    Console.WriteLine("DoWork2");
                }
            }
        }
    }
}
