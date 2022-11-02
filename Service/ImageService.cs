using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Service.Interfaces;

namespace Service;

public class ImageService : IImageService
{
    private readonly BlobContainerClient _imageContainer;

    public ImageService()
    {
        string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage")!;
        _imageContainer = new BlobContainerClient(connectionString, "images");
    }

    public string GetUrl(string imageName)
    {
        BlobClient blob = _imageContainer.GetBlobClient(imageName);
        return blob.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddHours(1)).ToString();
    }
}
