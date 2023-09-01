using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditBuild : EditMenuBase
{
    [Header("Function")]
    public Button CorrectBtn;

    private MapData _mapData = MainGameData.s_mapData;

    protected override void Awake()
	{
        base.Awake();
        _mapData.EditTouchMap.InsertDic(BuildClick);
        CorrectBtn.onClick.AddListener(() => MainGameData.s_editProgress.SetValue(EditProgrss.main));
    }

	private void BuildClick(AreaInfo targetarea)
    {
        if (MainGameData.s_editProgress.Value == EditProgrss.build)
        {
            bool CheckOnOff = _mapData.EditTouchMode == 1 ? true : false;

            if (targetarea.OutLineObj.activeSelf == CheckOnOff)
                return;

            targetarea.OutLineObj.SetActive(CheckOnOff);
            targetarea.OutLineObj.GetComponent<MeshRenderer>().material = editManager.AreaMat;

            if (_mapData.AreaDic.ContainsKey(targetarea.NodeNum))
            {
                _mapData.AreaDic[targetarea.NodeNum].Notmove = CheckOnOff;
            }
        }
    }
}
