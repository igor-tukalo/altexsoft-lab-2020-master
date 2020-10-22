--¬ыбрать первые 3 рецепта с ингридиентами, которые наход€тс€ в категории с ID = 3 или любой подкатегории
WITH tree (id, parent_id, "name", "level", "path")
AS (
    SELECT T1.id, T1.ParentId, T1.Name, 0, cast('' as nvarchar)
    FROM Categories T1
    WHERE T1.Id=3

    UNION ALL

    SELECT T2.id, T2.ParentId, T2.Name, T."level"+1, cast(T."path" +'.'  as nvarchar)
    FROM
        Categories T2 INNER JOIN tree T ON T.id = T2.ParentId
)
SELECT Recipe, Category, Ingredients.Name Ingredient
FROM
	(
	SELECT
		row_number() over(PARTITION BY T."name" ORDER BY T."path", Recipes.Name) as rn, 
		Recipes.Name Recipe,
		T."path" "path",
		space(T."level" * 5) + T."name" Category,
		Recipes.Id IdRecipe
	FROM
		tree T join Recipes on Recipes.IdCategory=t.id
	WHERE
		Recipes.Id in (select IdRecipe from AmountIngredients)
	)tb
	join AmountIngredients on AmountIngredients.IdRecipe=tb.IdRecipe
	join Ingredients on AmountIngredients.IdIngredient=Ingredients.Id
WHERE rn<4
ORDER BY "path";