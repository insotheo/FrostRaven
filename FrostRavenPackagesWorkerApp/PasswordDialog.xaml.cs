using System;
using System.Windows;
using System.Windows.Input;

namespace FrostRavenPackagesWorkerApp
{
    /// <summary>
    /// Interaction logic for PasswordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : Window, IDisposable
    {

        public PasswordDialog()
        {
            InitializeComponent();
            ConfirmBtn.Click += (object _, RoutedEventArgs _) => { Close(); };
            PackPasswordPB.KeyDown += PackPasswordPB_KeyDown;
            PackPasswordPB.Focus();
        }

        private void PackPasswordPB_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                this.Close();
            }
        }

        internal string GetEnteredPassword() => PackPasswordPB.Password; 

        public void Dispose()
        {
            PackPasswordPB.Clear();
        }

    }
}
