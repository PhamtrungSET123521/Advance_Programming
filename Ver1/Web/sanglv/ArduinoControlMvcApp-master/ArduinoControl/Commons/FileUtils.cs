using System;
using System.IO;
using System.Linq;

namespace ArduinoControl.Commons
{
    public class FileUtils
    {
        public static string read(string fileName, string defaultValue = null)
        {
            try
            {
                return String.Join('\n', File.ReadAllLines(fileName));
            } catch
            {
                return defaultValue;
            }
        }

        public static bool write(string fileName, string data)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(data);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
