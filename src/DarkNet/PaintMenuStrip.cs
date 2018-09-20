using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkNet
{
    /// <summary>
    /// A class for manipulating the menu strip in Paint.Net
    /// </summary>
    public class PaintMenuStrip
    {
        internal object MenuInstance = null;

        /// <summary>
        /// Adds a new menu item to the strip
        /// </summary>
        /// <param name="Item">The item of which to add</param>
        public void AddMenuItem(ToolStripMenuItem Item)
        {
            // Add a menu item to the control
            (MenuInstance as MenuStrip).Items.Add(Item);
        }

        /// <summary>
        /// Gets a reference to the layers menu item
        /// </summary>
        /// <returns>A toolstrip item</returns>
        public ToolStripMenuItem GetLayersMenu()
        {
            var MenuItem = MenuInstance.GetMemberValue("layersMenu") as ToolStripMenuItem;

            // Ensure the item is prepared
            InitializeStripItem(MenuItem);

            return MenuItem;
        }

        /// <summary>
        /// Gets a reference to the view menu item
        /// </summary>
        /// <returns>A toolstrip item</returns>
        public ToolStripMenuItem GetViewMenu()
        {
            var MenuItem = MenuInstance.GetMemberValue("viewMenu") as ToolStripMenuItem;

            // Ensure the item is prepared
            InitializeStripItem(MenuItem);

            return MenuItem;
        }

        /// <summary>
        /// Gets a reference to the image menu item
        /// </summary>
        /// <returns>A toolstrip item</returns>
        public ToolStripMenuItem GetImageMenu()
        {
            var MenuItem = MenuInstance.GetMemberValue("imageMenu") as ToolStripMenuItem;

            // Ensure the item is prepared
            InitializeStripItem(MenuItem);

            return MenuItem;
        }

        /// <summary>
        /// Gets a reference to the file menu item
        /// </summary>
        /// <returns>A toolstrip item</returns>
        public ToolStripMenuItem GetFileMenu()
        {
            var MenuItem = MenuInstance.GetMemberValue("fileMenu") as ToolStripMenuItem;

            // Ensure the item is prepared
            InitializeStripItem(MenuItem);

            return MenuItem;
        }

        /// <summary>
        /// Gets a reference to the effects menu item
        /// </summary>
        /// <returns>A toolstrip item</returns>
        public ToolStripMenuItem GetEffectsMenu()
        {
            var MenuItem = MenuInstance.GetMemberValue("effectsMenu") as ToolStripMenuItem;

            // Ensure the item is prepared
            InitializeStripItem(MenuItem);

            return MenuItem;
        }

        /// <summary>
        /// Gets a reference to the edit menu item
        /// </summary>
        /// <returns>A toolstrip item</returns>
        public ToolStripMenuItem GetEditMenu()
        {
            var MenuItem = MenuInstance.GetMemberValue("editMenu") as ToolStripMenuItem;

            // Ensure the item is prepared
            InitializeStripItem(MenuItem);

            return MenuItem;
        }

        /// <summary>
        /// Gets a reference to the adjustments menu item
        /// </summary>
        /// <returns>A toolstrip item</returns>
        public ToolStripMenuItem GetAdjustmentsMenu()
        {
            var MenuItem = MenuInstance.GetMemberValue("adjustmentsMenu") as ToolStripMenuItem;

            // Ensure the item is prepared
            InitializeStripItem(MenuItem);

            return MenuItem;
        }

        /// <summary>
        /// Gets the menu strip as a built-in .net control
        /// </summary>
        /// <returns>A menustrip object</returns>
        public MenuStrip AsMenuStrip()
        {
            return MenuInstance as MenuStrip;
        }

        /// <summary>
        /// Creates a new menuitem for Paint.Net with the given properties
        /// </summary>
        /// <param name="text">The display text</param>
        /// <param name="image">The display image</param>
        /// <param name="onClick">Click event handler</param>
        /// <param name="keyStroke">The shortcut keys</param>
        /// <returns>The menuitem object</returns>
        public static ToolStripMenuItem MakeMenuItem(string text, Image image, EventHandler onClick, Keys keyStroke = Keys.None)
        {
            // Get the type and make a new instance
            var PdnMenuItem = Type.GetType("PaintDotNet.Menus.PdnMenuItem,PaintDotNet"); // Ctor: Text, Image, OnClick
            var Result = Activator.CreateInstance(PdnMenuItem) as ToolStripMenuItem;

            // Set properties
            Result.Text = text;
            Result.Image = image;
            if (onClick != null)
            {
                Result.Click += onClick;
            }

            // We are a plugin making this
            Result.SetMemberValue("IsPlugin", true);

            // Set shortcut
            if (keyStroke != Keys.None)
                Result.ShortcutKeys = keyStroke;

            return Result;
        }

        /// <summary>
        /// Patches the VerifyItems routine in the item instance
        /// </summary>
        /// <param name="Item">The item of which to patch</param>
        private static void InitializeStripItem(ToolStripMenuItem Item)
        {
            // Patch out the VerifyItems check...
            var VerifyPatch = Item.GetMemberFunction("VerifyItems");
            var VerifyNop = typeof(PaintMenuStrip).GetMethod("VerifyItemsNop", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            // Patch if we found it
            if (VerifyPatch != null)
                Global.HookingInstance.Hook((MethodInfo)VerifyPatch, (MethodInfo)VerifyNop);
        }

        // Used to bypass verification, lmfao what a joke
        private static void VerifyItemsNop()
        {
        }
    }
}
