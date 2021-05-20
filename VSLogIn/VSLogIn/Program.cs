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

        static void Main(string[] args)
        {
            Console.Title = "[VS] Log In Bypass";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[VS] Bypass routine has been started!");
            while (true)
            {
                try
                {
                    Console.SetWindowPosition(0, 0);
                    Console.SetWindowSize(37, 1);
                    Process[] p = Process.GetProcessesByName("ServiceHub.IdentityHost");
                    if(p.Length != 0)
                    {
                        Process identifier = p.FirstOrDefault();
                        Suspend(identifier);
                        Console.WriteLine("[VS] Identifier bypassed!");
                        identifier.WaitForExit();
                        Resume(identifier);
                        TerminateProcess(identifier);
                    }
                    Thread.Sleep(150);
                } catch { }
            }
        }

        public static bool TerminateProcess(Process proces) // https://slaner.tistory.com/26
        {
            IntPtr hProcess = NativeMethods.OpenProcess((int)Enums.ThreadAccess.TERMINATE, false, proces.Id);
            Boolean result = false;
            if (hProcess == IntPtr.Zero)
            {
                result = false;
            }

            if (NativeMethods.TerminateProcess(hProcess, 0) != 0)
            {
                result = true;
            }
            else
            {
                result =  false;
            }

            NativeMethods.CloseHandle(hProcess);
            return result;
        }

        public static void Suspend(Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = NativeMethods.OpenThread(Enums.ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                NativeMethods.SuspendThread(pOpenThread);
            }
        }
        public static void Resume(Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = NativeMethods.OpenThread(Enums.ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                NativeMethods.ResumeThread(pOpenThread);
            }
        }
    }
}
