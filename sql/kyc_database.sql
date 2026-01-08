-- Active: 1767419929582@@127.0.0.1@3306@mysql
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'kyc.Database')
BEGIN
    CREATE DATABASE [kyc.Database];
END
GO

USE [kyc.Database];
GO

SET NOCOUNT ON;

-- Provinces
CREATE TABLE dbo.Province (
    ProvinceId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProvinceName NVARCHAR(200) NOT NULL
);

-- Districts
CREATE TABLE dbo.District (
    DistrictId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    DistrictName NVARCHAR(200) NOT NULL,
    ProvinceId INT NOT NULL,
    CONSTRAINT FK_District_Province FOREIGN KEY (ProvinceId) REFERENCES dbo.Province (ProvinceId) ON DELETE NO ACTION
);

CREATE INDEX IX_District_ProvinceId ON dbo.District (ProvinceId);

-- Municipalities
CREATE TABLE dbo.Municipality (
    MunicipalityId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    MunicipalityName NVARCHAR(200) NOT NULL,
    DistrictId INT NOT NULL,
    CONSTRAINT FK_Municipality_District FOREIGN KEY (DistrictId) REFERENCES dbo.District (DistrictId) ON DELETE NO ACTION
);

CREATE INDEX IX_Municipality_DistrictId ON dbo.Municipality (DistrictId);

-- Wards
CREATE TABLE dbo.Ward (
    WardId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    WardNo INT NOT NULL,
    MunicipalityId INT NOT NULL,
    CONSTRAINT FK_Ward_Municipality FOREIGN KEY (MunicipalityId) REFERENCES dbo.Municipality (MunicipalityId) ON DELETE NO ACTION
);

-- Ensure ward numbers are unique within a municipality
CREATE UNIQUE INDEX UX_Ward_Municipality_WardNo ON dbo.Ward (MunicipalityId, WardNo);

-- KYC records
CREATE TABLE dbo.KycRecord (
    KycId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    FullName NVARCHAR(200) NOT NULL,
    PhoneNo NVARCHAR(50) NULL,
    Email NVARCHAR(200) NULL,
    DateOfBirth DATE NULL,
    ProvinceId INT NULL,
    DistrictId INT NULL,
    MunicipalityId INT NULL,
    WardId INT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT (SYSUTCDATETIME()),

    CONSTRAINT FK_KycRecord_Province FOREIGN KEY (ProvinceId) REFERENCES dbo.Province (ProvinceId) ON DELETE NO ACTION,
    CONSTRAINT FK_KycRecord_District FOREIGN KEY (DistrictId) REFERENCES dbo.District (DistrictId) ON DELETE NO ACTION,
    CONSTRAINT FK_KycRecord_Municipality FOREIGN KEY (MunicipalityId) REFERENCES dbo.Municipality (MunicipalityId) ON DELETE NO ACTION,
    CONSTRAINT FK_KycRecord_Ward FOREIGN KEY (WardId) REFERENCES dbo.Ward (WardId) ON DELETE NO ACTION
);

CREATE INDEX IX_KycRecord_ProvinceId ON dbo.KycRecord (ProvinceId);
CREATE INDEX IX_KycRecord_DistrictId ON dbo.KycRecord (DistrictId);
CREATE INDEX IX_KycRecord_MunicipalityId ON dbo.KycRecord (MunicipalityId);
CREATE INDEX IX_KycRecord_WardId ON dbo.KycRecord (WardId);

-- Optional: Example seed data (uncomment and edit as needed)
INSERT INTO dbo.Province (ProvinceName) VALUES (N'Province A'),(N'Province B');
INSERT INTO dbo.District (DistrictName, ProvinceId) VALUES (N'District 1', 1);
INSERT INTO dbo.Municipality (MunicipalityName, DistrictId) VALUES (N'Municipality X', 1);
INSERT INTO dbo.Ward (WardNo, MunicipalityId) VALUES (1, 1);
INSERT INTO dbo.KycRecord (FullName, PhoneNo, Email, DateOfBirth, ProvinceId, DistrictId, MunicipalityId, WardId) VALUES
(N'Jane Doe', '555-0100', 'jane@example.com', '1990-01-01', 1, 1, 1, 1);

PRINT 'Database and tables created (if not exists).';
GO
