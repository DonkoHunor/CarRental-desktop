using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarRental_desktop
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new Application();
            if (args.Length == 1 && args[0] == "--stat")
            {
                Statisztika.Beolvasas();
                Console.WriteLine("20.000 Ft-nál olcsóbb napidíjú autók száma: " + Statisztika.Olcsok());
                Console.WriteLine(Statisztika.Van() + " az adatok között 26.000 Ft-nál drágább napidíjú autó");
                Console.WriteLine("Legdrágább napidíjú autó: " + Statisztika.Draga());
                Console.WriteLine("Autók száma: ");
                Statisztika.Markak();
                Console.Write("Adjon meg egy rendszámot: ");
                string plate = Console.ReadLine().ToUpper();
                Console.WriteLine(Statisztika.Car(plate));

            } else
            {                
                app.Run(new MainWindow());
            }
        }
    }
}
