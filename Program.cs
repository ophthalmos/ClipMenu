namespace ClipMenu
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using Mutex singleMutex = new(true, "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}", out bool isNewInstance);
            if (isNewInstance)
            {

                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new FrmClipMenu());
            }
            else { MessageBox.Show("ClipMenu wird bereits ausgeführt!", "ClipMenu"); } // make the currently running instance jump on top of all the other windows
        }
    }
}
