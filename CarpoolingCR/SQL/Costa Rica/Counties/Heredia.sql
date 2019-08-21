declare @ProvinceName varchar(50)
declare @ProvinceId int

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