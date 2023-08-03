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

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
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
    private AStarNode[,] _nodeArray;
    private MapData _mapInfo = MainGameData.s_mapInfo;

    private Vector2Int _startPos, _targetPos;
    private List<Vector2Int> _openPosList,_closePosList, _finalPosList;
    private Vector2Int _curPos;

    public List<MapAreaInfo> PathFindingAstar(MapAreaInfo startArea, MapAreaInfo finalArea)
    {
        _nodeArray = new AStarNode[_mapInfo.Width, _mapInfo.Hegith];
        foreach (var info in _mapInfo.PointList)
        {
            _nodeArray[info.Key.x, info.Key.y] = new AStarNode(info.Value);
        }

        _startPos = startArea.NodeNum;
        _targetPos = finalArea.NodeNum;

        _openPosList = new List<Vector2Int>() { _startPos };
        _closePosList = new List<Vector2Int>();
        _finalPosList = new List<Vector2Int>();

        while (_openPosList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            _curPos = _openPosList[0];
            for (int i = 1; i < _openPosList.Count; i++)
                if (FindNode(_openPosList[i]).F <= FindNode(_curPos).F && FindNode(_openPosList[i]).H < FindNode(_curPos).H)
                    _curPos = _openPosList[i];

            _openPosList.Remove(_curPos);
            _closePosList.Add(_curPos);

            // 마지막
            if (_curPos == _targetPos)
            {
                Vector2Int TargetCurPos = _targetPos;
                while (TargetCurPos != _startPos)
                {
                    _finalPosList.Add(TargetCurPos);
                    TargetCurPos = FindNode(TargetCurPos).ParentPos;
                }
                _finalPosList.Add(_startPos);
                _finalPosList.Reverse();

                break;
            }

            // ↑ → ↓ ←
            OpenListAdd(_curPos.x, _curPos.y + 1);
            OpenListAdd(_curPos.x + 1, _curPos.y);
            OpenListAdd(_curPos.x, _curPos.y - 1);
            OpenListAdd(_curPos.x - 1, _curPos.y);
        }

        List<MapAreaInfo> mapInfoList = new List<MapAreaInfo>();
        for (int i = 1; i < _finalPosList.Count; i++)
        {
            mapInfoList.Add(FindNode(_finalPosList[i]).InfoNum);
        }

        return mapInfoList;
    }

    private AStarNode FindNode(Vector2Int pos)
    {
        return _nodeArray[pos.x, pos.y];
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= 0 && checkX < _mapInfo.Width && checkY >= 0 && checkY < _mapInfo.Hegith && 
            !_nodeArray[checkX, checkY].NotMove && !_closePosList.Contains(new Vector2Int(checkX, checkY)))
        {
            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            AStarNode neighborNode = _nodeArray[checkX, checkY];
            AStarNode checknode = FindNode(_curPos);
            int moveCost = checknode.G + (checknode.Pos.x == checkX || checknode.Pos.y == checkY ? 10 : 14);

            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (moveCost < neighborNode.G || !_openPosList.Contains(neighborNode.Pos))
            {
                neighborNode.G = moveCost;
                neighborNode.H = (Mathf.Abs(neighborNode.Pos.x - _targetPos.x) + Mathf.Abs(neighborNode.Pos.y - _targetPos.y)) * 10;
                neighborNode.ParentPos = _curPos;

                _openPosList.Add(neighborNode.Pos);
            }
        }
    }
}
