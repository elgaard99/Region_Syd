-- Drop foreign key constraints from Assignments_Addresses
ALTER TABLE Assignments_Addresses DROP CONSTRAINT FK1_AssignmentsAddresses_Assignments;
ALTER TABLE Assignments_Addresses DROP CONSTRAINT FK2_AssignmentsAddresses_Addresses;
ALTER TABLE Assignments_Addresses DROP CONSTRAINT FK3_AssignmentsAddresses_Addresses;

-- Drop foreign key constraints from Assignments
ALTER TABLE Assignments DROP CONSTRAINT FK_Assignments_AssignmentTypes;

-- Drop foreign key constraints from Addresses
ALTER TABLE Addresses DROP CONSTRAINT FK1_Addresses_ZipTowns;
ALTER TABLE Addresses DROP CONSTRAINT FK2_Addresses_Regions;

-- Drop tables now that constraints are removed
DROP TABLE IF EXISTS Assignments_Addresses;
DROP TABLE IF EXISTS Assignments;
DROP TABLE IF EXISTS AssignmentTypes;
DROP TABLE IF EXISTS Addresses;
DROP TABLE IF EXISTS ZipTowns;
DROP TABLE IF EXISTS Regions;

TRUNCATE TABLE Addresses;
SELECT TOP (1000) [AddressId]
      ,[Zip]
      ,[RegionId]
      ,[Road]
  FROM [master].[dbo].[Addresses]
