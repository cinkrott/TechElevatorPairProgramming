USE master
GO

DROP DATABASE IF EXISTS ArtGallery
GO

CREATE DATABASE ArtGallery
GO

USE ArtGallery
GO

CREATE TABLE Customer(
	Id int IDENTITY (1000, 1) PRIMARY KEY,
	Name varchar (255),
	Address varchar (255),
	Phone varchar (255),
	
);
CREATE TABLE Art(
	Id int IDENTITY (1000, 1) PRIMARY KEY,
	Title varchar (255),
	Artist varchar (255)
);

CREATE TABLE Purchases(
	Id int IDENTITY (1000, 1) PRIMARY KEY,
	Date varchar (30),
	Price decimal (18, 2),	
	Customer_Id int,
	Art_Id int,


);

INSERT INTO Customer (Name, Address, Phone)
VALUES ('Elizabeth Jackson', '123 4th Avenue Fonthill, ON L3H 4S4', '(206) 284-6783')

INSERT INTO Purchases (Date, Price, Customer_Id, Art_Id)
VALUES ('09/17/2000', 7000.00, 1000, 1001)

INSERT INTO Purchases (Date, Price, Customer_Id, Art_Id)
VALUES ('05/11/2000', 7000.00, 1000, 1000)

INSERT INTO Purchases (Date, Price, Customer_Id, Art_Id)
VALUES ('02/14/2000', 7000.00, 1000, 1002)

INSERT INTO Purchases (Date, Price, Customer_Id, Art_Id)
VALUES ('07/15/2000', 7000.00, 1000, 1000)

INSERT INTO Art (title, Artist)
VALUES ('South toward Emerald Sea', 'Dennis Fring')

INSERT INTO Art (title, Artist)
VALUES ('Laugh with Teeth', 'Carol Channing')

INSERT INTO Art (title, Artist)
VALUES ('At the Movies', 'Carol Channing')


GO

ALTER TABLE Purchases
ADD FOREIGN KEY(Customer_Id)
REFERENCES Customer(Id);

ALTER TABLE Purchases
ADD FOREIGN KEY(Art_Id)
REFERENCES Art(Id);

GO




