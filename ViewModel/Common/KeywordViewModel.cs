namespace ViewModel.Common;

public class KeywordViewModel
{
    public string Keyword { get; set; }
}

public class KeywordViewModel<TData> : KeywordViewModel
{
    public TData Data { get; set; }
}