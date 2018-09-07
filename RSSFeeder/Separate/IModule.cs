using System;
using System.Windows.Controls;

namespace RSSFeeder.Separate
{
    public interface IModule
    {
        Action<int> ChangeModule { set; }
        /// <summary>
        /// Reference to view
        /// </summary>
        UserControl UserInterface { get; }
        /// <summary>
        /// Disabling the current view, for viewmodel with implementation IDisposable
        /// </summary>
        void Deactivate();
    }
}
