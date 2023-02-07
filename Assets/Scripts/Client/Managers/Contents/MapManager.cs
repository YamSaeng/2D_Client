using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct st_Position
{
    public int _Y;
    public int _X;

    public st_Position(int Y, int X)
    {
        _Y = Y;
        _X = X;
    }
}

public struct st_AStarNode : IComparable<st_AStarNode>
{
    public int _F;
    public int _G;
    public int _X;
    public int _Y;

    public int CompareTo(st_AStarNode Other)
    {
        if (_F == Other._F)
        {
            return 0;
        }
        else
        {
            return _F < Other._F ? 1 : -1;
        }
    }
}

public class MapManager
{
    public Grid CurrentGrid { get; private set; }

    public int MinX { get; set; }
    public int MinY { get; set; }
    public int MaxX { get; set; }
    public int MaxY { get; set; }
    public int SizeX { get { return MaxX - MinX + 1; } }
    public int SizeY { get { return MaxY - MinY + 1; } }

    bool[,] _CollisionMapInfo;

    // 유니티 좌표를 받아와서 해당 타일로 오브젝트가 움직일 수 있는지 확인
    public bool CanGo(Vector3Int CellPos)
    {
        //받아온 CellPos X 값 확인
        if (CellPos.x < MinX || CellPos.x > MaxX)
        {
            return false;
        }

        //받아온 CellPos Y 값 확인
        if (CellPos.y < MinY || CellPos.y > MaxY)
        {
            return false;
        }

        int x = CellPos.x - MinX;
        int y = MaxY - CellPos.y;        
        // 컬리전이 없다면 갈수 있는것이므로 ! 붙여줌
        // 즉 1 이면 0, 0이면 1 리턴
        return !_CollisionMapInfo[y, x];
    }

    public void LoadMap(en_ResourceName MapName)
    {
        DestroyMap();
                
        GameObject Go = Managers.Resource.Instantiate(MapName);
        Go.name = "Map";

        GameObject TilemapCollision = Util.FindChild(Go, "Tilemap_Collision", true);
        if (TilemapCollision != null)
        {
            TilemapCollision.SetActive(false);
        }

        CurrentGrid = Go.GetComponent<Grid>();

        //Collision 관련 text파일을 읽어들인다.
        TextAsset MapInfo = Managers.Resource.Load<TextAsset>($"Map/{MapName}");
        StringReader Reader = new StringReader(MapInfo.text);

        MinX = int.Parse(Reader.ReadLine());
        MaxX = int.Parse(Reader.ReadLine());
        MinY = int.Parse(Reader.ReadLine());        
        MaxY = int.Parse(Reader.ReadLine());

        int xCount = MaxX - MinX + 1;
        int yCount = MaxY - MinY + 1;

        _CollisionMapInfo = new bool[yCount, xCount];

        for (int y = 0; y < yCount; y++)
        {
            string line = Reader.ReadLine();
            for (int x = 0; x < xCount; x++)
            {
                if (line[x] == '1')
                {
                    _CollisionMapInfo[y, x] = true;
                }
                else
                {
                    _CollisionMapInfo[y, x] = false;
                }
            }
        }
    }

    public void DestroyMap()
    {
        GameObject Map = GameObject.Find("Map");
        if (Map != null)
        {
            GameObject.Destroy(Map);
            CurrentGrid = null;
        }
    }

    #region A* PathFinding

    int[] _DeltaY = new int[] { 1, -1, 0, 0 };
    int[] _DeltaX = new int[] { 0, 0, -1, 1 };
    int[] _Cost = new int[] { 10, 10, 10, 10 };

