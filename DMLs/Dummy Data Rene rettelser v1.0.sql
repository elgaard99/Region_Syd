INSERT INTO AssignmentTypes (AssignmentTypeId, Type)
VALUES 
('A', 'ALvorlig'),
('B', 'Alvorlig'),
('C', 'Ikke-Akut'),
('D', 'Rutine');

INSERT INTO Assignments (RegionAssignmentId, AssignmentTypeId, Start, Finish, Description, IsMatched, AmbulanceID)
VALUES 
-- 2 of type A
('12-AB', 'A', '2024-09-06 10:40:00', '2024-09-06 13:40:00', 'Patienten er Psykotisk', 0, 'AmAReg1'),
('14-YZ', 'A', '2024-09-12 11:00:00', '2024-09-12 13:30:00', 'Patienten har feber', 0, 'AmNReg13'),

-- 3 of type B
('21-BA', 'B', '2024-09-06 14:00:00', '2024-09-06 17:30:00', 'Kræver forsigtig kørsel', 0, 'AmBReg2'),
('55-GH', 'B', '2024-09-08 12:15:00', '2024-09-08 14:00:00', 'Kræver ilt og ro', 0, 'AmEReg4'),
('88-MN', 'B', '2024-09-09 12:00:00', '2024-09-09 14:00:00', 'Kræver stabil overvøgning', 0, 'AmHReg7'),

-- Mix of C and D for the rest
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
(1, 'Region Hovedstaden'),    -- Capital Region -> Region Hovedstaden
(2, 'Region Midtjylland'),    -- Central Denmark -> Region Midtjylland
(3, 'Region Nordjylland'),    -- North Denmark -> Region Nordjylland
(4, 'Region Sjælland'),       -- Zealand -> Region Sj�lland
(5, 'Region Syddanmark');     -- Southern Denmark -> Region Syddanmark

INSERT INTO Addresses (AddressId, Zip, RegionId, Road)
VALUES 
(1, 1000, 1, 'Amagerbrogade 12'),    -- Capital Region
(2, 2000, 1, 'Frederiksberg Allé 22'), -- Capital Region
(3, 3000, 4, 'Strandvejen 45'),      -- Zealand
(4, 4000, 4, 'Algade 33'),           -- Zealand
(5, 5000, 5, 'Vesterbro 67'),        -- Southern Denmark
(6, 9000, 3, 'Østre Alle 15'),       -- North Denmark
(7, 8000, 2, 'Skejbyvej 50'),        -- Central Denmark
(8, 8000, 2, 'Søndergade 20'),       -- Central Denmark
(9, 9000, 3, 'Vesterbro 2');         -- North Denmark

INSERT INTO Assignments_Addresses (RegionAssignmentId, StartAddress, EndAddress)
VALUES 
-- Assignments for Region Hovedstaden (RegionId 1)
('12-AB', 1, 2),
('14-YZ', 2, 5),

-- Assignments for Region Sj�lland (RegionId 4)
('21-BA', 3, 4),
('33-CD', 4, 7),

-- Assignments for Region Syddanmark (RegionId 5)
('44-EF', 5, 1),
('77-KL', 5, 6),

-- Assignments for Region Nordjylland (RegionId 3)
('55-GH', 6, 9),
('66-IJ', 9, 6),

-- Assignments for Region Midtjylland (RegionId 2)
('88-MN', 7, 8),
('99-OP', 8, 3),

-- Additional to ensure diversity of regions
('10-QR', 3, 7),
('11-ST', 6, 5),
('12-UV', 4, 9),
('13-WX', 8, 1);