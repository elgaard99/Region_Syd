using Microsoft.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;

namespace Region_Syd.Model
{
    public class AssignmentRepo : IRepository<Assignment>
    {
        private readonly string _connectionString;
        private readonly List<Region> _regions;

        public AssignmentRepo(string connectionString, List<Region> regions)
        {
            _connectionString = connectionString;
            _regions = regions;
        }

        public AssignmentRepo(string connectionString, IEnumerable<Region> regions)
        {
            _connectionString = connectionString;
            _regions = regions.ToList();
        }

        public void ReassignAmbulance(Assignment a1, Assignment a2)
        {
			if (DateTime.Compare(a1.Start, a2.Start) > 0) //assignment 1 skal have 2's ambulance
			{
                a1.AmbulanceId = a2.AmbulanceId;
				SetIsMatchedTrue(a1, a2);
                Update(a1);
                Update(a2);
                // AssignmentSavings(a1); IMPLEMENTER
			}
			else if (DateTime.Compare(a1.Start, a2.Start) < 0) //assignment 2 skal have 1's ambulance
			{
                a2.AmbulanceId = a1.AmbulanceId;
                SetIsMatchedTrue(a1, a2);
                Update(a1);
                Update(a2);
                // AssignmentSavings(a2); IMPLEMENTER;
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
                            (SELECT Assignments.RegionAssignmentId, Assignments.AssignmentTypeId, Type, Start, Finish, Description, IsMatched, AmbulanceId, S.Zip AS StartZip, SZT.Town AS StartTown, S.RegionId AS StartRegionId, S.Road AS StartAddress, E.Zip AS EndZip, EZT.Town AS EndTown, E.RegionId AS EndRegionId, E.Road AS EndAddress
	                            FROM Assignments_Addresses 
	                            FULL OUTER JOIN Assignments ON Assignments.RegionAssignmentId=Assignments_Addresses.RegionAssignmentId
	                            FULL OUTER JOIN AssignmentTypes ON AssignmentTypes.AssignmentTypeId=Assignments.AssignmentTypeId
	                            FULL OUTER JOIN Addresses AS S ON S.AddressId=Assignments_Addresses.StartAddress
	                            FULL OUTER JOIN Addresses AS E ON E.AddressId=Assignments_Addresses.EndAddress
	                            FULL OUTER JOIN ZipTowns AS SZT ON SZT.Zip=S.Zip
	                            FULL OUTER JOIN ZipTowns AS EZT ON EZT.Zip=E.Zip) AS A
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
                                (SELECT Assignments.RegionAssignmentId, Assignments.AssignmentTypeId, Type, Start, Finish, Description, IsMatched, AmbulanceId, S.Zip AS StartZip, SZT.Town AS StartTown, S.RegionId AS StartRegionId, S.Road AS StartAddress, E.Zip AS EndZip, EZT.Town AS EndTown, E.RegionId AS EndRegionId, E.Road AS EndAddress
	                            FROM Assignments_Addresses 
	                            FULL OUTER JOIN Assignments ON Assignments.RegionAssignmentId=Assignments_Addresses.RegionAssignmentId
	                            FULL OUTER JOIN AssignmentTypes ON AssignmentTypes.AssignmentTypeId=Assignments.AssignmentTypeId
	                            FULL OUTER JOIN Addresses AS S ON S.AddressId=Assignments_Addresses.StartAddress
	                            FULL OUTER JOIN Addresses AS E ON E.AddressId=Assignments_Addresses.EndAddress
	                            FULL OUTER JOIN ZipTowns AS SZT ON SZT.Zip=S.Zip
	                            FULL OUTER JOIN ZipTowns AS EZT ON EZT.Zip=E.Zip) AS A
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
            string query = "UPDATE Assignments SET IsMatched = @IsMatched, AmbulanceID = @AmbulanceId WHERE RegionAssignmentId = @RegionAssignmentId";

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

        Assignment ReadAssignment(SQLiteDataReader reader)
        {

            Func<string, int, string, string> Address = (street, zip, town) => $"{street}, {zip} {town}";
            string StartStreet = (string)reader["StartAddress"];
            int StartZip = Convert.ToInt16(reader["StartZip"]);
            string StartTown = (string)reader["StartTown"];

            string EndStreet = (string)reader["EndAddress"];
            int EndZip = Convert.ToInt16(reader["EndZip"]);
            string EndTown = (string)reader["EndTown"];

            Assignment assignment = new Assignment(
                id: (string)reader["RegionAssignmentId"],
                ambulanceId: (string)reader["AmbulanceId"],
                startAddress: Address(StartStreet, StartZip, StartTown),
                endAddress: Address(EndStreet, EndZip, EndTown),
                start: DateTime.Parse( (string)reader["Start"] ),
                finish: DateTime.Parse( (string)reader["Finish"] ),
                description: (string)reader["Description"],
                type: (string)reader["AssignmentTypeId"],
                startRegion: _regions.Find(r => r.RegionId == Convert.ToInt32(reader["StartRegionId"])),
                endRegion: _regions.Find(r => r.RegionId == Convert.ToInt32(reader["EndRegionId"])),
                isMatched: Convert.ToBoolean(reader["IsMatched"])
            );

            return assignment;

        }
    }
}
