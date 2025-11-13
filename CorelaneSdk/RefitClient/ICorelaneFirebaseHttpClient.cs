using Microsoft.AspNetCore.Http;
using Refit;

namespace CorelaneSdk.RefitClient;

public interface ICorelaneFirebaseHttpClient
{
    [Multipart]
    [Post("/api/v1/Firebase/UploadFilesToFirebase")]
    Task<Models.Core.ApiResponse<string>> UploadFilesToFirebaseAsync([AliasAs("file")] StreamPart file, [Query] string path);

    // Alternatif - IFormFile ile
    [Multipart]
    [Post("/api/v1/Firebase/UploadFilesToFirebase")]
    Task<Models.Core.ApiResponse<string>> UploadFilesToFirebaseWithFormFileAsync([AliasAs("file")] IFormFile file, [Query] string path);
}
