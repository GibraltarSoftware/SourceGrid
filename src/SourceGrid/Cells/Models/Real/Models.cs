using System;

namespace SourceGrid.Cells.Models
{
	public class NullValueModel : IValueModel
	{
        // This class may not really need this to be ThreadStatic, because it can't be modified...
        // ...but just to make sure, let's do it ThreadStatic, anyway.
		[ThreadStatic] private static NullValueModel t_Default;

	    public static NullValueModel Default // Multi-thread safe with [ThreadStatic] backing store
	    {
	        get
	        {
                if (t_Default == null)
                    t_Default = new NullValueModel();

	            return t_Default;
	        }
	    }

		/// <summary>
		/// Constructor
		/// </summary>
		public NullValueModel()
		{
		}

		#region IModel Members
		public object GetValue(CellContext cellContext)
		{
			return null;
		}

		public void SetValue(CellContext cellContext, object p_Value)
		{
			throw new ApplicationException("This model doesn't support editing");
		}
		public string GetDisplayText(CellContext cellContext)
		{
			return null;
		}
		#endregion
	}

	/// <summary>
	/// A model that contains the value of cell. Usually used for a Real Cell or cells with a static text.
	/// </summary>
	public class ValueModel : IValueModel
	{
		public ValueModel()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="val"></param>
		public ValueModel(object val)
		{
			m_Value = val;
		}

		private object m_Value;
		#region IModel Members

		public object GetValue(CellContext cellContext)
		{
			return m_Value;
		}

		public void SetValue(CellContext cellContext, object p_Value)
		{
			DevAge.ComponentModel.ValueEventArgs valArgs = new DevAge.ComponentModel.ValueEventArgs(p_Value);
			if (cellContext.Grid != null)
				cellContext.Grid.Controller.OnValueChanging(cellContext, valArgs);
			m_Value = valArgs.Value;
			if (cellContext.Grid != null)
				cellContext.Grid.Controller.OnValueChanged(cellContext, EventArgs.Empty);
		}
		#endregion
	}

	/// <summary>
	/// CheckBox model.
	/// </summary>
	public class CheckBox : ICheckBox
	{
		#region ICheckBox Members

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="cellContext"></param>
		/// <returns></returns>
		public CheckBoxStatus GetCheckBoxStatus(CellContext cellContext)
		{
			bool enableEdit = false;
			if (cellContext.Cell.Editor != null && cellContext.Cell.Editor.EnableEdit)
				enableEdit = true;

			object val = cellContext.Cell.Model.ValueModel.GetValue(cellContext);
			if (val == null)
				return new CheckBoxStatus(enableEdit, DevAge.Drawing.CheckBoxState.Undefined, m_Caption);
			else if (val is bool)
				return new CheckBoxStatus(enableEdit, (bool)val, m_Caption);
			else
				throw new SourceGridException("Cell value not supported for this cell. Expected bool value or null.");
		}
		/// <summary>
		/// Set the checked value
		/// </summary>
		/// <param name="cellContext"></param>
		/// <param name="pChecked"></param>
		public void SetCheckedValue(CellContext cellContext, bool? pChecked)
		{
			if (cellContext.Cell.Editor != null && cellContext.Cell.Editor.EnableEdit)
				cellContext.Cell.Editor.SetCellValue(cellContext, pChecked);
		}
		#endregion

		private string m_Caption = null;
		public string Caption
		{
			get{return m_Caption;}
			set{m_Caption = value;}
		}
	}

	public class SortableHeader : ISortableHeader
	{
		#region ISortableHeader Members

		public SortStatus GetSortStatus(CellContext cellContext)
		{
			return m_SortStatus;
		}

        public void SetSortMode(CellContext cellContext, DevAge.Drawing.HeaderSortStyle pStyle)
		{
			m_SortStatus.Style = pStyle;
		}

		#endregion

        private SortStatus m_SortStatus = new SortStatus(DevAge.Drawing.HeaderSortStyle.None, null);
		public SortStatus SortStatus
		{
			get{return m_SortStatus;}
			set{m_SortStatus = value;}
		}
	}

	public class ToolTip : IToolTipText
	{
		#region IToolTipText Members

		public string GetToolTipText(CellContext cellContext)
		{
			return m_ToolTipText;
		}

		#endregion

		private string m_ToolTipText;
		public string ToolTipText
		{
			get{return m_ToolTipText;}
			set{m_ToolTipText = value;}
		}
	}

	public class Image : IImage
	{
		public Image()
		{
		}

		public Image(System.Drawing.Image image)
		{
			mImage = image;
		}

		#region IImage Members
		/// <summary>
		/// Get the image of the specified cell. 
		/// </summary>
		/// <param name="cell"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		public System.Drawing.Image GetImage(CellContext cellContext)
		{
			return mImage;
		}
		#endregion

		private System.Drawing.Image mImage;
		public System.Drawing.Image ImageValue
		{
			get{return mImage;}
			set{mImage = value;}
		}
	}

	/// <summary>
	/// Model that implements the IImage interface, used to read the Image directly from the Value of the cell.
	/// </summary>
	public class ValueImage : IImage
	{
	    [ThreadStatic] private static ValueImage t_Default;

	    public static ValueImage Default // Multi-thread safe with [ThreadStatic] backing store
	    {
            get
            {
                if (t_Default == null)
                    t_Default = new ValueImage();

                return t_Default;
            }
	    }

		private readonly DevAge.ComponentModel.Validator.ValidatorTypeConverter imageConverter = new DevAge.ComponentModel.Validator.ValidatorTypeConverter(typeof(System.Drawing.Image));

		#region IImage Members

		public System.Drawing.Image GetImage(CellContext cellContext)
		{
			object val = cellContext.Cell.Model.ValueModel.GetValue(cellContext);
			return (System.Drawing.Image)imageConverter.ObjectToValue(val);
		}

		#endregion
	}
}
