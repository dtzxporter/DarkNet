using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkNet
{
    /// <summary>
    /// A class for manipulating the main workspace in Paint.Net
    /// </summary>
    public class PaintWorkspace
    {
        internal object WorkspaceInstance = null;

        /// <summary>
        /// Get the workspace from the given main instance
        /// </summary>
        /// <param name="Instance">The main form instance</param>
        /// <returns>A workspace object</returns>
        public static PaintWorkspace GetWorkspaceFromInstance(Form Instance)
        {
            var Result = new PaintWorkspace();

            Result.WorkspaceInstance = Instance.GetMemberValue("appWorkspace");

            return Result;
        }

        /// <summary>
        /// Creates a new DocumentWorkspace in the AppWorkspace
        /// </summary>
        /// <returns>The new DocumentWorkspace</returns>
        public PaintDocumentWorkspace CreateNewDocumentWorkspace()
        {
            return new PaintDocumentWorkspace() { DocumentInstance = WorkspaceInstance.CallMemberFunction("AddNewDocumentWorkspace") };
        }

        /// <summary>
        /// Removes the given workspace from the AppWorkspace
        /// </summary>
        /// <param name="Workspace">The DocumentWorkspace to remove</param>
        public void RemoveDocumentWorkspace(PaintDocumentWorkspace Workspace)
        {
            WorkspaceInstance.CallMemberFunctionArgs("RemoveDocumentWorkspace", null, new object[] { Workspace.DocumentInstance });
        }

        /// <summary>
        /// Gets a reference to the toolbar control
        /// </summary>
        /// <returns>A toolbar object</returns>
        public PaintToolbar GetToolbar()
        {
            var Result = new PaintToolbar();

            Result.ToolbarInstance = WorkspaceInstance.GetMemberValue("toolBar");

            return Result;
        }

        /// <summary>
        /// Gets a reference to the current documents
        /// </summary>
        /// <returns>A list of document workspace objects</returns>
        public List<PaintDocumentWorkspace> GetDocumentWorkspaces()
        {
            var Result = new List<PaintDocumentWorkspace>();

            // Fetch the documents as a list of themselves
            var Documents = WorkspaceInstance.GetMemberValue("documentWorkspaces") as List<object>;

            // Iterate and generate documents
            foreach (var Document in Documents)
            {
                Result.Add(new PaintDocumentWorkspace() { DocumentInstance = Document });
            }

            return Result;
        }

        /// <summary>
        /// Gets a reference to the current active document
        /// </summary>
        /// <returns>The active document workspace object</returns>
        public PaintDocumentWorkspace GetActiveDocumentWorkspace()
        {
            var Result = new PaintDocumentWorkspace();

            Result.DocumentInstance = WorkspaceInstance.GetMemberValue("activeDocumentWorkspace");

            return Result;
        }

        /// <summary>
        /// Execute a function on the main thread
        /// </summary>
        /// <param name="Task">The function to run</param>
        public void RunOnMainThread(Action Task)
        {
            var WorkspaceControl = WorkspaceInstance as UserControl;
            WorkspaceControl.Invoke(Task);
        }
    }
}
