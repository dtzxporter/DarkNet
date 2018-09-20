using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DarkNet
{
    public class PaintResources
    {
        /// <summary>
        /// Gets an embedded image resource file from Paint.Net
        /// </summary>
        /// <param name="Name">The full file name</param>
        /// <returns>An image if found</returns>
        public static Image GetImageResource(string Name)
        {
            // Get an image resource from the paint.net config
            var Resources = Assembly.GetAssembly(Type.GetType("PaintDotNet.Resources.PdnResources,PaintDotNet.Resources"));

            var Stream = Resources.GetManifestResourceStream(Name);
            return (Stream != null) ? new Bitmap(Stream) : null;
        }
    }
}
