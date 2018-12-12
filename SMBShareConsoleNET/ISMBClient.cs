using System.Threading.Tasks;

namespace SMBShareConsoleNET
{
    public interface ISmbClient
    {
        Task<string> ReadAsync(string file);
        Task WriteAsync(string file, string data);
    }
}