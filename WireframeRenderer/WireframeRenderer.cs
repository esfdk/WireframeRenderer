namespace WireframeRenderer
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The wireframe renderer.
    /// </summary>
    public class WireframeRenderer
    {
        /// <summary>
        /// The main method of the program.
        /// </summary>
        /// <param name="args">The args.</param>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Renderer());
        }
    }
}
