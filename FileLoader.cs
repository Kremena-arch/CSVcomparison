using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class FileLoader
{
    public static string LoadFromFile(string path)
    {
        try
        {
            return File.ReadAllText(path);
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error loading file {path}: {ex.Message}");
            return "";
        }
    }

    public static async Task<string> LoadFromUrlAsync(string url)
    {
        try
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error fetching file from URL {url}: {ex.Message}");
            return "";
        }
    }
}