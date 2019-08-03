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

