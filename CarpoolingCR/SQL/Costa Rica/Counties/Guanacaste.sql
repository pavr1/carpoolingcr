declare @ProvinceName varchar(50)
declare @ProvinceId int

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