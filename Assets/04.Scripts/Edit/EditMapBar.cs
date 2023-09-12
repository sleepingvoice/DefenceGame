using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditMapBar : MonoBehaviour
{
	[HideInInspector] public MapInfo MapInfo;
	[HideInInspector] public EditMapWindow Window;

	public TMP_Text NumTex;
	public TMP_Text NameTex;
	public Button SelectBtn;
	public GameObject SelectImg;

	public void InitBar(MapInfo Info, EditMapWindow Parent)
	{
		Window = Parent;
		MapInfo = Info;

		NumTex.text = MapInfo.mapid.ToString();
		NameTex.text = MapInfo.mapName;
	}
}
