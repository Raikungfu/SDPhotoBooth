using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class FirebaseService
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;

    public FirebaseService(IConfiguration configuration)
    {
        var firebaseConfig = configuration.GetSection("Firebase").Get<Dictionary<string, string>>();
        if (firebaseConfig == null || !firebaseConfig.Any())
        {
            throw new InvalidOperationException("Firebase configuration is missing or invalid.");
        }

        if (firebaseConfig.ContainsKey("private_key"))
        {
            firebaseConfig["private_key"] = firebaseConfig["private_key"].Replace("\\n", "\n");
        }

        var jsonCredentials = System.Text.Json.JsonSerializer.Serialize(firebaseConfig);

        var credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromJson(jsonCredentials);
        _storageClient = StorageClient.Create(credential);
        _bucketName = firebaseConfig["project_id"] + ".appspot.com";
    }

    public async Task<string> UploadImageAsync(byte[] imageBytes)
    {
        using (var stream = new MemoryStream(imageBytes))
        {
            string fileName = $"{Guid.NewGuid()}.jpg"; 
            var objectName = $"uploads/{fileName}";

            var storageObject = await _storageClient.UploadObjectAsync(_bucketName, objectName, "image/jpeg", stream);

            return $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/{Uri.EscapeDataString(objectName)}?alt=media";
        }
    }


}
