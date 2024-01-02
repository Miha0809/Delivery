using System.Security.Cryptography;
using System.Text;

namespace Delivery.Services;

public class Hash
{
    public static string GetHash(string input)
    {
        using (var hash = SHA512.Create())
        {
            // Convert the input string to a byte array and compute the hash.
            var data = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }

    // Verify a hash against a string.
    public static bool VerifyHash(string input, string hash)
    {
        // Hash the input.
        var hashOfInput = GetHash(input);

        // Create a StringComparer an compare the hashes.
        var comparer = StringComparer.OrdinalIgnoreCase;

        return comparer.Compare(hashOfInput, hash) == 0;
    }
}