using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace ImpersonationSystemService.WindowsApi
{
    // https://social.msdn.microsoft.com/Forums/vstudio/en-US/0c0ca087-5e7b-4046-93cb-c7b3e48d0dfb/how-run-client-application-as-a-windows-service-in-c?forum=csharpgeneral

    public class ProcessAsUser
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool CreateProcessAsUser(
            IntPtr hToken,
            string lpApplicationName,
            string lpCommandLine,
            ref SecurityAttributes lpProcessAttributes,
            ref SecurityAttributes lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            ref StartupInfo lpStartupInfo,
            out ProcessInformation lpProcessInformation);

        [DllImport("advapi32.dll", EntryPoint = "DuplicateTokenEx", SetLastError = true)]
        private static extern bool DuplicateTokenEx(
            IntPtr hExistingToken,
            uint dwDesiredAccess,
            ref SecurityAttributes lpThreadAttributes,
            int ImpersonationLevel,
            int dwTokenType,
            ref IntPtr phNewToken);


        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(
            IntPtr ProcessHandle,
            uint DesiredAccess,
            ref IntPtr TokenHandle);

        [DllImport("userenv.dll", SetLastError = true)]
        private static extern bool CreateEnvironmentBlock(
            ref IntPtr lpEnvironment,
            IntPtr hToken,
            bool bInherit);

        [DllImport("userenv.dll", SetLastError = true)]
        private static extern bool DestroyEnvironmentBlock(
            IntPtr lpEnvironment);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(
            IntPtr hObject);

        private const short SW_SHOW = 5;
        private const uint TOKEN_QUERY = 0x0008;
        private const uint TOKEN_DUPLICATE = 0x0002;
        private const uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        private const int GENERIC_ALL_ACCESS = 0x10000000;
        private const int STARTF_USESHOWWINDOW = 0x00000001;
        private const int STARTF_FORCEONFEEDBACK = 0x00000040;
        private const uint CREATE_UNICODE_ENVIRONMENT = 0x00000400;

        private static bool LaunchProcessAsUser(string cmdLine, IntPtr token, IntPtr envBlock)
        {
            bool result = false;

            ProcessInformation pi = new ProcessInformation();
            SecurityAttributes saProcess = new SecurityAttributes();
            SecurityAttributes saThread = new SecurityAttributes();
            saProcess.nLength = (uint) Marshal.SizeOf(saProcess);
            saThread.nLength = (uint) Marshal.SizeOf(saThread);

            StartupInfo si = new StartupInfo();
            si.cb = (uint) Marshal.SizeOf(si);

            //if this member is NULL, the new process inherits the desktop
            //and window station of its parent process. If this member is
            //an empty string, the process does not inherit the desktop and
            //window station of its parent process; instead, the system
            //determines if a new desktop and window station need to be created.
            //If the impersonated user already has a desktop, the system uses the
            //existing desktop.

            si.lpDesktop = @"WinSta0\Default"; //Modify as needed
            si.dwFlags = STARTF_USESHOWWINDOW | STARTF_FORCEONFEEDBACK;
            si.wShowWindow = SW_SHOW;
            //Set other si properties as required.

            result = CreateProcessAsUser(
                token,
                null,
                cmdLine,
                ref saProcess,
                ref saThread,
                false,
                CREATE_UNICODE_ENVIRONMENT,
                envBlock,
                null,
                ref si,
                out pi);

            if (result == false)
            {
                int error = Marshal.GetLastWin32Error();
                string message = string.Format("CreateProcessAsUser Error: {0}", error);
                Debug.WriteLine(message);
            }

            return result;
        }


        private static IntPtr GetPrimaryToken(int processId)
        {
            IntPtr token = IntPtr.Zero;
            IntPtr primaryToken = IntPtr.Zero;
            bool retVal = false;
            Process p = null;

            try
            {
                p = Process.GetProcessById(processId);
            }

            catch (ArgumentException)
            {
                string details = string.Format("ProcessID {0} Not Available", processId);
                Debug.WriteLine(details);
                throw;
            }

            //Gets impersonation token
            retVal = OpenProcessToken(p.Handle, TOKEN_DUPLICATE, ref token);
            if (retVal == true)
            {
                SecurityAttributes sa = new SecurityAttributes();
                sa.nLength = (uint) Marshal.SizeOf(sa);

                //Convert the impersonation token into Primary token
                retVal = DuplicateTokenEx(
                    token,
                    TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_QUERY,
                    ref sa,
                    (int) SecurityImpersonationLevel.SecurityIdentification,
                    (int) TokenType.TokenPrimary,
                    ref primaryToken);

                //Close the Token that was previously opened.
                CloseHandle(token);
                if (retVal == false)
                {
                    string message = string.Format("DuplicateTokenEx Error: {0}", Marshal.GetLastWin32Error());
                    Debug.WriteLine(message);
                }
            }

            else
            {
                string message = string.Format("OpenProcessToken Error: {0}", Marshal.GetLastWin32Error());
                Debug.WriteLine(message);
            }

            //We'll Close this token after it is used.
            return primaryToken;
        }

        private static IntPtr GetEnvironmentBlock(IntPtr token)
        {
            IntPtr envBlock = IntPtr.Zero;
            bool retVal = CreateEnvironmentBlock(ref envBlock, token, false);
            if (retVal == false)
            {
                //Environment Block, things like common paths to My Documents etc.
                //Will not be created if "false"
                //It should not adversley affect CreateProcessAsUser.

                string message = string.Format("CreateEnvironmentBlock Error: {0}", Marshal.GetLastWin32Error());
                Debug.WriteLine(message);
            }

            return envBlock;
        }

        /// <summary>
        /// Executes a process using the process id of the first explorer process it finds.
        /// </summary>
        /// <param name="command">command to execute</param>
        /// <returns></returns>
        public static bool Launch(string command) => Launch(command, int.MinValue);

        /// <summary>
        /// Executes a process using the process id which is specified
        /// </summary>
        /// <param name="command">command to execute</param>
        /// <param name="processId">id of process which is used to impersonate the command</param>
        /// <returns></returns>
        public static bool Launch(string command, int processId)
        {
            bool ret = false;

            //Either specify the processID explicitly
            //Or try to get it from a process owned by the user.
            //In this case assuming there is only one explorer.exe
            
            if (processId < 0)
                processId = GetExplorerProcessId();

            if (processId > 1)
            {
                IntPtr token = GetPrimaryToken(processId);

                if (token != IntPtr.Zero)
                {
                    IntPtr envBlock = GetEnvironmentBlock(token);
                    ret = LaunchProcessAsUser(command, token, envBlock);
                    if (envBlock != IntPtr.Zero)
                        DestroyEnvironmentBlock(envBlock);

                    CloseHandle(token);
                }
            }

            return ret;
        }

        private static int GetExplorerProcessId()
        {
            return Process.GetProcessesByName("explorer").FirstOrDefault()?.Id ?? int.MinValue;
        }
    }
}