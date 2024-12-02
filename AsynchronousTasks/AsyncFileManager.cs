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
            string contents = File.ReadAllTextAsync(path).Result;
            return contents;
        }

        public async static void WriteFile(string path, string contents) 
        {
            await File.WriteAllTextAsync(path, contents);
        }
    }
}
