using Gu;
using UnityEngine;
using UnityEngine.UI;

public class EditMapWindow : MonoBehaviour
{
    public ObjPool BarPool;
	public RawImage MenuImg;
	public Texture SampleTex;
	public Button StartGameBtn;

	private EditMapBar _selectMap = null;

	private void Awake()
	{
		StartGameBtn.onClick.AddListener(GameStart);
	}

	public void InitBar()
	{
		BarPool.ReturnObjectAll();

		if (_selectMap != null)
			_selectMap.SelectImg.SetActive(false);

		_selectMap = null;
		MenuImg.texture = SampleTex;
		StartGameBtn.interactable = false;

		var infoList = MainGameData.s_serverData.MapinfoSever.List;

		foreach (var info in infoList)
		{
			var obj = BarPool.GetObject().GetComponent<EditMapBar>();
			obj.InitBar(info, this);
			obj.SelectBtn.onClick.AddListener(() => SelectBar(obj));
		}
	}

	private void SelectBar(EditMapBar TargetBar)
	{
		if (_selectMap != null)
			_selectMap.SelectImg.SetActive(false);

		_selectMap = TargetBar;
		_selectMap.SelectImg.SetActive(true);

		GameManager.ins.SetImg(TargetBar.MapInfo.mapImg, (tex) => MenuImg.texture = tex);
		StartGameBtn.interactable = true;
	}

	private void GameStart()
	{
		MainGameData.s_serverData.NowMap.SetValue(_selectMap.MapInfo);
		GameManager.ins.GameStart();
	}
}
