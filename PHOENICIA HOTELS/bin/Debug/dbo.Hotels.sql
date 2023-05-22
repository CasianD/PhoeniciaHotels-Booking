CREATE TABLE [dbo].[Hotels] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (350)  NOT NULL,
    [Description] VARCHAR (2000) NOT NULL,
    [City]        VARCHAR (350)  NOT NULL,
    [Stars]       INT            NOT NULL,
    [Reviews]     INT            NOT NULL,
    [Parking]     INT            NOT NULL,
    [Breakfast]   INT            NOT NULL,
    [Seaview]     INT            NOT NULL,
    [Cityview]    INT            NOT NULL,
    [Picture] VARCHAR(300) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

