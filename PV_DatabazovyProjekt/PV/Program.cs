namespace PV
{
    using Microsoft.VisualBasic;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.Json;
    using System.Text.Json.Nodes;

    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();


            string jsonFile = File.ReadAllText("connection.json");
            JsonObject json = JsonSerializer.Deserialize<JsonObject>(jsonFile);

            consStringBuilder.UserID = json["UserID"].ToString();
            consStringBuilder.Password = json["Password"].ToString();
            consStringBuilder.InitialCatalog = json["InitialCatalog"].ToString();
            consStringBuilder.DataSource = json["DataSource"].ToString();
            consStringBuilder.ConnectTimeout = Convert.ToInt32(json["ConnectTimeout"].ToString());

            //NAIMPORTOVANE MNOU

            //string firstTable = File.ReadAllText("firsttable.json");
            //string secondTable = File.ReadAllText("secondtable.json");

            //List<FirstTable> list1 = JsonSerializer.Deserialize<List<FirstTable>>(firstTable);
            //List<SecondTable> list2 = JsonSerializer.Deserialize<List<SecondTable>>(secondTable);
            //foreach (var i in list1)
            //{
            //    Console.WriteLine(i);
            //}


            using (SqlConnection connection = new SqlConnection(consStringBuilder.ConnectionString))
            {

                bool working = true;
                while (working)
                {
                    try
                    {
                        connection.Open();


                        Console.WriteLine("\nVyber jednu z moznosti\n" +
                                          "-----------------------------\n" +
                                          "1. Precti tabulku\n" +
                                          "2. Vloz do tabulky\n" +
                                          "3. Uprav tabulku\n" +
                                          "4. Odstran z tabulky\n" +
                                          "5. Importuj soubor\n" +
                                          "0. Skonci program\n");

                        int choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {

                            case 1:
                                Console.Clear();
                                Console.WriteLine("Vyber tabulku, kterou chces precist\n" +
                                                  "-----------------------------\n" +
                                                  "1. Materialy\n" +
                                                  "2. Vyrobky\n" +
                                                  "3. Sklady\n" +
                                                  "4. Skladove polozky\n" +
                                                  "5. Objednavky\n");
                                int choice2 = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                switch (choice2)
                                {
                                    case 1:
                                        string query = "select * from Materialy";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            SqlDataReader reader = command.ExecuteReader();
                                            while (reader.Read())
                                            {
                                                Console.WriteLine("ID = " + reader[0].ToString() + ", Material = " + reader[1].ToString() + ", Typ = " + reader[2].ToString());
                                            }
                                            reader.Close();
                                        }
                                        break;
                                    case 2:
                                        query = "select Vyrobek.id, Vyrobek.nazev, cena, datum_vyroby, Materialy.nazev from Vyrobek inner join Materialy on Materialy.id = Vyrobek.id_materialu;";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            SqlDataReader reader = command.ExecuteReader();
                                            while (reader.Read())
                                            {
                                                Console.WriteLine("ID = " + reader[0].ToString() + ", Nazev vyrobku = " + reader[1].ToString() + ", Cena = " + reader[2].ToString() + ", Datum vyroby = " + reader[3].ToString() + ", Material = " + reader[4].ToString());
                                            }
                                            reader.Close();
                                        }
                                        break;
                                    case 3:
                                        query = "select * from Sklady";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            SqlDataReader reader = command.ExecuteReader();
                                            while (reader.Read())
                                            {
                                                Console.WriteLine("ID = " + reader[0].ToString() + ", Nazev skladu = " + reader[1].ToString() + ", Adresa = " + reader[2].ToString() + ", Kapacita skladu = " + reader[3].ToString());
                                            }
                                            reader.Close();
                                        }
                                        break;
                                    case 4:
                                        query = "select Skladove_polozky.id, Sklady.nazev, Vyrobek.nazev from Skladove_polozky inner join Sklady on Skladove_polozky.id_skladu = Sklady.id inner join Vyrobek on Skladove_polozky.id_vyrobku = Vyrobek.id;";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            SqlDataReader reader = command.ExecuteReader();
                                            while (reader.Read())
                                            {
                                                Console.WriteLine("ID = " + reader[0].ToString() + ", Nazev skladu = " + reader[1].ToString() + ", Nazev vyrobku = " + reader[2].ToString());
                                            }
                                            reader.Close();
                                        }
                                        break;
                                    case 5:
                                        query = "select Sklady.nazev as 'nazev skladu', Sklady.adresa as 'adresa skladu', prijemce, adresa_prijemce, datum from Objednavky inner join Sklady on Objednavky.id_skladu = Sklady.id;";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            SqlDataReader reader = command.ExecuteReader();
                                            while (reader.Read())
                                            {
                                                Console.WriteLine("Nazev skladu = " + reader[0].ToString() + ", Adresa skladu = " + reader[1].ToString() + ", Jmeno prijemce = " + reader[2].ToString() + ", Adresa prijemce = " + reader[3].ToString() + ", Datum a cas = " + reader[4].ToString());
                                            }
                                            reader.Close();
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Invalid choice.");
                                        break;
                                }
                                break;

                            case 2:
                                Console.Clear();
                                Console.WriteLine("Vyber tabulku, kterou chces napsat a pote vlozit\n" +
                                                  "-----------------------------\n" +
                                                  "1. Materialy\n" +
                                                  "2. Vyrobky\n" +
                                                  "3. Sklady\n" +
                                                  "4. Skladove polozky\n" +
                                                  "5. Objednavky\n");
                                int choice3 = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                switch (choice3)
                                {
                                    case 1:
                                        Console.WriteLine("Napis nazev materialu (napr. drevo, kamen)");
                                        string nazev = Console.ReadLine();
                                        Console.WriteLine("Napis typ materialu (napr. smrkove)");
                                        string typ = Console.ReadLine();

                                        string query = "INSERT INTO Materialy (nazev,typ) VALUES (@param1, @param2)";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.Parameters.AddWithValue("@param1", nazev);
                                            command.Parameters.AddWithValue("@param2", typ);
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine("Napis nazev vyrobku (napr. zidle, stul)");
                                        string nazev2 = Console.ReadLine();
                                        Console.WriteLine("Napis cenu");
                                        string cena = Console.ReadLine();
                                        Console.WriteLine("Napis datum vyroby");
                                        string datumVyroby = Console.ReadLine();
                                        Console.WriteLine("Napis ID materialu");
                                        string idMatrose = Console.ReadLine();

                                        query = "INSERT INTO Vyrobek (nazev,cena,datum_vyroby,id_materialu) VALUES (@param1, @param2, @param3, @param4)";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.Parameters.AddWithValue("@param1", nazev2);
                                            command.Parameters.AddWithValue("@param2", cena);
                                            command.Parameters.AddWithValue("@param3", datumVyroby);
                                            command.Parameters.AddWithValue("@param4", idMatrose);
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    case 3:
                                        Console.WriteLine("Napis nazev skladu");
                                        string nazev3 = Console.ReadLine();
                                        Console.WriteLine("Napis adresu");
                                        string adresa = Console.ReadLine();
                                        Console.WriteLine("Napis kapacitu (napr. 1000)");
                                        string kapacita = Console.ReadLine();

                                        query = "INSERT INTO Sklady (nazev,adresa,kapacita) VALUES (@param1, @param2, @param3)";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.Parameters.AddWithValue("@param1", nazev3);
                                            command.Parameters.AddWithValue("@param2", adresa);
                                            command.Parameters.AddWithValue("@param3", kapacita);
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    case 4:
                                        Console.WriteLine("Napis ID skladu");
                                        string idSkladu = Console.ReadLine();
                                        Console.WriteLine("Napis ID vyrobku')");
                                        string idVyrobku = Console.ReadLine();
                                        Console.WriteLine("Napis mnozstvi vyrobku na sklade')");
                                        string mnozstvi = Console.ReadLine();

                                        query = "INSERT INTO Skladove_polozky (id_skladu,id_vyrobku,mnozstvi) VALUES (@param1, @param2, @param3)";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.Parameters.AddWithValue("@param1", idSkladu);
                                            command.Parameters.AddWithValue("@param2", idVyrobku);
                                            command.Parameters.AddWithValue("@param3", mnozstvi);
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    case 5:
                                        Console.WriteLine("Napis ID skladu");
                                        string idSkladu2 = Console.ReadLine();
                                        Console.WriteLine("Napis jmeno prijemce");
                                        string prijemce = Console.ReadLine();
                                        Console.WriteLine("Napis ulici prijemce");
                                        string ulice = Console.ReadLine();
                                        Console.WriteLine("Napis datum a cas kdy ma objednavka prijet" +
                                                          "(napr. 2023-10-20 14:30:00)");
                                        string datumCas = Console.ReadLine();

                                        query = "INSERT INTO Objednavky (id_skladu,prijemce,adresa_prijemce,datum) VALUES (@param1, @param2, @param3, @param4)";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.Parameters.AddWithValue("@param1", idSkladu2);
                                            command.Parameters.AddWithValue("@param2", prijemce);
                                            command.Parameters.AddWithValue("@param3", ulice);
                                            command.Parameters.AddWithValue("@param4", datumCas);
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Invalid choice.");
                                        break;
                                }
                                break;

                            case 3:
                                Console.Clear();
                                Console.WriteLine("Vyber tabulku, kterou chces upravit\n" +
                                                  "-----------------------------\n" +
                                                  "1. Vyrobky\n" +
                                                  "2. Sklady\n");
                                int choice4 = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                switch (choice4)
                                {
                                    case 1:
                                        Console.WriteLine("Vyber ID Vyrobku, ktery chces zmenit");
                                        int idecko = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Vyber jednu z moznosti\n" +
                                                          "-----------------------------\n" +
                                                          "1. Vynasobit cenu cislem\n" +
                                                          "2. Vydelit cenu cislem");
                                        int moznost = Convert.ToInt32(Console.ReadLine());
                                        switch (moznost)
                                        {
                                            case 1:
                                                Console.WriteLine("Zadej nasobek (s carkou napr. 1,5)");
                                                float nasobek = float.Parse(Console.ReadLine());
                                                string query = "UPDATE Vyrobek SET cena = cena *  @nasobek  WHERE id = @idecko;";
                                                using (SqlCommand command = new SqlCommand(query, connection))
                                                {
                                                    command.Parameters.AddWithValue("@nasobek", nasobek);
                                                    command.Parameters.AddWithValue("@idecko", idecko);
                                                    command.ExecuteNonQuery();
                                                }
                                                break;

                                            case 2:
                                                Console.WriteLine("Zadej nasobek (s carkou napr. 1,5)");
                                                nasobek = float.Parse(Console.ReadLine());
                                                query = "UPDATE Vyrobek SET cena = cena /  @nasobek  WHERE id = @idecko;";
                                                using (SqlCommand command = new SqlCommand(query, connection))
                                                {
                                                    command.Parameters.AddWithValue("@nasobek", nasobek);
                                                    command.Parameters.AddWithValue("@idecko", idecko);
                                                    command.ExecuteNonQuery();
                                                }
                                                break;

                                            default:
                                                Console.WriteLine("Invalid choice.");
                                                break;
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine("Vyber ID Skladu, ktery chces zmenit");
                                        idecko = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("Vyber jednu z moznosti\n" +
                                                          "-----------------------------\n" +
                                                          "1. Vynasobit kapacitu cislem\n" +
                                                          "2. Vydelit kapacitu cislem");
                                        moznost = Convert.ToInt32(Console.ReadLine());
                                        switch (moznost)
                                        {
                                            case 1:
                                                Console.WriteLine("Zadej nasobek (s carkou napr. 1,5)");
                                                float nasobek = float.Parse(Console.ReadLine());
                                                string query = "UPDATE Sklady SET kapacita = kapacita *  @nasobek  WHERE id = @idecko;";
                                                using (SqlCommand command = new SqlCommand(query, connection))
                                                {
                                                    command.Parameters.AddWithValue("@nasobek", nasobek);
                                                    command.Parameters.AddWithValue("@idecko", idecko);
                                                    command.ExecuteNonQuery();
                                                }
                                                break;

                                            case 2:
                                                Console.WriteLine("Zadej nasobek (s carkou napr. 1,5)");
                                                nasobek = float.Parse(Console.ReadLine());
                                                query = "UPDATE Sklady SET kapacita = kapacita /  @nasobek  WHERE id = @idecko;";
                                                using (SqlCommand command = new SqlCommand(query, connection))
                                                {
                                                    command.Parameters.AddWithValue("@nasobek", nasobek);
                                                    command.Parameters.AddWithValue("@idecko", idecko);
                                                    command.ExecuteNonQuery();
                                                }
                                                break;

                                            default:
                                                Console.WriteLine("Invalid choice.");
                                                break;
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Invalid choice.");
                                        break;
                                }
                                break;

                            case 4:
                                Console.Clear();
                                Console.WriteLine("Vyber tabulku a pote zaznam, ktery chces smazat\n" +
                                                  "-----------------------------\n" +
                                                  "1. Materialy\n" +
                                                  "2. Vyrobky\n" +
                                                  "3. Sklady\n" +
                                                  "4. Skladove polozky\n" +
                                                  "5. Objednavky\n");
                                int choice5 = Convert.ToInt32(Console.ReadLine());
                                Console.Clear();
                                switch (choice5)
                                {
                                    case 1:
                                        Console.WriteLine("Vyber ID materialu, ktery chces smazat");
                                        int idecko = Convert.ToInt32(Console.ReadLine());
                                        string query = "DELETE FROM Materialy WHERE id = " + idecko + ";";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine("Vyber ID vyrobku, ktery chces smazat");
                                        idecko = Convert.ToInt32(Console.ReadLine());
                                        query = "DELETE FROM Vyrobek WHERE id = " + idecko + ";";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    case 3:
                                        Console.WriteLine("Vyber ID skladu, ktery chces smazat");
                                        Console.WriteLine("Vyber ID vyrobku, ktery chces smazat");
                                        idecko = Convert.ToInt32(Console.ReadLine());
                                        query = "DELETE FROM Sklady WHERE id = " + idecko + ";";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    case 4:
                                        Console.WriteLine("Vyber ID skladove polozky, kterou chces smazat");
                                        Console.WriteLine("Vyber ID vyrobku, ktery chces smazat");
                                        idecko = Convert.ToInt32(Console.ReadLine());
                                        query = "DELETE FROM Skladove_polozky WHERE id = " + idecko + ";";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    case 5:
                                        Console.WriteLine("Vyber ID objednavky, kterou chces smazat");
                                        Console.WriteLine("Vyber ID vyrobku, ktery chces smazat");
                                        idecko = Convert.ToInt32(Console.ReadLine());
                                        query = "DELETE FROM Objednavky WHERE id = " + idecko + ";";
                                        using (SqlCommand command = new SqlCommand(query, connection))
                                        {
                                            command.ExecuteNonQuery();
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("Invalid choice.");
                                        break;
                                }
                                break;

                            case 5:
                                Console.WriteLine("Zadej cestu k souboru (Pro tabulky Vyrobek a Material. Formaty: JSON)");
                                Console.WriteLine("Cestu piste ve stylu C:/neco/dalsineco/soubor.json");
                                string cesta = Console.ReadLine();
                                Console.WriteLine("Zadej jakou tabulku budes pridavat\n1. Materialy\n2. Vyrobky");
                                int typTabuky = Convert.ToInt32(Console.ReadLine());

                                switch (typTabuky)
                                {
                                    case 1:
                                        string newTable = File.ReadAllText(cesta);
                                        List<FirstTable> list3 = JsonSerializer.Deserialize<List<FirstTable>>(newTable);

                                        foreach (var i in list3)
                                        {
                                            string query = "INSERT INTO Materialy (nazev,typ) VALUES (@param1, @param2)";
                                            using (SqlCommand command = new SqlCommand(query, connection))
                                            {
                                                command.Parameters.AddWithValue("@param1", i.Nazev);
                                                command.Parameters.AddWithValue("@param2", i.Typ);
                                                command.ExecuteNonQuery();
                                            }
                                        }



                                        break;

                                    case 2:
                                        newTable = File.ReadAllText(cesta);
                                        List<SecondTable> list4 = JsonSerializer.Deserialize<List<SecondTable>>(newTable);

                                        foreach (var i in list4)
                                        {
                                            string query = "INSERT INTO Vyrobek (nazev,cena,datum_vyroby,id_materialu) VALUES (@param1, @param2, @param3, @param4)";
                                            using (SqlCommand command = new SqlCommand(query, connection))
                                            {
                                                command.Parameters.AddWithValue("@param1", i.Nazev);
                                                command.Parameters.AddWithValue("@param2", i.Cena);
                                                command.Parameters.AddWithValue("@param3", i.Datum_vyroby);
                                                command.Parameters.AddWithValue("@param4", i.Id_materialu);
                                                command.ExecuteNonQuery();
                                            }
                                        }

                                        break;

                                    default:
                                        Console.WriteLine("Invalid choice.");
                                        break;
                                }
                                break;




                            case 0:
                                Console.WriteLine("Ukoncil si program");
                                working = false;
                                break;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);

                    }

                    connection.Close();
                }

            }

        }
    }
}