DECLARE @brandId int
DECLARE @brandName varchar(20)

SET @brandName = 'Seat'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Alhambra' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Altea' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Altea XL' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Arosa' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Cordoba' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Cordoba Vario' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Exeo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Ibiza' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Ibiza ST' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Exeo ST' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Leon' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Leon ST' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Inca' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Mii' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Toledo' ,@brandId)
END

SET @brandName = 'Peugeot'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('1007' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('106' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('107' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('108' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('2008' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('205' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('205 Cabrio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('206' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('206 CC' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('206 SW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('207' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('207 CC' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('207 SW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('306' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('307' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('307 CC' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('307 SW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('308' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('308 CC' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('308 SW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('309' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('4007' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('4008' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('405' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('406' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('407' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('407 SW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('5008' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('508' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('508 SW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('605' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('806' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('607' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('807' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Bipper' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('RCZ' ,@brandId)
END

SET @brandName = 'Toyota'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('4-Runner' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Auris' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Avensis' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Avensis Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Avensis Van Verso' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Aygo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Camry' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Carina' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Celica' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Corolla' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Corolla Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Corolla sedan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Corolla Verso' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('FJ Cruiser' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('GT86' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Hiace' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Hiace Van' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Highlander' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Hilux' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Land Cruiser' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('MR2' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Paseo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Picnic' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Prius' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('RAV4' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Sequoia' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Starlet' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Supra' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Tundra' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Urban Cruiser' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Verso' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Yaris' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Yaris Verso' ,@brandId)
END


SET @brandName = 'Suzuki'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Alto' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Baleno' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Baleno kombi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Grand Vitara' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Grand Vitara XL-7' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Ignis' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Jimny' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Kizashi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Liana' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Samurai' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Splash' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Swift' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('SX4' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('SX4 Sedan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Vitara' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Wagon R+' ,@brandId)
END

SET @brandName = 'Hyundai'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Accent' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Atos' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Atos Prime' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Elantra' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Galloper' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Genesis' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Getz' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Grandeur' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('H 350' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('H1' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('H1 Bus' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('H1 Van' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('H200' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('i10' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('i20' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('i30' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('i30 CW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('i40' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('i40 CW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('ix20' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('ix35' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('ix55' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Lantra' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Matrix' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Santa Fe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Sonata' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Terracan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Trajet' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Tucson' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Veloster' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Creta' ,@brandId)
END

SET @brandName = 'Chevrolet'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Alero' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Aveo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Camaro' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Captiva' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Corvette' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Cruze' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Cruze SW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Epica' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Equinox' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Evanda' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('HHR' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Kalos' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Lacetti' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Lacetti SW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Lumina' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Malibu' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Matiz' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Monte Carlo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Nubira' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Orlando' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Spark' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Suburban' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Tacuma' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Tahoe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Trax' ,@brandId)
END

SET @brandName = 'Mazda'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('121' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('2' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('323' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('323 Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('323 Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('323 F' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('5' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('6' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('6 Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('626' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('626 Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('B-Fighter' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('B2500' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('BT' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('CX-3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('CX-5' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('CX-7' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('CX-9' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Demio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('MPV' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('MX-3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('MX-5' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('MX-6' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Premacy' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('RX-7' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('RX-8' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Xedox 6' ,@brandId)
END

SET @brandName = 'Mitsubishi'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('3000 GT' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('ASX' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Carisma' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Colt' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Colt CC' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Eclipse' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Fuso canter' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Galant' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Galant Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Grandis' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('L200' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('L200 Pick up' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('L200 Pick up Allrad' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('L300' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Lancer' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Lancer Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Lancer Evo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Lancer Sportback' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Outlander' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pajero' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pajeto Pinin' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pajero Pinin Wagon' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pajero Sport' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pajero Wagon' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Space Star' ,@brandId)
END

DECLARE @brandId int
DECLARE @brandName varchar(20)

SET @brandName = 'Renault'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Captur' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Clio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Clio Grandtour' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Espace' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Express' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Fluence' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Grand Espace' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Grand Modus' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Grand Scenic' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Kadjar' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Kangoo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Kangoo Express' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Koleos' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Laguna' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Laguna Grandtour' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Latitude' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Mascott' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Mégane' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Mégane CC' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Mégane Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Mégane Grandtour' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Mégane Coupé' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Mégane Scénic' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Scénic' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Space Star' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Talisman' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Talisman Grandtour' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Thalia' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Twingo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Wind' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Zoé' ,@brandId)
END

DECLARE @brandId int
DECLARE @brandName varchar(20)

SET @brandName = 'Citroen'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Berlingo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C-Crosser' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C-Elissee' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C-Zero' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C1' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C2' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C3 Picasso' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C4' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C4 Aircross' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C4 Cactus' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C4 Coupé' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C4 Grand Picasso' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C4 Sedan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C5' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C5 Break' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C5 Tourer' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C6' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('C8' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('DS3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('DS4' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('DSS' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Evasion' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Jumper' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Jumpy' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Saxo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Nemo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Xantia' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Xsara' ,@brandId)
	
END

DECLARE @brandName varchar(20)

SET @brandName = 'Kia'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Avella' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Besta' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Carens' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Carnival' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Ceed' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Ceed SW' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Cerato' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('K 2500' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Magentis' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Opirus' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Optima' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Picanto' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pregio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pride' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pro Ceed' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rio Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rio sedan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Sephia' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Shuma' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Sorento' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Soul' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Sportage' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Venga' ,@brandId)
	
END

SET @brandName = 'Nissan'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('100 NX' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('200 SX' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('350 Z' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('350 Z Roadster' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('370 Z' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Almera' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Almera Tino' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('K 2500' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Cabstar E - T' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Cabstar TL2 Valnik' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('e-NV200' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('GT-R' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Insterstar' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Juke' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('King Cab' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Leaf' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Maxima' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Maxima QX' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Micra' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Murano' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Navara' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Note' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('NP300 Pickup' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('NV200' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('NV400' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pathfinder' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Patrol' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('NV200' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Patrol GR' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pickup' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pixo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Primastar' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Primastar Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Primera' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Primera Combi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Pulsar' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Qashqai' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Serena' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Sunny' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Terrano' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Tiida' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Trade' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Vanette Cargo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('X-Trail' ,@brandId)
	
END

SET @brandName = 'BMW'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('i3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('i8' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('M3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('M4' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('M5' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('M6' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 1' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 1 Cabrio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 1 Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 2' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 2 Active Tourer' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 2 Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 2 Gran Tourer' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 3 Cabrio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 3 Compact' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 3 Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 3 GT' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 3 Touring' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 4' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 4 Cabrio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 4 Gran Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 5' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 5 GT' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 5 Touring' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 6' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 6 Cabrio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 6 Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 6 Gran Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 7' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Rad 8 Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('X1' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('X3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('X4' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('X5' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('X6' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Z3' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Z3 Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Z3 Roadster' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Z4' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Z4 Roadster' ,@brandId)
END

SET @brandName = 'Jeep'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Cherokee' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Commander' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Compass' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Grand Cherokee' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Patriot' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Renegade' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Wrangler' ,@brandId)
END

SET @brandName = 'Subaru'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('BRZ' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Forester' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Impreza' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Impreza Wagon' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Justy' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Legacy' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Legacy Wagon' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Legacy Outback' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Levorg' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Outback' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('SVX' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Tribeca' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Tribeca B9' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('XV' ,@brandId)
END

SET @brandName = 'Honda'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Accord' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Accord Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Accord Tourer' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('City' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Civic' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Civic Aerodeck' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Civic Coupe' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Civic Tourer' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Civic Type R' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('CR-V' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('CR-X' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('CR-Z' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('FR-V' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('HR-V' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Insight' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Integra' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Jazz' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Legend' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Prelude' ,@brandId)
END

SET @brandName = 'Land Rover'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('109' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Defender' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Discovery' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Discovery Sport' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Freelander' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Range Rover' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Range Rover Evoque' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Range Rover Sport' ,@brandId)
END

SET @brandName = 'Volkswagen'
INSERT INTO [dbo].[Brands] ([Name]) VALUES (@brandName)

SELECT @brandId = BrandId from brands where Name = @brandName

IF @brandId IS NOT NULL
BEGIN
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Amarok' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Beetle' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Bora' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Bora Variant' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Caddy' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Caddy Van' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Life' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('California' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Caravelle' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('CC' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Crafter' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Crafter Van' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Crafter Kombi' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('CrossTouran' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Eos' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Fox' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Golf' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Golf Cabrio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Golf Plus' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Golf Sportvan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Golf Variant' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Jetta' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('LT' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Lupo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Multivan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('New Beetle' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('New Beetle Cabrio' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Passat' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Passat Alltrack' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Passat CC' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Passat Variant' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Passat Variant Van' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Phaeton' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Polo' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Polo Van' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Polo Variant' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Scirocco' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Sharan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('T4' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('T4 Caravelle' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('T4 Multivan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('T5' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('T5 Caravelle' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('T5 Multivan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('T5 Transporter Shuttle' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Tiguan' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Touareg' ,@brandId)
	INSERT INTO [dbo].[Models]([Description],[BrandId]) VALUES ('Touran' ,@brandId)
END



--{"brand": "Dacia", "models": ["Dokker", "Duster", "Lodgy", "Logan", "Logan MCV", "Logan Van", "Sandero", "Solenza"]},

--{"brand": "Opel", "models": ["Agila", "Ampera", "Antara", "Astra", "Astra cabrio", "Astra caravan", "Astra coupé", "Calibra", "Campo", "Cascada", "Corsa", "Frontera", "Insignia", "Insignia kombi", "Kadett", "Meriva", "Mokka", "Movano", "Omega", "Signum", "Vectra", "Vectra Caravan", "Vivaro", "Vivaro Kombi", "Zafira"]},
--{"brand": "Alfa Romeo", "models": ["145", "146", "147", "155", "156", "156 Sportwagon", "159", "159 Sportwagon", "164", "166", "4C", "Brera", "GTV", "MiTo", "Crosswagon", "Spider", "GT", "Giulietta", "Giulia"]},
--{"brand": "Škoda", "models": ["Favorit", "Felicia", "Citigo", "Fabia", "Fabia Combi", "Fabia Sedan", "Felicia Combi", "Octavia", "Octavia Combi", "Roomster", "Yeti", "Rapid", "Rapid Spaceback", "Superb", "Superb Combi"]},

--{"brand": "Porsche", "models": ["911 Carrera", "911 Carrera Cabrio", "911 Targa", "911 Turbo", "924", "944", "997", "Boxster", "Cayenne", "Cayman", "Macan", "Panamera"]},




--{"brand": "Lexus", "models": ["CT", "GS", "GS 300", "GX", "IS", "IS 200", "IS 250 C", "IS-F", "LS", "LX", "NX", "RC F", "RX", "RX 300", "RX 400h", "RX 450h", "SC 430"]},

--{"brand": "Mercedes-Benz", "models": ["100 D", "115", "124", "126", "190", "190 D", "190 E", "200 - 300", "200 D", "200 E", "210 Van", "210 kombi", "310 Van", "310 kombi", "230 - 300 CE Coupé", "260 - 560 SE", "260 - 560 SEL", "500 - 600 SEC Coupé", "Trieda A", "A", "A L", "AMG GT", "Trieda B", "Trieda C", "C", "C Sportcoupé", "C T", "Citan", "CL", "CL", "CLA", "CLC", "CLK Cabrio", "CLK Coupé", "CLS", "Trieda E", "E", "E Cabrio", "E Coupé", "E T", "Trieda G", "G Cabrio", "GL", "GLA", "GLC", "GLE", "GLK", "Trieda M", "MB 100", "Trieda R", "Trieda S", "S", "S Coupé", "SL", "SLC", "SLK", "SLR", "Sprinter"]},
--{"brand": "Saab", "models": ["9-3", "9-3 Cabriolet", "9-3 Coupé", "9-3 SportCombi", "9-5", "9-5 SportCombi", "900", "900 C", "900 C Turbo", "9000"]},
--{"brand": "Audi", "models": ["100", "100 Avant", "80", "80 Avant", "80 Cabrio", "90", "A1", "A2", "A3", "A3 Cabriolet", "A3 Limuzina", "A3 Sportback", "A4", "A4 Allroad", "A4 Avant", "A4 Cabriolet", "A5", "A5 Cabriolet", "A5 Sportback", "A6", "A6 Allroad", "A6 Avant", "A7", "A8", "A8 Long", "Q3", "Q5", "Q7", "R8", "RS4 Cabriolet", "RS4/RS4 Avant", "RS5", "RS6 Avant", "RS7", "S3/S3 Sportback", "S4 Cabriolet", "S4/S4 Avant", "S5/S5 Cabriolet", "S6/RS6", "S7", "S8", "SQ5", "TT Coupé", "TT Roadster", "TTS"]},

--{"brand": "Dodge", "models": ["Avenger", "Caliber", "Challenger", "Charger", "Grand Caravan", "Journey", "Magnum", "Nitro", "RAM", "Stealth", "Viper"]},
--{"brand": "Chrysler", "models": ["300 C", "300 C Touring", "300 M", "Crossfire", "Grand Voyager", "LHS", "Neon", "Pacifica", "Plymouth", "PT Cruiser", "Sebring", "Sebring Convertible", "Stratus", "Stratus Cabrio", "Town & Country", "Voyager"]},
--{"brand": "Ford", "models": ["Aerostar", "B-Max", "C-Max", "Cortina", "Cougar", "Edge", "Escort", "Escort Cabrio", "Escort kombi", "Explorer", "F-150", "F-250", "Fiesta", "Focus", "Focus C-Max", "Focus CC", "Focus kombi", "Fusion", "Galaxy", "Grand C-Max", "Ka", "Kuga", "Maverick", "Mondeo", "Mondeo Combi", "Mustang", "Orion", "Puma", "Ranger", "S-Max", "Sierra", "Street Ka", "Tourneo Connect", "Tourneo Custom", "Transit", "Transit", "Transit Bus", "Transit Connect LWB", "Transit Courier", "Transit Custom", "Transit kombi", "Transit Tourneo", "Transit Valnik", "Transit Van", "Transit Van 350", "Windstar"]},
--{"brand": "Hummer", "models": ["H2", "H3"]},
--{"brand": "Infiniti", "models": ["EX", "FX", "G", "G Coupé", "M", "Q", "QX"]},
--{"brand": "Jaguar", "models": ["Daimler", "F-Pace", "F-Type", "S-Type", "Sovereign", "X-Type", "X-type Estate", "XE", "XF", "XJ", "XJ12", "XJ6", "XJ8", "XJ8", "XJR", "XK", "XK8 Convertible", "XKR", "XKR Convertible"]},


--{"brand": "Volvo", "models": ["240", "340", "360", "460", "850", "850 kombi", "C30", "C70", "C70 Cabrio", "C70 Coupé", "S40", "S60", "S70", "S80", "S90", "V40", "V50", "V60", "V70", "V90", "XC60", "XC70", "XC90"]},
--{"brand": "Daewoo", "models": ["Espero", "Kalos", "Lacetti", "Lanos", "Leganza", "Lublin", "Matiz", "Nexia", "Nubira", "Nubira kombi", "Racer", "Tacuma", "Tico"]},
--{"brand": "Fiat", "models": ["1100", "126", "500", "500L", "500X", "850", "Barchetta", "Brava", "Cinquecento", "Coupé", "Croma", "Doblo", "Doblo Cargo", "Doblo Cargo Combi", "Ducato", "Ducato Van", "Ducato Kombi", "Ducato Podvozok", "Florino", "Florino Combi", "Freemont", "Grande Punto", "Idea", "Linea", "Marea", "Marea Weekend", "Multipla", "Palio Weekend", "Panda", "Panda Van", "Punto", "Punto Cabriolet", "Punto Evo", "Punto Van", "Qubo", "Scudo", "Scudo Van", "Scudo Kombi", "Sedici", "Seicento", "Stilo", "Stilo Multiwagon", "Strada", "Talento", "Tipo", "Ulysse", "Uno", "X1/9"]},
--{"brand": "MINI", "models": ["Cooper", "Cooper Cabrio", "Cooper Clubman", "Cooper D", "Cooper D Clubman", "Cooper S", "Cooper S Cabrio", "Cooper S Clubman", "Countryman", "Mini One", "One D"]},
--{"brand": "Rover", "models": ["200", "214", "218", "25", "400", "414", "416", "620", "75"]},
--{"brand": "Smart", "models": ["Cabrio", "City-Coupé", "Compact Pulse", "Forfour", "Fortwo cabrio", "Fortwo coupé", "Roadster"]}]

