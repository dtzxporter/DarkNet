using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkNet
{
    /// <summary>
    /// A class for manipulating the main toolbar in Paint.Net
    /// </summary>
    public class PaintToolbar
    {
        internal object ToolbarInstance = null;

        /// <summary>
        /// Gets a reference to the main menu strip
        /// </summary>
        /// <returns>A menustrip object</returns>
        public PaintMenuStrip GetMainMenu()
        {
            var Result = new PaintMenuStrip();

            Result.MenuInstance = ToolbarInstance.GetMemberValue("mainMenu");

            return Result;
        }

        /// <summary>
        /// Gets a reference to the aux strip
        /// </summary>
        /// <returns>A menustrip object</returns>
        public PaintMenuStrip GetAuxMenu()
        {
            var Result = new PaintMenuStrip();

            Result.MenuInstance = ToolbarInstance.GetMemberValue("auxMenu");

            return Result;
        }

        /// <summary>
        /// Gets a reference to the common controls strip
        /// </summary>
        /// <returns>A toolstrip object</returns>
        public PaintToolStrip GetCommonActionsStrip()
        {
            var Result = new PaintToolStrip();

            Result.ToolstripInstance = ToolbarInstance.GetMemberValue("commonActionsStrip");

            return Result;
        }
    }
}
