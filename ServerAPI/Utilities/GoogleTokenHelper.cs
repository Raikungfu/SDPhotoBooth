using BussinessObject.Models;
using BussinessObject.ViewModels;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ServerAPI.Controllers;
using System.Security.Cryptography;
using System.Text;

namespace ServerAPI.Helpers
{
    public class GoogleTokenHelper
    {
        public static async Task<GoogleJsonWebSignature.Payload?> VerifyAccessTokenAsync(string idToken, string googleWebClientId, string googleMobileClientId)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { googleWebClientId, googleMobileClientId }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return payload;
            }
            catch
            {
                return null;
            }
        }
    }
}
