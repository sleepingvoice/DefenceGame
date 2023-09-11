using System.Collections;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private int MaxSpeed;
	private int MaxHp;

	public int NowHp;
	public int NowSpeed;

	public Type<int> TargetNum = new Type<int>(0);
	public Type<Vector3> MovePos = new Type<Vector3>(Vector3.zero);
	public bool Die = false;

	public void Init(int round)
	{
		TargetNum = new Type<int>();
		MovePos = new Type<Vector3>(Vector3.zero);

		MainGameData.s_gameData.EnemyList.Value.Add(this);

		var info= MainGameData.s_serverData.EnemyInfo[round];

		MaxSpeed = info.Speed;
		MaxHp = info.hp;
		NowHp = MaxHp;
		NowSpeed = MaxSpeed;

		MovePos.AddListener((value) =>
		{
			StopAllCoroutines();
			StartCoroutine(MoveEnemy());
		});

		TargetNum.AddListener((value) =>
		{
			if (MainGameData.s_serverData.Codinate.Count <= value)
			{
				TargetNum.SetValue(0);
			}
			else
			{
				MovePos.SetValue(MainGameData.s_serverData.Codinate[value].CenterPoint);
			}
		});

		TargetNum.SetValue(0);
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
		this.transform.LookAt(MovePos.Value);
		Vector3 TargetDir = Vector3.Normalize(MovePos.Value - this.transform.position);

		while (Vector3.Distance(MovePos.Value, this.transform.position) * 1000 > 1)
		{
			this.transform.Translate(TargetDir * NowSpeed * 0.01f,Space.World);
			yield return new WaitForEndOfFrame();
		}

		TargetNum.SetValue(TargetNum.Value+1);
	}
}
