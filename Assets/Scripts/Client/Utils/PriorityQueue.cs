using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 우선순위 큐
public class PriorityQueue<T> where T : IComparable<T>
{
    //배열
    List<T> _Heap = new List<T>();

    public void Push(T Data)
    {
        //힙의 맨 끝에 새로운 데이터를 삽입한다.
        _Heap.Add(Data);

        int Now = _Heap.Count - 1;

        //삽입한 노드 위치 설정
        while (Now > 0)
        {
            //부모 위치 구함
            int Next = (Now - 1) / 2;
            //부모의 데이터가 삽입한 데이터보다 크다면 탈출
            if (_Heap[Next].CompareTo(_Heap[Now]) > 0)
            {
                break;
            }

            //두 값을 교체한다.
            T Temp = _Heap[Now];
            _Heap[Now] = _Heap[Next];
            _Heap[Next] = Temp;

            //검사 위치를 이동한다.
            Now = Next;
        }
    }

    public T Pop()
    {
        //반환할 데이터를 따로 저장해둔다.
        T PopData = _Heap[0];

        //마지막 데이터를 루트로 이동시켜주고 삭제한다.
        int LastIndex = _Heap.Count - 1;
        _Heap[0] = _Heap[LastIndex];
        _Heap.RemoveAt(LastIndex);
        LastIndex--;

        //루트로 이동해준 데이터를 힙트리에 맞게 구성
        int Now = 0;

        while (true)
        {
            //왼쪽 자식의 인덱스 값 구하기
            int Left = 2 * Now + 1;
            //오른쪽 자식의 인덱스 값 구하기
            int Right = 2 * Now + 2;

            int Next = Now;

            //왼쪽값이 현재값보다 크면, 왼쪽아래로 이동해준다.
            if (Left <= LastIndex && _Heap[Left].CompareTo(_Heap[Next]) > 0)
            {
                Next = Left;
            }

            //오른쪽 값이 현재값((위에서 검사한 왼쪽값 검사한 값)보다 크면, 오른쪽 아래로 이동해준다.
            if (Right <= LastIndex && _Heap[Right].CompareTo(_Heap[Next]) > 0)
            {
                Next = Right;
            }

            //왼쪽/오른쪽 모두 현재값보다 작으면 종료
            if (Next == Now)
            {
                break;
            }

            //두 값을 교체해준다.
            T Temp = _Heap[Now];
            _Heap[Now] = _Heap[Next];
            _Heap[Next] = Temp;

            //검사 위치를 이동한다.
            Now = Next;
        }

        return PopData;
    }

    public int Count()
    {
        return _Heap.Count;
    }
}