    //CellPostion기준으로 좌표를 받아서 AStar를 이용해 길을 찾아낸 후 찾아낸 길을 배열형태로 반환해준다.
    public List<Vector3Int> FindPath(Vector3Int StartCellPosition, Vector3Int DestCellPostion, bool IgnoreDestCollistion = false)
    {
        List<st_Position> Path = new List<st_Position>();

        //점수 매기기
        // F = G + H
        // F = 최종 점수 ( 작을 수록 좋음, 경로에 따라 달라짐 )
        // G = 시작점에서 해당 좌표까지 이동하는데 드는 비용( 작을 수록 좋음, 경로에 따라 달라짐 )
        // H = 목적지에서 얼마나 가까운지 (작을 수록 좋음, 고정)

        //(y,x) 이미 방문했는지 여부 (방문 = closed 상태)
        bool[,] Closed = new bool[SizeY, SizeX]; //CloseList

        //(y,x) 가는 길을 한 번이라도 발견했는지 여부
        int[,] Open = new int[SizeY, SizeX];

        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                Open[y, x] = Int32.MaxValue;
            }
        }

        st_Position[,] Parents = new st_Position[SizeY, SizeX];

        //OpenList에 있는 정보들 중에서, 가장 좋은 후보를 빠르게 뽑아오기 위한 도구
        PriorityQueue<st_AStarNode> OpenListQue = new PriorityQueue<st_AStarNode>();

        st_Position StartPostion = CellToPostion(StartCellPosition);
        st_Position DestPostion = CellToPostion(DestCellPostion);

        //시작점 발견 (예약 진행) 하고 부모 저장
        Open[StartPostion._Y, StartPostion._X] = (Math.Abs(DestPostion._Y - StartPostion._Y) + Math.Abs(DestPostion._X - StartPostion._X));
        OpenListQue.Push(new st_AStarNode() { _F = (Math.Abs(DestPostion._Y - StartPostion._Y) + Math.Abs(DestPostion._X - StartPostion._X)), _G = 0, _Y = StartPostion._Y, _X = StartPostion._X });
        Parents[StartPostion._Y, StartPostion._X] = new st_Position(StartPostion._Y, StartPostion._X);

        while (OpenListQue.Count() > 0)
        {
            //노드 하나를 뽑아온다.
            st_AStarNode Node = OpenListQue.Pop();

            //방문했던 곳이라면 스킵한다.
            if (Closed[Node._Y, Node._X] == true)
            {
                continue;
            }

            //방문했다고 기록해둔다.
            Closed[Node._Y, Node._X] = true;

            //만약 뽑은 노드 위치가 목적지라면 종료해준다.
            if (Node._Y == DestPostion._Y && Node._X == DestPostion._X)
            {
                break;
            }

            // 상하좌우 이동할 수 있는 좌표인지 확인하면서 예약(OpenListQue에 Push)한다.
            for (int i = 0; i < _DeltaY.Length; i++)
            {
                st_Position NextPostion = new st_Position(Node._Y + _DeltaY[i], Node._X + _DeltaX[i]);

                //유효 범위를 벗어났거나 벽으로 막혀서 갈수 없으면 스킵한다.
                if (IgnoreDestCollistion == true || NextPostion._Y == DestPostion._Y || NextPostion._X != DestPostion._X)
                {
                    if (CanGo(PostionToCell(NextPostion)) == false)
                    {
                        continue;
                    }
                }

                //이미 방문했다면 스킵
                if(Closed[NextPostion._Y,NextPostion._X])
                {
                    continue;
                }

                //비용을 계산해준다.
                int G = Node._G + _Cost[i];
                int H = Math.Abs(DestPostion._Y - NextPostion._Y) + Math.Abs(DestPostion._X - NextPostion._X);

                //다른 경로에서 더 빠른 길을 이미 찾앗으면 넘겨준다.
                if(G + H > Open[NextPostion._Y,NextPostion._X])
                {
                    continue;
                }

                //예약 해준다.
                Open[NextPostion._Y, NextPostion._X] = G + H;
                OpenListQue.Push(new st_AStarNode() { _F = G + H, _G = G, _Y = NextPostion._Y, _X = NextPostion._X });
                //부모 위치 기록
                Parents[NextPostion._Y, NextPostion._X] = new st_Position(Node._Y, Node._X);
            }
        }

        //완성된 AStar 위치를 배열에 담아서 반환해준다.
        return CalcCellPathFromParent(Parents, DestPostion);
    }

    List<Vector3Int> CalcCellPathFromParent(st_Position[,] Parent, st_Position Dest)
    {
        List<Vector3Int> Cells = new List<Vector3Int>();

        int X = Dest._X;
        int Y = Dest._Y;

        while (Parent[Y, X]._Y != Y || Parent[Y, X]._X != X)
        {
            Cells.Add(PostionToCell(new st_Position(Y, X)));
            st_Position Postion = Parent[Y, X];
            X = Postion._X;
            Y = Postion._Y;
        }

        Cells.Add(PostionToCell(new st_Position(Y, X)));
        Cells.Reverse();

        return Cells;
    }

    st_Position CellToPostion(Vector3Int Cell)
    {
        st_Position Postion;
        Postion._Y = MaxY - Cell.y;
        Postion._X = Cell.x - MinX;

        return Postion;
    }

    Vector3Int PostionToCell(st_Position Postion)
    {
        Vector3Int Vector = new Vector3Int();

        Vector.x = Postion._X + MinX;
        Vector.y = MaxY - Postion._Y;
        Vector.z = 0;

        return Vector;
    }
    #endregion
}