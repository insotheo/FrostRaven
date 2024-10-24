using Microsoft.Win32;
using System;
using System.Windows;
using FrostRavenPackagesWorker;
using System.IO;

namespace FrostRavenPackagesWorkerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Package _gamepac;
        private const string titleStart = "FrostRavenPackagesWorkerApp";

        public MainWindow()
        {
            InitializeComponent();
            ExitBtn.Click += (object _, RoutedEventArgs _) => { Environment.Exit(0); };
            OpenGamePacBtn.Click += OpenGamePackage;
            CloseGamePacBtn.Click += CloseGamePackage;
            CreateGamePacBtn.Click += CreateGamePackage;
            CreateGamePacWithDirBtn.Click += CreateGamePackageWithDirectory;
        }

        private void CreateGamePackageWithDirectory(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "GamePackage| *.gamepac",
                Title = "Select a path and name for your game package!",
                AddExtension = true
            };
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog()
            {
                UseDescriptionForTitle = true,
                Description = "Select a folder for making a game package based on it",
            };
            if(sfd.ShowDialog() == true &&
                (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK))
            {
                using (PasswordDialog pswd = new PasswordDialog())
                {
                    pswd.ShowDialog();
                    PackageCreator.MakePackageWithFiles(fbd.SelectedPath, pswd.GetEnteredPassword(), Path.GetFileNameWithoutExtension(sfd.FileName), Path.GetDirectoryName(sfd.FileName));
                    _gamepac = new Package(sfd.FileName, pswd.GetEnteredPassword());
                    _gamepac.OpenForEditing();
                    Title = titleStart + $" - [{sfd.FileName}]";
                }
            }
        }

        private void CreateGamePackage(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "GamePackage| *.gamepac",
                Title = "Select a path and name for your game package!",
                AddExtension = true
            };
            if(sfd.ShowDialog() == true)
            {
                using (PasswordDialog pswd = new PasswordDialog()) {
                    pswd.ShowDialog();
                    string tmpDir = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(sfd.FileName));
                    Directory.CreateDirectory(tmpDir);
                    PackageCreator.MakePackageWithFiles(tmpDir, pswd.GetEnteredPassword(), Path.GetFileNameWithoutExtension(sfd.FileName), Path.GetDirectoryName(sfd.FileName));
                    _gamepac = new Package(sfd.FileName, pswd.GetEnteredPassword());
                    _gamepac.OpenForEditing();
                    Title = titleStart + $" - [{sfd.FileName}]";
                    Directory.Delete(tmpDir);
                }
            }
        }

        private void CloseGamePackage(object sender, RoutedEventArgs args)
        {
            if(_gamepac == null)
            {
                MessageBox.Show("Nothing to close!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            _gamepac.CloseForEditing();
            MessageBox.Show("Success!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            Title = titleStart;
        }

        private void OpenGamePackage(object sender, RoutedEventArgs args)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "GamePackage| *.gamepac",
                Multiselect = false,
                Title = "Select a game package"
            };
            if(ofd.ShowDialog() == true)
            {
                try
                {
                    using (PasswordDialog pswd = new PasswordDialog())
                    {
                        pswd.ShowDialog();
                        _gamepac = new Package(ofd.FileName, pswd.GetEnteredPassword());
                        _gamepac.OpenForEditing();
                        Title = titleStart + $" - [{ofd.FileName}]";
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}