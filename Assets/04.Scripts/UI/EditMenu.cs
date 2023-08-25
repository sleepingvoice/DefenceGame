using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMenu : MonoBehaviour
{
	public Button OutBtn;
	public Button BuildBtn;
	public Button DestinationBtn;
	public Button EnemyInfoBtn;
	public Button TowerInfoBtn;
	public Button SaveBtn;

	public void Awake()
	{
		OutBtn.onClick.AddListener(() => MainGameData.s_progressValue.SetValue(GameProgress.Lobby));
	}

}
