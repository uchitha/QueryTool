
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/13/2012 22:56:52
-- Generated from EDMX file: C:\WorkDir\qt\QueryToolWpf\Qt.Entity\QtModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [qt];
GO
IF SCHEMA_ID(N'qt') IS NULL EXECUTE(N'CREATE SCHEMA [qt]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[qt].[FK_QueryDatabaseType]', 'F') IS NOT NULL
    ALTER TABLE [qt].[Queries] DROP CONSTRAINT [FK_QueryDatabaseType];
GO
IF OBJECT_ID(N'[qt].[FK_QueryGroup]', 'F') IS NOT NULL
    ALTER TABLE [qt].[Queries] DROP CONSTRAINT [FK_QueryGroup];
GO
IF OBJECT_ID(N'[qt].[FK_OwnerQuery]', 'F') IS NOT NULL
    ALTER TABLE [qt].[Queries] DROP CONSTRAINT [FK_OwnerQuery];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[qt].[Queries]', 'U') IS NOT NULL
    DROP TABLE [qt].[Queries];
GO
IF OBJECT_ID(N'[qt].[DatabaseTypes]', 'U') IS NOT NULL
    DROP TABLE [qt].[DatabaseTypes];
GO
IF OBJECT_ID(N'[qt].[Users]', 'U') IS NOT NULL
    DROP TABLE [qt].[Users];
GO
IF OBJECT_ID(N'[qt].[Groups]', 'U') IS NOT NULL
    DROP TABLE [qt].[Groups];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Queries'
CREATE TABLE [qt].[Queries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ParameterCount] int  NULL,
    [DisplayOnHomeScreen] bit  NOT NULL,
    [Broken] bit  NULL,
    [Published] bit  NULL,
    [WrapColumns] bit  NULL,
    [DatabaseTypeId] int  NOT NULL,
    [GroupId] int  NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'DatabaseTypes'
CREATE TABLE [qt].[DatabaseTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [qt].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [qt].[Groups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DisplayOrder] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Queries'
ALTER TABLE [qt].[Queries]
ADD CONSTRAINT [PK_Queries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DatabaseTypes'
ALTER TABLE [qt].[DatabaseTypes]
ADD CONSTRAINT [PK_DatabaseTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [qt].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Groups'
ALTER TABLE [qt].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DatabaseTypeId] in table 'Queries'
ALTER TABLE [qt].[Queries]
ADD CONSTRAINT [FK_QueryDatabaseType]
    FOREIGN KEY ([DatabaseTypeId])
    REFERENCES [qt].[DatabaseTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QueryDatabaseType'
CREATE INDEX [IX_FK_QueryDatabaseType]
ON [qt].[Queries]
    ([DatabaseTypeId]);
GO

-- Creating foreign key on [GroupId] in table 'Queries'
ALTER TABLE [qt].[Queries]
ADD CONSTRAINT [FK_QueryGroup]
    FOREIGN KEY ([GroupId])
    REFERENCES [qt].[Groups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QueryGroup'
CREATE INDEX [IX_FK_QueryGroup]
ON [qt].[Queries]
    ([GroupId]);
GO

-- Creating foreign key on [UserId] in table 'Queries'
ALTER TABLE [qt].[Queries]
ADD CONSTRAINT [FK_OwnerQuery]
    FOREIGN KEY ([UserId])
    REFERENCES [qt].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnerQuery'
CREATE INDEX [IX_FK_OwnerQuery]
ON [qt].[Queries]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------