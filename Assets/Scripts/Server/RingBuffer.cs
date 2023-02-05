using System;
using System.IO;

namespace ServerCore
{
    public class RingBuf
    {
        enum en_RingBufInfo
        {
            DEFAULT_BUF_SIZE = 10001,
            BLANK = 1
        }

        public byte[] _RingBuf;
        int _PeekFront;
        int _PeekRear;
        int _Front;
        public int _Rear;
        int _BufferMaxSize;

        public RingBuf(int BufferSize)
        {
            _Front = 0;
            _Rear = 0;
            _PeekFront = 0;
            _PeekRear = 0;

            _RingBuf = new byte[BufferSize];
            _BufferMaxSize = BufferSize;
        }

        public int GetBufferSize()
        {
            return _BufferMaxSize;
        }

        public int GetPeekSize()
        {
            int Front = _Front;
            int Rear = _Rear;
            int UseSize = 0;

            if (Front <= Rear)
            {
                UseSize = Rear - Front;
            }
            else
            {
                UseSize = (_BufferMaxSize - Front) + Rear;
            }

            return UseSize;
        }

        public int GetUseSize()
        {
            int Front = _Front;
            int Rear = _Rear;
            int UseSize = 0;

            if (Rear >= Front)
            {
                UseSize = Rear - Front;
            }
            else
            {
                UseSize = (_BufferMaxSize - Front) + Rear;
            }

            return UseSize;
        }

        public int GetFreeSize()
        {
            int Front = _Front;
            int Rear = _Rear;
            int FreeSize = 0;

            if (Front > Rear)
            {
                FreeSize = Front - Rear - (int)en_RingBufInfo.BLANK;
            }
            else
            {
                FreeSize = (_BufferMaxSize - Rear) + Front - (int)en_RingBufInfo.BLANK;
            }

            return FreeSize;
        }

        public int GetDirectEnqueueSize()
        {
            int Front = _Front;
            int Rear = _Rear;
            int Size = 0;

            if (Front > Rear)
            {
                Size = Front - Rear - (int)en_RingBufInfo.BLANK;
            }
            else
            {
                if (0 == Front)
                {
                    Size = _BufferMaxSize - Rear - (int)en_RingBufInfo.BLANK;
                }
                else
                {
                    Size = _BufferMaxSize - Rear;
                }
            }

            return Size;
        }

        int GetDirectDequeueSize()
        {
            int TempFront = _Front;
            int TempRear = _Rear;
            int Size = 0;

            if (TempRear >= TempFront)
            {
                Size = TempRear - TempFront;
            }
            else
            {
                Size = _BufferMaxSize - TempFront;
            }

            return Size;
        }

        int GetPeekDirectDequeSize()
        {
            int Front = _PeekFront;
            int Rear = _PeekRear;
            int DequeSize = 0;

            if (Rear >= Front)
            {
                DequeSize = Rear - Front;
            }
            else
            {
                DequeSize = _BufferMaxSize - Front;
            }

            return DequeSize;
        }

        int Enqueue(byte[] EnqueBuf, int Size)
        {
            int DirectEnqSize = GetDirectEnqueueSize();
            int FreeSize = GetFreeSize();

            if (Size > FreeSize)
            {
                Size = FreeSize;
            }

            if (Size <= DirectEnqSize)
            {
                Buffer.BlockCopy(EnqueBuf, 0, _RingBuf, _Rear, Size);
            }
            else
            {
                Buffer.BlockCopy(EnqueBuf, 0, _RingBuf, _Rear, DirectEnqSize);
                Buffer.BlockCopy(EnqueBuf, DirectEnqSize, _RingBuf, 0, Size - DirectEnqSize);
            }

            _PeekRear = (_PeekRear + Size) % _BufferMaxSize;
            _Rear = (_Rear + Size) % _BufferMaxSize;

            return Size;
        }

        public int Dequeue(byte[] Dest, int Size)
        {
            int DirectDeqSize = GetDirectDequeueSize();
            int UseSize = GetUseSize();

            if (UseSize < Size)
            {
                Size = UseSize;
            }

            if (Size <= DirectDeqSize)
            {
                Buffer.BlockCopy(_RingBuf, _Front, Dest, 0, Size);
            }
            else
            {
                Buffer.BlockCopy(_RingBuf, _Front, Dest, 0, DirectDeqSize);
                Buffer.BlockCopy(_RingBuf, 0, Dest, DirectDeqSize, Size - DirectDeqSize);
            }

            _Front = (_Front + Size) % _BufferMaxSize;

            return Size;
        }

        public int Peek(byte[] Dest, int Size)
        {
            int DirectDeqSize = GetDirectDequeueSize();
            int UseSize = GetUseSize();

            if (UseSize < Size)
            {
                Size = UseSize;
            }

            if (Size <= DirectDeqSize)
            {
                Buffer.BlockCopy(_RingBuf, _Front, Dest, 0, Size);
            }
            else
            {
                Buffer.BlockCopy(_RingBuf, _Front, Dest, 0, DirectDeqSize);
                Buffer.BlockCopy(_RingBuf, 0, Dest, DirectDeqSize, Size - DirectDeqSize);
            }

            return Size;
        }

        public int MoveRear(int Size)
        {
            int FreeSize = GetFreeSize();
            if (FreeSize < Size)
            {
                Size = FreeSize;
            }

            _Rear = (_Rear + Size) % _BufferMaxSize;

            return Size;
        }

        public int MoveFront(int Size)
        {
            int UseSize = GetUseSize();
            if (UseSize < Size)
            {
                Size = UseSize;
            }

            _Front = (_Front + Size) % _BufferMaxSize;

            return Size;
        }

        void ClearBuffer()
        {
            _PeekFront = _PeekRear = _Front = _Rear = 0;
        }

        bool IsEmpty()
        {
            return (_Front == _Rear);
        }
    }
}