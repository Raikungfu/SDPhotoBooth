using BussinessObject.Models;
using BussinessObject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ServerAPI.Controllers;
using System.Security.Cryptography;
using System.Text;

namespace ServerAPI.Helpers
{
    public class GoogleTokenHelper
    {
        private static readonly HttpClient client = new HttpClient();

        public string DecryptToken(string encryptedToken, string EncryptionKey, string EncryptionIv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(EncryptionIv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = Convert.FromHexString(encryptedToken);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static async Task<TokenInfo> VerifyAccessTokenAsync(string accessToken, string googleClientId)
        {
            try
            {
                var response = await client.GetStringAsync($"https://oauth2.googleapis.com/tokeninfo?access_token={accessToken}");

                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("No response from Google tokeninfo endpoint.");
                    return null;
                }

                var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(response);

                if (tokenInfo != null)
                {
                    if (!tokenInfo.Aud.Equals(googleClientId))
                    {
                        Console.WriteLine("Invalid token audience.");
                        return null;
                    }

                    if (tokenInfo.Exp <= DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                    {
                        Console.WriteLine("Token has expired.");
                        return null;
                    }

                    Console.WriteLine($"Token is valid for {tokenInfo.Email}");
                    return tokenInfo;
                }

                Console.WriteLine("Invalid token information received.");
                return null;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP request failed: {httpEx.Message}");
                return null;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON parsing error: {jsonEx.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token verification failed: {ex.Message}");
                return null;
            }
        }
    }
}
