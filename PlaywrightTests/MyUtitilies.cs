namespace PlaywrightTests;

public class MyUtitilies
{
    public string GenerateID()
        => DateTime.Now.ToString("yyyyMMddHHmmssffff");

    public string SelectFromList(string [] list)
        => list[new Random().Next(list.Length)];
}