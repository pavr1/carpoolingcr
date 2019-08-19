declare @CountyId int
declare @CountyName varchar(50)
declare @ProvinceId int

set @CountyName = 'Liberia'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Liberia', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cañas Dulces', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mayorga', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Nacascolo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Curubandé', @CountyId)

set @CountyName = 'Nicoya'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Nicoya', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mansión', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Antonio', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Quebrada Honda', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sámara', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Nosara', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Belén', @CountyId)

set @CountyName = 'Santa Cruz'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Cruz', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Bolsón', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('27 de Abril', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tempate', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cartagena', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cuajiniquil', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Diriá', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cabo Velas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tamarindo', @CountyId)

set @CountyName = 'Bagaces'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Bagaces', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Fortuna', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mogote', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Río Naranjo', @CountyId)

set @CountyName = 'Carrillo'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Filadelfia', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Palmira', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sardinal', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Belén', @CountyId)

set @CountyName = 'Cañas'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cañas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Palmira', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Miguel', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Bebedero', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Porozal', @CountyId)

set @CountyName = 'Abangares'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Las Juntas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sierra', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Colorado', @CountyId)

set @CountyName = 'Tilarán'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tilarán', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Quebrada Grande', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tronadora', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Rosa', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Líbano', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tierras Morenas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Arenal', @CountyId)

set @CountyName = 'Nandayure'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Carmona', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Rita', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Zapotal', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pablo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Porvenir', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Bejuco', @CountyId)

set @CountyName = 'La Cruz'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Cruz', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Cecilia', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Garita', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Elena', @CountyId)

set @CountyName = 'Hojancha'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Hojancha', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Monte Romo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Puerto Carrillo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Huacas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Matambú', @CountyId)