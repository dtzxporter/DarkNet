using PaintDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkNet
{
    /// <summary>
    /// A class for manipulating a document in Paint.Net
    /// </summary>
    public class PaintDocument
    {
        internal object DocumentInstance = null;

        /// <summary>
        /// Adds a layer to the document
        /// </summary>
        /// <param name="PaintLayer">The layer of which to add</param>
        public void AddLayer(Layer PaintLayer)
        {
            var LayerList = DocumentInstance.GetMemberValue("layers") as LayerList;
            LayerList.Add(PaintLayer);
        }

        /// <summary>
        /// Gets a list of layers in the document
        /// </summary>
        /// <returns>A list of layer objects</returns>
        public List<Layer> GetLayers()
        {
            return DocumentInstance.GetMemberValue("layers") as List<Layer>;
        }

        /// <summary>
        /// Invalidate the render of the current document
        /// </summary>
        public void Invalidate()
        {
            DocumentInstance.CallMemberFunction("Invalidate");
        }
    }
}
