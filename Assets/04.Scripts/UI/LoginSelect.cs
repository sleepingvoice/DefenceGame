using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
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
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);;
            NickNameCheck.CheckGoogleLogin = true;
        });
        NormalBtn.onClick.AddListener(() =>
        {
            MainGameData.s_progressLogin.SetValue(LoginProgress.nickNameSelect);
            NickNameCheck.CheckGoogleLogin = false;
        });

        MainGameData.s_progressLogin.AddListener((value) => this.gameObject.SetActive(value == LoginProgress.selectLogin));

    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            Socket.ins.ws_SendMessage("Check_Login/" + Social.localUser.id);
        }
        else
        {
            Debug.LogError("로그인 정보가 잘못되었습니다");
        }
    }


}
