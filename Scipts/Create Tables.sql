CREATE TABLE [Users] (
  [Id] int not null PRIMARY KEY IDENTITY(1,1),
  [PhoneNum] nvarchar(12) not null,
  [Name] NvarChar(100) Not Null,
  [Address] nvarchar(250) not null,
  [Username] NvarChar(100) not null,
  [Password] Nvarchar(100) not null
);
go
CREATE TABLE [Dogs] (
  [Id] int not null PRIMARY KEY IDENTITY(1,1),
  [OwnerId] int FOREIGN KEY REFERENCES Users(Id),
  [Name] NvarChar(100),
  [Breed] NVARCHAR(100),
  [ReqDistance] INT not null,
  [WalkingTime] INT not null,
  [SpecialRequests] NVARCHAR

);
-- ALTER TABLE dbo.Walks Drop column WalkerName
-- ALTER TABLE dbo.Walks ADD column WalkerId int Foreign Key References Users(id)
GO
CREATE TABLE [Walks] (
  [Id] int not null PRIMARY KEY IDENTITY(1,1),
  [DogId] int FOREIGN KEY REFERENCES Dogs(Id),
  [WalkerId] int Foreign KEy REFERENCES Users(Id),
  [DistanceWalked] Float not null,
  [Lat] Float,
  [Long] Float,
  [OutsideTemp] Int,
  [WalkStarted] Datetime2,
  [WalkEnded] Datetime2
);
go
CREATE TABLE [Ratings] (
  [Id] int not null PRIMARY KEY IDENTITY(1,1),
  [WalkId] int FOREIGN KEY REFERENCES Walks(Id),
  [OwnerId] int Foreign Key References User(Id),
  [Score] Float not null,
  [Comment] NVarChar(1000),
  [WalkerId] int FOREIGN KEY REFERENCES Users(Id)
);