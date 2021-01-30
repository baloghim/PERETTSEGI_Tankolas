using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tankolas
{
    class Program
    {
        struct Adat
        {
            public int Tipus;
            public int Liter;
            public string Rendszam;
            public string Datum;
            public string Ido;
        }

        static void Main(string[] args)
        {
            Adat[] Tankolasok = new Adat[1000];

            Console.ForegroundColor = ConsoleColor.Green;

            // 1. feladat
            Console.WriteLine("1. feladat");
            int TankolasokSzama = 0;
            Beolvas(Tankolasok, "tankolas.txt", out TankolasokSzama);
            //Kiir(Tankolasok, 10);

            // 2. feladat
            Console.WriteLine("2. feladat");
            Console.WriteLine("Adatsorok száma: " + TankolasokSzama);

            // 3. feladat
            Console.WriteLine("3. feladat");
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("A (z) {0}. típusú üzemanyagból összesen {1} liter fogyott.", i, Osszesites(Tankolasok, TankolasokSzama, i));
            }

            // 4. feladat
            Console.WriteLine("4. feladat");
            Console.Write("Keresett rendszám: ");
            string Rendszam = Console.ReadLine();
            RendszamKeres(Tankolasok, TankolasokSzama, Rendszam);

            // 6. feladat
            Console.WriteLine("6. feladat");
            Statisztika(Tankolasok, TankolasokSzama, "stat.txt");


            Console.ReadKey();
        }

        static void Beolvas(Adat[] T, string FajlNev, out int TankolasokSzama)
        {
            StreamReader f = new StreamReader(FajlNev);
            string sor = "";
            int i = 0;
            while ((sor = f.ReadLine()) != null)
            {
                string[] temp = sor.Split(' ');
                T[i].Tipus = int.Parse(temp[0]);
                T[i].Liter = int.Parse(temp[1]);
                T[i].Rendszam = temp[2];
                T[i].Datum = temp[3];
                T[i].Ido = temp[4];
                i++;
            }
            f.Close();
            TankolasokSzama = i;
        }

        static void Kiir(Adat[] T, int db)
        {
            for (int i = 0; i < db; i++)
            {
                Console.WriteLine(T[i].Tipus + " " + T[i].Liter + " " + T[i].Rendszam + " " + T[i].Datum + " " + T[i].Ido);
            }
        }

        static int Osszesites(Adat[] T, int RekordokSzama, int UzemanyagTipus)
        {
            int L = 0;

            for (int i = 0; i < RekordokSzama; i++)
            {
                if (T[i].Tipus == UzemanyagTipus)
                {
                    L += T[i].Liter;
                }
            }

            return L;
        }

        static void RendszamKeres(Adat[] T, int RekordokSzama, string R)
        {
            int Db = 0;
            for (int i = 0; i < RekordokSzama; i++)
            {
                if (T[i].Rendszam == R.ToUpper())
                {
                    Console.WriteLine(T[i].Tipus + " " + T[i].Liter + " " + T[i].Datum + " " + T[i].Ido);
                    Db++;
                }
            }
            if (Db == 0)
            {
                Console.WriteLine("Ez az autó nem tankolt a kútnál!");
            }
        }

        static void Statisztika(Adat[] T, int RekordokSzama, string Fajlnev)
        {
            List<string> RendszamLista = new List<string>();

            for (int i = 0; i < RekordokSzama; i++)
            {
                if (!RendszamLista.Contains(T[i].Rendszam))
                {
                    RendszamLista.Add(T[i].Rendszam);
                }
            }

            RendszamLista.Sort();

            StreamWriter Ki = new StreamWriter(Fajlnev);
            foreach (var R in RendszamLista)
            {
                int TankolasDb = 0;
                for (int i = 0; i < RekordokSzama; i++)
                {
                    if (R == T[i].Rendszam)
                    {
                        TankolasDb++;
                    }
                }
                Ki.WriteLine(R + " " + TankolasDb + " tankolas");
            }
            Ki.Flush();
            Ki.Close();
        }


    }
}
