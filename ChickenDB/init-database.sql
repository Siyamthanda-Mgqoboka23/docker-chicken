USE master;
GO

-- Create the Farm database only if it does not already exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Farm')
BEGIN
    CREATE DATABASE Farm;
END
GO

-- Switch to the Farm database
USE Farm;
GO

-- Create Chicken table
CREATE TABLE Chicken (
    ChickID INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL,
    Breed VARCHAR(50) NOT NULL,
    Age INT NOT NULL,
    EggProduction DECIMAL(5,2) NOT NULL, -- eggs per day
    IsPregnant BIT NOT NULL, -- 1 for yes, 0 for no
    LastVetCheck DATE NOT NULL
);
GO

-- Insert sample data into Chicken table
INSERT INTO Chicken (Name, Breed, Age, EggProduction, IsPregnant, LastVetCheck)
VALUES
('Clucky', 'Leghorn', 2, 0.75, 0, '2024-01-15'),
('Feathers', 'Rhode Island Red', 3, 0.60, 1, '2024-02-20'),
('Pecky', 'Plymouth Rock', 1, 0.80, 0, '2024-03-10'),
('Eggy', 'Sussex', 4, 0.50, 1, '2024-01-30');
GO