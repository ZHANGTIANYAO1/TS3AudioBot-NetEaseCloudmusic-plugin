using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TS3AudioBot.Helper;

public static class Utils
{
    // Http请求
    public static async Task<T> HttpGetAsync<T>(string url, string cookie = "")
    {
        var request = WebWrapper.Request(url);
        if (!string.IsNullOrEmpty(cookie))
        {
            request.WithHeader("Cookie", cookie);
        }
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
}