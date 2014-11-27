using System;
using System.Collections.Generic;
using System.Text;

namespace DevAge.ComponentModel
{
    /// <summary>
    /// A class to support list binding to a DataView object.
    /// Implement the IBoundList.
    /// </summary>
    public class BoundDataView : IBoundList
    {
        public System.Data.DataView mDataView;

        public BoundDataView(System.Data.DataView dataView)
        {
            mDataView = dataView;

            mDataView.ListChanged += new System.ComponentModel.ListChangedEventHandler(mDataView_ListChanged);
        }

        public event System.ComponentModel.ListChangedEventHandler ListChanged;
        protected virtual void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
        {
            if (ListChanged != null)
                ListChanged(this, e);
        }
        void mDataView_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            OnListChanged(e);
        }

        private System.Data.DataRowView mEditingRow;
        public virtual int BeginAddNew()
        {
            if (mEditingRow != null)
                throw new DevAgeApplicationException("There is already a row in editing state, call EndEdit first");

            mEditingRow = mDataView.AddNew();

            mEditingRow.BeginEdit();

            System.Collections.IList list = (System.Collections.IList)mDataView;

            return list.IndexOf(mEditingRow);
        }

        public virtual void BeginEdit(int index)
        {
            if (mEditingRow != null)
                throw new DevAgeApplicationException("There is already a row in editing state, call EndEdit first");

            mEditingRow = mDataView[index];

            mEditingRow.BeginEdit();
        }

        public virtual void EndEdit(bool cancel)
        {
            if (mEditingRow == null)
                return;

            if (cancel)
                mEditingRow.CancelEdit();
            else
                mEditingRow.EndEdit();

            mEditingRow = null;

            //when CancelEdit the DataView doesn't automatically call a ListChanged event, so I will call it manually 
            if (cancel)
                OnListChanged(new System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.Reset, -1));
        }

        /// <summary>
        /// Gets the current edited object
        /// </summary>
        public virtual object EditedObject
        {
            get { return mEditingRow; }
        }

        public virtual int IndexOf(object item)
        {
            System.Collections.IList list = (System.Collections.IList)mDataView;

            return list.IndexOf(item);
        }

        public virtual void RemoveAt(int index)
        {
            mDataView[index].Delete();
        }

        public virtual object this[int index]
        {
            get { return mDataView[index]; }
        }

        public virtual int Count
        {
            get { return mDataView.Count; }
        }

        public virtual System.ComponentModel.PropertyDescriptorCollection GetItemProperties()
        {
			//Removed for mono compatibility
            //return System.Windows.Forms.ListBindingHelper.GetListItemProperties(mDataView);
			
			if (mDataView == null)
				return new System.ComponentModel.PropertyDescriptorCollection(null);
			else
				return ((System.ComponentModel.ITypedList)mDataView).GetItemProperties(null);
        }

        public System.ComponentModel.PropertyDescriptor GetItemProperty(string name, StringComparison comparison)
        {
            foreach (System.ComponentModel.PropertyDescriptor prop in GetItemProperties())
            {
                if (prop.Name.Equals(name, comparison))
                    return prop;
            }

            return null;
        }

        public virtual object GetItemValue(int index, System.ComponentModel.PropertyDescriptor property)
        {
            return property.GetValue(mDataView[index]);
        }

        public virtual void SetEditValue(System.ComponentModel.PropertyDescriptor property, object value)
        {
            if (mEditingRow == null)
                throw new DevAgeApplicationException("There isn't a row in editing state, call BeginAddNew or BeginEdit first");

            property.SetValue(mEditingRow, value);
        }

        public virtual void ApplySort(System.ComponentModel.ListSortDescriptionCollection sorts)
        {
            System.ComponentModel.IBindingListView listView = (System.ComponentModel.IBindingListView)mDataView;

            if (sorts != null && sorts.Count > 0)
                listView.ApplySort(sorts);
            else
                listView.RemoveSort();
        }

        private bool mAllowEdit = true;
        public virtual bool AllowEdit
        {
            get { return mDataView.AllowEdit && mAllowEdit; }
            set { mAllowEdit = value; }
        }

        private bool mAllowNew = true;
        public virtual bool AllowNew
        {
            get { return mDataView.AllowNew && mAllowNew; }
            set { mAllowNew = value; }
        }

        private bool mAllowDelete = true;
        public virtual bool AllowDelete
        {
            get { return mDataView.AllowDelete && mAllowDelete; }
            set { mAllowDelete = value; }
        }

        private bool mAllowSort = true;
        public virtual bool AllowSort
        {
            get { return mAllowSort; }
            set { mAllowSort = value; }
        }
    }
}
