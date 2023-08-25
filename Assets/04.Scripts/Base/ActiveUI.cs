using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUI : MonoBehaviour
{
	public List<GameObject> ActiveList;

	private void Awake()
	{
		foreach (var active in ActiveList)
		{
			active.gameObject.SetActive(true);
		}
	}
}
