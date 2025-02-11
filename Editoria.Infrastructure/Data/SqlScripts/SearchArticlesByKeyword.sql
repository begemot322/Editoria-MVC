CREATE PROCEDURE SearchArticlesByKeyword
    @Keyword NVARCHAR(100)
AS
BEGIN
    SELECT ArticleId,Title,Text,PublicationDate,AuthorComment,ImageUrl,IssueId,AuthorId,CategoryId
    FROM Articles
    WHERE Title LIKE '%' + @Keyword + '%'
       OR Text LIKE '%' + @Keyword + '%'
END;
