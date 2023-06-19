using System.Collections;
using UnityEngine;
using Gu;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public static GameManager ins => Instance;

    public AreaManager AreaManager;
	public TowerManager TowerManager;
	public UpGradeManager UpGradeManager;
	public Camera MainCam;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		SetTowerClass();
	}


	//타워 능력치 받아오기
	private void SetTowerClass()
	{
		var List = JsonUtility.FromJson<TowerList>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/TowerClass.json"));
		foreach (var value in List.Tower)
		{
			MainGameInfo.TowerState.Add(value.RankValue, value);
		}
	}
}
