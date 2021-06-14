using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.IO;

namespace AzureBlobStorage
{
	class Program
	{
		public const string ConnectionString = "ConnectionString";
		public const string ClientName = "akshay";
		static void Main(string[] args)
		{
			BlobContainerClient blobContainerClient = new BlobContainerClient(ConnectionString, ClientName);
			blobContainerClient.CreateIfNotExists();

			string localPath = "./data/";
			string extension = ".xlsx";

			string fileName = "testfile3"+ extension;
			string localFilePath = Path.Combine(localPath, fileName);

			var blobClient = blobContainerClient.GetBlobClient(Guid.NewGuid().ToString()+ extension);
			Console.WriteLine($"Shared access signature URL to access content");

			Console.WriteLine(blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(15)));

			byte[] buff = File.ReadAllBytes(localFilePath);

			using(var stream = new MemoryStream(buff, writable: false))
			{
				blobClient.UploadAsync(stream);
			}

			Console.Read();
		}
	}
}
