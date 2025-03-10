﻿CREATE TABLE [dbo].[Users] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (255)  NOT NULL,
    [UserName]         NVARCHAR (255)  NOT NULL,
    [Surname]      NVARCHAR (100)  NOT NULL,
    [FatherName]   NVARCHAR (100)  NULL,
    [Email]        NVARCHAR (70)   NOT NULL,
    [PasswordHash] NVARCHAR (1000) NOT NULL,
    [Address]      NVARCHAR (255)  NULL,
    [MobilePhone]        NVARCHAR (50)   NOT NULL,
    [CardNumber]   NVARCHAR (50)   NULL,
    [TableNumber] NVARCHAR(50) NULL,
    [Birthdate]   DATE   NULL,
    [DateOfEmployment] DATE  NULL,
    [DateOfDissmissal] DATE NULL,
    [Note] NVARCHAR(MAX) NULL,
    [Gender] INT NOT NULL Default 1,
    [UserType] INT Default 2,
    [UpdatedBy]    INT             NULL,
    [CreatedBy]    INT             NULL,
    [DeletedBy]    INT             NULL,
    [CreatedDate]  DATETIME2 (7)   Default GETDATE(),
    [DeletedDate]  DATETIME2 (7)   NULL,
    [UpdatedDate]  DATETIME2 (7)   NULL,
    [IsDeleted]    BIT             Default 0
);