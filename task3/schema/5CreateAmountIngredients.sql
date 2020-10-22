/****** Object:  Table [dbo].[AmountIngredients]    Script Date: 10.09.2020 9:09:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AmountIngredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [float] NULL,
	[Unit] [nvarchar](50) NULL,
	[IdRecipe] [int] NOT NULL,
	[IdIngredient] [int] NOT NULL,
 CONSTRAINT [PK_AmountIngredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AmountIngredients]  WITH CHECK ADD  CONSTRAINT [FK_AmountIngredients_Ingredients] FOREIGN KEY([IdIngredient])
REFERENCES [dbo].[Ingredients] ([Id])
GO

ALTER TABLE [dbo].[AmountIngredients] CHECK CONSTRAINT [FK_AmountIngredients_Ingredients]
GO

ALTER TABLE [dbo].[AmountIngredients]  WITH CHECK ADD  CONSTRAINT [FK_AmountIngredients_Recipes] FOREIGN KEY([IdRecipe])
REFERENCES [dbo].[Recipes] ([Id])
GO

ALTER TABLE [dbo].[AmountIngredients] CHECK CONSTRAINT [FK_AmountIngredients_Recipes]
GO


