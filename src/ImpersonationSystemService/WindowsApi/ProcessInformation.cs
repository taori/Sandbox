using System;
using System.Runtime.InteropServices;

namespace ImpersonationSystemService.WindowsApi
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ProcessInformation
    {
        public IntPtr ProcessHandle;
        public IntPtr ThreadHandle;
        public uint ProcessId;
        public uint ThreadId;
    }
}