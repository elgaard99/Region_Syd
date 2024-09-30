DROP TABLE IF EXISTS Assignments_Addresses; 
DROP TABLE IF EXISTS Assignments;
DROP TABLE IF EXISTS Addresses;
DROP TABLE IF EXISTS ZipTowns;
DROP TABLE IF EXISTS Regions;
DROP TABLE IF EXISTS AssignmentTypes;

CREATE TABLE IF NOT EXISTS AssignmentTypes(
	AssignmentTypeId TEXT,
	Type TEXT,
	
	PRIMARY KEY (AssignmentTypeId)
);

CREATE TABLE IF NOT EXISTS Regions (
	RegionId INT,
	Region TEXT NOT NULL,
	SavedHours REAL,
	
	PRIMARY KEY(RegionId)
);

CREATE TABLE IF NOT EXISTS ZipTowns (
	Zip INT,
	Town TEXT NOT NULL,	

	PRIMARY KEY(Zip)
);

CREATE TABLE IF NOT EXISTS Addresses (
	AddressId INT,
	Zip INT,
	RegionId INT,
	Road TEXT NOT NULL,	

	PRIMARY KEY (AddressId),
	FOREIGN KEY (Zip) 
      REFERENCES ZipTowns (Zip),
	FOREIGN KEY (RegionId) 
      REFERENCES Regions (RegionId) 
);

CREATE TABLE IF NOT EXISTS Assignments (
	RegionAssignmentId TEXT,
	AssignmentTypeId TEXT,
	Start TEXT,
	Finish TEXT,
	Description TEXT,
	IsMatched INT, 	
	AmbulanceID TEXT,
	
	PRIMARY KEY (RegionAssignmentID),
	FOREIGN KEY (AssignmentTypeId) 
      REFERENCES AssignmentTypes (AssignmentTypeId) 
);

CREATE TABLE IF NOT EXISTS Assignments_Addresses (
	RegionAssignmentId TEXT,
	StartAddress INT,
	EndAddress INT,
 
	PRIMARY KEY (RegionAssignmentId),
	FOREIGN KEY (RegionAssignmentId) 
      REFERENCES Assignments(RegionAssignmentId),
	FOREIGN KEY (StartAddress) 
      REFERENCES Addresses (AddressId),
	FOREIGN KEY (EndAddress) 
      REFERENCES Addresses (AddressId)
);