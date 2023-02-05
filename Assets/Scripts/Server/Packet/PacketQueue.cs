using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPacketMessage
{
    // 처리할 패킷
    public CMessage Message { get; set; }
    // 패킷을 처리할 함수
    public Action<CMessage> UnPackMessageFunc { get; set; }
}

public class CPacketQueue
{
    public static CPacketQueue GetInstance { get; } = new CPacketQueue();

    Queue<CPacketMessage> _packetQueue = new Queue<CPacketMessage>();
    object _lock = new object();

    public void Push(CMessage Packet, Action<CMessage> action)
    {
        lock (_lock)
        {
            CPacketMessage PacketMessage = new CPacketMessage();     
            PacketMessage.Message = Packet;
            PacketMessage.UnPackMessageFunc = action;

            _packetQueue.Enqueue(PacketMessage);            
        }        
    }

    public CPacketMessage Pop()
    {
        lock (_lock)
        {
            if (_packetQueue.Count == 0)
                return null;

            return _packetQueue.Dequeue();
        }
    }

    public List<CPacketMessage> PopAll()
    {
        List<CPacketMessage> list = new List<CPacketMessage>();

        lock (_lock)
        {
            while (_packetQueue.Count > 0)
                list.Add(_packetQueue.Dequeue());
        }

        return list;
    } 
}
