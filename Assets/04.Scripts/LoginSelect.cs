using GooglePlayGames;
using UnityEngine;
using UnityEngine.UI;

public class LoginSelect : MonoBehaviour
{
    public Button LoginBtn;
    public Button NormalBtn;

    private void Awake()
    {
        LoginBtn.onClick.AddListener(() =>
        {
            Login();
        });
        NormalBtn.onClick.AddListener(() =>
        {
            MainGameData.s_progressLogin.SetValue(LoginProgress.main);
        });

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        MainGameData.s_progressLogin.AddListener((value) => this.gameObject.SetActive(value == LoginProgress.select));
    }

    private void Login()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (!success)
                    Debug.Log("로그인 정보가 잘못되었습니다");
                else
                {
                    MainGameData.s_progressLogin.SetValue(LoginProgress.finish);
                    MainGameData.s_progressMainGame.SetValue(GameProgress.Lobby);
                }
            });
        }
    }

}
