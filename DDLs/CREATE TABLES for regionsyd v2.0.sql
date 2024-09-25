
DROP TABLE IF EXISTS addresses;
CREATE TABLE IF NOT EXISTS addresses (
	addressId INT,
	zip INT,
	regionId INT,
	road TEXT NOT NULL,	

	PRIMARY KEY (addressId),
	FOREIGN KEY (zip) 
      REFERENCES zipTowns (zip),
	FOREIGN KEY (regionId) 
      REFERENCES regions (regionId) 
);

DROP TABLE IF EXISTS assignmentTypes;
CREATE TABLE IF NOT EXISTS assignmentTypes(
	assignmentTypeId TEXT,
	type TEXT,
	
	PRIMARY KEY (assignmentTypeId)
);

DROP TABLE IF EXISTS assignments;
CREATE TABLE IF NOT EXISTS assignments (
	regionAssignmentId TEXT,
	assignmentTypeId TEXT,
	start TEXT,
	finish TEXT,
	description TEXT,
	isMatched INT, 	
	ambulanceID TEXT,
	
	PRIMARY KEY (regionAssignmentID),
	FOREIGN KEY (assignmentTypeId) 
      REFERENCES assignmentTypes (assignmentTypeId) 
);

DROP TABLE IF EXISTS assignments_addresses; 
CREATE TABLE IF NOT EXISTS assignments_addresses (
	regionAssignmentId TEXT,
	startAddress INT,
	endAddress INT,
 
	PRIMARY KEY (regionAssignmentId),
	FOREIGN KEY (regionAssignmentId) 
      REFERENCES assignments(regionAssignmentId),
	FOREIGN KEY (startAddress) 
      REFERENCES addresses (addressId),
	FOREIGN KEY (endAddress) 
      REFERENCES addresses (addressId)
);

DROP TABLE IF EXISTS regions;
CREATE TABLE IF NOT EXISTS regions (
	regionId INT,
	region TEXT NOT NULL,	
	
	PRIMARY KEY(regionId)
);

DROP TABLE IF EXISTS zipTowns;
CREATE TABLE IF NOT EXISTS zipTowns (
	zip INT,
	town TEXT NOT NULL,	

	PRIMARY KEY(zip)
);
