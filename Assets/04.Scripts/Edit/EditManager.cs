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
        MainGameData.s_mapData.EditTouchMap.InsertDic(BuildClick);
     }

	private void Start()
	{
        MainGameData.s_editProgress.SetValue(EditProgrss.main);
    }

    private void BuildClick(AreaInfo targetarea)
    {
        if (MainGameData.s_editProgress.Value == EditProgrss.build)
        {

            targetarea.OutLineObj.SetActive(MainGameData.s_mapData.EditTouchMode == 1 ? true : false);
        }
    }

	private void Active(GameProgress progress)
    {
        this.gameObject.SetActive(progress == GameProgress.Edit);

        if (progress == GameProgress.Edit)
        {
            GameManager.ins.AreaManager.SetMapObj();
        }
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
