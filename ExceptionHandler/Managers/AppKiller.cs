using System.Diagnostics;
using System.Windows.Forms;

namespace ExceptionHandler.Managers
{
    public class AppKiller : IAppKiller
    {
        public void Kill()
        {
            KillApp();
            Application.Exit();
        }

        //-- This is in a private routine for .NET security reasons
        //-- if this line of code is in a sub, the entire sub is tagged as full trust
        private static void KillApp()
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
