using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public static class MySecure
{
    public static string DecryptInputMask(string Code)
    {
        return Right(Replicate("*", 20) + Right(DecryptText(Code), 4), 20);
    }

    public static string EncryptText(string strText)
    {
        return Encrypt(strText, ConfigurationManager.AppSettings["MacAddress"]);
    }

    public static string DecryptText(string strText)
    {
        return Decrypt(strText, ConfigurationManager.AppSettings["MacAddress"]);
    }

    public static string Encrypt(string strText, string strEncrKey)
    {
        byte[] byKey = { };
        byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
        try
        {
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string Decrypt(string strText, string sDecrKey)
    {
        byte[] byKey = { };
        byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
        byte[] inputByteArray = new byte[strText.Length + 1];
        try
        {
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(strText);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string Left(string param, int length)
    {
        string result = param.Substring(0, length);
        return result;
    }
    public static string Right(string param, int length)
    {
        string result = param.Substring(param.Length - length, length);
        return result;
    }

    public static string Mid(string param, int startIndex, int length)
    {
        string result = param.Substring(startIndex, length);
        return result;
    }

    public static string Mid(string param, int startIndex)
    {
        string result = param.Substring(startIndex);
        return result;
    }

    public static string Replicate(string Character, int Loop)
    {
        string str = string.Empty;
        for (int i = 1; i <= Loop; i++)
        {
            str += Character;
        }
        return str;
    }

    public static bool IsBase64String(string s)
    {
        s = s.Trim();
        return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
    }

}