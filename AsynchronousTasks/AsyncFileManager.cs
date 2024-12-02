using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousTasks
{
    public class AsyncFileManager
    {
        public static string ReadFile(string path)
        {
            if (path.EndsWith(".txt"))
            {
                string contents = File.ReadAllTextAsync(path).Result;
                return contents;
            } 
            else
            {
                throw new ArgumentException(message: "The file provided for reading is not a text file");
            }
        }

        public async static void WriteFile(string path, string contents)
        {
            if (path.EndsWith(".txt"))
            {
                if (!File.Exists(path))
                {
                    await File.WriteAllTextAsync(path, contents + Environment.NewLine);
                }
                else
                {
                    await File.AppendAllTextAsync(path, contents + Environment.NewLine);
                }
            }
            else
            {
                throw new ArgumentException(message: "The file provided for writing is not a text file");
            }
        }
    }
}
