/* Create Database */
/*
CREATE DATABASE robotDb;
*/

/*
CREATE TABLE Users (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL,
	UserName VARCHAR(255) NOT NULL,
	Password VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL
);

CREATE TABLE Identities (
	Id VARCHAR(255) NOT NULL PRIMARY KEY,
	Name VARCHAR(255) NOT NULL
);

CREATE TABLE Companies(
    Id INT IDENTITY(1,1) PRIMARY KEY,
	IdentityId VARCHAR(255) NOT NULL FOREIGN KEY REFERENCES Identities(Id)
);

CREATE TABLE Clients (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdentityId VARCHAR(255) NOT NULL FOREIGN KEY REFERENCES Identities(Id)
);

CREATE TABLE Environments(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL,
	Length INT NOT NULL,
	Width INT NOT NULL,
	ClientId INT NOT NULL FOREIGN KEY REFERENCES Clients(Id)
);

CREATE TABLE Robots(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL,
	State INT NOT NULL,
	EnvironmentId INT NOT NULL FOREIGN KEY REFERENCES Environments(Id)
);

CREATE TABLE Warnings (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Message VARCHAR(255) NOT NULL,
	State INT NOT NULL,
	HourDay DATETIME,
	RobotId INT NOT NULL FOREIGN KEY REFERENCES Robots(Id),
	IdentityId VARCHAR(255) FOREIGN KEY REFERENCES Identities(Id)
);

CREATE TABLE Tasks(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL,
	InitHour VARCHAR(255) NOT NULL,
	EndHour VARCHAR(255) NOT NULL,
	WeekDays VARCHAR(255) NOT NULL,
	Repeat BIT,
	Execution BIT,
	Stop BIT
);

CREATE TABLE TasksRobots(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	RobotId INT NOT NULL FOREIGN KEY REFERENCES Robots(Id),
	TaskId INT NOT NULL FOREIGN KEY REFERENCES Tasks(Id),
);

CREATE TABLE Materials(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL
);

CREATE TABLE QuantityMaterials(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Quantity INT NOT NULL,
	MaterialId INT NOT NULL FOREIGN KEY REFERENCES Materials(Id),
	EnvironmentId INT FOREIGN KEY REFERENCES Environments(Id),
	TaskId INT FOREIGN KEY REFERENCES Tasks(Id)
);
*/

/*
INSERT INTO Users(Id, Name)
VALUES ('ffbd5b4d-53f5-4b95-b7ed-d140fb806cbe','Tino');

INSERT INTO Clients(UserId)
VALUES ('ffbd5b4d-53f5-4b95-b7ed-d140fb806cbe');

INSERT INTO Environments(Name, Length, Width, ClientId)
VALUES ('test', 10, 20, 1);

INSERT INTO Materials(Name)
VALUES ('test');

INSERT INTO QuantityMaterials(Quantity, MaterialId, EnvironmentId)
VALUES (10,1,1);

INSERT INTO Robots(Name, State, EnvironmentId)
VALUES ('test', 0, 1);
INSERT INTO Robots(Name, State, EnvironmentId)
VALUES ('test2', 0, 1);

INSERT INTO Tasks(Name, InitHour, EndHour, WeekDays)
VALUES ('test', '08:00:00', '09:00:00', 'seg');
INSERT INTO Tasks(Name, InitHour, EndHour, WeekDays)
VALUES ('test', '10:00:00', '11:00:00', 'seg');
INSERT INTO Tasks(Name, InitHour, EndHour, WeekDays)
VALUES ('test', '11:00:00', '12:00:00', 'seg');
INSERT INTO Tasks(Name, InitHour, EndHour, WeekDays)
VALUES ('test', '14:00:00,09:00:00,10:00:00', '15:00:00,10:00:00,11:00:00', 'seg,ter,quar');

INSERT INTO TasksRobots(TaskId, RobotId)
VALUES (1, 1);
INSERT INTO TasksRobots(TaskId, RobotId)
VALUES (1, 2);
INSERT INTO TasksRobots(TaskId, RobotId)
VALUES (2, 2);
INSERT INTO TasksRobots(TaskId, RobotId)
VALUES (3, 2);
INSERT INTO TasksRobots(TaskId, RobotId)
VALUES (4, 1);
INSERT INTO TasksRobots(TaskId, RobotId)
VALUES (4, 2);

INSERT INTO TasksRobots(TaskId, RobotId)
VALUES (7, 1);
*/

/*
select * from Users;
select * from Clients;

select * from Environments;
select * from Materials;
select * from QuantityMaterials;

select * from AspNetUsers;
select * from Identities;
select * from Clients;

select * from Environments;
select * from QuantityMaterials;
select * from Robots;

select * from Robots;

delete from TasksRobots;
delete from Tasks;

drop database robotDb

DBCC CHECKIDENT ('Warnings',RESEED,0)
*/