declare @CountyId int
declare @CountyName varchar(50)
declare @ProvinceId int

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