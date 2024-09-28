using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Region_Syd.Model
{
    class RegionRepo : IRepository<Region>
    {
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
                        double distanceSaved = Convert.ToDouble(sqlite_datareader["DistanceSaved"]);
                        double hoursSaved = Convert.ToDouble(sqlite_datareader["SavedHours"]);
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
            throw new NotImplementedException();
        }
    }
}
