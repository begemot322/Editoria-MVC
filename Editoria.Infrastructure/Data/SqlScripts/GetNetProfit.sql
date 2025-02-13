CREATE FUNCTION GetNetProfit(@IssueId INT)
RETURNS DECIMAL(18, 2)
AS
BEGIN
    DECLARE @Revenue DECIMAL(18, 2);
    DECLARE @TaxRate DECIMAL(5, 2);
    DECLARE @Count INT;
    DECLARE @NetProfit DECIMAL(18, 2);

    -- �������� ������� � ���������� ��������� ����������
    SELECT @Revenue = ISNULL(SUM(Cost), 0),
           @Count = COUNT(*)
    FROM Advertisements
    WHERE IssueId = @IssueId;

    -- ���������� ������� ��������� ������ �� �������
    IF @Revenue <= 150
        SET @TaxRate = 0;
    ELSE IF @Revenue <= 500
        SET @TaxRate = 0.05;
    ELSE
        SET @TaxRate = 0.10;

    -- ����������� ��������� ������ � ����������� �� ���������� ������
    IF @Count BETWEEN 4 AND 7
        SET @TaxRate = @TaxRate + 0.02; 
    ELSE IF @Count > 7
        SET @TaxRate = @TaxRate + 0.05;

    SET @NetProfit = @Revenue * (1 - @TaxRate);

    -- �������������� ���� 2%, ���� ������� > 1000
    IF @NetProfit > 1000
        SET @NetProfit = @NetProfit * 0.98; 

    RETURN @NetProfit;
END;
