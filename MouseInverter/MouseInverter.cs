using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices; // For SwapMouseButton

namespace MouseInverter
{
    class Inverter
    {
        // For SwapMouseButton
        [DllImport("user32.dll")]
        public static extern Int32 SwapMouseButton(Int32 bSwap);

        private Point currentPosition;

        private bool running;

        private bool exit;

        static int Main(string[] args)
        {

            Inverter inverter = new Inverter();

            Console.CancelKeyPress += delegate
            {
                inverter.Stop();
            };

            inverter.Start();
            while (true)
            {
                Thread.Sleep(Timeout.Infinite);
            }
        }

        public bool Running
        {
            get
            {
                return this.running;
            }
        }
                
        private void MouseLoop()
        {
            Thread.CurrentThread.IsBackground = true;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            while (!this.exit)
            {
                Point newPosition = Cursor.Position;

                int bottom = this.currentPosition.Y - (newPosition.Y - this.currentPosition.Y);
                int maxHeight = SystemInformation.VirtualScreen.Height;
                if (bottom > maxHeight - 2)
                {
                    bottom = maxHeight - 2;
                }
                else if (bottom < 2)
                {
                    bottom = 2;
                }

                int right = this.currentPosition.X - (newPosition.X - this.currentPosition.X);
                int origRight = right;
                int maxWidth = SystemInformation.VirtualScreen.Width;
                if (right > maxWidth - 2)
                {
                    right = maxWidth - 2;
                }
                else if (right < 2)
                {
                    right = 2;
                }

                Cursor.Position = new Point(right, bottom);
                this.currentPosition = Cursor.Position;
                Thread.Sleep(1);
            }
            this.exit = false;
        }

        public void Start()
        {
            this.currentPosition = Cursor.Position;
            this.running = true;
            SwapMouseButton(1);
            (new Thread(new ThreadStart(this.MouseLoop))).Start();
        }

        public void Stop()
        {
            SwapMouseButton(0);
            this.running = false;
            this.exit = true;
        }
    }
}
