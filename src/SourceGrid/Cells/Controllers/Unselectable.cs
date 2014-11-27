using System;

namespace SourceGrid.Cells.Controllers
{
	/// <summary>
	/// Implements a behavior that cannot receive the focus. This behavior can be shared between multiple cells.
	/// </summary>
	public class Unselectable : ControllerBase
	{
	    [ThreadStatic] private static Unselectable t_Default;

	    public static Unselectable Default // Multi-thread safe with [ThreadStatic] backing store
	    {
	        get
	        {
	            if (t_Default == null)
                    t_Default = new Unselectable();

	            return t_Default;
	        }
	    }

		public override void OnFocusEntering(CellContext sender, System.ComponentModel.CancelEventArgs e)
		{
			base.OnFocusEntering (sender, e);

			e.Cancel = !CanReceiveFocus(sender, e);
		}
		public override bool CanReceiveFocus(CellContext sender, EventArgs e)
		{
			return false;
		}
	}
}
