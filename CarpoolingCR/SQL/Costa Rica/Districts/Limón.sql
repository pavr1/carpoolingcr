declare @CountyId int
declare @CountyName varchar(50)
declare @ProvinceId int

set @CountyName = 'Limón'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Limón', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Valle La Estrella', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Río Blanco', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Matama', @CountyId)

set @CountyName = 'Pococí'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guápiles', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Jiménez', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Rita', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Roxana', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cariari', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Colorado', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Colonia', @CountyId)

set @CountyName = 'Siquirres'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Siquirres', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pacuarito', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Florida', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Germania', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cairo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Alegría', @CountyId)

set @CountyName = 'Talamanca'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Bratsi', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sixaola', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cahuita', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Telire', @CountyId)

set @CountyName = 'Matina'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Matina', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Batán', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Carrandi', @CountyId)

set @CountyName = 'Guácimo'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guácimo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mercedes', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pocora', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Río Jiménez', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Duacarí', @CountyId)