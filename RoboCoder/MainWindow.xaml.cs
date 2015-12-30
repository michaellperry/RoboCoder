using Assisticant;
using Gma.System.MouseKeyHook;
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

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(1000);
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            System.Windows.Forms.SendKeys.SendWait("public override void BeginIni");
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            Thread.Sleep(1000);
            System.Windows.Forms.SendKeys.SendWait("{UP}");
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            Thread.Sleep(1000);
            System.Windows.Forms.SendKeys.SendWait("this.AddTe");
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            Thread.Sleep(1000);
            System.Windows.Forms.SendKeys.SendWait("{(}\"Hello, World!\"{)};");
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
            m_GlobalHook.KeyDown -= GlobalHookKeyDown;

            m_GlobalHook.Dispose();
        }
    }
}
