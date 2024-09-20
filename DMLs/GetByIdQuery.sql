SELECT * FROM 
	(SELECT ASSIGNMENTS.RegionAssignmentId, AssignmentTypeId, _Start, Finish, _Description, IsMatched, AmbulanceId, StartAdress, EndAdress
	FROM ASSIGNMENTS_ADDRESS FULL OUTER JOIN ASSIGNMENTS ON ASSIGNMENTS.RegionAssignmentId=ASSIGNMENTS_ADDRESS.RegionAssignmentId) AS A
WHERE A.RegionAssignmentId = '34-CD'

