using System;
using System.Threading.Tasks;

namespace SMBShareConsoleNET
{
    internal class SmbConsole
    {
        private static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Process().Wait();
                    Task.Delay(10000000);
                }
            }
            catch (AggregateException ae)
            {
                var flattenedException = ae.Flatten();
                foreach (var innerException in flattenedException.InnerExceptions)
                {
                    Console.WriteLine(
                        $"{innerException.GetType()}\n\n{innerException.Message}\n\n{innerException.StackTrace}");
                }
            }
        }

        private static async Task Process()
        {
            const string shareLocation = @"\\\sharename";
            const string readFileName = "test.txt";
            const string writeFileName = "writeFile.txt";
            const string data = "I'm fabulous";

            Console.WriteLine("Starting SMB Client Console Application");
            using (var client = new SmbClient(shareLocation, "", @""))
            {
                Console.WriteLine("Starting file operations on share!");
                var fileContents = await client.ReadAsync(shareLocation +"\\" + readFileName);
                Console.WriteLine($"Contents of file {readFileName} : {fileContents}");
                await client.WriteAsync(shareLocation + "\\" + writeFileName, data);
                Console.WriteLine($"Wrote {data} to file {writeFileName}");
            }
        }
    }
}
