using Assisticant;
using Gma.System.MouseKeyHook;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace RoboCoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private IKeyboardMouseEvents _globalHook;
        private bool _modified;

        private void ScriptTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _modified = true;
        }

        private void GlobalHookKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!IsKeyboardFocusWithin)
            {
                if (_modified)
                {
                    ScriptTextBox.GetBindingExpression(
                        TextBox.TextProperty)
                        .UpdateSource();
                    _modified = false;
                }
                ForView.Unwrap<MainViewModel>(DataContext, vm => vm.KeyDown(e));
            }
        }

        private void GlobalHookKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!IsKeyboardFocusWithin)
                ForView.Unwrap<MainViewModel>(DataContext, vm => vm.KeyUp(e));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _globalHook = Hook.GlobalEvents();

            _globalHook.KeyDown += GlobalHookKeyDown;
            _globalHook.KeyUp += GlobalHookKeyUp;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            ForView.Unwrap<MainViewModel>(DataContext, vm => vm.Close());
            _globalHook.KeyDown -= GlobalHookKeyDown;
            _globalHook.KeyUp -= GlobalHookKeyUp;

            _globalHook.Dispose();
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                DefaultExt = "scr",
                Title = "RoboCoder",
                CheckFileExists = false
            };

            if (dialog.ShowDialog() ?? false)
            {
                ForView.Unwrap<MainViewModel>(DataContext, vm => vm.OpenFile(dialog.FileName));
            }
        }

        private void Error_Click(object sender, RoutedEventArgs e)
        {
            ForView.Unwrap<MainViewModel>(DataContext, vm =>
                MessageBox.Show(vm.Error, "RoboCoder"));
        }
    }
}
