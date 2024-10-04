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

        public void AssignmentSavings(Assignment assignment)
        {
            TimeSpan Duration = assignment.Finish - assignment.Start; // skal tilføje Duration til assignment.StartRegion instancen.
            
            double hours = Duration.TotalHours * 2;

            assignment.StartRegion.HoursSaved += hours;

        }

        public void ReassignAmbulance(Assignment a1, Assignment a2)
        {
			if (DateTime.Compare(a1.Start, a2.Start) > 0) //assignment 1 skal have 2's ambulance
			{
                a1.AmbulanceId = a2.AmbulanceId;
                SetIsMatchedTrue(a1, a2);
                Update(a1);
                Update(a2);
                AssignmentSavings(a1);
            }
            else if (DateTime.Compare(a1.Start, a2.Start) < 0) //assignment 2 skal have 1's ambulance
            {
                a2.AmbulanceId = a1.AmbulanceId;
                SetIsMatchedTrue(a1, a2);
                Update(a1);
                Update(a2);
                AssignmentSavings(a2); 
			}
		}
        
        public void SetIsMatchedTrue (Assignment a1, Assignment a2)
        {
            a1.IsMatched = true;
            a2.IsMatched = true;
        }
        public void ResetDBToDummyData()
        {
            string dropTableQuery = "ALTER TABLE Assignments_Addresses DROP CONSTRAINT FK1_AssignmentsAddresses_Assignments; ALTER TABLE Assignments_Addresses DROP CONSTRAINT FK2_AssignmentsAddresses_Addresses; ALTER TABLE Assignments_Addresses DROP CONSTRAINT FK3_AssignmentsAddresses_Addresses; " +
                                    "ALTER TABLE Assignments DROP CONSTRAINT FK_Assignments_AssignmentTypes; " +
                                    "ALTER TABLE Addresses DROP CONSTRAINT FK1_Addresses_ZipTowns; ALTER TABLE Addresses DROP CONSTRAINT FK2_Addresses_Regions; " +
                                    "DROP TABLE IF EXISTS Assignments_Addresses; DROP TABLE IF EXISTS Assignments; DROP TABLE IF EXISTS AssignmentTypes; DROP TABLE IF EXISTS Addresses; DROP TABLE IF EXISTS ZipTowns; DROP TABLE IF EXISTS Regions;";
            string createTablesQuery = "CREATE TABLE AssignmentTypes (AssignmentTypeId NChar(1),Type NvarChar(10),CONSTRAINT PK_AssignmentTypes PRIMARY KEY (AssignmentTypeId),)CREATE TABLE Assignments (RegionAssignmentId NvarChar(20),AssignmentTypeId NChar(1),Start DATETIME,Finish DATETIME,Description NVarChar(100),IsMatched BIT, AmbulanceID NvarChar(20),CONSTRAINT PK_Assignments PRIMARY KEY (RegionAssignmentId),CONSTRAINT FK_Assignments_AssignmentTypes FOREIGN KEY (AssignmentTypeId) REFERENCES AssignmentTypes(AssignmentTypeId),)CREATE TABLE ZipTowns (Zip SMALLINT,Town NVarChar(50) NOT NULL,\tCONSTRAINT PK_ZipTowns PRIMARY KEY (Zip),)CREATE TABLE Regions (RegionId TINYINT,Region NVarChar(20) NOT NULL,\tCONSTRAINT PK_Regions PRIMARY KEY (RegionId))CREATE TABLE Addresses (AddressId INT,Zip SMALLINT,RegionId TINYINT,Road NVarChar(50) NOT NULL,\tCONSTRAINT PK_Addresses PRIMARY KEY (AddressId),CONSTRAINT FK1_Addresses_ZipTowns FOREIGN KEY (Zip) REFERENCES ZipTowns(Zip), CONSTRAINT FK2_Addresses_Regions FOREIGN KEY (RegionId) REFERENCES Regions(RegionId), )CREATE TABLE Assignments_Addresses (RegionAssignmentId NVarChar(20),StartAddress INT,EndAddress INT, CONSTRAINT PK_Assignments_Addresses PRIMARY KEY (RegionAssignmentId),CONSTRAINT FK1_AssignmentsAddresses_Assignments FOREIGN KEY (RegionAssignmentId) REFERENCES Assignments(RegionAssignmentid),CONSTRAINT FK2_AssignmentsAddresses_Addresses FOREIGN KEY (StartAddress) REFERENCES Addresses(AddressId),CONSTRAINT FK3_AssignmentsAddresses_Addresses FOREIGN KEY (EndAddress) REFERENCES Addresses(AddressId),)";
            string dummyDataQuery = "INSERT INTO AssignmentTypes (AssignmentTypeId, Type)VALUES ('A', 'ALvorlig'),('B', 'Alvorlig'),('C', 'Ikke-Akut'),('D', 'Rutine');INSERT INTO Assignments (RegionAssignmentId, AssignmentTypeId, Start, Finish, Description, IsMatched, AmbulanceID)VALUES ('12-AB', 'A', '2024-09-06 10:40:00', '2024-09-06 13:40:00', 'Patienten er Psykotisk', 0, 'AmAReg1'),('14-YZ', 'A', '2024-09-12 11:00:00', '2024-09-12 13:30:00', 'Patienten har feber', 0, 'AmNReg13'),('21-BA', 'B', '2024-09-06 14:00:00', '2024-09-06 17:30:00', 'Kræver forsigtig kørsel', 0, 'AmBReg2'),('55-GH', 'B', '2024-09-08 12:15:00', '2024-09-08 14:00:00', 'Kræver ilt og ro', 0, 'AmEReg4'),('88-MN', 'B', '2024-09-09 12:00:00', '2024-09-09 14:00:00', 'Kræver stabil overvøgning', 0, 'AmHReg7'),('33-CD', 'C', '2024-09-05 11:00:00', '2024-09-05 13:30:00', 'Kræver ilt i ambulancen', 0, 'AmCReg2'),('44-EF', 'C', '2024-09-07 08:30:00', '2024-09-07 10:45:00', 'Patienten er aggressiv', 0, 'AmDReg3'),('66-IJ', 'C', '2024-09-08 15:00:00', '2024-09-08 16:45:00', 'Patienten har kramper', 0, 'AmFReg5'),('77-KL', 'D', '2024-09-09 09:00:00', '2024-09-09 11:30:00', 'Patienten er urolig', 0, 'AmGReg6'),('99-OP', 'D', '2024-09-10 07:00:00', '2024-09-10 09:15:00', 'Patienten er bevidstløs', 0, 'AmIReg8'),('10-QR', 'D', '2024-09-10 10:30:00', '2024-09-10 12:45:00', 'Kræver hurtig indgriben', 0, 'AmJReg9'),('11-ST', 'C', '2024-09-11 14:00:00', '2024-09-11 16:00:00', 'Patienten har hjertestop', 0, 'AmKReg10'),('12-UV', 'D', '2024-09-11 17:30:00', '2024-09-11 19:45:00', 'Patienten har vejrtrøkningsproblemer', 0, 'AmLReg11'),('13-WX', 'C', '2024-09-12 08:30:00', '2024-09-12 10:30:00', 'Kræver rolig transport', 0, 'AmMReg12');INSERT INTO ZipTowns (Zip, Town)VALUES (1000, 'Copenhagen'),(2000, 'Frederiksberg'),(3000, 'Helsingør'),(4000, 'Roskilde'),(5000, 'Odense'),(8000, 'Aarhus'),(9000, 'Aalborg');INSERT INTO Regions (RegionId, Region)VALUES (1, 'Region Hovedstaden'),    (2, 'Region Midtjylland'),    (3, 'Region Nordjylland'),    (4, 'Region Sjælland'),       (5, 'Region Syddanmark');     INSERT INTO Addresses (AddressId, Zip, RegionId, Road)VALUES (1, 1000, 1, 'Amagerbrogade 12'),    (2, 2000, 1, 'Frederiksberg Allé 22'), (3, 3000, 4, 'Strandvejen 45'),      (4, 4000, 4, 'Algade 33'),           (5, 5000, 5, 'Vesterbro 67'),        (6, 9000, 3, 'Østre Alle 15'),       (7, 8000, 2, 'Skejbyvej 50'),        (8, 8000, 2, 'Søndergade 20'),       (9, 9000, 3, 'Vesterbro 2');         INSERT INTO Assignments_Addresses (RegionAssignmentId, StartAddress, EndAddress)VALUES (RegionId 1)('12-AB', 1, 2),('14-YZ', 2, 5),(RegionId 4)('21-BA', 3, 4),('33-CD', 4, 7),(RegionId 5)('44-EF', 5, 1),('77-KL', 5, 6),(RegionId 3)('55-GH', 6, 9),('66-IJ', 9, 6),(RegionId 2)('88-MN', 7, 8),('99-OP', 8, 3),-- Additional to ensure diversity of regions('10-QR', 3, 7),('11-ST', 6, 5),('12-UV', 4, 9),('13-WX', 8, 1);";
            string query = @"
-- Drop tables if they exist (SQLite drops foreign keys when tables are dropped)
DROP TABLE IF EXISTS Assignments_Addresses;
DROP TABLE IF EXISTS Assignments;
DROP TABLE IF EXISTS AssignmentTypes;
DROP TABLE IF EXISTS Addresses;
DROP TABLE IF EXISTS ZipTowns;
DROP TABLE IF EXISTS Regions;

-- Create tables
CREATE TABLE AssignmentTypes (
    AssignmentTypeId TEXT PRIMARY KEY,
    Type TEXT
);

CREATE TABLE Assignments (
    RegionAssignmentId TEXT PRIMARY KEY,
    AssignmentTypeId TEXT,
    Start TEXT,
    Finish TEXT,
    Description TEXT,
    IsMatched INTEGER, -- Use INTEGER for boolean in SQLite (0 or 1)
    AmbulanceID TEXT,
    FOREIGN KEY (AssignmentTypeId) REFERENCES AssignmentTypes(AssignmentTypeId)
);

CREATE TABLE ZipTowns (
    Zip INTEGER PRIMARY KEY,
    Town TEXT NOT NULL
);

CREATE TABLE Regions (
    RegionId INTEGER PRIMARY KEY,
    Region TEXT NOT NULL
);

CREATE TABLE Addresses (
    AddressId INTEGER PRIMARY KEY,
    Zip INTEGER,
    RegionId INTEGER,
    Road TEXT NOT NULL,
    FOREIGN KEY (Zip) REFERENCES ZipTowns(Zip),
    FOREIGN KEY (RegionId) REFERENCES Regions(RegionId)
);

CREATE TABLE Assignments_Addresses (
    RegionAssignmentId TEXT,
    StartAddress INTEGER,
    EndAddress INTEGER,
    PRIMARY KEY (RegionAssignmentId),
    FOREIGN KEY (RegionAssignmentId) REFERENCES Assignments(RegionAssignmentId),
    FOREIGN KEY (StartAddress) REFERENCES Addresses(AddressId),
    FOREIGN KEY (EndAddress) REFERENCES Addresses(AddressId)
);

-- Insert data
INSERT INTO AssignmentTypes (AssignmentTypeId, Type)
VALUES 
    ('A', 'ALvorlig'),
    ('B', 'Alvorlig'),
    ('C', 'Ikke-Akut'),
    ('D', 'Rutine');

INSERT INTO Assignments (RegionAssignmentId, AssignmentTypeId, Start, Finish, Description, IsMatched, AmbulanceID)
VALUES 
    ('12-AB', 'A', '2024-09-06 10:40:00', '2024-09-06 13:40:00', 'Patienten er Psykotisk', 0, 'AmAReg1'),
    ('14-YZ', 'A', '2024-09-12 11:00:00', '2024-09-12 13:30:00', 'Patienten har feber', 0, 'AmNReg13'),
    ('21-BA', 'B', '2024-09-06 14:00:00', '2024-09-06 17:30:00', 'Kræver forsigtig kørsel', 0, 'AmBReg2'),
    ('55-GH', 'B', '2024-09-08 12:15:00', '2024-09-08 14:00:00', 'Kræver ilt og ro', 0, 'AmEReg4'),
    ('88-MN', 'B', '2024-09-09 12:00:00', '2024-09-09 14:00:00', 'Kræver stabil overvøgning', 0, 'AmHReg7'),
    ('33-CD', 'C', '2024-09-05 11:00:00', '2024-09-05 13:30:00', 'Kræver ilt i ambulancen', 0, 'AmCReg2'),
    ('44-EF', 'C', '2024-09-07 08:30:00', '2024-09-07 10:45:00', 'Patienten er aggressiv', 0, 'AmDReg3'),
    ('66-IJ', 'C', '2024-09-08 15:00:00', '2024-09-08 16:45:00', 'Patienten har kramper', 0, 'AmFReg5'),
    ('77-KL', 'D', '2024-09-09 09:00:00', '2024-09-09 11:30:00', 'Patienten er urolig', 0, 'AmGReg6'),
    ('99-OP', 'D', '2024-09-10 07:00:00', '2024-09-10 09:15:00', 'Patienten er bevidstløs', 0, 'AmIReg8'),
    ('10-QR', 'D', '2024-09-10 10:30:00', '2024-09-10 12:45:00', 'Kræver hurtig indgriben', 0, 'AmJReg9'),
    ('11-ST', 'C', '2024-09-11 14:00:00', '2024-09-11 16:00:00', 'Patienten har hjertestop', 0, 'AmKReg10'),
    ('12-UV', 'D', '2024-09-11 17:30:00', '2024-09-11 19:45:00', 'Patienten har vejrtrøkningsproblemer', 0, 'AmLReg11'),
    ('13-WX', 'C', '2024-09-12 08:30:00', '2024-09-12 10:30:00', 'Kræver rolig transport', 0, 'AmMReg12');

INSERT INTO ZipTowns (Zip, Town)
VALUES 
    (1000, 'Copenhagen'),
    (2000, 'Frederiksberg'),
    (3000, 'Helsingør'),
    (4000, 'Roskilde'),
    (5000, 'Odense'),
    (8000, 'Aarhus'),
    (9000, 'Aalborg');

INSERT INTO Regions (RegionId, Region)
VALUES 
    (1, 'Region Hovedstaden'),
    (2, 'Region Midtjylland'),
    (3, 'Region Nordjylland'),
    (4, 'Region Sjælland'),
    (5, 'Region Syddanmark');

INSERT INTO Addresses (AddressId, Zip, RegionId, Road)
VALUES 
    (1, 1000, 1, 'Amagerbrogade 12'),
    (2, 2000, 1, 'Frederiksberg Allé 22'),
    (3, 3000, 4, 'Strandvejen 45'),
    (4, 4000, 4, 'Algade 33'),
    (5, 5000, 5, 'Vesterbro 67'),
    (6, 9000, 3, 'Østre Alle 15'),
    (7, 8000, 2, 'Skejbyvej 50'),
    (8, 8000, 2, 'Søndergade 20'),
    (9, 9000, 3, 'Vesterbro 2');

INSERT INTO Assignments_Addresses (RegionAssignmentId, StartAddress, EndAddress)
VALUES 
    ('12-AB', 1, 2),
    ('14-YZ', 2, 5),
    ('21-BA', 3, 4),
    ('33-CD', 4, 7),
    ('44-EF', 5, 1),
    ('77-KL', 5, 6),
    ('55-GH', 6, 9),
    ('66-IJ', 9, 6),
    ('88-MN', 7, 8),
    ('99-OP', 8, 3),
    ('10-QR', 3, 7),
    ('11-ST', 6, 5),
    ('12-UV', 4, 9),
    ('13-WX', 8, 1);
";



            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
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
                start: DateTime.Parse((string)reader["Start"]),
                finish: DateTime.Parse((string)reader["Finish"]),
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