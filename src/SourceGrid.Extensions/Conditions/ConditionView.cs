using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGrid.Conditions
{
    public class ConditionView : ICondition
    {
        public ConditionView(SourceGrid.Cells.Views.IView view)
        {
            mView = view;
        }

        public delegate bool EvaluateFunctionDelegate(DataGridColumn column, int gridRow, object itemRow);

        public EvaluateFunctionDelegate EvaluateFunction;

        private SourceGrid.Cells.Views.IView mView;
        public SourceGrid.Cells.Views.IView View
        {
            get { return mView; }
        }

        #region ICondition Members
        public bool Evaluate(DataGridColumn column, int gridRow, object itemRow)
        {
            if (EvaluateFunction == null)
                return false;

            return EvaluateFunction(column, gridRow, itemRow);
        }

        public SourceGrid.Cells.ICellVirtual ApplyCondition(SourceGrid.Cells.ICellVirtual cell)
        {
            SourceGrid.Cells.ICellVirtual copied = cell.Copy();
            copied.View = View;

            return copied;
        }
        #endregion
    }
}
