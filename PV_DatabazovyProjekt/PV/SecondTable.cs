using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PV
{
    internal class SecondTable
    {
        string nazev;
        int cena;
        DateTime datum_vyroby;
        int id_materialu;
        public SecondTable(string nazev, int cena, DateTime datum_vyroby, int id_materialu)
        {
            Nazev = nazev;
            Cena = cena;
            Datum_vyroby = datum_vyroby;
            Id_materialu = id_materialu;
        }

        public string Nazev { get => nazev; set => nazev = value; }
        public int Cena { get => cena; set => cena = value; }
        public DateTime Datum_vyroby { get => datum_vyroby; set => datum_vyroby = value; }
        public int Id_materialu { get => id_materialu; set => id_materialu = value; }

        public override string? ToString()
        {
            return Nazev + " " + Cena + " " + Datum_vyroby + " " + Id_materialu;
        }
    }
}
