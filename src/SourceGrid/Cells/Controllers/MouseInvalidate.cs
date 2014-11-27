using System;
using System.Windows.Forms;

namespace SourceGrid.Cells.Controllers
{
	/// <summary>
	/// A behavior that invalidates the cell on mouse events
	/// </summary>
	public class MouseInvalidate : ControllerBase
	{
        [ThreadStatic] private static MouseInvalidate t_Default;

	    /// <summary>
	    /// Default implementation.
	    /// </summary>
        /// <remarks>This default MouseInvalidate controller is shared across the same UI thread, but not between threads.</remarks>
        public static MouseInvalidate Default // Multi-thread safe with [ThreadStatic] backing store
	    {
	        get
	        {
	            if (t_Default == null)
                    t_Default = new MouseInvalidate();

	            return t_Default;
	        }
	    }

		/// <summary>
		/// Constructor
		/// </summary>
		public MouseInvalidate()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnMouseDown(CellContext sender, MouseEventArgs e)
		{
			base.OnMouseDown (sender, e);

			sender.Grid.InvalidateCell(sender.Position);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnMouseUp(CellContext sender, MouseEventArgs e)
		{
			base.OnMouseUp (sender, e);

			sender.Grid.InvalidateCell(sender.Position);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnMouseEnter(CellContext sender, EventArgs e)
		{
			base.OnMouseEnter (sender, e);

			sender.Grid.InvalidateCell(sender.Position);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnMouseLeave(CellContext sender, EventArgs e)
		{
			base.OnMouseLeave (sender, e);

			sender.Grid.InvalidateCell(sender.Position);
		}
	}
}
