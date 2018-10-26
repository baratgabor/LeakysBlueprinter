using LeakysBlueprinter.UI.WPF.ViewModels;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LeakysBlueprinter.UI.WPF.Views
{
    public partial class MainContentView : UserControl
    {
        public MainContentView()
        {
            InitializeComponent();
        }

        public MainContentView(MainContentViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
