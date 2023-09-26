using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginNickName : MonoBehaviour
{
	public TMP_InputField Input_Nick;
	public Button NickBtn;
	[HideInInspector] public bool CheckGoogleLogin = false;

	private void Awake()
	{
		NickBtn.onClick.AddListener(AddID);
		MainGameData.s_progressLogin.AddListener((value) => this.gameObject.SetActive(value == LoginProgress.nickNameSelect));
	}

	private void AddID()
	{
		if (Input_Nick.text != "")
		{
			if (CheckGoogleLogin)
			{
				Socket.ins.ws_SendMessage("Add_UserID/" + Input_Nick.text +"/"+ Social.localUser.id);
			}
			else
				Socket.ins.ws_SendMessage("Add_GuestID/" + Input_Nick.text);
		}
	}
}
