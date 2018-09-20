using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkNet
{
    /// <summary>
    /// A class that allows manipulating the main form
    /// </summary>
    public class PaintForm
    {
        /// <summary>
        /// Gets a reference to the main form, used in reflection
        /// </summary>
        /// <returns></returns>
        public static Form GetMainWindow()
        {
            // Return the main form instance
            var Instance = Form.FromHandle(Process.GetCurrentProcess().MainWindowHandle) as Form;
            while (Instance == null)
            {
                Instance = Form.FromHandle(Process.GetCurrentProcess().MainWindowHandle) as Form;
            }
            return Instance;
        }
    }
}
