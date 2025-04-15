using System;
using System.Diagnostics;
using System.IO;
using OpenCvSharp;

class WaveLoopGenerator
{
    static void Main()
    {
        string inputImagePath = "/workspaces/Module11Challenge/WaveLoopGenerator/ChatGPT Image Apr 15, 2025, 01_30_12 AM.png"; // Your image file in project root
        string tempFramesDir = "frames";
        string outputVideoPath = "seamless_wave_loop.mp4";

        int duration = 10; // seconds
        int fps = 30;
        int totalFrames = duration * fps;

        // Create folder for frames
        if (!Directory.Exists(tempFramesDir))
            Directory.CreateDirectory(tempFramesDir);

        Mat image = Cv2.ImRead(inputImagePath);
        int width = image.Width;
        int height = image.Height;

        Console.WriteLine("🔄 Generating frames...");

        for (int i = 0; i < totalFrames; i++)
        {
            int shift = (i * width) / totalFrames;
            int dx = shift;
            int dy = shift;

            // Build transformation matrix manually
            Mat transformMatrix = new Mat(2, 3, MatType.CV_32F);
            transformMatrix.Set(0, 0, 1f);
            transformMatrix.Set(0, 1, 0f);
            transformMatrix.Set(0, 2, dx % width);
            transformMatrix.Set(1, 0, 0f);
            transformMatrix.Set(1, 1, 1f);
            transformMatrix.Set(1, 2, dy % height);

            Mat transformed = new Mat();
            Cv2.WarpAffine(image, transformed, transformMatrix, new Size(width, height),
                InterpolationFlags.Linear, BorderTypes.Wrap);

            string framePath = Path.Combine(tempFramesDir, $"frame_{i:D4}.png");
            Cv2.ImWrite(framePath, transformed);
        }

        Console.WriteLine("🎬 Encoding video with FFmpeg...");

        // Run FFmpeg command to compile frames
        var ffmpegCommand = $"ffmpeg -y -framerate {fps} -i {tempFramesDir}/frame_%04d.png -c:v libx264 -pix_fmt yuv420p {outputVideoPath}";
        Process.Start("cmd.exe", $"/C {ffmpegCommand}").WaitForExit();

        Console.WriteLine($"✅ Done! Video saved to {outputVideoPath}");

        // Optional: Delete frames
        Directory.Delete(tempFramesDir, true);
    }
}
