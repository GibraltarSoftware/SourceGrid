using System;
namespace DevAge.ComponentModel
{
    /// <summary>
    /// A generic binded list interface. See BoundDataView or BoundList for a concreate implementation.
    /// It is used as an abstraction layer for list objects (typically a list can be a DataView or a IList class)
    /// Can be used to bind a list control (like SourceGrid).
    /// </summary>
    public interface IBoundList
    {
        bool AllowDelete { get; set; }
        bool AllowEdit { get; set; }
        bool AllowNew { get; set; }
        bool AllowSort { get; set; }
        void ApplySort(System.ComponentModel.ListSortDescriptionCollection sorts);
        int BeginAddNew();
        void BeginEdit(int index);
        int Count { get; }
        object EditedObject { get; }
        void EndEdit(bool cancel);
        System.ComponentModel.PropertyDescriptorCollection GetItemProperties();
        System.ComponentModel.PropertyDescriptor GetItemProperty(string name, StringComparison comparison);
        object GetItemValue(int index, System.ComponentModel.PropertyDescriptor property);
        int IndexOf(object item);
        event System.ComponentModel.ListChangedEventHandler ListChanged;
        void RemoveAt(int index);
        void SetEditValue(System.ComponentModel.PropertyDescriptor property, object value);
        object this[int index] { get; }
    }
}
