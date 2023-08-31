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
		OutBtn.onClick.AddListener(() => MainGameData.s_progressValue.SetValue(GameProgress.EditSelect));
		BuildBtn.onClick.AddListener(() => MainGameData.s_editProgress.SetValue(EditProgrss.build));
		DestinationBtn.onClick.AddListener(() => MainGameData.s_editProgress.SetValue(EditProgrss.destination));
		EnemyInfoBtn.onClick.AddListener(() => MainGameData.s_editProgress.SetValue(EditProgrss.enemy));
		TowerInfoBtn.onClick.AddListener(() => MainGameData.s_editProgress.SetValue(EditProgrss.tower));
		SaveBtn.onClick.AddListener(() => MainGameData.s_editProgress.SetValue(EditProgrss.save));
	}
}
