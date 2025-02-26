namespace Infrastructure.CrossCutting.Identity;

public static class BCryptHash
{
    public static string HashPassword(string password) =>    
        BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());

    public static bool VerifyPassword(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}
