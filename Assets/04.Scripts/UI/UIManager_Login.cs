using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class UIManager_Login : MonoBehaviour
{
    private Action _socketAct = null;

    private void Start()
    {
        Socket.ins.SocketEventDic.Add("Check_Login", CheckLogin);
        Socket.ins.SocketEventDic.Add("Add_ID", AddId);

        PlayGamesPlatform.Activate();


        Socket.ins.OpenActDic.Add(() =>
        {
            if (PlayerPrefs.HasKey("LoginKey"))
            {
                Socket.ins.ws_SendMessage("Check_Login/" + PlayerPrefs.GetString("LoginKey"));
            }
        });
    }


    private void CheckLogin(string callback)
    {
        Debug.LogError(callback);
        if (callback == "false")
        {
            _socketAct = () =>
            {
                Debug.Log("아이디 정보가 없습니다.");
                MainGameData.s_progressLogin.SetValue(LoginProgress.nickNameSelect);
            };
        }
        else
        {
            _socketAct = () =>
            {
                var str = callback.Split(':');
                MainGameData.s_serverData.UserNickName = str[0];
                MainGameData.s_serverData.UserId = int.Parse(str[1]);
                MainGameData.s_progressLogin.SetValue(LoginProgress.finish);
                MainGameData.s_progressMainGame.SetValue(GameProgress.Lobby);
            };
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
            _socketAct = () =>
            {
                PlayerPrefs.SetString("LoginKey", callback);
                Socket.ins.ws_SendMessage("Check_Login/" + PlayerPrefs.GetString("LoginKey"));
            };
        }
    }


    private void Update()
    {
        if (_socketAct != null)
        {
            _socketAct.Invoke();
            _socketAct = null;
        }
    }
}
