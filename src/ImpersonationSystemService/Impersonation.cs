using System;
using System.Runtime.InteropServices;

namespace ImpersonationSystemService
{[StructLayout(LayoutKind.Sequential)]
   internal struct PROCESS_INFORMATION
   {
       public IntPtr hProcess;
       public IntPtr hThread;
       public uint dwProcessId;
       public uint dwThreadId;
   }



   [StructLayout(LayoutKind.Sequential)]
   internal struct SECURITY_ATTRIBUTES
   {
       public uint nLength;
       public IntPtr lpSecurityDescriptor;
       public bool bInheritHandle;
   }


   [StructLayout(LayoutKind.Sequential)]
   public struct STARTUPINFO
   {
       public uint cb;
       public string lpReserved;
       public string lpDesktop;
       public string lpTitle;
       public uint dwX;
       public uint dwY;
       public uint dwXSize;
       public uint dwYSize;
       public uint dwXCountChars;
       public uint dwYCountChars;
       public uint dwFillAttribute;
       public uint dwFlags;
       public short wShowWindow;
       public short cbReserved2;
       public IntPtr lpReserved2;
       public IntPtr hStdInput;
       public IntPtr hStdOutput;
       public IntPtr hStdError;

   }

   internal enum SECURITY_IMPERSONATION_LEVEL
   {
       SecurityAnonymous,
       SecurityIdentification,
       SecurityImpersonation,
       SecurityDelegation
   }

   internal enum TOKEN_TYPE
   {
       TokenPrimary = 1,
       TokenImpersonation
   }
}