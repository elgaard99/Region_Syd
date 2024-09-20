SELECT * FROM 
-- Select mangler Zip AS EndZip, RegionId AS EndRegionId, ADDRESS.Address AS EndAddress
	(SELECT ASSIGNMENTS.RegionAssignmentId, ASSIGNMENTS.AssignmentTypeId, Type, Start, Finish, Description, IsMatched, AmbulanceId, Zip AS StartZip, RegionId AS StartRegionId, ADDRESS.Address AS StartAddress
	FROM (((ASSIGNMENTS_ADRESS 
	FULL OUTER JOIN ASSIGNMENTS ON ASSIGNMENTS.RegionAssignmentId=ASSIGNMENTS_ADRESS.RegionAssignmentId) 
	FULL OUTER JOIN ASSIGNMENT_TYPES ON ASSIGNMENT_TYPES.AssignmentTypeId=ASSIGNMENTS.AssignmentTypeId)
	FULL OUTER JOIN ADDRESS ON ADDRESS.AddressId=ASSIGNMENTS_ADRESS.StartAddress)) AS A
	-- UNION
	-- (SELECT Zip AS EndZip, RegionId AS EndRegionId, ADDRESS.Address AS EndAddress
	-- From (A
	-- FULL OUTER JOIN ADDRESS ON ADDRESS.AddressID=A.EndAdress)) AS B
WHERE A.RegionAssignmentId = '33-CD'

