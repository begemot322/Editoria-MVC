CREATE FUNCTION GetCategoriesByPriority(
	@minPriority INT, 
	@maxPriority INT)
RETURNS TABLE
AS
RETURN 
(
    SELECT 
        c.CategoryId, 
        c.Name, 
        c.Description, 
        c.Priority,
		c.IsActive
    FROM 
        Categories c
    WHERE
	    c.Priority BETWEEN @minPriority AND @maxPriority
)
