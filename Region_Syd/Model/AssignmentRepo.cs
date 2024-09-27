using Microsoft.Data.SqlClient;
using System.Data.SQLite;

namespace Region_Syd.Model
{
    public class AssignmentRepo : IRepository<Assignment>
    {

        private readonly string _connectionString;

        public AssignmentRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void ReassignAmbulance(Assignment a1, Assignment a2)
        {
			if (DateTime.Compare(a1.Start, a2.Start) > 0) //assignment 1 skal have 2's ambulance
			{
                a1.AmbulanceId = a2.AmbulanceId;
				SetIsMatchedTrue(a1, a2);
                Update(a1);
                Update(a2);

			}
			else if (DateTime.Compare(a1.Start, a2.Start) < 0) //assignment 2 skal have 1's ambulance
			{
                a2.AmbulanceId = a1.AmbulanceId;
                SetIsMatchedTrue(a1, a2);
                Update(a1);
                Update(a2);
			}
		}
        
        public void SetIsMatchedTrue (Assignment a1, Assignment a2)
        {
            a1.IsMatched = true;
            a2.IsMatched = true;
        }

        // implementation af IRepository

        public IEnumerable<Assignment> GetAll()
        {
            var assignments = new List<Assignment>();

            string query = @"SELECT * FROM	
                            (SELECT ASSIGNMENTS.RegionAssignmentId, ASSIGNMENTS.AssignmentTypeId, Type, Start, Finish, Description, IsMatched, AmbulanceId, S.Zip AS StartZip, SZT.Town AS StartTown, S.RegionId AS StartRegionId, S.Road AS StartAddress, E.Zip AS EndZip, EZT.Town AS EndTown, E.RegionId AS EndRegionId, E.Road AS EndAddress
	                            FROM ASSIGNMENTS_ADDRESS 
	                            FULL OUTER JOIN ASSIGNMENTS ON ASSIGNMENTS.RegionAssignmentId=ASSIGNMENTS_ADDRESS.RegionAssignmentId
	                            FULL OUTER JOIN ASSIGNMENT_TYPES ON ASSIGNMENT_TYPES.AssignmentTypeId=ASSIGNMENTS.AssignmentTypeId
	                            FULL OUTER JOIN ADDRESS AS S ON S.AddressId=ASSIGNMENTS_ADDRESS.StartAddress
	                            FULL OUTER JOIN ADDRESS AS E ON E.AddressId=ASSIGNMENTS_ADDRESS.EndAddress
	                            FULL OUTER JOIN ZIPTOWNS AS SZT ON SZT.Zip=S.Zip
	                            FULL OUTER JOIN ZIPTOWNS AS EZT ON EZT.Zip=E.Zip) AS A
                            WHERE A.RegionAssignmentId IS NOT NULL";

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        assignments.Add(
                            ReadAssignment(reader)
                            );

                    }
                }
            }

            return assignments;
        }

        public Assignment GetById(string regionalAssignmentId)
        {

            

            Assignment assignment = null;
            string query = @"SELECT * FROM 
                                (SELECT ASSIGNMENTS.RegionAssignmentId, ASSIGNMENTS.AssignmentTypeId, Type, Start, Finish, Description, IsMatched, AmbulanceId, S.Zip AS StartZip, SZT.Town AS StartTown, S.RegionId AS StartRegionId, S.Road AS StartAddress, E.Zip AS EndZip, EZT.Town AS EndTown, E.RegionId AS EndRegionId, E.Road AS EndAddress
	                            FROM ASSIGNMENTS_ADDRESS 
	                            FULL OUTER JOIN ASSIGNMENTS ON ASSIGNMENTS.RegionAssignmentId=ASSIGNMENTS_ADDRESS.RegionAssignmentId
	                            FULL OUTER JOIN ASSIGNMENT_TYPES ON ASSIGNMENT_TYPES.AssignmentTypeId=ASSIGNMENTS.AssignmentTypeId
	                            FULL OUTER JOIN ADDRESS AS S ON S.AddressId=ASSIGNMENTS_ADDRESS.StartAddress
	                            FULL OUTER JOIN ADDRESS AS E ON E.AddressId=ASSIGNMENTS_ADDRESS.EndAddress
	                            FULL OUTER JOIN ZIPTOWNS AS SZT ON SZT.Zip=S.Zip
	                            FULL OUTER JOIN ZIPTOWNS AS EZT ON EZT.Zip=E.Zip) AS A
                            WHERE A.RegionAssignmentId = @RegionAssignmentId";

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@RegionAssignmentId", regionalAssignmentId);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        assignment = ReadAssignment(reader);                        
                    }
                }
            }

            return assignment;
        }

        public void Add(Assignment entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Assignment entity)
        {            
            string query = "UPDATE ASSIGNMENTS SET IsMatched = @IsMatched, AmbulanceID = @AmbulanceId WHERE RegionAssignmentId = @RegionAssignmentId";

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@RegionAssignmentId", entity.RegionAssignmentId);
                command.Parameters.AddWithValue("@IsMatched", entity.IsMatched);
                command.Parameters.AddWithValue("@AmbulanceId", entity.AmbulanceId);
                connection.Open();
                command.ExecuteNonQuery();
            }

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        private Assignment ReadAssignment(SQLiteDataReader reader)
        {

            Func<string, int, string, string> Address = (street, zip, town) => $"{street}, {zip} {town}";
            string StartStreet = (string)reader["StartAddress"];
            int StartZip = Convert.ToInt16(reader["StartZip"]);
            string StartTown = (string)reader["StartTown"];

            string EndStreet = (string)reader["EndAddress"];
            int EndZip = Convert.ToInt16(reader["EndZip"]);
            string EndTown = (string)reader["EndTown"];

            Assignment assignment = new Assignment
            {
                RegionAssignmentId = (string)reader["RegionAssignmentId"],
                AmbulanceId = (string)reader["AmbulanceId"],
                StartAddress = Address(StartStreet, StartZip, StartTown),
                EndAddress = Address(EndStreet, EndZip, EndTown),
                Start = (DateTime)reader["Start"],
                Finish = (DateTime)reader["Finish"],
                Description = (string)reader["Description"],
                AssignmentType = reader["AssignmentTypeId"].ToString().ToAssignmentTypeEnum(),
                StartRegion = (RegionEnum)Convert.ToInt32(reader["StartRegionId"]),
                EndRegion = (RegionEnum)Convert.ToInt32(reader["EndRegionId"]),
                IsMatched = (bool)reader["IsMatched"]
            };

            return assignment;

        }

        public string GetRegion()
        {
            string query = @"SELECT region FROM regions WHERE regionId = 1;";
            string? region = null;

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand sqlite_cmd = new SQLiteCommand(query, connection);
                connection.Open();

                using (SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader())
                {
                    if (sqlite_datareader.Read())
                    {
                        region = (string)sqlite_datareader["region"];
                    }
                }

                return region;
            }

        }
    }
}
