using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TranquilTurtle
{
    /// <summary>
    /// Lógica de interacción para FocusBlockerWindow.xaml
    /// </summary>
    public partial class FocusBlockerWindow : Window
    {
        private DispatcherTimer timer;
        private TimeSpan remainingTime;
        private MainWindow mainWindow;
        public FocusBlockerWindow(TimeSpan duration, MainWindow main)
        {
            InitializeComponent();
            remainingTime = duration;
            mainWindow = main;

            TimerText.Text = remainingTime.ToString(@"mm\:ss");

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
           remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
           TimerText.Text = remainingTime.ToString(@"mm\:ss");

            if (remainingTime.TotalSeconds <= 0) 
            { 
                timer.Stop();
                MessageBox.Show("Great job! Focus time finished!", "Session Complete", MessageBoxButton.OK, MessageBoxImage.Information);

               

                //show main window and close this
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) 
        {
            if (remainingTime.TotalSeconds > 0) 
            { 
                e.Cancel = true;
            }
        }
    }
}
