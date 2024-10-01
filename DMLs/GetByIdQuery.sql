SELECT * FROM 
-- StackOverflow link til table aliases på ADDRESS https://stackoverflow.com/questions/42742944/mysql-join-with-2-foreign-keys-in-same-table-referencing-same-key
	(SELECT ASSIGNMENTS.RegionAssignmentId, ASSIGNMENTS.AssignmentTypeId, Type, Start, Finish, Description, IsMatched, AmbulanceId, S.Zip AS StartZip, SZT.Town AS StartTown, S.RegionId AS StartRegionId, S.Road AS StartAddress, E.Zip AS EndZip, EZT.Town AS EndTown, E.RegionId AS EndRegionId, E.Road AS EndAddress
	FROM ASSIGNMENTS_ADDRESS 
	FULL OUTER JOIN ASSIGNMENTS ON ASSIGNMENTS.RegionAssignmentId=ASSIGNMENTS_ADDRESS.RegionAssignmentId
	FULL OUTER JOIN ASSIGNMENT_TYPES ON ASSIGNMENT_TYPES.AssignmentTypeId=ASSIGNMENTS.AssignmentTypeId
	FULL OUTER JOIN ADDRESS AS S ON S.AddressId=ASSIGNMENTS_ADDRESS.StartAddress
	FULL OUTER JOIN ADDRESS AS E ON E.AddressId=ASSIGNMENTS_ADDRESS.EndAddress
	FULL OUTER JOIN ZIPTOWNS AS SZT ON SZT.Zip=S.Zip
	FULL OUTER JOIN ZIPTOWNS AS EZT ON EZT.Zip=E.Zip) AS A
WHERE A.RegionAssignmentId = '33-CD'

