using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace GyroMouse
{
    /// <summary>
    /// The controller class that links serial signals to the control of mouse.
    /// It also provides methods to change mouse properties, i.e y-axis inversion, safety mode and click only mode for left mouse button.
    /// It also handles events from serial port to move the mouse pointer to desired location.
    /// </summary>
    class MouseController
    {
        public string Port { get; set; }
        public int BaudRate { get; set; }
        public bool Debug { get; set; }
        public int ClickFloor { get; set; }
        public bool SafetyMechanism { get; set; }
        public bool YInverted { get; set; }
        public bool ClickMode { get; set; }
        public MainWindow Window { get; set; }

        private const int MAP_VAL = 65400;
        private const int X_MAX_DEGREE = 25;
        private const int Y_MAX_DEGREE = 18;
        private const int FILTER_RESOLUTION = 10;
        private const int FILTER2_RESOLUTION = 6;
        private SerialPort SPort;
        private string str;
        private string[] GetV = new string[3];
        private int X;
        private int Y;
        private int counter = 0;
        private int counter2 = 0;
        private int[] BufferX = new int[FILTER_RESOLUTION];
        private int[] BufferY = new int[FILTER_RESOLUTION];
        private int[] BufferX2 = new int[FILTER2_RESOLUTION];
        private int[] BufferY2 = new int[FILTER2_RESOLUTION];
        private int YInversionMultiplier = -1;


        public MouseController() : this("COM3", 9600, false) { }
        public MouseController(string port, int baudRate) : this(port, baudRate, false) { }
        public MouseController(string port, int baudRate, bool debug)
        {
            Port = port;
            BaudRate = baudRate;
            Debug = debug;
            SafetyMechanism = false;
            YInverted = false;
            ClickMode = false;
            for(int i = 0; i < FILTER_RESOLUTION; i++)
            {
                BufferX[i] = 0;
                BufferY[i] = 0;
            }
            for (int i = 0; i < FILTER2_RESOLUTION; i++)
            {
                BufferX2[i] = 0;
                BufferY2[i] = 0;
            }
            Init();
        }

        /// <summary>
        /// Creates a new serial port with port name and baud rate but does not open it.
        /// Binds data receive event from the serial port to data received method.
        /// </summary>
        private void Init()
        {
            SPort = new SerialPort(portName: Port, baudRate: BaudRate);
            //Console.WriteLine($"Port : {Port} Baud Rate : {BaudRate}");
            SPort.DataReceived += SPort_DataReceived;
        }

        /// <summary>
        /// This methods runs everytime some data is received in serial port.
        /// Based on the packet's leading string, it controls mouse or shows error message.
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            str = SPort.ReadLine();
            if (str[0] == 'G')
            {
                GetV = str.Split(' ');
                X = (int)(((Convert.ToDouble(GetV[1]) + X_MAX_DEGREE) / (2 * X_MAX_DEGREE)) * MAP_VAL);
                Y = (int)(((Convert.ToDouble(GetV[2]) * YInversionMultiplier + Y_MAX_DEGREE) / (2 * Y_MAX_DEGREE)) * MAP_VAL);
                if (SafetyMechanism && X == 0 && Y == 0) End();
                BufferX[counter] = X;
                BufferY[counter] = Y;
                counter = (counter + 1) % FILTER_RESOLUTION;
                X = (int)BufferX.Average();
                Y = (int)BufferY.Average();
                BufferX2[counter2] = X;
                BufferY2[counter2] = Y;
                counter2 = (counter2 + 1) % FILTER2_RESOLUTION;
                X = (int)BufferX2.Average();
                Y = (int)BufferY2.Average();
                VirtualMouse.MoveTo(X, Y);
            }
            else if (str[0] == 'L')
            {
                int LMB = Convert.ToInt32(str[2]);
                if(LMB == 1)
                {
                    VirtualMouse.LeftDown();
                }
                else
                {
                    VirtualMouse.LeftUp();
                }
            }
            else if (str[0] == 'R')
            {
                int RMB = Convert.ToInt32(str[2]);
                if (RMB == 1)
                {
                    VirtualMouse.RightDown();
                }
                else
                {
                    VirtualMouse.RightUp();
                }
            }
            else if (str[0] == 'M')
            {
                //str.TrimStart('M', ' ');
                //str = str.Trim();
                //Console.WriteLine(str);
            }
        }

        /// <summary>
        /// Opens the port while handling exceptions.
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            try
            {
                SPort.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured while opening port");
                if (Debug)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return false;
            }
        }

        /// <summary>
        /// Closes existing serial port connection.
        /// </summary>
        /// <returns></returns>
        public bool End()
        {
            try
            {
                SPort.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured while opening port");
                if (Debug)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return false;
            }
        }

        /// <summary>
        /// Toggles Y axis movement inversion for mouse pointer movement.
        /// </summary>
        /// <returns> Inverted condition </returns>
        public bool ToggleYInversion()
        {
            if(YInverted == false)
            {
                YInverted = true;
                YInversionMultiplier = 1;
            }
            else
            {
                YInverted = false;
                YInversionMultiplier = -1;
            }
            return YInverted;
        }

        /// <summary>
        /// Enables Y axis inversion for mouse pointer movement.
        /// </summary>
        public void EnableYInversion()
        {
            YInverted = true;
            YInversionMultiplier = 1;
        }

        /// <summary>
        /// Disables Y axis inversion for mouse pointer movement.
        /// </summary>
        public void DisableYInversion()
        {
            YInverted = false;
            YInversionMultiplier = -1;
        }
    }
}
