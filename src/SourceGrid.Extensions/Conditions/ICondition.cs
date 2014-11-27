using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGrid.Conditions
{
    public interface ICondition
    {
        bool Evaluate(DataGridColumn column, int gridRow, object itemRow);

        SourceGrid.Cells.ICellVirtual ApplyCondition(SourceGrid.Cells.ICellVirtual cell);
    }
}
