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

        public event EventHandler BlockAppClicked;
        public event EventHandler LoadBlockedAppsClicked;
        private void BlockApp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadBlockedApps_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}