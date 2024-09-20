CREATE TABLE ASSIGNMENT_TYPES (
AssignmentTypeId NChar(1),
Type NvarChar(10),

CONSTRAINT PK_Assignment_Types PRIMARY KEY (AssignmentTypeId),
)

CREATE TABLE ASSIGNMENTS (
RegionAssignmentId NvarChar(20),
AssignmentTypeId NChar(1),
Start DATETIME,
Finish DATETIME,
Description NVarChar(100),
IsMatched BIT, 	
AmbulanceID NvarChar(20),

CONSTRAINT PK_Assignments PRIMARY KEY (RegionAssignmentId),
CONSTRAINT FK_Assignments_AssignmentTypes FOREIGN KEY (AssignmentTypeId) REFERENCES ASSIGNMENT_TYPES(AssignmentTypeId),
)

CREATE TABLE ZIPTOWNS (
Zip SMALLINT,
Town NVarChar(50),	

CONSTRAINT PK_ZipTowns PRIMARY KEY (Zip),
)

CREATE TABLE REGIONS (
RegionId NvarChar(3),
Region NVarChar(20),	

CONSTRAINT PK_Regions PRIMARY KEY (RegionId)
)

CREATE TABLE ADDRESS (
AddressId INT,
Zip SMALLINT,
RegionId NvarChar(3),
Address NVarChar(50),	

CONSTRAINT PK_Address PRIMARY KEY (AddressId),
CONSTRAINT FK1_Address_ZipTowns FOREIGN KEY (Zip) REFERENCES ZIPTOWNS(Zip), 
CONSTRAINT FK2_Address_Regions FOREIGN KEY (RegionId) REFERENCES REGIONS(RegionId), 
)

CREATE TABLE ASSIGNMENTS_ADRESS (
RegionAssignmentId NVarChar(20),
StartAddress INT,
EndAddress INT,
 
CONSTRAINT PK_Assignments_Address PRIMARY KEY (RegionAssignmentId),
CONSTRAINT FK1_AssignmentsAddress_Assignments FOREIGN KEY (RegionAssignmentId) REFERENCES ASSIGNMENTS(RegionAssignmentid),
CONSTRAINT FK2_AssignmentsAddress_Address FOREIGN KEY (StartAddress) REFERENCES ADDRESS(AddressId),
CONSTRAINT FK3_AssignmentsAddress_Address FOREIGN KEY (EndAddress) REFERENCES ADDRESS(AddressId),
)

