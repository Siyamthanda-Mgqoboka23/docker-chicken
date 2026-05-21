USE master;
GO

-- Create the Farm database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Farm')
BEGIN
    CREATE DATABASE Farm;
END
GO

-- Switch to the Farm database
USE Farm;
GO

-- Create the Chickens table
Create TABLE Chickens (
    ChickId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Breed NVARCHAR(50) NOT NULL,
    Age INT NOT NULL,
    EggProduction DECIMAL(5,2) NOT NULL,
    IsPregnant BIT NOT NULL, -- 1 for yes, 0 for no
    LastVetCheck DATE NOT NULL
);
GO

--Insert data into Chickens table
INSERT INTO Chickens (Name, Breed, Age, EggProduction, IsPregnant, LastVetCheck) VALUES
('Clucky', 'Leghorn', 2, 150.00, 0, '2026-01-15'),
('Feathers', 'Rhode Island Red', 3, 200.00, 1, '2026-02-20'),
('Pecky', 'Plymouth Rock', 1, 100.00, 0, '2026-03-10');
GO
