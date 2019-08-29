declare @countryName varchar(20)
declare @countryId  int

SET @countryName = 'Costa Rica'
SELECT @countryId = CountryId  from Countries WHERE Name = @countryName

INSERT INTO [dbo].[Provinces]([Name],[CountryId]) VALUES ('San José',@countryId  )
INSERT INTO [dbo].[Provinces]([Name],[CountryId]) VALUES ('Alajuela',@countryId  )
INSERT INTO [dbo].[Provinces]([Name],[CountryId]) VALUES ('Heredia',@countryId  )
INSERT INTO [dbo].[Provinces]([Name],[CountryId]) VALUES ('Cartago',@countryId  )
INSERT INTO [dbo].[Provinces]([Name],[CountryId]) VALUES ('Puntarenas',@countryId  )
INSERT INTO [dbo].[Provinces]([Name],[CountryId]) VALUES ('Guanacaste',@countryId  )
INSERT INTO [dbo].[Provinces]([Name],[CountryId]) VALUES ('Limón',@countryId  )

--------------------------------------------------------------------------------------------------------------

declare @ProvinceName varchar(50)
declare @ProvinceId int

set @ProvinceName = 'San José'

select @ProvinceId = ProvinceId from provinces where name = @ProvinceName

INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('San José',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Escazú',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Desamparados',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Puriscal',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Tarrazú',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Aserrí',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Mora',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Goicoechea',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Santa Ana',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Alajuelita',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Coronado',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Acosta',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Tibás',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Moravia',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Montes de Oca',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Turrubares',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Dota',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Curridabat',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Pérez Zeledón',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('León Cortés',@ProvinceId)

--------------------------------------------------------------------------------------------------------------
set @ProvinceName = 'Alajuela'

select @ProvinceId = ProvinceId from provinces where name = @ProvinceName

INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Alajuela',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('San Ramón',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Grecia',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('San Mateo',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Atenas',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Naranjo',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Palmares',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Poás',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Orotina',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('San Carlos',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Zarcero',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Sarchí',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Upala',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Los Chiles',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Guatuso',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Río Cuarto',@ProvinceId)

--------------------------------------------------------------------------------------------------------------
set @ProvinceName = 'Heredia'

select @ProvinceId = ProvinceId from provinces where name = @ProvinceName

INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Heredia',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Barva',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Santo Domingo',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Santa Bárbara',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('San Rafael',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('San Isidro',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Belén',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Flores',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('San Pablo',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Sarapiquí',@ProvinceId)
--------------------------------------------------------------------------------------------------------------
set @ProvinceName = 'Cartago'

select @ProvinceId = ProvinceId from provinces where name = @ProvinceName

INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Cartago',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Paraíso',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('La Unión',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Jiménez',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Turrialba',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Alvarado',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Oreamuno',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('El Guarco',@ProvinceId)
--------------------------------------------------------------------------------------------------------------
set @ProvinceName = 'Puntarenas'

select @ProvinceId = ProvinceId from provinces where name = @ProvinceName

INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Puntarenas',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Esparza',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Buenos Aires',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Osa',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Quepos',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Coto Brus',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Parrita',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Corredores',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Garabito',@ProvinceId)
--------------------------------------------------------------------------------------------------------------
set @ProvinceName = 'Guanacaste'

select @ProvinceId = ProvinceId from provinces where name = @ProvinceName

INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Liberia',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Nicoya',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Santa Cruz',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Bagaces',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Carrillo',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Cañas',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Abangares',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Tilarán',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Nandayure',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('La Cruz',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Hojancha',@ProvinceId)
--------------------------------------------------------------------------------------------------------------
set @ProvinceName = 'Limón'

select @ProvinceId = ProvinceId from provinces where name = @ProvinceName

INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Limón',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Pococí',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Siquirres',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Talamanca',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Matina',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Guácimo',@ProvinceId)
--------------------------------------------------------------------------------------------------------------
declare @CountyId int
declare @CountyName varchar(50)

set @CountyName = 'San José'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Carmen', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Merced', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Hospital', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Catedral', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Zapote', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Fco de Dos Ríos', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Uruca', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mata Redonda', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pavas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Hatillo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Sebastián', @CountyId)

set @CountyName = 'Escazú'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Escazú', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Antonio', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)


set @CountyName = 'Desamparados'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Desamparados', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Miguel', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan de Dios', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael Arriba', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Antonio', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Frailes', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Patarrá', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Cristóbal', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Rosario', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Damas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael Abajo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Gravilias', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Los Guido', @CountyId)


set @CountyName = 'Puriscal'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santiago', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mercedes Sur', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Barbacoas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Grifo Alto', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Candelarita', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Desamparaditos', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Antonio', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Chires', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Cangreja', @CountyId)

set @CountyName = '	Tarrazú'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Marcos', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Lorenzo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Carlos', @CountyId)

set @CountyName = 'Aserrí'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Aserrí', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tarbaca', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Vuelta de Jorco', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Gabriel', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Legua', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Monterrey', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Salitrillos', @CountyId)

set @CountyName = 'Mora'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Ciudad Colón', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guayabo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tabarcia', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Piedras Negras', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Picagres', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Jaris', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Quitirrisí', @CountyId)

set @CountyName = 'Goicoechea'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guadalupe', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Francisco', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Calle Blancos', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mata de Plátano', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Ipís', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Rancho Redondo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Purral', @CountyId)

set @CountyName = 'Santa Ana'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Ana', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Salitral', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pozos', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Uruca', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Piedades', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Brasil', @CountyId)

set @CountyName = 'Alajuelita'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Alajuelita', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Josecito', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Antonio', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Concepción', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Felipe', @CountyId)

set @CountyName = 'Coronado'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Dulce Nombre', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Patalillo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cascajal', @CountyId)

set @CountyName = 'Acosta'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Ignacio', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Guaitil', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Palmichal', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cangrejal', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sabanillas', @CountyId)

set @CountyName = 'Tibás'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cinco Esquinas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Anselmo llorente', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('León XIII', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Colima', @CountyId)

set @CountyName = 'Moravia'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Vicente', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Jerónimo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Trinidad', @CountyId)

set @CountyName = 'Montes de Oca'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pedro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sabanilla', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Mercedes', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Rafael', @CountyId)

set @CountyName = 'Turrubares'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pablo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pedro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Juan de Mata', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Luis', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Carara', @CountyId)

set @CountyName = 'Dota'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa María', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Jardín', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Copey', @CountyId)

set @CountyName = 'Curridabat'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Curridabat', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Granadilla', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Sánchez', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Tirrases', @CountyId)

set @CountyName = 'Pérez Zeledón'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro de El General', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('El General', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Daniel Flores', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Rivas', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pedro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Platanares', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Pejibaye', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Cajón', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Barú', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Río Nuevo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Páramo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('La Amistad', @CountyId)

set @CountyName = 'León Cortés'
select @CountyId = CountyId from Counties where name = @CountyName

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Pablo', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Andrés', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Llano Bonito', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Isidro', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Santa Cruz', @CountyId)
INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('San Antonio', @CountyId)
--------------------------------------------------------------------------------------------------------------
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

INSERT INTO [dbo].[Districts]([Name],[CountyId]) VALUES ('Ciudad Quesada', @CountyId)
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
--------------------------------------------------------------------------------------------------------------
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
--------------------------------------------------------------------------------------------------------------
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
--------------------------------------------------------------------------------------------------------------
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
--------------------------------------------------------------------------------------------------------------
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
--------------------------------------------------------------------------------------------------------------
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
--------------------------------------------------------------------------------------------------------------

