using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace PV
{
    internal class FirstTable
    {
        string nazev;
        string typ;

        public FirstTable(string nazev, string typ)
        {
            Nazev = nazev;
            Typ = typ;
        }

        public string Nazev { get => nazev; set => nazev = value; }
        public string Typ { get => typ; set => typ = value; }

        public override string? ToString()
        {
            return Nazev + " " + Typ;
        }
    }
}
