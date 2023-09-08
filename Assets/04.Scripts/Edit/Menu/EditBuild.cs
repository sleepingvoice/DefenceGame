using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditBuild : EditMenuBase
{
    [Header("Function")]
    public Button CorrectBtn;
    public Color BuildColor;

    private MapData _mapData = MainGameData.s_mapData;
    private Material BuildMat;

    protected override void Awake()
	{
        base.Awake();
        _mapData.EditTouchMap.InsertDic(BuildClick);
        CorrectBtn.onClick.AddListener(() => MainGameData.s_editProgress.SetValue(EditProgrss.main));
    }

	protected override void Start()
	{
        base.Start();
        BuildMat = Instantiate(editManager.AreaMat);
        BuildMat.SetColor("_OutlineColor", BuildColor);
    }

	private void BuildClick(AreaInfo targetarea)
    {
        if (MainGameData.s_editProgress.Value == EditProgrss.build)
        {
            bool CheckOnOff = _mapData.EditTouchMode == 1 ? true : false;

            if (targetarea.OutLineObj.activeSelf == CheckOnOff)
                return;

            targetarea.OutLineObj.SetActive(CheckOnOff);
            targetarea.OutLineObj.GetComponent<MeshRenderer>().material = CheckOnOff ? BuildMat : editManager.NormalMat;

            if (_mapData.AreaDic.ContainsKey(targetarea.NodeNum))
            {
                _mapData.AreaDic[targetarea.NodeNum].Notmove = CheckOnOff;
            }
        }
    }
}
