using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public static class Utils
{
    // Http请求
    public static async Task<string> HttpGetAsync(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Accept = "text/html, application/xhtml+xml, */*";
        request.ContentType = "application/json";
        HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        {
            return await reader.ReadToEndAsync();
        }
    }

    public static string HttpGet(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Accept = "text/html, application/xhtml+xml, */*";
        request.ContentType = "application/json";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
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