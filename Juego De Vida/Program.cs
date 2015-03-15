using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juego_De_Vida
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            DialogResult result;


            //Action<int> action = x =>
            for (int x = 0; x < 1; x++ )
            {
                int f = ThreadSafeRandom.Next(100);
                if (f == 99)
                    System.Console.WriteLine(x);
            }
            //Parallel.For(0, 10000, action);

           // Console.ReadKey();

            result = MessageBox.Show("WARNING: If you are phosebsitive when exposed to flashing lights or changing patterns please do not run at a fast speed, keep it to Slow-Mid at all times if you decide to carry on.\n\nupon pressing yes you relinquish the writer of this program from all liability due to photosensitive seizures caused by this program.", "Photosensitive seizure warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MasterGUI());
            }
            else if (result == DialogResult.No)
            {
                MessageBox.Show("Sorry you couldn't stay, goodBye!");
            }
        }
    }


    public class ThreadSafeRandom
    {
        private static Random _global = new Random();
        [ThreadStatic]
        private static Random _local = new Random();

        public static int Next(int x)
        {
            Random inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new Random(seed);
            }
            int g = inst.Next(x);
            return g;
        } 
        public int Nexta(int x)
        {
            return _local.Next(x);
        }
    }
}
