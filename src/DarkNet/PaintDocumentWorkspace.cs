using PaintDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkNet
{
    /// <summary>
    /// A class for manipulating the documents in Paint.Net
    /// </summary>
    public class PaintDocumentWorkspace
    {
        internal object DocumentInstance = null;

        // TODO: Figure out history / undo logic and implement when adding / removing layers

        /// <summary>
        /// Gets a reference to the document itself
        /// </summary>
        /// <returns>A document object</returns>
        public PaintDocument GetDocument()
        {
            var Result = new PaintDocument();

            Result.DocumentInstance = DocumentInstance.GetMemberValue("Document");

            return Result;
        }

        /// <summary>
        /// Adds a layer to the document
        /// </summary>
        /// <param name="PaintLayer">The layer of which to add</param>
        public void AddLayer(Layer PaintLayer)
        {
            // Check if we need to invoke first!
            var DocumentWorkControl = DocumentInstance as Control;
            if (DocumentWorkControl.InvokeRequired)
            {
                DocumentWorkControl.Invoke((Action)delegate
                {
                    AddLayer(PaintLayer);
                });
            }
            else
            {
                var Document = this.GetDocument();
                Document.AddLayer(PaintLayer);
                Document.Invalidate();
            }
        }

        /// <summary>
        /// Gets the currently active layer
        /// </summary>
        /// <returns>A layer object</returns>
        public Layer GetActiveLayer()
        {
            return DocumentInstance.GetMemberValue("activeLayer") as Layer;
        }

        /// <summary>
        /// Returns a list of layers in the document
        /// </summary>
        /// <returns>A list of layer objects</returns>
        public List<Layer> GetLayers()
        {
            return this.GetDocument().GetLayers();
        }

        /// <summary>
        /// Gets the current selection
        /// </summary>
        /// <returns>A selection object</returns>
        public PaintSelection GetCurrentSelection()
        {
            var Result = new PaintSelection();

            Result.SelectionInstance = DocumentInstance.GetMemberValue("selection");

            return Result;
        }

        /// <summary>
        /// Save the DocumentWorkspace as a file
        /// </summary>
        public void DoSaveAs()
        {
            this.DocumentInstance.CallMemberFunction("DoSaveAs");
        }

        /// <summary>
        /// Sets the Document on the Workspace
        /// </summary>
        /// <param name="Doc">The document to set</param>
        public void SetDocument(ref Document Doc)
        {
            // Setup document items
            this.DocumentInstance.SetMemberValue("Document", Doc);
        }
        
        /// <summary>
        /// Save the given layer as a new file
        /// </summary>
        /// <param name="PaintLayer">The layer to save</param>
        public void SaveLayerAs(PaintWorkspace Workspace, Layer PaintLayer)
        {
            // We need to make a new workspace for saving...
            var NewWorkspace = Workspace.CreateNewDocumentWorkspace();

            // Make a new document, and clone the layer to the document.
            var NewDoc = new Document(PaintLayer.Width, PaintLayer.Height);
            NewDoc.Layers.Clear();
            NewDoc.Layers.Add(PaintLayer.CloneT<Layer>());

            // Setup document
            NewWorkspace.SetDocument(ref NewDoc);

            // Invoke save
            this.DoSaveAs();

            // Remove the document from the workspace
            Workspace.RemoveDocumentWorkspace(NewWorkspace);
        }
    }
}