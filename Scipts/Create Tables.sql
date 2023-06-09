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
  [Walking time] INT not null,
  [SpecialRequests] NVARCHAR
);
GO
CREATE TABLE [Walks] (
  [Id] int not null PRIMARY KEY IDENTITY(1,1),
  [DogId] int FOREIGN KEY REFERENCES Dogs(Id),
  [DistanceWalked] Float not null,
  [Lat] Float,
  [Long] Float,
  [Walker name] NvarChar(100),
  [OutsideTemp] Int,
  [WalkStarted] Datetime not null,
  [WalkEnded] Datetime
);
go
CREATE TABLE [Ratings] (
  [Id] int not null PRIMARY KEY IDENTITY(1,1),
  [WalkId] int FOREIGN KEY REFERENCES Walks(Id),
  [Score] Float not null,
  [Comment] NVarChar(1000)
);