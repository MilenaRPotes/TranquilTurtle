using System;
using System.Collections.Generic;
using System.Windows;
using TranquilTurtle.Views;
using TranquilTurtle.Models;
using TranquilTurtle.Presenters;
using System.Windows.Threading;
using System.Windows.Controls;
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
        public event EventHandler StartTimerClicked;

        private MainPresenter presenter;
        //private DispatcherTimer uiCountdownTimer;
        //private TimeSpan remainingTime;
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

        public TimeSpan GetTimerDuration()
        {
            if (TimerComboBox.SelectedItem is ComboBoxItem selectedItem && int.TryParse(selectedItem.Tag.ToString(), out int minutes)) 
            { 
                return TimeSpan.FromMinutes(minutes);
            
            }

            return TimeSpan.FromMinutes(1); // default fallback
        }

        public void ShowBlockingUI(TimeSpan duration) 
        {

            FocusBlockerWindow blockerWindow = new FocusBlockerWindow(duration, this);
            
            blockerWindow.Show();
            this.Hide();

            //var focuswindow = new FocusBlockerWindow(duration,this);
            //focuswindow.Show();

            //this.Hide();

        }

   

        public void HideBlockingUI() 
        {
            SetStatus("Focus session completed. Well done!");

        }




        #region trigger events to Presenter 
        //Button events that trigger events to Presenter 
        private void BlockApp_Click(object sender, RoutedEventArgs e)
        {
            BlockAppClicked?.Invoke(this, EventArgs.Empty);

            StartTimerClicked?.Invoke(this, EventArgs.Empty);
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
        #endregion
    }
}