using System;
using System.Runtime.InteropServices;

namespace ImpersonationSystemService.WindowsApi
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SecurityAttributes
    {
        public uint Length;
        public IntPtr SecurityDescriptor;
        public bool InheritHandle;
    }
}