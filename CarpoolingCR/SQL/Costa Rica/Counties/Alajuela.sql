declare @ProvinceName varchar(50)
declare @ProvinceId int

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
