﻿using System.Security.Cryptography;
using System.Text;

namespace DisasterAlleviation
{
    public class Hash
    {
        public static string C_MD5(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                password = StrMD5(md5Hash, password);
            }
            return password;
        }

        public static string StrMD5(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.  
            return sBuilder.ToString();
        }
    }
}
