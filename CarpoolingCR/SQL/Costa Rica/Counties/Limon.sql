declare @ProvinceName varchar(50)
declare @ProvinceId int

set @ProvinceName = 'Limón'

select @ProvinceId = ProvinceId from provinces where name = @ProvinceName

INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Limón',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Pococí',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Siquirres',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Talamanca',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Matina',@ProvinceId)
INSERT INTO [dbo].[Counties] ([Name],[ProvinceId]) VALUES ('Guácimo',@ProvinceId)