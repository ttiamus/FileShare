CREATE TABLE [dbo].[DirectoryFiles]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FileName] VARCHAR(50) NOT NULL, 
    [Size] INT NOT NULL, 
    [MimeType] VARCHAR(50) NOT NULL, 
    [DateAdded] DATE NOT NULL, 
    [UploadedBy] VARCHAR(20) NOT NULL
)
