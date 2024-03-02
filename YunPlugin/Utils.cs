using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TS3AudioBot.Helper;

public static class Utils
{
    // Http请求
    public static async Task<T> HttpGetAsync<T>(string url, Dictionary<string, string> header = null)
    {
        var request = WebWrapper.Request(url);
        
        if (header != null)
        {
            foreach (var item in header)
            {
                request.WithHeader(item.Key, item.Value);
            }
        }

        // YunPlugin.YunPlgun.GetLogger().Info($"HttpGetAsync: {url} header: {request.Headers}");

        return await request.AsJson<T>();
    }

    // "洗牌"
    public static void ShuffleArrayList<T>(List<T> arrayList)
    {
        Random random = new Random();

        int n = arrayList.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            var value = arrayList[k];
            arrayList[k] = arrayList[n];
            arrayList[n] = value;
        }
    }

    // 获得时间戳
    public static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }

    public static string ExtractIdFromAddress(string address)
    {
        string pattern = @"id=(\d+)";
        Match match = Regex.Match(address, pattern);
        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        else
        {
            return address;
        }
    }

    public static bool IsNumber(string strNumber)
    {
        Regex regex = new Regex(@"^\d+$");
        return regex.IsMatch(strNumber);
    }
}