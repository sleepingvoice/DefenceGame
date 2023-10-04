using System.Collections.Generic;
using UnityEngine;

public class ActiveUI : MonoBehaviour
{
	public List<GameObject> ActiveList;

	private void Awake()
	{
		foreach (var active in ActiveList)
		{
			if (active == null)
				continue;
			active.gameObject.SetActive(true);
		}
	}
}
