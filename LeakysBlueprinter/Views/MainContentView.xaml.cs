using LeakysBlueprinter.ViewModels;
using Microsoft.Win32;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LeakysBlueprinter.Views
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
