declare @CountyId int
declare @CountyName varchar(50)
declare @ProvinceId int

set @CountyName = 'Cartago'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Oriental', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Occidental', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Carmen', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Nicolás', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Agua Caliente', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guadalupe', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Corralillo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tierra Blanca', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Dulce Nombre', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Llano Grande', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Quebradilla', @CountyId)

set @CountyName = 'Paraíso'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Paraíso', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santiago', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Orosi', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cachí', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Ll. Santa Lucía', @CountyId)

set @CountyName = 'La Unión'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tres Ríos', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Diego', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Concepción', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Dulce Nombre', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Ramón', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Río Azul', @CountyId)

set @CountyName = 'Jiménez'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Juan Viñas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tucurrique', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pejibaye', @CountyId)

set @CountyName = 'Turrialba'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Turrialba', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Suiza', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Peralta', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Cruz', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Teresita', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pavones', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tuis', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tayutic', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Rosa', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tres Equis', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('	La Isabel', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Chirripó', @CountyId)

set @CountyName = 'Alvarado'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pacayas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cervantes', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Capellades', @CountyId)

set @CountyName = 'Oreamuno'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cot', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Potrero Cerrado', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cipreses', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Rosa', @CountyId)

set @CountyName = 'El Guarco'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('El Tejar', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tobosi', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Patio de Agua', @CountyId)