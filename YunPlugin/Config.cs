using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace YunPlugin
{
    public class YAMLSerialize
    {
        static public void Serializer<T>(T obj, string path)            // 序列化操作  
        {
            StreamWriter yamlWriter = File.CreateText(path);
            Serializer yamlSerializer = new Serializer();
            yamlSerializer.Serialize(yamlWriter, obj);
            yamlWriter.Close();
        }

        static public T Deserializer<T>(string path)           // 泛型反序列化操作  
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            StreamReader yamlReader = File.OpenText(path);
            Deserializer yamlDeserializer = new Deserializer();

            //读取持久化对象  
            T info = yamlDeserializer.Deserialize<T>(yamlReader);
            yamlReader.Close();
            return info;
        }
    }
    public class Config
    {
        public int version { get; set; }
        public Mode playMode { get; set; }
        public string neteaseApi { get; set; }
        public bool isPrivateFMMode { get; set; }
        public bool isQrlogin { get; set; }
        public int cookieUpdateIntervalMin { get; set; }
        public bool autoPause { get; set; }
        public Dictionary<string, string> Header { get; set; }

        [YamlIgnore]
        private string Path { get; set; }
        [YamlIgnore]
        public int CurrentVersion = 1;

        public static Config GetConfig(string path)
        {
            try
            {
                var config = YAMLSerialize.Deserializer<Config>(path);
                config.Path = path;
                if (config.version < config.CurrentVersion)
                {
                    config.version = config.CurrentVersion;
                    config.Save();
                }
                return config;
            }
            catch (FileNotFoundException)
            {
                var config = new Config
                {
                    version = 1,
                    playMode = Mode.SeqPlay,
                    neteaseApi = "https://163.lilac.pp.ua",
                    isPrivateFMMode = false,
                    isQrlogin = false,
                    autoPause = true,
                    cookieUpdateIntervalMin = 30,
                    Header = new Dictionary<string, string>
                    {
                        { "Cookie", "" },
                        { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Edg/122.0.0.0" }
                    },
                    Path = path
                };
                config.Save();
                return config;
            }
        }

        public void Save()
        {
            YAMLSerialize.Serializer(this, Path);
        }
    }
}
