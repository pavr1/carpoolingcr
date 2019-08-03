declare @CountyId int
declare @CountyName varchar(50)
declare @ProvinceId int

set @CountyName = 'Alajuela'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Alajuela', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San José', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Carrizal', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Antonio', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guácima', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sabanilla', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Río Segundo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Desamparados', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Turrúcares', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tambor', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Garita', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sarapiquí', @CountyId)

set @CountyName = 'San Ramón'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Ramón', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santiago', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Piedades Norte', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Piedades Sur', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Ángeles', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Alfaro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Volio', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Concepción', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Zapotal', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Peñas Blancas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Lorenzo4​', @CountyId)

set @CountyName = 'Grecia'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Grecia', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San José', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Roque', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tacares', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Puente de Piedra', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Bolívar', @CountyId)

set @CountyName = 'San Mateo'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Mateo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Desmonte', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Jesús María', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Labrador', @CountyId)

set @CountyName = 'Atenas'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Atenas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Jesús', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mercedes', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Concepción', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San José', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Eulalia', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Escobal', @CountyId)

set @CountyName = 'Naranjo'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Naranjo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Miguel', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San José', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cirrí', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Jerónimo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('El Rosario', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Palmitos', @CountyId)

set @CountyName = 'Palmares'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Palmares', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Zaragoza', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Buenos Aires', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santiago', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Candelaria', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Esquipulas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Granja', @CountyId)

set @CountyName = 'Poás'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pedro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Carrillos', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sabana Redonda', @CountyId)

set @CountyName = 'Orotina'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Orotina', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mastate', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Hacienda Vieja', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Coyolar', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Ceiba', @CountyId)

set @CountyName = 'San Carlos'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Quesada', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Florencia', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Buenavista', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Aguas Zarcas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Venecia', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pital', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Fortuna', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Tigra', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Palmera', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Venado', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cutris', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Monterrey', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pocosol', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pocosol', @CountyId)

set @CountyName = 'Zarcero'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Zarcero', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Laguna', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tapezco', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guadalupe', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Palmira', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Zapote', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Brisas', @CountyId)

set @CountyName = 'Sarchí'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sarchí Norte', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sarchí Sur', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Toro Amarillo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pedro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Rodríguez', @CountyId)

set @CountyName = 'Upala'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Upala', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Aguas Claras', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San José', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Bijagua', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Delicias', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Dos Ríos', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Yolillal', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Canalete', @CountyId)

set @CountyName = 'Los Chiles'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Los Chiles', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Caño Negro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('El Amparo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Jorge', @CountyId)

set @CountyName = 'Guatuso'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Buenavista', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cote', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Katira', @CountyId)

set @CountyName = 'Rio Cuarto'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Río Cuarto', @CountyId)