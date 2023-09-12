using System.Collections.Generic;
using UnityEngine;


public class AStarNode
{
	public AStarNode(AreaInfo info)
	{
		Pos = info.NodeNum;
		NotMove = info.Notmove;
		InfoNum = info;
	}

	public Vector2Int Pos;
	public bool NotMove;
	public AreaInfo InfoNum;
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
	private AStarNode[,] _nodeArray;
	private ServerData _mapInfo = MainGameData.s_serverData;
	private ClientData _clientInfo = MainGameData.s_clientData;

	private Vector2Int _startPos, _targetPos;
	private List<Vector2Int> _openPosList, _closePosList, _finalPosList;
	private Vector2Int _curPos;

	public List<AreaInfo> PathFindingAstar(AreaInfo startArea, AreaInfo finalArea)
	{
		_nodeArray = new AStarNode[_clientInfo.Width, _clientInfo.Hegith];
		foreach (var info in _mapInfo.AreaDic)
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
			// ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
			_curPos = _openPosList[0];
			for (int i = 1; i < _openPosList.Count; i++)
				if (FindNode(_openPosList[i]).F <= FindNode(_curPos).F && FindNode(_openPosList[i]).H < FindNode(_curPos).H)
					_curPos = _openPosList[i];

			_openPosList.Remove(_curPos);
			_closePosList.Add(_curPos);

			// ������
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

			// �� �� �� ��
			OpenListAdd(_curPos.x, _curPos.y + 1);
			OpenListAdd(_curPos.x + 1, _curPos.y);
			OpenListAdd(_curPos.x, _curPos.y - 1);
			OpenListAdd(_curPos.x - 1, _curPos.y);
		}

		List<AreaInfo> mapInfoList = new List<AreaInfo>();
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
		// �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ�
		if (checkX >= 0 && checkX < _clientInfo.Width && checkY >= 0 && checkY < _clientInfo.Hegith &&
			!_nodeArray[checkX, checkY].NotMove && !_closePosList.Contains(new Vector2Int(checkX, checkY)))
		{
			// �̿���忡 �ְ�, ������ 10, �밢���� 14���
			AStarNode neighborNode = _nodeArray[checkX, checkY];
			AStarNode checknode = FindNode(_curPos);
			int moveCost = checknode.G + (checknode.Pos.x == checkX || checknode.Pos.y == checkY ? 10 : 14);

			// �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
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
