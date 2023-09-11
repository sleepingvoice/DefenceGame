using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditBuild : EditMenuBase
{
    [Header("Function")]
    public Button CorrectBtn;
    public Color BuildColor;
    public bool CheckClick = false;

    private ServerData _mapData = MainGameData.s_serverData;
    private Material BuildMat;

    protected override void Awake()
	{
        base.Awake();
        MainGameData.EditTouchMap.InsertDic(BuildClick);
        CorrectBtn.onClick.AddListener(() => MainGameData.s_progressEdit.SetValue(EditProgrss.main));
    }

	protected override void Start()
	{
        base.Start();
        BuildMat = Instantiate(editManager.AreaMat);
        BuildMat.SetColor("_OutlineColor", BuildColor);
    }

	private void BuildClick(AreaInfo targetarea)
    {
        if (MainGameData.s_progressEdit.Value == EditProgrss.build)
        {
            if (targetarea.OutLineObj.activeSelf == CheckClick)
                return;

            targetarea.OutLineObj.SetActive(CheckClick);
            targetarea.OutLineObj.GetComponent<MeshRenderer>().material = CheckClick ? BuildMat : editManager.NormalMat;

            if (_mapData.AreaDic.ContainsKey(targetarea.NodeNum))
            {
                _mapData.AreaDic[targetarea.NodeNum].Notmove = CheckClick;
            }
        }
    }
}
