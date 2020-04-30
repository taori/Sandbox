using System;
using System.Runtime.InteropServices;

namespace ImpersonationSystemService.WindowsApi
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SecurityAttributes
    {
        public uint nLength;
        public IntPtr lpSecurityDescriptor;
        public bool bInheritHandle;
    }
}