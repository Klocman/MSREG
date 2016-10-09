using System;
using System.IO;
using System.IO.Ports;

namespace Klocman.IO
{
    public class SafeSerialPort : SerialPort
    {
        private Stream _theBaseStream;

        public SafeSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
            : base(portName, baudRate, parity, dataBits, stopBits)
        {
        }

        public SafeSerialPort()
        {
        }

        public new void Open()
        {
            //try
            {
                base.Open();
                _theBaseStream = BaseStream;
                GC.SuppressFinalize(BaseStream);
            }
            /*catch
            {

            }*/
        }

        public new void Dispose()
        {
            Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Container?.Dispose();

            try
            {
                if (_theBaseStream.CanRead)
                {
                    _theBaseStream.Dispose();
                    //_theBaseStream.Close();
                    GC.ReRegisterForFinalize(_theBaseStream);
                }
            }
            catch
            {
                // ignore exception - bug with USB - serial adapters.
            }
            base.Dispose(disposing);
        }
    }
}