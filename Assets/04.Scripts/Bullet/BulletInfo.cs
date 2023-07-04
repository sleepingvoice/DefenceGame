using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gu;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
	public ObjPool Model;
	public ObjPool Effect;

	public void SetBullet(GameObject model,GameObject effect)
	{
		Model.SetPrefab = model;
		Effect.SetPrefab = effect;

		Model.RemoveList();
		Effect.RemoveList();
		
		Model.GetObject();
		Effect.GetObject().GetComponent<ParticleSystem>().Stop();
	}


	public async UniTaskVoid ShotBullet(Vector3 StartPos, EnemyInfo Target, float BulletSpeed, Action Act)
	{
		this.transform.position = StartPos + Vector3.up * 3.5f;
		Vector3 dir;
		while (Vector3.Distance(this.transform.position, Target.transform.position) * 10 > 1)
		{
			if (Target.NowHp <= 0)
				break;

			dir = Target.transform.position - this.transform.position;
			dir = dir.normalized;
			this.transform.position += dir * BulletSpeed * 0.2f;
			await UniTask.Delay(20);
		}
		this.transform.position = Target.transform.position;
		Effect.GetComponentInChildren<ParticleSystem>().Play();

		await UniTask.Delay(100);

		Act.Invoke();

		Effect.GetComponentInChildren<ParticleSystem>().Stop();
		Model.ReturnObjectAll();
		Effect.ReturnObjectAll();

		GameManager.ins.BulletManager.FinishBullet(this.gameObject);
	}

}
