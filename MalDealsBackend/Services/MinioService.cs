using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Minio;
using Minio.Exceptions;

namespace MalDealsBackend.Services
{
    public class MinioService
    {
        private readonly IMinioClient _minioClient;
        private readonly string _bucketName = "uploads";

        public MinioService()
        {
            _minioClient = new MinioClient()
                .WithEndpoint("http://localhost:9000")
                .WithCredentials("admin", "password")
                .Build();
        }

        public async Task UploadFileAsync(IFormFile file) {
            
        }
    }
}