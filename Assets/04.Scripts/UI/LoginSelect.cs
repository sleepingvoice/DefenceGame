using System;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;

public class LoginSelect : MonoBehaviour
{
    public Button LoginBtn;
    public Button NormalBtn;

    public LoginNickName NickNameCheck;

	private void Awake()
	{
        LoginBtn.onClick.AddListener(() =>
        {
            Login();
            NickNameCheck.CheckGoogleLogin = true;
        });
        NormalBtn.onClick.AddListener(() =>
        {
            MainGameData.s_progressLogin.SetValue(LoginProgress.nickNameSelect);
            NickNameCheck.CheckGoogleLogin = false;
        });

        MainGameData.s_progressLogin.AddListener((value) => this.gameObject.SetActive(value == LoginProgress.selectLogin));

    }

    private void Login()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Socket.ins.ws_SendMessage("Check_Login/" + Social.localUser.id);
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (!success)
                {
                    Debug.Log("로그인 정보가 잘못되었습니다");
                }
                else
                {
                    Socket.ins.ws_SendMessage("Check_Login/" + Social.localUser.id);
                }
            });
        }
    }


}
