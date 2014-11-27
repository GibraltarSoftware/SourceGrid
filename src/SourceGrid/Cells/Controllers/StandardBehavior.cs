using System;
using System.Windows.Forms;

namespace SourceGrid.Cells.Controllers
{
	/// <summary>
	/// Common behavior of the cell. 
	/// </summary>
    /// <remarks>This controller can be shared between multiple cells and is usually used as the default Grid.Controller.
    /// Removing this controller can cause unexpected behaviors.</remarks>
	public class StandardBehavior : ControllerBase
	{
	    [ThreadStatic] private static StandardBehavior t_Default;

	    /// <summary>
	    /// The default behavior of a cell.
	    /// </summary>
        /// <remarks>This default StandardBehavior controller is shared across the same UI thread, but not between threads.</remarks>
        public static StandardBehavior Default // Multi-thread safe with [ThreadStatic] backing store
	    {
	        get
	        {
	            if (t_Default == null)
                    t_Default = new StandardBehavior();

	            return t_Default;
	        }
	    }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnKeyDown (CellContext sender, KeyEventArgs e)
		{
			base.OnKeyDown(sender, e);

			if (e.KeyCode == Keys.F2 && 
				sender.Cell.Editor != null && ((sender.Cell.Editor.EditableMode & EditableMode.F2Key) == EditableMode.F2Key))
			{
				e.Handled = true;
				sender.StartEdit();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnKeyPress (CellContext sender, KeyPressEventArgs e)
		{
			base.OnKeyPress(sender, e);

			if ( sender.Cell.Editor != null &&  
				(sender.Cell.Editor.EditableMode & EditableMode.AnyKey) == EditableMode.AnyKey && 
				sender.IsEditing() == false &&
				char.IsControl( e.KeyChar ) == false )
			{
				e.Handled = true;
				sender.StartEdit();
				sender.Cell.Editor.SendCharToEditor(e.KeyChar);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnDoubleClick (CellContext sender, EventArgs e)
		{
			base.OnDoubleClick(sender, e);

			if ( sender.Cell.Editor != null && 
				(sender.Cell.Editor.EditableMode & EditableMode.DoubleClick) == EditableMode.DoubleClick &&
				sender.Grid.Selection.ActivePosition == sender.Position)
				sender.StartEdit();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnClick (CellContext sender, EventArgs e)
		{
			base.OnClick(sender, e);

			if ( sender.Cell.Editor != null && 
				(sender.Cell.Editor.EditableMode & EditableMode.SingleClick) == EditableMode.SingleClick &&
				sender.Grid.Selection.ActivePosition == sender.Position)
				sender.StartEdit();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnFocusEntered(CellContext sender, EventArgs e)
		{
			base.OnFocusEntered(sender, e);

			//If not visible I move the scroll to show it
			sender.Grid.ShowCell(sender.Position, true);

			//Getsione dell'edit sul focus, non lo metto all'interno della cella perch� un utente potrebbe chiamare direttamente il metodo SetFocusCell senza passare dalla cella
			if ( sender.Cell.Editor != null && (sender.Cell.Editor.EditableMode & EditableMode.Focus) == EditableMode.Focus)
				sender.StartEdit();

			if (sender.Grid!=null)
				sender.Grid.InvalidateCell(sender.Position);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void OnFocusLeft(CellContext sender, EventArgs e)
		{
			base.OnFocusLeft (sender, e);

			if (sender.Grid!=null)
				sender.Grid.InvalidateCell(sender.Position);
		}


		/// <summary>
		/// Fired when the SetValue method is called.
		/// </summary>
		/// <param name="e"></param>
		public override void OnValueChanged(CellContext sender, EventArgs e)
		{
			base.OnValueChanged(sender, e);

			if (sender.Grid!=null)
				sender.Grid.InvalidateCell(sender.Position);
		}

		/// <summary>
		/// Fired when editing is ended
		/// </summary>
		/// <param name="e"></param>
		public override void OnEditEnded(CellContext sender, EventArgs e)
		{
			base.OnEditEnded (sender, e);

			//Invalidate the selection to redraw the selection border
			sender.Grid.Selection.Invalidate();
		}

		public override void OnEditStarting(CellContext sender, System.ComponentModel.CancelEventArgs e)
		{
			base.OnEditStarting (sender, e);

			//Invalidate the selection to redraw the selection border
			sender.Grid.Selection.Invalidate();
		}

        public override bool CanReceiveFocus(CellContext sender, EventArgs e)
        {
            //Return false if the row or the column is not visible
            if (sender.Grid.Columns.IsColumnVisible(sender.Position.Column) == false)
                return false;
            if (sender.Grid.Rows.IsRowVisible(sender.Position.Row) == false)
                return false;

            return base.CanReceiveFocus(sender, e);
        }
	}
}
