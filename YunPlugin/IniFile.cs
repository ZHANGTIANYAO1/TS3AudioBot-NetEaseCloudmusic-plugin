using Nini.Config;
using System.Reflection;

public class IniFile   // ini设置文件读取
{
    IniConfigSource INI;
    string EXE = Assembly.GetExecutingAssembly().GetName().Name;

    public IniFile(string IniPath = null)
    {
        INI = new IniConfigSource(IniPath ?? EXE + ".ini");
    }

    public string Read(string Key, string defaultVal = "", string Section = null)
    {
        return INI.Configs[Section ?? EXE].Get(Key, defaultVal);
    }

    public void Write(string Key, string Value, string Section = null)
    {
        INI.Configs[Section ?? EXE].Set(Key, Value);
        INI.Save();
    }

    public void DeleteKey(string Key, string Section = null)
    {
        Write(Key, null, Section ?? EXE);
    }

    public void DeleteSection(string Section = null)
    {
        Write(null, null, Section ?? EXE);
    }

    public bool KeyExists(string Key, string Section = null)
    {
        return Read(Key, Section: Section).Length > 0;
    }
}
