using Minio;
using Minio.DataModel.Args;

namespace MalDealsBackend.Services
{
    public class MinioService
    {
        private readonly IMinioClient _minioClient;
        private readonly ConfigManager _config;

        public MinioService(ConfigManager config)
        {
            _config = config;
            _minioClient = new MinioClient()
                .WithEndpoint("localhost", 9000)
                .WithCredentials("admin", "password")
                .Build();
        }

        public async Task UploadFileAsync(string bucketName, IFormFile file)
        {
            var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!bucketExists)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            }
            IsAllowedImageType(file);
            using var stream = file.OpenReadStream();
            var args = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(file.FileName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType);
            await _minioClient.PutObjectAsync(args);
        }

        public async Task<List<string>> UploadMultipleFileAsync(string bucketName, List<IFormFile> files)
        {
            bool bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!bucketExists)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            }

            var uploadTasks = files.Select(async file =>
            {
                IsAllowedImageType(file);
                using var stream = file.OpenReadStream();
                var fileName = file.FileName;

                var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType);
                await _minioClient.PutObjectAsync(putObjectArgs);
                return $"{bucketName}:{fileName}";
            });
            return [.. (await Task.WhenAll(uploadTasks))];
        }

        public async Task<string> GetPresignedUrlAsync(string bucketName, string fileName)
        {

            var statObjectArgs = new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName);
            await _minioClient.StatObjectAsync(statObjectArgs);

            var args = new PresignedGetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithExpiry(604800);
            return await _minioClient.PresignedGetObjectAsync(args);
        }

        public async Task<bool> DeleteFileAsync(string bucketName, string fileName)
        {
            var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName);

            await _minioClient.RemoveObjectAsync(removeObjectArgs);
            return true;
        }

        public void IsAllowedImageType(IFormFile file)
        {
            HashSet<string> allowedImageTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "image/jpeg", "image/png", "image/gif", "image/webp"
            };
            HashSet<string> allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".jpg", ".jpeg", ".png", ".gif", ".webp"
            };

            // Prüfen des MIME-Typs
            if (!allowedImageTypes.Contains(file.ContentType))
            {
                throw new Exception($"Invalid file type: {file.ContentType}. Only images are allowed.");
            }

            // Prüfen der Dateiendung
            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception($"Invalid file extension: {extension}. Only images are allowed.");
            }
        }
    }
}