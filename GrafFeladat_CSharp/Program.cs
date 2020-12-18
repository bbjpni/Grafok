using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafFeladat_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var graf = new Graf(6);

            graf.Hozzaad(0, 1);
            graf.Hozzaad(1, 2);
            graf.Hozzaad(0, 2);
            graf.Hozzaad(2, 3);
            graf.Hozzaad(3, 4);
            graf.Hozzaad(4, 5);
            graf.Hozzaad(2, 4);
            Console.WriteLine("\nGráf:");
            Console.WriteLine(graf);
            Console.WriteLine("\nSzélességi Bejárás:");
            graf.SzelessegiBejar(5);
            Console.WriteLine("\nMélységi Bejárás:");
            graf.MelysegiBejar(2);
            Console.WriteLine("\nÖsszeföggés: ");
            Console.WriteLine(graf.Osszefuggo() ? "Öszzefüggő" : "nem összefüggő");
            //graf.Hozzaad(0, 1);
            //graf.Hozzaad(1, 2);
            //Dictionary<int, int> moho = graf.MohoSzinezes();
            //foreach (KeyValuePair<int, int> item in moho)
            //{
            //    Console.WriteLine("Csúcs: "+ item.Key + " > Szín: " + item.Value);
            //}
            Console.WriteLine("\nFeszítőfa:");
            Console.WriteLine(graf.Feszitofa().ToString());
            

            Console.ReadLine();
        }
    }
}
