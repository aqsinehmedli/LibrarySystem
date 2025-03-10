﻿using System.Security.Cryptography;
using System.Text;

namespace A.Common.Secutiry;

public static class PasswordHasher
{
    public static string ComputeStringToSha256Hash(string plainText)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString("x2"));
        }
        return sb.ToString();
    }

}
