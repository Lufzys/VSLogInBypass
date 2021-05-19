using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VSLogIn
{
    class Program
    {
        [DllImport("kernel32")]
        public static extern Int32 TerminateProcess(IntPtr hProcess, Int32 ExitCode);

        [DllImport("kernel32")]
        public static extern void CloseHandle(IntPtr hProcess);

        [DllImport("kernel32")]
        public static extern IntPtr OpenProcess(Int32 Access, Boolean InheritHandle, Int32 ProcessId);
        public const Int32 PROCESS_TERMINATE = 0x1;

        static void Main(string[] args)
        {
            Console.Title = "[VS] Log In Bypass";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[VS] Bypass routine has been started!");
            while(true)
            {
                foreach(Process p in Process.GetProcessesByName("ServiceHub.IdentityHost"))
                {
                    TerminateProcessFromPID(p.Id);
                }
                Thread.Sleep(150);
            }
        }

        public static bool TerminateProcessFromPID(int pId) // https://slaner.tistory.com/26
        {
            IntPtr hProcess = OpenProcess(PROCESS_TERMINATE, false, pId);
            Boolean result = false;
            if (hProcess == IntPtr.Zero)
            {
                result = false;
            }

            if (TerminateProcess(hProcess, 0) != 0)
            {
                result = true;
            }
            else
            {
                result =  false;
            }

            CloseHandle(hProcess);
            return result;
        }
    }
}
