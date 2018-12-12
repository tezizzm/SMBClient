using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SMBShareConsoleNET
{
    public class SmbClient : ISmbClient, IDisposable
    {
        public string UserName { get; }
        public string Password { get; }
        public string Domain { get; }
        public string UncPath { get; }

        private readonly WindowsNetworkFileShare _fileShare;

        public SmbClient(string uncPath, string userName = "", string password = "", string domain = "")
        {
            UncPath = uncPath ?? throw new ArgumentNullException(nameof(uncPath));
            UserName = userName;
            Password = password;
            Domain = domain;

            Console.WriteLine($@"Establishing credentials for user {domain}\{userName}");
            var credential = new NetworkCredential(UserName, Password, Domain);

            Console.WriteLine($"Logging into share at path {uncPath}");
            _fileShare = new WindowsNetworkFileShare(uncPath, credential);
            Console.WriteLine($"Logged into share at path {uncPath}");
        }

        public async Task WriteAsync(string file, string data)
        {
            Console.WriteLine($"Writing data to {file}");
            using (var streamWriter = new StreamWriter(file))
            {
                await streamWriter.WriteLineAsync(data);
            }
        }

        public async Task<string> ReadAsync(string file)
        {
            Console.WriteLine($"Reading data from {file}");
            using (var streamReader = new StreamReader(file))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        public void Dispose()
        {
            _fileShare?.Dispose();
        }
    }
}
