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
                        double hoursSaved = (sqlite_datareader["SavedHours"] as double?)  ?? 0;
                        int regionId = Convert.ToInt32(sqlite_datareader["RegionId"]);

                        Region region = new Region(name, hoursSaved, regionId);

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
            string query = "UPDATE Regions SET SavedHours = @SavedHours WHERE RegionId = @RegionId";

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@RegionId", entity.RegionId);
                command.Parameters.AddWithValue("@SavedHours", entity.HoursSaved);
                connection.Open();
                command.ExecuteNonQuery();
            }

        }
        public Region CalculateTotalSavings()
        {
            IEnumerable<Region> regions = GetAll();
            double totalHours = 0.0;

            foreach (Region region in regions)
            {
                totalHours += region.HoursSaved;
            }
            Region totalRegion = new Region(name: "Region Danmark", hoursSaved: totalHours, regionId: -1);
            return totalRegion;
        }
    }
}
