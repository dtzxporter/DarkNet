using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkNet
{
    /// <summary>
    /// A class for manipulating the tool strip in Paint.Net
    /// </summary>
    public class PaintToolStrip
    {
        internal object ToolstripInstance = null;

        /// <summary>
        /// Adds a new tool item to the strip
        /// </summary>
        /// <param name="Item">The item of which to add</param>
        public void AddToolItem(ToolStripButton Item)
        {
            // Add a tool item to the control
            var Strip = ToolstripInstance as ToolStrip;
            Strip.Items.Add(Item);
        }

        /// <summary>
        /// Gets the tool strip as a built-in .net control
        /// </summary>
        /// <returns>A toolstrip object</returns>
        public ToolStrip AsToolStrip()
        {
            return ToolstripInstance as ToolStrip;
        }
    }
}
