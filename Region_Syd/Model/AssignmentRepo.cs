using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Region_Syd.Model
{
    public class AssignmentRepo : IRepository<Assignment>
    {

        private readonly string _connectionString;
        private List<Assignment> _allAssignments;

        public AssignmentRepo(string connectionString)
        {

            _connectionString = connectionString;

            _allAssignments = new List<Assignment>();
            /*
            _allAssignments.Add(new Assignment()
            {
                RegionAssignmentId = "12-AB",
                StartAddress = "Sygehus Syd",
                EndAddress = "Riget",
                Start = new DateTime(2024, 09, 06, 10, 40, 00),
                Finish = new DateTime(2024, 09, 06, 13, 40, 00),
                // Description er maks 31 chars for at vises korrekt i view
                Description = "PAtienten er PsyKOtisK",
                AssignmentType = AssignmentTypeEnum.C,
                StartRegion = RegionEnum.RSj,
                EndRegion = RegionEnum.RH,
                IsMatched = false,


            });
            _allAssignments.Add(new Assignment()
            {
                RegionAssignmentId = "21-BA",
                StartAddress = "Riget",
                EndAddress = "Sygehus Syd",
                Start = new DateTime(2024, 09, 06, 14, 00, 00),
                Finish = new DateTime(2024, 09, 06, 17, 30, 00),
				// Description er maks 31 chars for at vises korrekt i view
				Description = "Kræver forsigtig kørsel",
                AssignmentType = AssignmentTypeEnum.D,
                StartRegion = RegionEnum.RH,
                EndRegion = RegionEnum.RSj,
                IsMatched = false,
            });
            _allAssignments.Add(new Assignment()
            {
                RegionAssignmentId = "33-CD",
                StartAddress = "Roskilde Hos.",
                EndAddress = "Kongensgade 118, 9320 Hjallerup",
                Start = new DateTime(2024, 09, 05, 15, 00, 00),
                Finish = new DateTime(2024, 09, 06, 13, 00, 00),
				// Description er maks 31 chars for at vises korrekt i view
				Description = "Kræver ilt i ambulancen",
                AssignmentType = AssignmentTypeEnum.D,
                StartRegion = RegionEnum.RSj,
                EndRegion = RegionEnum.RN,
                IsMatched = true,
            });
            */
        }
        public void AddToAllAssignments(Assignment assignment)
        {
            _allAssignments.Add(assignment);
        }
        public List<Assignment> GetAllAssignments()
        {
            return _allAssignments;
        }
        public void RemoveAssignment(Assignment assignment)
        {
            _allAssignments.Remove(assignment);
        }

        public void ReassignAmbulance(Assignment a1, Assignment a2)
        {
			if (DateTime.Compare(a1.Start, a2.Start) > 0) //assignment 1 skal have 2's ambulance
			{
                a1.AmbulanceId = a2.AmbulanceId;
				SetIsMatchedTrue(a1, a2);

			}
			else if (DateTime.Compare(a1.Start, a2.Start) < 0) //assignment 2 skal have 1's ambulance
			{
                a2.AmbulanceId = a1.AmbulanceId;
                SetIsMatchedTrue(a1, a2);
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

            // vores prøve query, som mangler kolonner.
            string query1 = @"SELECT ASSIGNMENTS_ADDRESS.AssignmentId, ASSIGNMENTS.AssignmentId, ASSIGNMENTS.IsMatched
                            FROM ASSIGNMENTS LEFT JOIN ASSIGNMENTS_ADDRESS ON ASSIGNMENTS.AssignmentsId=ASSIGNMENTS_ADDRESS.AssignmentsId;";


            string query = "SELECT * FROM ASSIGNMENTS_ADDRESS";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assignments.Add(new Assignment
                        {
                            //RegionAssignmentId = 
                            //StartAddress =
                            //EndAddress = 
                            //Start = 
                            //Finish = 
                            //AssignmentType = 
                            //StartRegion = 
                            //EndRegion = 
                            //IsMatched = 
                            //Description =
                        });
                    }
                }
            }

            return assignments;
        }

        public Assignment GetById(string regionalAssignmentId)
        {
            Assignment assignment = null;
            string query = @"SELECT * FROM 
                                (SELECT ASSIGNMENTS.RegionAssignmentId, AssignmentTypeId, _Start, Finish, _Description, IsMatched, AmbulanceId, StartAdress, EndAdress
                                FROM ASSIGNMENTS_ADDRESS FULL OUTER JOIN ASSIGNMENTS ON ASSIGNMENTS.RegionAssignmentId=ASSIGNMENTS_ADDRESS.RegionAssignmentId) AS A
                            WHERE A.RegionAssignmentId = @RegionAssignmentId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RegionAssignmentId", regionalAssignmentId);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        assignment = new Assignment
                        {
                            RegionAssignmentId = (string)reader["RegionAssignmentId"],
                            AmbulanceId = (string)reader["AmbulanceId"],
                            StartAddress = (int)reader["StartAdress"] == 2 ? "startTEST" : null,
                            EndAddress = (int)reader["EndAdress"] == 3 ? "endTEST" : null,
                            Start = (DateTime)reader["_Start"],
                            Finish = (DateTime)reader["Finish"],
                            Description = (string)reader["_Description"],
                            AssignmentType = (AssignmentTypeEnum)0,
                            StartRegion = (RegionEnum)1,
                            EndRegion = (RegionEnum)2,
                            IsMatched = true
                        };
                    }
                }
            }

            return assignment;
        }

        public void Add(Assignment entity)
        {
            //string query = "INSERT INTO ASSIGNMENTS_ADDRESS (Number) VALUES (@Number)";

            //using (SqlConnection connection = new SqlConnection(_connectionString))
            //{
            //    SqlCommand command = new SqlCommand(query, connection);
            //    command.Parameters.AddWithValue("@Number", semester.Number);
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //}
        }

        public void Update(Assignment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
