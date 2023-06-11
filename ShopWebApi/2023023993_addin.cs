using System;

/// <summary>
/// 好东西
/// </summary>
public static class AddGoodThing
{
  

    public static string EncryptPassword(string password)
    {
        char[] chars = password.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            chars[i] = (char)(chars[i] + 1);
        }

        return new string(chars);
    }

    public static string DecryptPassword(string encryptedPassword)
    {
        char[] chars = encryptedPassword.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            chars[i] = (char)(chars[i] - 1);
        }

        return new string(chars);
    }
}
