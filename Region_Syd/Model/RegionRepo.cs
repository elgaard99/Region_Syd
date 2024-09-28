using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    public class RegionRepo : IRepository<Region>
    {
        private readonly string _connectionString;

        public RegionRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(Region entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Region> GetAll()
        {
            var regions = new List<Region>();

            string query = @"SELECT * FROM Regions";
                            
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand sqlite_cmd = new SQLiteCommand(query, connection);
                connection.Open();

                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    while (sqlite_datareader.Read())
                    {
                        string name = (string)sqlite_datareader["Region"];
                        double distanceSaved = (sqlite_datareader["SavedDistance"] as double?)  ?? 0;
                        double hoursSaved = (sqlite_datareader["SavedHours"] as double?)  ?? 0;
                        int regionId = Convert.ToInt32(sqlite_datareader["RegionId"]);

                        Region region = new Region(name, hoursSaved, distanceSaved, regionId);

                        regions.Add(region);
                    }
                }
            }

            return regions;
        }

        public Region GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Region entity)
        {
            string query = "UPDATE Regions SET SavedHours = @SavedHours, SavedDistance = @SavedDistance WHERE RegionId = @RegionId";

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@RegionId", entity.RegionId);
                command.Parameters.AddWithValue("@SavedHours", entity.HoursSaved);
                command.Parameters.AddWithValue("@SavedDistance", entity.DistanceSaved);
                connection.Open();
                command.ExecuteNonQuery();
            }

        }
    }
}
