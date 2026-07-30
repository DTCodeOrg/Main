namespace Main.Common;

public class TokenResult
{
    public TokenResult (bool valid)
    {
        IsSuccess = valid;
    }

    public bool IsSuccess
    {
        get; set;
    }

    public string AccessToken
    {
        get; set;
    }

    public string RefreshToken
    {
        get; set;
    }
}
