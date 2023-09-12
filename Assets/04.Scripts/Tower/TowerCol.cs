using System.Collections.Generic;
using UnityEngine;

public class TowerCol : MonoBehaviour
{
	public List<Collider> EntryEnemyList = new List<Collider>();
	private List<Collider> NotEnemy = new List<Collider>();

	private void Start()
	{
		MainGameData.s_gameData.EnemyList.AddListener((value) =>
		{
			NotEnemy = new List<Collider>();

			foreach (var obj in EntryEnemyList)
			{
				if (!value.Contains(obj.GetComponent<EnemyInfo>()))
				{
					NotEnemy.Add(obj);
				}
			}

			foreach (var obj in NotEnemy)
			{
				EntryEnemyList.Remove(obj);
			}
		});
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<EnemyInfo>())
		{
			EntryEnemyList.Add(other);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (EntryEnemyList.Contains(other))
		{
			EntryEnemyList.Remove(other);
		}
	}
}
