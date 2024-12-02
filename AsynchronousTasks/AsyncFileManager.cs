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
            if (!File.Exists(path))
            {
               await  File.WriteAllTextAsync(path, contents + Environment.NewLine);
            }
            else
            {
                await File.AppendAllTextAsync(path, contents + Environment.NewLine);
            }
            }
    }
}
