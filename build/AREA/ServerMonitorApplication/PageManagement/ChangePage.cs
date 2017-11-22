using System;

namespace ServerMonitorApplication.PageManagement
{
    public class ChangePage
    {
        #region Property

        /// <summary>
        /// View model type
        /// </summary>
        public Type ViewModelType { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="viewModelType">Page type</param>
        public ChangePage(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        #endregion
    }
}
