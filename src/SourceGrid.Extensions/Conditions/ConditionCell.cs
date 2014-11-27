using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGrid.Conditions
{
    public class ConditionCell : ICondition
    {
        public ConditionCell(SourceGrid.Cells.ICellVirtual cell)
        {
            mCell = cell;
        }

        public delegate bool EvaluateFunctionDelegate(DataGridColumn column, int gridRow, object itemRow);

        public EvaluateFunctionDelegate EvaluateFunction;

        private SourceGrid.Cells.ICellVirtual mCell;
        public SourceGrid.Cells.ICellVirtual Cell
        {
            get { return mCell; }
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
            return Cell;
        }
        #endregion
    }
}
