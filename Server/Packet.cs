using Basic_Chess_Server.Ceras;
using System;


namespace Basic_Chess_Server.Server
{
    public class Packet : IDisposable
    {
        private ArrayList<byte> buffer; 

        private int size;
        private int currentPos;

        public Packet(int startSize)
        {
            size = startSize;
            buffer = new ArrayList<byte>(startSize);
            currentPos = 0;
        }

        public Packet(int startSize, int packetType)
        {
            buffer = new ArrayList<byte>(startSize);
            Write(packetType);
        }

        public void Write<T>(T obj)
        {
            int writtenBytes = CerasController.Instance.GetCeras().Serialize(obj, ref buffer, currentPos);
            UpdateCurrentPos(writtenBytes);
        }

        public void SetBytes(byte[] bytes)
        {
            Console.WriteLine("Bytes Length : " + bytes.Length);

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = bytes[i];
            }
        }

        private void UpdateCurrentPos(int writtenBytes)
        {
            currentPos += writtenBytes;
        }

        public void PrintBuffer()
        {
            foreach (byte b in buffer)
            {
                Console.Write(b);
            }
            Console.WriteLine();
        }

        public void Read<T>(ref T obj)
        {
            CerasController.Instance.GetCeras().Deserialize(ref obj, buffer, ref currentPos);
        }

        public byte[] GetBuffer()
        {
            return buffer;
        }


        #region Dispose
        private bool isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                buffer = null;
                size = 0;
                currentPos = 0;
            }

            isDisposed = true;
        }
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

