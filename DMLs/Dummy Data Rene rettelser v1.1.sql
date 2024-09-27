CREATE TABLE AssignmentTypes (
AssignmentTypeId NChar(1),
Type NvarChar(10),

CONSTRAINT PK_AssignmentTypes PRIMARY KEY (AssignmentTypeId),
)

CREATE TABLE Assignments (
RegionAssignmentId NvarChar(20),
AssignmentTypeId NChar(1),
Start DATETIME,
Finish DATETIME,
Description NVarChar(100),
IsMatched BIT, 	
AmbulanceID NvarChar(20),

CONSTRAINT PK_Assignments PRIMARY KEY (RegionAssignmentId),
CONSTRAINT FK_Assignments_AssignmentTypes FOREIGN KEY (AssignmentTypeId) REFERENCES AssignmentTypes(AssignmentTypeId),
)

CREATE TABLE ZipTowns (
Zip SMALLINT,
Town NVarChar(50) NOT NULL,	

CONSTRAINT PK_ZipTowns PRIMARY KEY (Zip),
)

CREATE TABLE Regions (
RegionId TINYINT,
Region NVarChar(20) NOT NULL,	

CONSTRAINT PK_Regions PRIMARY KEY (RegionId)
)

CREATE TABLE Addresses (
AddressId INT,
Zip SMALLINT,
RegionId TINYINT,
Road NVarChar(50) NOT NULL,	

CONSTRAINT PK_Addresses PRIMARY KEY (AddressId),
CONSTRAINT FK1_Addresses_ZipTowns FOREIGN KEY (Zip) REFERENCES ZipTowns(Zip), 
CONSTRAINT FK2_Addresses_Regions FOREIGN KEY (RegionId) REFERENCES Regions(RegionId), 
)

CREATE TABLE Assignments_Addresses (
RegionAssignmentId NVarChar(20),
StartAddress INT,
EndAddress INT,
 
CONSTRAINT PK_Assignments_Addresses PRIMARY KEY (RegionAssignmentId),
CONSTRAINT FK1_AssignmentsAddresses_Assignments FOREIGN KEY (RegionAssignmentId) REFERENCES Assignments(RegionAssignmentid),
CONSTRAINT FK2_AssignmentsAddresses_Addresses FOREIGN KEY (StartAddress) REFERENCES Addresses(AddressId),
CONSTRAINT FK3_AssignmentsAddresses_Addresses FOREIGN KEY (EndAddress) REFERENCES Addresses(AddressId),
)