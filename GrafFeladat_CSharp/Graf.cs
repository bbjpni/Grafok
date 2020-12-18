using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }

        public void SzelessegiBejar(int kezdopont)
        {
            HashSet<int> bejart = new HashSet<int>();

            // A következőnek vizsgált elem a kezdőpont
            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Enqueue(kezdopont);
            bejart.Add(kezdopont);

            // Amíg van következő, addig megyünk
            while (kovetkezok.Count != 0)
            {
                int k = kovetkezok.Dequeue();
                Console.WriteLine(this.csucsok[k]);

                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == k && !bejart.Contains(el.Csucs2))
                    {
                        kovetkezok.Enqueue(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
            }
        }

        public void MelysegiBejar(int kezdopont)
        {
            HashSet<int> bejart = new HashSet<int>();

            Stack<int> kovetkezok = new Stack<int>();
            kovetkezok.Push(kezdopont);
            bejart.Add(kezdopont);

            while (kovetkezok.Count != 0)
            {
                int k = kovetkezok.Pop();
                Console.WriteLine(this.csucsok[k]);

                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == k && !bejart.Contains(el.Csucs2))
                    {
                        kovetkezok.Push(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
            }
        }

        public void Torles(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            for (int i = 0; i <= elek.Count; i += 2)
            {
                El el = elek[i];
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2 || el.Csucs1 == cs2 && el.Csucs2 == cs1)
                {
                    elek.Remove(el);
                    el = elek[i];
                    elek.Remove(el);
                    return;
                }
            }
        }

        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }

        public bool Osszefuggo()
        {
            HashSet<int> bejart = new HashSet<int>();

            // A következőnek vizsgált elem a kezdőpont
            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Enqueue(0);
            bejart.Add(0);

            while (kovetkezok.Count != 0)
            {
                int k = kovetkezok.Dequeue();
                Console.WriteLine(this.csucsok[k]);

                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == k && !bejart.Contains(el.Csucs2))
                    {
                        kovetkezok.Enqueue(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
                }
            }
            return csucsokSzama == bejart.Count;
        }

        //public Dictionary<int, int> MohoSzinezes()
        //{
        //    Dictionary<int, int> szinezes = new Dictionary<int, int>();
        //    int maxSzín = this.csucsokSzama;

        //    for (int aktualisCsucs = 0; aktualisCsucs < this.csucsokSzama; aktualisCsucs++)
        //    {
        //        HashSet<int> valaszthatoSzinek = new HashSet<int>();
        //        valaszthatoSzinek.Add(aktualisCsucs);

        //        foreach (var el in this.elek)
        //        {
        //            if (el.Csucs1 == aktualisCsucs)
        //            {
        //                if (szinezes.ContainsKey(el.Csucs2))
        //                {
        //                    int szin = szinezes[el.Csucs2];
        //                    valaszthatoSzinek.Remove(szin);
        //                }
        //            }
        //        }
        //        int valasztottSzin = csucsokSzama+1;
        //        foreach (var n in valaszthatoSzinek)
        //        {
        //            valasztottSzin = n < valasztottSzin ? n : valasztottSzin;
        //            Console.WriteLine(n);                
        //        }
        //        szinezes.Add(aktualisCsucs, valasztottSzin);
        //    }

        //    return szinezes;


        public Graf Feszitofa() {
            Graf fa = new Graf(this.csucsokSzama);
            HashSet<int> bejart = new HashSet<int>();
            Queue<int> kovetkezok = new Queue<int>();

            kovetkezok.Enqueue(0);
            bejart.Add(0);

            while (kovetkezok.Count != 0)
            {
                int aktualisCsucs = kovetkezok.Dequeue();
                foreach (var el in this.elek)
                {
                    if (el.Csucs1 == aktualisCsucs)
                    {
                        if (!bejart.Contains(el.Csucs2))
                        {
                            bejart.Add(el.Csucs2);
                            kovetkezok.Enqueue(el.Csucs2);
                            fa.Hozzaad(el.Csucs1, el.Csucs2);
                        }
                    }
                }
            }
            return fa;
        }
    }
}