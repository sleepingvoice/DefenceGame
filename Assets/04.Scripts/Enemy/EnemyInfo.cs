using System.Collections;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private int MaxSpeed;
	private int MaxHp;

	public int NowHp;
	public int NowSpeed;

	public Type<int> TargetNum = new Type<int>(0);
	public bool Die = false;

	private Type<Vector3> _movePos = new Type<Vector3>(Vector3.zero);
	public Vector2Int CheckMovePos;

	public void Init(int round)
	{
		TargetNum = new Type<int>();
		_movePos = new Type<Vector3>(Vector3.zero);

		MainGameData.s_gameData.EnemyList.Value.Add(this);

		var info= MainGameData.s_serverData.EnemyInfo[round];

		MaxSpeed = info.Speed;
		MaxHp = info.hp;
		NowHp = MaxHp;
		NowSpeed = MaxSpeed;

		_movePos.AddListener((value) =>
		{
			StopAllCoroutines();
			StartCoroutine(MoveEnemy());
		});

		TargetNum.AddListener((value) =>
		{
			if (MainGameData.s_gameData.MoveList.Count <= value)
			{
				TargetNum.SetValue(0);
			}
			else
			{
				_movePos.SetValue(MainGameData.s_gameData.MoveList[value].CenterPoint);
				CheckMovePos = MainGameData.s_gameData.MoveList[value].NodeNum;
			}
		});

		TargetNum.SetValue(1);
	}

	public void Demaged(int Damage)
	{
		NowHp -= Damage;
		if (NowHp <= 0 && !Die)
		{
			Die = true;
			MainGameData.s_gameData.NowMoney.SetValue(MainGameData.s_gameData.NowMoney.Value + 100);
			MainGameData.s_gameData.NowEnemyNum.SetValue(MainGameData.s_gameData.NowEnemyNum.Value - 1);
			GameManager.ins.EnemyManager.DieEnemy(this);
		}
	}

	public IEnumerator MoveEnemy()
	{
		this.transform.LookAt(_movePos.Value);
		Vector3 TargetDir = Vector3.Normalize(_movePos.Value - this.transform.position);

		while (Vector3.Distance(_movePos.Value, this.transform.position) * 10 > 1)
		{
			this.transform.Translate(TargetDir * NowSpeed * 0.01f,Space.World);
			yield return new WaitForEndOfFrame();
		}

		TargetNum.SetValue(TargetNum.Value+1);
	}
}
