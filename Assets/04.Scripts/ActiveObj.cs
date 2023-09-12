using System.Collections.Generic;
using UnityEngine;

public class ActiveObj : MonoBehaviour
{
	public List<GameObject> ActiveObjList;

	public void Awake()
	{
		foreach (var obj in ActiveObjList)
			obj.SetActive(true);
	}
}
