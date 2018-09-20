using PaintDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkNet
{
    /// <summary>
    /// A class for manipulating a selection in Paint.Net
    /// </summary>
    public class PaintSelection
    {
        internal object SelectionInstance = null;

        /// <summary>
        /// Gets the selection as a region
        /// </summary>
        /// <returns>a region object</returns>
        public PdnRegion GetAsRegion()
        {
            return SelectionInstance.CallMemberFunction("CreateRegion") as PdnRegion;
        }

        /// <summary>
        /// Returns whether or not a selection has been made
        /// </summary>
        /// <returns>True if a selection exists, false otherwise</returns>
        public bool IsEmpty()
        {
            var Result = SelectionInstance.GetMemberValue("IsEmpty") as bool?;
            if (Result == null)
            {
                return true;
            }
            else if (Result == true)
            {
                return true;
            }
            return false;
        }
    }
}
