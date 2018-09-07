using System;
using System.Windows.Controls;
using RSSFeeder.View;
using RSSFeeder.ViewModel;

namespace RSSFeeder.Separate
{
    public class SeparateOption : IModule
    {
        private OptionUserControl view;
        private OptionViewModel viewModel;
        private Action<int> changeModule;

        public UserControl UserInterface
        {
            get
            {
                if (view == null) CreateView();
                return view;
            }
        }

        Action<int> IModule.ChangeModule { set { changeModule = value; } }

        /// <summary>
        /// Creating view
        /// </summary>
        private void CreateView()
        {
            view = new OptionUserControl();
            viewModel = new OptionViewModel();
            viewModel.ChangeModule = changeModule;
            view.DataContext = viewModel;
        }

        public void Deactivate()
        {
            view = null;
        }
    }
}
