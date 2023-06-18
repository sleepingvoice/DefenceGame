using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AStarNode
{
    public AStarNode(MapAreaInfo info)
    {
        Pos = info.NodeNum;
        NotMove = info.NotMove;
        InfoNum = info;
    }

    public Vector2Int Pos;
    public bool NotMove;
    public MapAreaInfo InfoNum;
    public Vector2Int ParentPos;

    // G : �������κ��� �̵��ߴ� �Ÿ�, H : |����|+|����| ��ֹ� �����Ͽ� ��ǥ������ �Ÿ�, F : G + H
    public int G, H;
    public int F
    {
        get
        {
            return G + H;
        }
    }
}

public class AstarCheck : MonoBehaviour
{
    private AStarNode[,] NodeArray;
    private AreaInfo MapInfo = MainGameInfo.MapInfo;

    private Vector2Int StartPos, TargetPos;
    private List<Vector2Int> OpenPosList,ClosePosList, FinalPosList;
    private Vector2Int CurPos;

    public List<MapAreaInfo> PathFindingAstar(MapAreaInfo StartArea, MapAreaInfo FinalArea)
    {
        NodeArray = new AStarNode[MapInfo.Width, MapInfo.Hegith];
        foreach (var info in MapInfo.PointList)
        {
            NodeArray[info.Key.x, info.Key.y] = new AStarNode(info.Value);
        }

        StartPos = StartArea.NodeNum;
        TargetPos = FinalArea.NodeNum;

        OpenPosList = new List<Vector2Int>() { StartPos };
        ClosePosList = new List<Vector2Int>();
        FinalPosList = new List<Vector2Int>();

        while (OpenPosList.Count > 0)
        {
            // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
            CurPos = OpenPosList[0];
            for (int i = 1; i < OpenPosList.Count; i++)
                if (FindNode(OpenPosList[i]).F <= FindNode(CurPos).F && FindNode(OpenPosList[i]).H < FindNode(CurPos).H)
                    CurPos = OpenPosList[i];

            OpenPosList.Remove(CurPos);
            ClosePosList.Add(CurPos);

            // ������
            if (CurPos == TargetPos)
            {
                Vector2Int TargetCurPos = TargetPos;
                while (TargetCurPos != StartPos)
                {
                    FinalPosList.Add(TargetCurPos);
                    TargetCurPos = FindNode(TargetCurPos).ParentPos;
                }
                FinalPosList.Add(StartPos);
                FinalPosList.Reverse();

                break;
            }

            // �� �� �� ��
            OpenListAdd(CurPos.x, CurPos.y + 1);
            OpenListAdd(CurPos.x + 1, CurPos.y);
            OpenListAdd(CurPos.x, CurPos.y - 1);
            OpenListAdd(CurPos.x - 1, CurPos.y);
        }

        List<MapAreaInfo> MapInfoList = new List<MapAreaInfo>();
        for (int i = 0; i < FinalPosList.Count; i++)
        {
            MapInfoList.Add(FindNode(FinalPosList[i]).InfoNum);
        }

        return MapInfoList;
    }

    private AStarNode FindNode(Vector2Int Pos)
    {
        return NodeArray[Pos.x, Pos.y];
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ�
        if (checkX >= 0 && checkX < MapInfo.Width && checkY >= 0 && checkY < MapInfo.Hegith && !NodeArray[checkX, checkY].NotMove && !ClosePosList.Contains(new Vector2Int(checkX, checkY)))
        {
            // �̿���忡 �ְ�, ������ 10, �밢���� 14���
            AStarNode NeighborNode = NodeArray[checkX, checkY];
            AStarNode Checknode = FindNode(CurPos);
            int MoveCost = Checknode.G + (Checknode.Pos.x == checkX || Checknode.Pos.y == checkY ? 10 : 14);

            // �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
            if (MoveCost < NeighborNode.G || !OpenPosList.Contains(NeighborNode.Pos))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.Pos.x - TargetPos.x) + Mathf.Abs(NeighborNode.Pos.y - TargetPos.y)) * 10;
                NeighborNode.ParentPos = CurPos;

                OpenPosList.Add(NeighborNode.Pos);
            }
        }
    }
}
