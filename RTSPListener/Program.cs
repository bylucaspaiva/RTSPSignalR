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

            using var capture = new VideoCapture(rtspUrl);

            if (!capture.IsOpened())
            {
                Console.WriteLine("Failed to open RTSP stream.");
                return;
            }

            Console.WriteLine("Starting to capture frames...");

            using var mat = new Mat();
            while (true)
            {
                capture.Read(mat);
                if (mat.Empty())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Empty frame, exiting...");
                    break;
                }

                byte[] imageBytes = mat.ToBytes(".jpg");

                await hubConnection.InvokeAsync("SendImage", imageBytes);

                await Task.Delay(250);
            }

            await hubConnection.StopAsync();
        }
    }
}
