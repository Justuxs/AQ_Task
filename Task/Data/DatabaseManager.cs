using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Task.Data
{
    public class DatabaseManager
    {
        private const string ConnectionString = "Data Source=mydatabase.db";

        public void CreateDatabase()
        {
            SQLitePCL.Batteries.Init();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                string createDrawingTableQuery = @"CREATE TABLE IF NOT EXISTS Harness_drawing (
                                                ID INTEGER PRIMARY KEY,
                                                Harness VARCHAR(30),
                                                Harness_version VARCHAR(30),
                                                Drawing VARCHAR(30),
                                                Drawing_version VARCHAR(30)
                                              );";
                using (var command = new SqliteCommand(createDrawingTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                string createWiresTableQuery = @"CREATE TABLE IF NOT EXISTS Harness_wires (
                                                ID INTEGER PRIMARY KEY,
                                                Harness_ID INTEGER,
                                                Length REAL,
                                                Color VARCHAR(30),
                                                Housing_1 VARCHAR(30),
                                                Housing_2 VARCHAR(30),
                                                FOREIGN KEY (Harness_ID) REFERENCES Harness_drawing (ID)
                                            );";
                using (var command = new SqliteCommand(createWiresTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void DropDatabase()
        {
            SQLitePCL.Batteries.Init();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DROP TABLE IF EXISTS Harness_wires;";
                    command.ExecuteNonQuery();
                    command.CommandText = "DROP TABLE IF EXISTS Harness_drawing;";
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }


        public void InsertData()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                string insertDrawingQuery = "INSERT INTO Harness_drawing (ID, Harness, Harness_version, Drawing, Drawing_version) VALUES  (40953, 'S2563532M', 'S-6', 'EP', 'S-4'), (40442, 'S2563545M', 'S12','EP', 'S-4'), (39087, 'S2563549M', 'S-9', 'EP', 'S-4'), (39077, 'S2641137M', 'S-9','EP', 'S-4'), (38643, 'S2656843M', '5','EP', 'S-4');";
                using (var command = new SqliteCommand(insertDrawingQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                string insertWiresQuery = "INSERT INTO Harness_wires (ID, Harness_ID, Length, Color, Housing_1, Housing_2) VALUES " +
                    "(3115654, 38643, 950, 'R', 'C604:19', 'P2.BX2:1'), " +
                    "(3115655, 38643, 450, 'R', 'C604:23', 'C521:1'), " +
                    "(3158749, 39077, 665, 'BN', 'E71.B:1', 'C604:21'), " +
                    "(3158750, 39077, 665, 'GR', 'E71.B:4', 'C604:23'), " +
                    "(3159894, 39087, 465, 'W', 'E71.A:1', 'C681'), " +
                    "(3159895, 39087, 680, 'SB', 'E71.P:3', 'G504-2'), " +
                    "(3277678, 40442, 475, 'GN', 'P2.E85:1', 'C680'), " +
                    "(3277679, 40442, 980, 'R', 'P2.BX2:1', 'E30.P:1'), " +
                    "(3328453, 40953, 365, 'W', 'C621:6', 'C681'), " +
                    "(3328454, 40953, 305, 'SB', 'C620:24', 'G508-3');";
                using (var command = new SqliteCommand(insertWiresQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        public List<Harness_wire> SelectHarnessWiresByHarnessID(int harnessID)
        {
            List<Harness_wire> harnessWires = new List<Harness_wire>();

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Harness_wires WHERE Harness_ID = @harnessID";
                using (var command = new SqliteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@harnessID", harnessID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Harness_wire harnessWire = new Harness_wire();
                            harnessWire.ID = reader.GetInt32(0);
                            harnessWire.harness_ID = reader.GetInt32(1);
                            harnessWire.length = reader.IsDBNull(2) ? null : (float?)reader.GetDouble(2);
                            harnessWire.color = reader.IsDBNull(3) ? null : reader.GetString(3);
                            harnessWire.housing_1 = reader.IsDBNull(4) ? null : reader.GetString(4);
                            harnessWire.housing_2 = reader.IsDBNull(5) ? null : reader.GetString(5);

                            harnessWires.Add(harnessWire);
                        }
                    }
                }
            }

            return harnessWires;
        }


        public List<Harness_drawing> SelectHarnessDrawings()
        {
            List<Harness_drawing> harness_drawings= new List<Harness_drawing> ();
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                string selectDrawingQuery = "SELECT * FROM Harness_drawing;";
                using (var command = new SqliteCommand(selectDrawingQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Harness_drawing harness_drawing = new Harness_drawing();
                            harness_drawing.ID = reader.GetInt32(0);
                            harness_drawing.harness = reader.IsDBNull(1) ? null : reader.GetString(1);
                            harness_drawing.harness_version = reader.IsDBNull(2) ? null : reader.GetString(2);
                            harness_drawing.drawing = reader.IsDBNull(3) ? null : reader.GetString(3);
                            harness_drawing.drawing_version = reader.IsDBNull(4) ? null : reader.GetString(4);
                            harness_drawing.harness_wires = this.SelectHarnessWiresByHarnessID(harness_drawing.ID);

                            harness_drawings.Add(harness_drawing);
                        }
                    }

                }

                connection.Close();
            }
            return harness_drawings;
        }
    }
}