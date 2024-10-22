using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using OpenCvSharp;

namespace RTSPListener
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "http://localhost:5081/imageHub";
            string rtspUrl = "rtsp://localhost:8554/webcam";

            var hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            await hubConnection.StartAsync();

            while (true)
            {
                try
                {
                    using var capture = new VideoCapture(rtspUrl);

                    if (!capture.IsOpened())
                    {
                        Console.WriteLine("Failed to open RTSP stream. Retrying in 5 seconds...");
                        await Task.Delay(5000);
                        continue;
                    }

                    Console.WriteLine("Starting to capture frames...");

                    using var mat = new Mat();
                    var lastProcessedTime = DateTime.Now;

                    while (true)
                    {
                        var currentTime = DateTime.Now;

                        if (!capture.Read(mat) || mat.Empty())
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Empty frame or failed to read frame. Reconnecting...");
                            break;
                        }

                        if ((currentTime - lastProcessedTime).TotalMilliseconds >= 1000)
                        {
                            byte[] imageBytes = mat.ToBytes(".jpg");

                            await hubConnection.InvokeAsync("SendImage", imageBytes);

                            lastProcessedTime = currentTime;
                        }

                        // Small delay to prevent high CPU usage
                        await Task.Delay(5); 

                    }

                    capture.Release();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}. Retrying in 5 seconds...");
                    await Task.Delay(5000);
                }
            }
        }
    }
}
