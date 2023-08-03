using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetEditUI : MonoBehaviour
{
    public Button ChangeBtn;
    public Button Before;

    private MapData MapInfo = MainGameData.s_mapInfo;
    private bool Edit = false;

    void Awake()
    {
        MainGameData.s_progressValue.AddListener(active);
        Before.onClick.AddListener(() => MainGameData.s_progressValue.SetValue(GameProgress.Lobby));
        ChangeBtn.onClick.AddListener(ChangeOn);
    }

    private void active(GameProgress Progress)
    {
        this.gameObject.SetActive(Progress == GameProgress.Edit);
    }

    private void ChangeOn()
    {
        foreach (var obj in MapInfo.NotBuildObj)
        {
            obj.SetActive(Edit);
        }
        Edit = !Edit;
    }
}
