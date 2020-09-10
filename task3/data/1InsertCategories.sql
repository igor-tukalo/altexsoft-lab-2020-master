USE [cookBook]
GO

INSERT INTO [dbo].[Categories]
           ([Name]
           ,[ParentId])
VALUES
	('Categories',0),
	('Salads',1),
	('Bakery products',1),
	('Meat, poultry',1),
	('Fish, seafood',1),
	('The vinaigrette',2),
	('Mimosa',2),
	('Olivier salad',2),
	('Kefir baked goods',3),
	('Kefir pies',9),
	('Bread on kefir',9),
	('Kefir cupcakes',9),
	('Muffins on kefir',12),
	('Cutlets',4),
	('Meatballs',4),
	('Cabbage rolls',4),
	('Lazy stuffed cabbage',16);
GO


