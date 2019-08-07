declare @CountyId int
declare @CountyName varchar(50)
declare @ProvinceId int

set @CountyName = 'Puntarenas'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Puntarenas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pitahaya', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Chomes', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Lepanto', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Paquera', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Manzanillo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guacimal', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Barranca', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Monteverde', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cóbano', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Chacarita', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Chira', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Acapulco', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('El Roble', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Arancibia', @CountyId)

set @CountyName = 'Esparza'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Espíritu Santo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan G.', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Macacona', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Jerónimo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Caldera', @CountyId)

set @CountyName = 'Buenos Aires'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Buenos Aires', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Volcán', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Potrero Grande', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Boruca', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pilas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Colinas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Chánguena', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Biolley', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Brunka', @CountyId)

set @CountyName = 'Montes de Oro'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Miramar', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Unión', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro', @CountyId)

set @CountyName = 'Osa'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Ciudad Cortés', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Palmar', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sierpe', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Bahía Ballena', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Piedras Blancas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Bahía Drake', @CountyId)

set @CountyName = 'Quepos'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Quepos', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Savegre', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Naranjito', @CountyId)

set @CountyName = 'Golfito'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Golfito', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Puerto Jiménez', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guaycará', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pavón', @CountyId)

set @CountyName = 'Coto Brus'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Vito', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sabalito', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Aguabuena', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Limoncito', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pittier', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Gutiérrez Brown', @CountyId)

set @CountyName = 'Parrita'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Parrita', @CountyId)

set @CountyName = 'Corredores'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Corredor', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Cuesta', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Paso Canoas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Laurel', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Jacó', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tárcoles', @CountyId)