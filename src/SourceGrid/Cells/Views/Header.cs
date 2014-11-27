using System;
using System.Drawing;
using System.Windows.Forms;
using DevAge.Drawing;

namespace SourceGrid.Cells.Views
{
	/// <summary>
	/// Summary description for a 3D Header.
	/// </summary>
	[Serializable]
	public class Header : Cell
	{
        [ThreadStatic] private static Header t_DefaultHeader;
	    [ThreadStatic] private static RectangleBorder t_DefaultBorder;
	    [ThreadStatic] private static bool t_Initialized; // false by default for each thread

	    public new static RectangleBorder DefaultBorder // Multi-thread safe with [ThreadStatic] backing store
	    {
	        get { Initialize(); return t_DefaultBorder; }
            set { Initialize(); t_DefaultBorder = value; }
	    }

	    /// <summary>
	    /// Represents a default Header, with a 3D border and a LightGray BackColor
	    /// </summary>
        /// <remarks>This default Header cell is shared across the same UI thread, but not between threads.
        /// It can cloned for a unique instance.</remarks>
        public new static Header Default // Multi-thread safe with [ThreadStatic] backing store
	    {
	        get { Initialize(); return t_DefaultHeader; }
	    }

        public new static void Initialize()
        {
            if (t_Initialized) // Only do this once per thread.
                return;

            t_DefaultBorder = RectangleBorder.NoBorder; // Make sure we set this first; it's used for new Header().
            t_Initialized = true; // Mark it initialized for the current thread so we don't overwrite them again.
            t_DefaultHeader = new Header(); // Must be after we mark us initialized, or we get infinite recursion!
        }

		#region Constructors

		static Header()
		{
			Initialize(); // Initialize for the current thread when first loaded.
		}

		/// <summary>
		/// Use default setting
		/// </summary>
		public Header()
		{
            Background = new DevAge.Drawing.VisualElements.HeaderThemed();
            Border = Header.DefaultBorder;
		}

		/// <summary>
		/// Copy constructor.  This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
		/// </summary>
		/// <param name="p_Source"></param>
		public Header(Header p_Source):base(p_Source)
		{
            Background = (DevAge.Drawing.VisualElements.IHeader)p_Source.Background.Clone();
        }
		#endregion

		#region Clone
		/// <summary>
		/// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
		/// </summary>
		/// <returns></returns>
		public override object Clone()
		{
			return new Header(this);
		}
		#endregion

        #region Visual Elements

        public new DevAge.Drawing.VisualElements.IHeader Background
        {
            get { return (DevAge.Drawing.VisualElements.IHeader)base.Background; }
            set { base.Background = value; }
        }

        protected override void PrepareView(CellContext context)
        {
            base.PrepareView(context);

            if (context.CellRange.Contains(context.Grid.MouseDownPosition))
                Background.Style = DevAge.Drawing.ControlDrawStyle.Pressed;
            else if (context.CellRange.Contains(context.Grid.MouseCellPosition))
                Background.Style = DevAge.Drawing.ControlDrawStyle.Hot;
            else
                Background.Style = DevAge.Drawing.ControlDrawStyle.Normal;
        }
        #endregion
	}
}
