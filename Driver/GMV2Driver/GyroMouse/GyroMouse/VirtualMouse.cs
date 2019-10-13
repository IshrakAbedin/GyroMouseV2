using System.Runtime.InteropServices;

namespace GyroMouse
{
    /// <summary>
    /// It is the core driver class that contains all the methods for controlling the mouse
    /// It taps into the windows two button mouse system using the dll.
    /// The mouse pointer is mapped before converting the values based on resolution.
    /// Hence, the mouse becomes resolution independent.
    /// Mouse pointer locations is mapped onto an unsigned int16 ranging from 1 to 65535 both horizontally and vertically.
    /// This class supports absolute and relative movement of mouse pointer.
    /// Up, Down and Click methods are available for left and right mouse buttons.
    /// </summary>
    public static class VirtualMouse
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        /// <summary>
        /// Move mouse pointer relatively to previous location.
        /// </summary>
        /// <param name="xDelta"> Horizontal pointer location difference. </param>
        /// <param name="yDelta"> Vertical pointer location difference.</param>
        public static void Move(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }

        /// <summary>
        /// Move mouse pointer to absolute location.
        /// </summary>
        /// <param name="x"> Horizontal location. Value must be between 1 to 65535. </param>
        /// <param name="y"> Vertical location. Value must be between 1 to 65535.</param>
        public static void MoveTo(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, 0);
        }

        /// <summary>
        /// Perform a left click.
        /// </summary>
        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        /// <summary>
        /// Perform a left mouse button down action.
        /// </summary>
        public static void LeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        /// <summary>
        /// Perform a left mouse button up action.
        /// </summary>
        public static void LeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        /// <summary>
        /// Perform a right click.
        /// </summary>
        public static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        /// <summary>
        /// Perform a right mouse button down action.
        /// </summary>
        public static void RightDown()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        /// <summary>
        /// Perform a right mouse button up action.
        /// </summary>
        public static void RightUp()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }
    }
}
