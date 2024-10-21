using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageReceiver
{
    public class ImageHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendImage(byte[] imageBytes)
        {
            string fileName = $"image_{DateTime.Now.Ticks}.jpg";
            string directory = Path.Combine(Directory.GetCurrentDirectory(), "ReceivedImages");
            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, fileName);
            await File.WriteAllBytesAsync(filePath, imageBytes);
            Console.WriteLine($"Image saved: {filePath}");
        }
    }
}
