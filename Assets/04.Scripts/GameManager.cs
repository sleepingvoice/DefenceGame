using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gu;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public static GameManager ins => Instance;

    public AreaManager AreaManager;
	public TowerManager TowerManager;
	public Camera MainCam;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}


}
