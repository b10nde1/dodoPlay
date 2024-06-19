namespace PlaywrightTests;

public class MyUtitilies
{
    public string GenerateID()
        => DateTime.Now.ToString("yyyyMMddHHmmssffff");
}