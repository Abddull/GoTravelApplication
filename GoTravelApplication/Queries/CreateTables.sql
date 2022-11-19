CREATE DATABASE GoTravel;
GO

USE GoTravel;
GO

CREATE TABLE dbo.Customers(
	CustomerId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	UserName varchar(24) NOT NULL UNIQUE,
	Password varchar(24) NOT NULL);
GO

CREATE TABLE dbo.Receptionists(
	ReceptionistId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	UserName varchar(24) NOT NULL UNIQUE,
	Password varchar(24) NOT NULL);
GO

CREATE TABLE dbo.Moderators(
	ModeratorId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	UserName varchar(24) NOT NULL UNIQUE,
	Password varchar(24) NOT NULL);
GO

CREATE TABLE dbo.Administrators(
	AdminId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	UserName varchar(24) NOT NULL UNIQUE,
	Password varchar(24) NOT NULL);
GO

CREATE TABLE dbo.Bookings(
	BookingId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	Title varchar(240) NOT NULL UNIQUE,
	Description varchar(240) NOT NULL,
	Price float NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL);
GO

CREATE TABLE dbo.Pictures(
	PictureId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	FileName varchar(240) NOT NULL,
	BookingId int NOT NULL,
	CONSTRAINT FK_PicBookingId FOREIGN KEY (BookingId) REFERENCES dbo.Bookings(BookingId));
GO

CREATE TABLE dbo.CustomerBookings(
	CustomerBookingId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	PurchaseDate DATE NOT NULL,
	Status varchar(50) NOT NULL,
	BookingId int NOT NULL,
	CustomerId int NOT NULL,
	CONSTRAINT FK_CustBookCustomerId FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(CustomerId),
	CONSTRAINT FK_CustBookBookingId FOREIGN KEY (BookingId) REFERENCES dbo.Bookings(BookingId));
GO

CREATE TABLE dbo.CartBooking(
	CartId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	BookingId int NOT NULL,
	CustomerId int NOT NULL,
	CONSTRAINT FK_CartCustomerId FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(CustomerId),
	CONSTRAINT FK_CartBookingId FOREIGN KEY (BookingId) REFERENCES dbo.Bookings(BookingId));
GO

CREATE TABLE dbo.CustomerReviews(
	Id int PRIMARY KEY NOT NULL IDENTITY(1,1),
	Rating int NOT NULL,
	Description varchar(240) NOT NULL,
	CustomerId int NOT NULL,
	CONSTRAINT FK_CustReviewCustomerId FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(CustomerId));
GO

CREATE TABLE dbo.ModeratorReviews(
	Id int PRIMARY KEY NOT NULL IDENTITY(1,1),
	Rating int NOT NULL,
	Description varchar(240) NOT NULL,
	ModeratorId int NOT NULL,
	CONSTRAINT FK_ModReviewModeratorId FOREIGN KEY (ModeratorId) REFERENCES dbo.Moderators(ModeratorId));
GO

CREATE TABLE dbo.ReceptionistChanges(
	ChangeId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	OldStatus varchar(50) NOT NULL,
	NewStatus varchar(50) NOT NULL,
	ChangeTime DATETIME NOT NULL,
	ReceptionistId int NOT NULL,
	CustomerBookingId int NOT NULL,
	CONSTRAINT FK_ChangeReceptionistId FOREIGN KEY (ReceptionistId) REFERENCES dbo.Receptionists(ReceptionistId),
	CONSTRAINT FK_ChangeCustomerBookingId FOREIGN KEY (CustomerBookingId) REFERENCES dbo.CustomerBookings(CustomerBookingId));
GO

CREATE TABLE dbo.ModRequests(
	RequestId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	Title varchar(50) NOT NULL,
	Description varchar(500) NOT NULL,
	Status varchar(50) NOT NULL,
	RequestTime DATETIME NOT NULL,
	ModeratorId int NOT NULL,
	CONSTRAINT RequestModeratorId FOREIGN KEY (ModeratorId) REFERENCES dbo.Moderators(ModeratorId));
GO

CREATE TABLE dbo.AdminResponse(
	ResponseId int PRIMARY KEY NOT NULL IDENTITY(1,1),
	Title varchar(50) NOT NULL,
	Description varchar(500) NOT NULL,
	ResponseTime DATETIME NOT NULL,
	ModeratorId int NOT NULL,
	AdminId int NOT NULL,
	CONSTRAINT ResponseModeratorId FOREIGN KEY (ModeratorId) REFERENCES dbo.Moderators(ModeratorId),
	CONSTRAINT ResponseAdminId FOREIGN KEY (AdminId) REFERENCES dbo.Administrators(AdminId));
GO