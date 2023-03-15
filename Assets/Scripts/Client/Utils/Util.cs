using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject Go) where T : UnityEngine.Component
    {
        T Component = Go.GetComponent<T>();
        if (Component == null)
        {
            Component = Go.AddComponent<T>();
        }

        return Component;
    }

    public static GameObject FindChild(GameObject Go, string Name = null, bool Recursive = false)
    {
        Transform transform = FindChild<Transform>(Go, Name, Recursive);
        if (transform == null)
        {
            return null;
        }

        return transform.gameObject;
    }

    //Recursive 재귀적으로 호출할 것인지의 여부
    //오브젝트를 찾을때 자식을 찾고 그 자식의 자식도 찾을 것인지를 의미한다.
    public static T FindChild<T>(GameObject Go, string Name = null, bool Recursive = false) where T : UnityEngine.Object
    {
        if (Go == null)
        {
            return null;
        }

        if (Recursive == false)
        {
            for (int i = 0; i < Go.transform.childCount; i++)
            {
                Transform transform = Go.transform.GetChild(i);
                if (string.IsNullOrEmpty(Name) || transform.name == Name)
                {
                    T Component = transform.GetComponent<T>();
                    if (Component != null)
                    {
                        return Component;
                    }
                }
            }
        }
        else
        {
            foreach (T Component in Go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(Name) || Component.name == Name)
                {
                    return Component;
                }
            }
        }

        return null;
    }

    public static T ByteToStruct<T>(byte[] Buffer) where T : struct
    {
        int Size = Marshal.SizeOf(typeof(T));

        if (Size > Buffer.Length)
        {
            throw new Exception();
        }

        IntPtr Ptr = Marshal.AllocHGlobal(Size);
        Marshal.Copy(Buffer, 0, Ptr, Size);
        T Struct = (T)Marshal.PtrToStructure(Ptr, typeof(T));
        Marshal.FreeHGlobal(Ptr);

        return Struct;
    }
}
