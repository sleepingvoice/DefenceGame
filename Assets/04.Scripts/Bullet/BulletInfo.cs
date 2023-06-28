using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gu;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
	public GameObject Effect;

	public void SetBullet(GameObject Model,GameObject Effect)
	{
		Instantiate(Model, this.transform);
		Effect = Instantiate(Effect, this.transform);
		Effect.GetComponent<ParticleSystem>().Stop();
	}

	public async UniTaskVoid ShotBullet(Vector3 StartPos, Transform EndPos, float BulletTime,Action Act)
	{
		float time = BulletTime;
		this.transform.position = StartPos;
		while (Vector3.Distance(this.transform.position, EndPos.transform.position) * 1000 < 1)
		{
			this.transform.position = Vector3.Lerp(this.transform.position, EndPos.transform.position, time);
			time -= 0.02f;
			await UniTask.Delay(20);	
		}
		this.transform.position = EndPos.transform.position;
		Effect.GetComponent<ParticleSystem>().Play();
		await UniTask.Delay(100);
		Act.Invoke();
		GameManager.ins.BulletManager.FinishBullet(this.gameObject);
	}
}
