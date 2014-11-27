using System;
using System.Collections.Generic;
using System.Text;

namespace DevAge.ComponentModel
{
    /// <summary>
    /// Class used to implement an empty ITypeDescriptorContext.
    /// This class seems to be required by the mono framework, ms framework accept null as ITypeDescriptorContext
    /// </summary>
    public class EmptyTypeDescriptorContext : System.ComponentModel.ITypeDescriptorContext
    {
        [ThreadStatic] private static EmptyTypeDescriptorContext t_Empty;

        /// <summary>
        /// Empty ITypeDescriptorContext instance.
        /// </summary>
        /// <remarks>For now I use null because mono seems to don't like this class (and throw anyway an exception)</remarks>
        public static EmptyTypeDescriptorContext Empty // Multi-thread safe with [ThreadStatic] backing store
        {
            get
            {
                /*
                 * Example of how to perform initialization of [ThreadStatic] backing store if this were to return a
                 * readonly instance rather than just a null.  Use of ThreadStatic attribute is necessary so that
                 * multiple UI threads do not share UI objects which have to be kept local to one thread, just in
                 * case that might apply to this class.  See MSDN for more info on using ThreadStatic attribute.
                 * 
                if (t_Empty == null)
                    t_Empty = new EmptyTypeDescriptorContext();
                */

                return t_Empty; // Starts as null by default for each thread
            }
        }

        private readonly EmptyContainer container = new EmptyContainer();

        #region ITypeDescriptorContext Members

        public System.ComponentModel.IContainer Container
        {
            get { return container; }
        }

        public object Instance
        {
            get { return null; }
        }

        public void OnComponentChanged()
        {

        }

        public bool OnComponentChanging()
        {
            return true;
        }

        public System.ComponentModel.PropertyDescriptor PropertyDescriptor
        {
            get { return null; }
        }

        #endregion

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            return null;
        }

        #endregion
    }

    public class EmptyContainer : System.ComponentModel.IContainer
    {
        #region IContainer Members
        public void Add(System.ComponentModel.IComponent component, string name)
        {
            throw new NotImplementedException();
        }

        public void Add(System.ComponentModel.IComponent component)
        {
            throw new NotImplementedException();
        }

        public System.ComponentModel.ComponentCollection Components
        {
            get { return new System.ComponentModel.ComponentCollection(null); }
        }

        public void Remove(System.ComponentModel.IComponent component)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {
        }
        #endregion
    }

}
