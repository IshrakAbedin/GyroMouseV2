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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;

namespace GyroMouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// UI for user interaction and turning on/off options for mouse pointer movement.
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MouseController Controller;
        private int baudRate = 57600;
        private List<string> comPorts;
        private bool safety = false;
        private bool y_inverted = false;
        private bool connected = false;
        private bool click_only = false;
        private SolidColorBrush messageColor = Brushes.Orange;
        private SolidColorBrush warningColor = Brushes.Red;
        private SolidColorBrush connectedColor = Brushes.SteelBlue;
        private SolidColorBrush disconnectedColor = Brushes.DarkRed;

        /// <summary>
        /// Initializes window and basic components
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            SetStatusDisconnected();
            Refresh();
        }

        /// <summary>
        /// Connect button action.
        /// Establishes serial connection based on the COM port selected.
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void Button_Connect_Click(object sender, RoutedEventArgs e)
        {
            if(ListBox_Ports.SelectedItem != null)
            {
                Controller = new MouseController(ListBox_Ports.SelectedItem.ToString(), baudRate, false);
                var successful = Controller.Start();
                if (successful)
                {
                    Controller.Window = this;
                    Controller.SafetyMechanism = safety;
                    Controller.ClickMode = click_only;
                    ShowMessage("Connecton Successful");
                    if (y_inverted) Controller.EnableYInversion();
                    else Controller.DisableYInversion();
                    connected = true;
                    SetStatusConnected();
                }
                else
                {
                    ShowMessage("Connection failed");
                    SetStatusDisconnected();
                }
            }
            else
            {
                ShowWarning("No COM ports selected");
                SetStatusDisconnected();
            }
        }

        /// <summary>
        /// Disconnect button action.
        /// Breaks serial connection.
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void Button_Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (connected)
            {
                var successful = Controller.End();
                if (successful)
                {
                    ShowMessage("Connecton termination Successful");
                    SetStatusDisconnected();
                    connected = false;
                }
                else
                {
                    ShowMessage("Connection termination failed");
                    SetStatusConnected();
                }
            }
            else ShowWarning("There is no connection to disconnect");
        }

        /// <summary>
        /// Refresh button action.
        /// Refreshes COM port list using refresh method.
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Refreshes COM port list.
        /// </summary>
        private void Refresh()
        {
            try
            {
                comPorts = SerialPort.GetPortNames().ToList();
                ListBox_Ports.ItemsSource = comPorts;
                ShowMessage("Refreshed ports list");
            }
            catch (Exception)
            {
                ShowWarning("Failed to fetch available COM ports");
            }
        }

        /// <summary>
        /// Displays a message in the bottom log bar.
        /// </summary>
        /// <param name="message"> Message to show </param>
        private void ShowMessage(string message)
        {
            TextBlock_Log.Foreground = messageColor;
            TextBlock_Log.Text = message;
        }

        /// <summary>
        /// Displays a warning in the bottom log bar.
        /// </summary>
        /// <param name="message"> Message to show </param>
        private void ShowWarning(string message)
        {
            TextBlock_Log.Foreground = warningColor;
            TextBlock_Log.Text = message;
        }

        /// <summary>
        /// Y inversion check box checking action.
        /// Turns on Y axis inversion
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void CheckBox_YInverted_Checked(object sender, RoutedEventArgs e)
        {
            if(Controller != null)
                Controller.EnableYInversion();
            y_inverted = true;
            ShowMessage("Y Axis inversion Enabled");
        }

        /// <summary>
        /// Y inversion check box unchecking action.
        /// Turns off Y axis inversion
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void CheckBox_YInverted_Unchecked(object sender, RoutedEventArgs e)
        {
            if(Controller != null)
                Controller.DisableYInversion();
            y_inverted = false;
            ShowMessage("Y Axis inversion Disabled");
        }

        /// <summary>
        /// Safety check box checking action.
        /// Turns on safety. Moving mouse pointer to top left corner disconnects connection.
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void CheckBox_Safety_Checked(object sender, RoutedEventArgs e)
        {
            if(Controller != null)
            {
                Controller.SafetyMechanism = true;
            }
            safety = true;
            ShowWarning("Take mouse pointer to top left corner to Stop");
        }

        /// <summary>
        /// Safety check box unchecking action.
        /// Turns off safety. Moving mouse pointer to top left corner  does not disconnect connection.
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void CheckBox_Safety_Unchecked(object sender, RoutedEventArgs e)
        {
            if (Controller != null)
            {
                Controller.SafetyMechanism = false;
            }
            safety = false;
            ShowMessage("Safety Disabled");
        }

        /// <summary>
        /// Changes the middle rectangle color to ConnectedColor to indicate connection establishment
        /// </summary>
        private void SetStatusConnected()
        {
            Rectangle_Status.Fill = connectedColor;
        }

        /// <summary>
        /// Changes the middle rectangle color to DisconnectedColor to indicate a severed connection.
        /// </summary>
        private void SetStatusDisconnected()
        {
            Rectangle_Status.Fill = disconnectedColor;
        }

        /// <summary>
        /// Help button click action.
        /// Shows help.
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void Button_Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("After connecting Arduino to USB, press refresh. The COM port for Arduino will appear. Select it and press 'CONNECT' to start.");
        }

        /// <summary>
        /// About button click action.
        /// Shows developer list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developed by:\nMohammad Ishrak Abedin \t 160041051\nSaad Bin Ashraf \t\t 160041068\nRizvi Ahmed \t\t 160041076");
        }

        /// <summary>
        /// ClickMode check box checking action.
        /// Turns on click only no drag mode.
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void CheckBox_ClickMode_Checked(object sender, RoutedEventArgs e)
        {
            click_only = true;
            if(Controller != null)
            {
                Controller.ClickMode = click_only;
            }

        }

        /// <summary>
        /// ClickMode check box unchecking action.
        /// Turns off click only no drag mode. Drag now is enabled along with click.
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void CheckBox_ClickMode_Unchecked(object sender, RoutedEventArgs e)
        {
            click_only = false;
            if(Controller != null)
            {
                Controller.ClickMode = click_only;
            }
        }
    }
}
