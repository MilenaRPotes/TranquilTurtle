using System;
using System.Collections.Generic;
using System.Windows;
using TranquilTurtle.Views;
using TranquilTurtle.Models;
using TranquilTurtle.Presenters;
//using TranquilTurtle.Services;

namespace TranquilTurtle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        public event EventHandler BlockAppClicked;
        public event EventHandler LoadBlockedAppsClicked;
        public event EventHandler EditAppClicked;
        public event EventHandler DeleteAppClicked;

        private MainPresenter presenter;
        public MainWindow()
        {
            InitializeComponent();
            presenter = new MainPresenter(this, new BlockedAppsModel());
            //Example: Close whatsApp Desktop if are open
            //AppBlockerService.KillApp("WhatsApp");
        }


        // Implementing IMainView
        public string ProcessNameInput => TbProcessName.Text;

        public string SelectedApp => BlockedAppsListBox.SelectedItem as string ?? string.Empty;


        public void SetBlockedAppList(List<string> apps)
        {
            BlockedAppsListBox.ItemsSource = null;
            BlockedAppsListBox.ItemsSource = apps;
        }

        public void SetStatus(string message) 
        { 
            TbStatust.Text = message;
        }

       

        //Button events that trigger events to Presenter 
        private void BlockApp_Click(object sender, RoutedEventArgs e)
        {
            BlockAppClicked?.Invoke(this, EventArgs.Empty);
        }

        private void LoadBlockedApps_Click(object sender, RoutedEventArgs e)
        {
            LoadBlockedAppsClicked?.Invoke(this, EventArgs.Empty);
        }

        private void EditSelected_Click(object sender, RoutedEventArgs e)
        {
            EditAppClicked?.Invoke(this, EventArgs.Empty);
        }

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            DeleteAppClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}