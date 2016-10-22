using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Unix;
using Mono.Unix.Native;
using Nancy.Hosting.Self;

namespace NancyMonoDemo
{
    class Program
    {
        const string DefaultUrl = "http://localhost:8081";

        static void Main(string[] args)
        {
            Console.WriteLine($"Starting Nancy {DefaultUrl}...");

            var host = new NancyHost(new Uri(DefaultUrl));
            host.Start();

            if (IsRunningOnMono())
            {
                var terminationSignals = GetUnixTerminationSignals();
                UnixSignal.WaitAny(terminationSignals);
            }
            else
            {
                Console.ReadLine();
            }

            host.Stop();
        }

        private static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        private static UnixSignal[] GetUnixTerminationSignals()
        {
            return new[]
            {
                new UnixSignal(Signum.SIGINT),
                new UnixSignal(Signum.SIGTERM),
                new UnixSignal(Signum.SIGQUIT),
                new UnixSignal(Signum.SIGHUP)
            };
        }
    }
}
