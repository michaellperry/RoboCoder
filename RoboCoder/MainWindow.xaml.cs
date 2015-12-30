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

        private IKeyboardMouseEvents m_GlobalHook;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.KeyDown += GlobalHookKeyDown;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            ForView.Unwrap<MainViewModel>(DataContext, vm => vm.Close());
            m_GlobalHook.KeyDown -= GlobalHookKeyDown;

            m_GlobalHook.Dispose();
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                DefaultExt = "scr",
                Title = "RoboCoder"
            };

            if (dialog.ShowDialog() ?? false)
            {
                ForView.Unwrap<MainViewModel>(DataContext, vm => vm.OpenFile(dialog.FileName));
            }
        }
    }
}
