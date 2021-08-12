--Assumed all procedures have same price regardless of pet type

USE master

GO

DROP DATABASE IF EXISTS AnimalHospital

GO

CREATE DATABASE AnimalHospital

Go

USE AnimalHospital

CREATE TABLE Owner (
Id int IDENTITY (1000, 1) PRIMARY KEY,
Name varchar(30),
Address varchar(200),
);

CREATE TABLE Pet (
Id int IDENTITY (1000, 1) PRIMARY KEY,
Name varchar(30),
Type varchar (30),
Age int,
Owner_Id int
);

CREATE TABLE Visit (
Id int IDENTITY (1000, 1) PRIMARY KEY,
Pet_Id int,
Procedure_Id int,
Date varchar(30)
);

CREATE TABLE Procedure1 (
Id int IDENTITY (1000, 1) PRIMARY KEY,
Name varchar(30),
Price decimal(18, 2)
);

INSERT INTO Owner (Name, Address)
VALUES ('Sam Cook', '123 This Street')

INSERT INTO Owner (Name, Address)
VALUES ('Terry Kim', '123 Any Street')

INSERT INTO Pet (Name, Type, Age, Owner_Id)
VALUES ('Spot', 'Dog', 2, 1001)

INSERT INTO Pet (Name, Type, Age, Owner_Id)
VALUES ('Rover', 'Dog', 12, 1000)

INSERT INTO Pet (Name, Type, Age, Owner_Id)
VALUES ('Morris', 'Cat', 4, 1000)

INSERT INTO Pet (Name, Type, Age, Owner_Id)
VALUES ('Tweedy', 'Bird', 2, 1001)

INSERT INTO Procedure1 (Name, Price)
VALUES ('Rabies Vaccination', 30.00)

INSERT INTO Procedure1 (Name, Price)
VALUES ('Examine and Treat Wound', 50.00)

INSERT INTO Procedure1 (Name, Price)
VALUES ('Heart Worm Test', 7.00)

INSERT INTO Procedure1 (Name, Price)
VALUES ('Tetanus Vaccination', 30.00)

INSERT INTO Procedure1 (Name, Price)
VALUES ('Annual Check Up', 40.00)

INSERT INTO Procedure1 (Name, Price)
VALUES ('Eye Wash', 10.00)

INSERT INTO Visit (Pet_Id, Procedure_Id, Date)
VALUES (1000, 1003, 'JAN 21/2002')

INSERT INTO Visit (Pet_Id, Procedure_Id, Date)
VALUES (1000, 1002, 'MAR 10/2002')

INSERT INTO Visit (Pet_Id, Procedure_Id, Date)
VALUES (1001, 1000, 'JAN 13/2002')
INSERT INTO Visit (Pet_Id, Procedure_Id, Date)
VALUES (1001, 1001, 'MAR 27/2002')
INSERT INTO Visit (Pet_Id, Procedure_Id, Date)
VALUES (1001, 1002, 'APR 02/2002')

INSERT INTO Visit (Pet_Id, Procedure_Id, Date)
VALUES (1002, 1000, 'JAN 23/2001')
INSERT INTO Visit (Pet_Id, Procedure_Id, Date)
VALUES (1002, 1000, 'JAN 13/2002')

INSERT INTO Visit (Pet_Id, Procedure_Id, Date)
VALUES (1003, 1004, 'APR 30/2002')
INSERT INTO Visit (Pet_Id, Procedure_Id, Date)
VALUES (1003, 1005, 'APR 30/2002')

ALTER TABLE Pet
ADD FOREIGN KEY (Owner_Id)
REFERENCES Owner(Id)

ALTER TABLE Visit
ADD FOREIGN KEY (Pet_Id)
REFERENCES Pet(Id)

ALTER TABLE Visit
ADD FOREIGN KEY (Procedure_Id)
REFERENCES Procedure1(Id)

SELECT *
FROM Visit
