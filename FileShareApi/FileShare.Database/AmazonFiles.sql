CREATE TABLE [dbo].[AmazonFiles]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FileKey] VARCHAR(MAX) NOT NULL, 
    [FileName] VARCHAR(50) NOT NULL, 
    [Size] INT NOT NULL, 
    [MimeType] VARCHAR(50) NOT NULL, 
    [DateAdded] DATE NOT NULL, 
    [UploadedBy] VARCHAR(20) NOT NULL
)
