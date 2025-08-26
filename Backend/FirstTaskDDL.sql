Create Database IDA
go
Create Table Dept(
Id INT primary key identity(1,1),
name nvarchar(255) not null
)

CREATE TABLE [Group](
Id INT PRIMARY KEY IDENTITY(1,1),
name nvarchar(255),
DeptId INT not null,
CONSTRAINT FK_GROUP_DEPT
FOREIGN KEY(DeptId) REFERENCES Dept(id)
ON DELETE NO ACTION,
)

CREATE TABLE Citizen(
Id INT PRIMARY KEY IDENTITY(1,1),
[Name] nvarchar(255) not null,
NatId varchar(14) not null
)

CREATE Table Emp(
Id int PRIMARY KEY IDENTITY(1,1),
CtznId int not null,
DeptId INT not null,

CONSTRAINT FK_EMP_CITIZEN
FOREIGN KEY(ctznId) REFERENCES Citizen(id),
CONSTRAINT FK_EMP_DEPT
FOREIGN KEY (DeptId) REFERENCES Dept(id),
)

CREATE TABLE [User](
Id int PRIMARY KEY IDENTITY(1,1),
UserName nvarchar(255) not null,
[Password] nvarchar(255) not null,
EmpId INT not null,
ExtClctr BIT NOT NULL DEFAULT 0,
[Stopped] BIT NOT NULL DEFAULT 1,
CONSTRAINT FK_USER_EMP
FOREIGN KEY (EMPId) REFERENCES Emp(id),

)

CREATE TABLE [joinUserGroup](
UserId int NOT Null,
FOREIGN KEY (UserId) REFERENCES [User](id),
GroupId int not null,
FOREIGN KEY (GroupId) REFERENCES [Group](id),

PRIMARY KEY (UserId, GroupId)
)