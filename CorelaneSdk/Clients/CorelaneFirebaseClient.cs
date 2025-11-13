using CorelaneSdk.RefitClient;
using Microsoft.AspNetCore.Http;
using Refit;

namespace CorelaneSdk.Clients;

public interface ICorelaneFirebaseClient
{
    Task<Models.Core.ApiResponse<string>> UploadFilesToFirebaseAsync(IFormFile file, string path);
}

public class CorelaneFirebaseClient : ICorelaneFirebaseClient
{
    private readonly ICorelaneFirebaseHttpClient _corelaneFirebaseHttpClient;

    public CorelaneFirebaseClient(ICorelaneFirebaseHttpClient corelaneFirebaseHttpClient)
    {
        _corelaneFirebaseHttpClient = corelaneFirebaseHttpClient;
    }

    public async Task<Models.Core.ApiResponse<string>> UploadFilesToFirebaseAsync(IFormFile file, string path)
    {
        try
        {
            // IFormFile'ı StreamPart'a çevir
            using var stream = file.OpenReadStream();
            var streamPart = new StreamPart(stream, file.FileName, file.ContentType);

            return await _corelaneFirebaseHttpClient.UploadFilesToFirebaseAsync(streamPart, path);
        }
        catch (Exception)
        {
            // Fallback olarak direkt IFormFile kullan
            return await _corelaneFirebaseHttpClient.UploadFilesToFirebaseWithFormFileAsync(file, path);
        }
    }
}