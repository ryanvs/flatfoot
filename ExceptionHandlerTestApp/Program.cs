using ExceptionHandler.Mail;
using ExceptionHandler.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExceptionHandlerTestApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var exBuilder = new ExceptionMessageBuilder(new AssemblyInfo());
            var postalWorker = new SmtpPostalWorker()
            {
                Host = "TODO",
                Port = 25,
                EnableSsl = false,
            };
            var exNotify = new ExceptionNotification(exBuilder, new MailClient(postalWorker));
            var appKiller = new AppKiller();
            var exManager = new UnhandledExceptionManager(exNotify, appKiller);
            exManager.AddHandler();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
