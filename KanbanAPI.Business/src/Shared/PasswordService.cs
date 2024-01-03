using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using KanbanAPI.Domain;

namespace KanbanAPI.Business;

public class PasswordService
{
    public static void HashPassword(string password, out string hashedPassword, out byte[] passwordSalt)
    {
        var hmac = new HMACSHA256();
        passwordSalt = hmac.Key;
        hashedPassword = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    public static bool VerifyPasswordHash(string originalPassword, string hashedPassword, byte[] passwordSalt)
    {
        var hmac = new HMACSHA512(passwordSalt);
        var hashedOriginal = Encoding.UTF8.GetString(hmac.ComputeHash(Encoding.UTF8.GetBytes(originalPassword)));
        return hashedOriginal == hashedPassword;
    }
}