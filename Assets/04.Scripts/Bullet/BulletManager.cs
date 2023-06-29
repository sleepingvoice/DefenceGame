using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gu;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
	public ObjPool ObjPool;

	public List<GameObject> BulletModelList;
	public List<GameObject> BulletEffectList;

	public BulletInfo BulletSet(KeyValuePair<int, int> Value)
	{
		var Bullet = ObjPool.GetObject();
		Bullet.GetComponent<BulletInfo>().SetBullet(BulletModelList[Value.Key], BulletEffectList[Value.Value]);
		return Bullet.GetComponent<BulletInfo>();
	}

	public void FinishBullet(GameObject obj)
	{
		ObjPool.DisableObject(obj);
	}
}