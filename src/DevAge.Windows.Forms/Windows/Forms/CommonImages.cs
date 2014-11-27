using System;
using System.Drawing;

namespace DevAge.Windows.Forms
{
	public class Resources
	{
        static Resources()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes cursor resources for the current thread.
        /// </summary>
        /// <remarks>The first thread to load this class will initialize its own copies of the cursor arrow resources
        /// automatically.  Other threads will initialize their own copies of these cursor arrow resources upon first
        /// access, or they may call this method to force initialization.  It is safe to call this more than once, but
        /// only the first call has an effect.  Thereafter the same initialized cursor resources in this class are
        /// shared across the entire UI thread (but not between threads).  This is necessary to avoid sharing the
        /// System.Windows.Forms.Cursor objects between threads in a multi-UI-thread scenario.</remarks>
        public static void Initialize()
        {
            if (tRightArrow == null)
            {
                using (System.IO.MemoryStream mem = new System.IO.MemoryStream(Properties.Resources.CURSOR_Right))
                {
                    tRightArrow = new System.Windows.Forms.Cursor(mem);
                }
            }
            if (tLeftArrow == null)
            {
                using (System.IO.MemoryStream mem = new System.IO.MemoryStream(Properties.Resources.CURSOR_Left))
                {
                    tLeftArrow = new System.Windows.Forms.Cursor(mem);
                }
            }
        }

        public static Icon IconSortDown // Depends on Properties.Resources.ICO_SortDown to be multi-thread safe
		{
			get{return Properties.Resources.ICO_SortDown;}
		}
        public static Icon IconSortUp // Depends on Properties.Resources.ICO_SortUp to be multi-thread safe
		{
            get { return Properties.Resources.ICO_SortUp; }
		}

        [ThreadStatic] private static System.Windows.Forms.Cursor tRightArrow;

        public static System.Windows.Forms.Cursor CursorRightArrow // Multi-thread safe with [ThreadStatic] backing store
        {
            get { Initialize(); return tRightArrow; }
        }

        [ThreadStatic] private static System.Windows.Forms.Cursor tLeftArrow;

        public static System.Windows.Forms.Cursor CursorLeftArrow // Multi-thread safe with [ThreadStatic] backing store
        {
            get { Initialize(); return tLeftArrow; }
        }
	}
}
