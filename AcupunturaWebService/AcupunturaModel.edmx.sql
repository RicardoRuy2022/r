
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/22/2014 16:52:20
-- Generated from EDMX file: E:\Diogo\Faculdade\I.S\AcupunturaWebService\AcupunturaWebService\AcupunturaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbf32984ba147740e780c7a409010da49a];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UtilizadorSet'
CREATE TABLE [dbo].[UtilizadorSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [isAdmin] bit  NOT NULL
);
GO

-- Creating table 'TerapeutaSet'
CREATE TABLE [dbo].[TerapeutaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(max)  NOT NULL,
    [bi] int  NOT NULL,
    [data_nascimento] datetime  NOT NULL,
    [Utilizador_Id] int  NOT NULL
);
GO

-- Creating table 'PacienteSet'
CREATE TABLE [dbo].[PacienteSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(max)  NOT NULL,
    [bi] int  NOT NULL,
    [data_nascimento] datetime  NOT NULL,
    [Terapeuta_Id] int  NOT NULL
);
GO

-- Creating table 'SintomaSet'
CREATE TABLE [dbo].[SintomaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nome] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ConsultaSet'
CREATE TABLE [dbo].[ConsultaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [data_consulta] nvarchar(max)  NOT NULL,
    [diagnostico] nvarchar(max)  NOT NULL,
    [Paciente_Id] int  NOT NULL
);
GO

-- Creating table 'SintomaConsulta'
CREATE TABLE [dbo].[SintomaConsulta] (
    [Sintoma_Id] int  NOT NULL,
    [Consulta_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UtilizadorSet'
ALTER TABLE [dbo].[UtilizadorSet]
ADD CONSTRAINT [PK_UtilizadorSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TerapeutaSet'
ALTER TABLE [dbo].[TerapeutaSet]
ADD CONSTRAINT [PK_TerapeutaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PacienteSet'
ALTER TABLE [dbo].[PacienteSet]
ADD CONSTRAINT [PK_PacienteSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SintomaSet'
ALTER TABLE [dbo].[SintomaSet]
ADD CONSTRAINT [PK_SintomaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConsultaSet'
ALTER TABLE [dbo].[ConsultaSet]
ADD CONSTRAINT [PK_ConsultaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Sintoma_Id], [Consulta_Id] in table 'SintomaConsulta'
ALTER TABLE [dbo].[SintomaConsulta]
ADD CONSTRAINT [PK_SintomaConsulta]
    PRIMARY KEY NONCLUSTERED ([Sintoma_Id], [Consulta_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Utilizador_Id] in table 'TerapeutaSet'
ALTER TABLE [dbo].[TerapeutaSet]
ADD CONSTRAINT [FK_UtilizadorTerapeuta]
    FOREIGN KEY ([Utilizador_Id])
    REFERENCES [dbo].[UtilizadorSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UtilizadorTerapeuta'
CREATE INDEX [IX_FK_UtilizadorTerapeuta]
ON [dbo].[TerapeutaSet]
    ([Utilizador_Id]);
GO

-- Creating foreign key on [Terapeuta_Id] in table 'PacienteSet'
ALTER TABLE [dbo].[PacienteSet]
ADD CONSTRAINT [FK_TerapeutaPaciente]
    FOREIGN KEY ([Terapeuta_Id])
    REFERENCES [dbo].[TerapeutaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TerapeutaPaciente'
CREATE INDEX [IX_FK_TerapeutaPaciente]
ON [dbo].[PacienteSet]
    ([Terapeuta_Id]);
GO

-- Creating foreign key on [Paciente_Id] in table 'ConsultaSet'
ALTER TABLE [dbo].[ConsultaSet]
ADD CONSTRAINT [FK_PacienteConsulta]
    FOREIGN KEY ([Paciente_Id])
    REFERENCES [dbo].[PacienteSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PacienteConsulta'
CREATE INDEX [IX_FK_PacienteConsulta]
ON [dbo].[ConsultaSet]
    ([Paciente_Id]);
GO

-- Creating foreign key on [Sintoma_Id] in table 'SintomaConsulta'
ALTER TABLE [dbo].[SintomaConsulta]
ADD CONSTRAINT [FK_SintomaConsulta_Sintoma]
    FOREIGN KEY ([Sintoma_Id])
    REFERENCES [dbo].[SintomaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Consulta_Id] in table 'SintomaConsulta'
ALTER TABLE [dbo].[SintomaConsulta]
ADD CONSTRAINT [FK_SintomaConsulta_Consulta]
    FOREIGN KEY ([Consulta_Id])
    REFERENCES [dbo].[ConsultaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SintomaConsulta_Consulta'
CREATE INDEX [IX_FK_SintomaConsulta_Consulta]
ON [dbo].[SintomaConsulta]
    ([Consulta_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------