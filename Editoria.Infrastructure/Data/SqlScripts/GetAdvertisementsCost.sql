CREATE FUNCTION GetAdvertisementsCost(@IssueId INT)
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @TotalCost DECIMAL(18, 2);

    SELECT @TotalCost = SUM(Cost)
    FROM Advertisements
    WHERE IssueId = @IssueId;

    RETURN ISNULL(@TotalCost, 0);
END;