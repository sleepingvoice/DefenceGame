using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditSave : EditMenuBase
{
	public TMP_InputField MapNameInput;
	public CaptureImg Capture;
	public TMP_InputField NameField;

	public Button SaveBtn;

	protected override void Awake()
	{
		base.Awake();

		SaveBtn.onClick.AddListener(SetSave);
	}

	private void SetSave()
	{
		if (NameField.text == "")
			return;

		StartCoroutine(SaveFunction());
	}

	private IEnumerator SaveFunction()
	{
		var sendData = new MapInfo_Send();
		sendData.codinate =JsonUtility.ToJson(editManager.EditNode);
		sendData.enemyInfo = JsonUtility.ToJson(editManager.EnemyList);
		MapInfoList saveMapinfo = new MapInfoList();
		foreach (var info in MainGameData.s_serverData.AreaDic.Values)
		{
			var infosave = new MapAreaInfoSave();
			infosave.NodeNum = info.NodeNum;
			infosave.NotMove = info.Notmove;

			saveMapinfo.InfoList.Add(infosave);
		}
		sendData.movelist = JsonUtility.ToJson(saveMapinfo);
		sendData.userId = MainGameData.s_serverData.UserId;
		sendData.mapName = MapNameInput.text;

		yield return StartCoroutine(Capture.CaptureCam((value) => sendData.mapImg = value));

		var sendJson = JsonUtility.ToJson(sendData);

		Socket.ins.ws_SendMessage("Add_Map/" + sendJson);

		MainGameData.s_progressEdit.SetValue(EditProgrss.main);
		MainGameData.s_progressMainGame.SetValue(GameProgress.EditSelect);
		GameManager.ins.GetMapinfo();
	}
}
