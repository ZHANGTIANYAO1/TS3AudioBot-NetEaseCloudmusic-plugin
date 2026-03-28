using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace YunBot.Services;

/// <summary>
/// Handles image downloading, detection and compression for TeamSpeak avatar uploads.
/// TS3 avatar permission limit is typically 500KB, but we target 300KB for safety.
/// </summary>
public static class ImageProcessor
{
    private const int MaxAvatarBytes = 300 * 1024; // 300KB safe limit
    private const int DefaultMaxDimension = 300;
    private const int FallbackMaxDimension = 128;
    private const int DefaultQuality = 85;
    private const int MinQuality = 25;

    /// <summary>
    /// Downloads an image from URL and processes it to fit TS3 avatar limits.
    /// </summary>
    public static async Task<MemoryStream> DownloadAndProcessAvatarAsync(HttpClient http, string imageUrl)
    {
        var imageBytes = await http.GetByteArrayAsync(imageUrl);
        return ProcessAvatar(imageBytes);
    }

    /// <summary>
    /// Processes raw image bytes: resizes if needed, compresses to fit TS3 limits.
    /// </summary>
    public static MemoryStream ProcessAvatar(byte[] imageBytes)
    {
        if (imageBytes.Length <= MaxAvatarBytes)
        {
            // Check if dimensions are also reasonable
            try
            {
                var info = Image.Identify(imageBytes);
                if (info != null && info.Width <= DefaultMaxDimension && info.Height <= DefaultMaxDimension)
                {
                    var ms = new MemoryStream(imageBytes);
                    ms.Position = 0;
                    return ms;
                }
            }
            catch
            {
                // If we can't identify, just return raw bytes
                var ms = new MemoryStream(imageBytes);
                ms.Position = 0;
                return ms;
            }
        }

        return CompressImage(imageBytes);
    }

    /// <summary>
    /// Processes a base64-encoded image (e.g. QR codes from login).
    /// Handles "data:image/...;base64," prefix.
    /// </summary>
    public static MemoryStream ProcessBase64Avatar(string base64Image)
    {
        var parts = base64Image.Split(',');
        var raw = Convert.FromBase64String(parts.Length > 1 ? parts[1] : parts[0]);
        return ProcessAvatar(raw);
    }

    private static MemoryStream CompressImage(byte[] imageBytes)
    {
        using var image = Image.Load(imageBytes);

        // Resize if dimensions exceed limit
        if (image.Width > DefaultMaxDimension || image.Height > DefaultMaxDimension)
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(DefaultMaxDimension, DefaultMaxDimension),
                Mode = ResizeMode.Max
            }));
        }

        // Try progressively lower quality until under size limit
        var output = new MemoryStream();
        for (var quality = DefaultQuality; quality >= MinQuality; quality -= 10)
        {
            output.SetLength(0);
            output.Position = 0;
            image.SaveAsJpeg(output, new JpegEncoder { Quality = quality });

            if (output.Length <= MaxAvatarBytes)
            {
                output.Position = 0;
                Console.WriteLine($"[YunBot] Image compressed: {imageBytes.Length} -> {output.Length} bytes (quality={quality})");
                return output;
            }
        }

        // Last resort: resize to very small
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(FallbackMaxDimension, FallbackMaxDimension),
            Mode = ResizeMode.Max
        }));

        output.SetLength(0);
        output.Position = 0;
        image.SaveAsJpeg(output, new JpegEncoder { Quality = 50 });
        output.Position = 0;
        Console.WriteLine($"[YunBot] Image aggressively compressed: {imageBytes.Length} -> {output.Length} bytes");
        return output;
    }
}
