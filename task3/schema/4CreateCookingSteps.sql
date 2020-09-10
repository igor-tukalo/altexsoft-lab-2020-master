USE [cookBook]
GO

/****** Object:  Table [dbo].[CookingSteps]    Script Date: 10.09.2020 9:09:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CookingSteps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Step] [int] NULL,
	[IdRecipe] [int] NULL,
 CONSTRAINT [PK_CookingStep] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[CookingSteps]  WITH CHECK ADD  CONSTRAINT [FK_CookingStep_Recipes] FOREIGN KEY([IdRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO

ALTER TABLE [dbo].[CookingSteps] CHECK CONSTRAINT [FK_CookingStep_Recipes]
GO


