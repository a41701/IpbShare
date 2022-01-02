using IpbShare.Infrastructure;
using System;
using System.Threading.Tasks;

namespace IpbShareConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("IPB Share");

            DateTime dt = DateTime.Now;

            using var uow = new UnitOfWork();
        }
    }
}
