using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditManager : MonoBehaviour
{
    private MapData MapInfo = MainGameData.s_mapData;
    private bool Edit = false;

    private void Awake()
    {
        MainGameData.s_progressValue.AddListener(Active);

    }

	private void Start()
	{
        MainGameData.s_editProgress.SetValue(EditProgrss.main);
    }

	private void Active(GameProgress progress)
    {
        this.gameObject.SetActive(progress == GameProgress.Edit);

        if (progress == GameProgress.Edit)
        {
            GameManager.ins.AreaManager.SetMapObj();
        }
    }

	private void Init()
    {

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
