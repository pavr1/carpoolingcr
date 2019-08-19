declare @CountyId int
declare @CountyName varchar(50)
declare @ProvinceId int

set @CountyName = 'Heredia'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Heredia', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mercedes', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Francisco', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Ulloa', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Vara Blanca', @CountyId)

set @CountyName = 'Barva'
select @CountyId = CountyId from Counties where name = @CountyName
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Barva', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pedro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pablo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Roque', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Lucía', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('SJ. de la Montaña', @CountyId)

set @CountyName = 'Santo Domingo'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santo Domingo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Vicente', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Miguel', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Paracito', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santo Tomás', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Rosa', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tures', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pará', @CountyId)

set @CountyName = 'Santa Bárbara'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Bárbara', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pedro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Jesús', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santo Domingo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Purabá', @CountyId)

set @CountyName = 'San Rafael'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Josecito', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santiago', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Ángeles', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Concepción', @CountyId)

set @CountyName = 'San Isidro'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San José', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Concepción', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Francisco', @CountyId)

set @CountyName = 'Belén'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Antonio', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Ribera', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Asunción', @CountyId)

set @CountyName = 'Flores'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Joaquín', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Barrantes', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Llorente', @CountyId)

set @CountyName = 'San Pablo'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pablo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Rincón Sabanilla', @CountyId)

set @CountyName = 'Sarapiquí'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Puerto Viejo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Virgen', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Horquetas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Ll. del Gaspar', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cureña', @CountyId)