using GooglePlayGames;
using UnityEngine;

public class UIManager_Login : MonoBehaviour
{
    public bool CheckWindow;
	private void Start()
    {
        Socket.ins.SocketEventDic.Add("Check_Login", CheckLogin);
        Socket.ins.SocketEventDic.Add("Add_ID", AddId);

        PlayGamesPlatform.Activate();

        if (PlayerPrefs.HasKey("LoginKey") && !CheckWindow)
        {
            Socket.ins.ws_SendMessage("Check_Login/" + PlayerPrefs.GetString("LoginKey"));
        }
    }


    private void CheckLogin(string callback)
    {
        if (callback == "false")
        {
            GameManager.ins.SoketAct.Enqueue(() =>
            {
                Debug.Log("아이디 정보가 없습니다.");
                MainGameData.s_progressLogin.SetValue(LoginProgress.nickNameSelect);
            });
        }
        else
        {
            GameManager.ins.SoketAct.Enqueue(() =>
            {
                if (!PlayerPrefs.HasKey("LoginKey")) // 구글로그인의 경우 아이디가 같지만 저장이 안되있을수 있다.
                {
                    PlayerPrefs.SetString("LoginKey", Social.localUser.id);
                }

                var str = callback.Split(':');
                MainGameData.s_serverData.UserNickName = str[0];
                MainGameData.s_serverData.UserId = int.Parse(str[1]);
                MainGameData.s_progressLogin.SetValue(LoginProgress.finish);
                MainGameData.s_progressMainGame.SetValue(GameProgress.Lobby);
            });
        }
    }

    private void AddId(string callback)
    {
        if (callback == "")
        {
            Debug.Log("에러입니다.");
        }
        else
        {
            GameManager.ins.SoketAct.Enqueue(() =>
            {
                PlayerPrefs.SetString("LoginKey", callback);
                Socket.ins.ws_SendMessage("Check_Login/" + PlayerPrefs.GetString("LoginKey"));
            });
        }
    }
}
