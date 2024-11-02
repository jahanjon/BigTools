namespace Domain.Dto.Common;

public class KeywordDto
{
    public string Keyword { get; set; }
}

public class KeywordDto<TData> : KeywordDto
{
    public TData Data { get; set; }
}