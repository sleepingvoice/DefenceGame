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
	public EnemyManager EnemyManager;
	public Camera MainCam;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		SetTowerClass();
	}


	//Ÿ�� �ɷ�ġ �޾ƿ���
	private void SetTowerClass()
	{
		var List = JsonUtility.FromJson<TowerList>(System.IO.File.ReadAllText(Application.streamingAssetsPath + "/TowerClass.json"));
		foreach (var value in List.Tower)
		{
			MainGameData.TowerState.Add(value.RankValue, value);
		}
	}
}
