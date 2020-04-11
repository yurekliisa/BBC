/****** Script for SelectTopNRows command from SSMS  ******/
/*Slider dogrulama Querysi*/

SELECT TOP (1000)    
      Count(*),Sum([Puan])
      ,[TarifAndReceteId]    
  FROM [BBCDb.Dev].[dbo].[Popularities]  group by TarifAndReceteId



/*Update Table*/
UPDATE Contents 
SET  MainImage = 'Image ' + Cast(TarifandReceteId as varchar(10))

UPDATE Contents 
SET  ShortDescription = 'Short Description ' + Cast(TarifandReceteId as varchar(10))

UPDATE Contents 
SET  Title = 'Title ' + Cast(TarifandReceteId as varchar(10))

