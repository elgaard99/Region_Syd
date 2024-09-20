SELECT * FROM 
-- StackOverflow link til table aliases på ADDRESS https://stackoverflow.com/questions/42742944/mysql-join-with-2-foreign-keys-in-same-table-referencing-same-key
	(SELECT ASSIGNMENTS.RegionAssignmentId, ASSIGNMENTS.AssignmentTypeId, Type, Start, Finish, Description, IsMatched, AmbulanceId, S.Zip AS StartZip, S.RegionId AS StartRegionId, S.Address AS StartAddress, E.Zip AS EndZip, E.RegionId AS EndRegionId, E.Address AS EndAddress
	FROM (((ASSIGNMENTS_ADRESS 
	FULL OUTER JOIN ASSIGNMENTS ON ASSIGNMENTS.RegionAssignmentId=ASSIGNMENTS_ADRESS.RegionAssignmentId) 
	FULL OUTER JOIN ASSIGNMENT_TYPES ON ASSIGNMENT_TYPES.AssignmentTypeId=ASSIGNMENTS.AssignmentTypeId)
	FULL OUTER JOIN ADDRESS AS S ON S.AddressId=ASSIGNMENTS_ADRESS.StartAddress
	FULL OUTER JOIN ADDRESS AS E ON E.AddressId=ASSIGNMENTS_ADRESS.EndAddress)) AS A
WHERE A.RegionAssignmentId = '33-CD'

