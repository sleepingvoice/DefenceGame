using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private int MaxSpeed;
	private int MaxHp;

	public int NowHp;
	public int NowSpeed;

	public Type<int> TargetNum = new Type<int>(0);
	public Type<Vector3> MovePos = new Type<Vector3>(Vector3.zero);

	public void Init(EnemyData Data,int Hp,int Speed)
	{
		TargetNum = new Type<int>();
		MovePos = new Type<Vector3>(Vector3.zero);

		MainGameData.EnemyInfo.EnemyList.Value.Add(this);

		MaxSpeed = Speed;
		MaxHp = Hp;
		NowHp = MaxHp;
		NowSpeed = MaxSpeed;

		MovePos.AddListener((value) =>
		{
			StopAllCoroutines();
			StartCoroutine(MoveEnemy());
		});

		TargetNum.AddListener((value) =>
		{
			if (Data.TargetList.Count <= value)
			{
				TargetNum.SetValue(0);
			}
			else
			{
				MovePos.SetValue(Data.TargetList[value].CenterPoint);
			}
		});

		TargetNum.SetValue(0);
	}

	public void Demaged(int Damage)
	{
		NowHp -= Damage;
		if (NowHp <= 0)
		{
			MainGameData.Money.SetValue(MainGameData.Money.Value + 100);
			MainGameData.EnemyNum.SetValue(MainGameData.EnemyNum.Value - 1);
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
