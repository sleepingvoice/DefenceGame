using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditCodinateMenu : MonoBehaviour
{
	public Button SelectBtn;
	public int CodinateNum;
	public TMP_Text Tex;
	public AreaInfo TargetInfo = null;

	public void SelectCheck(bool Select)
	{
		var tmpColor = this.GetComponent<Image>().color;
		if (Select)
		{
			tmpColor = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 1f);
			this.GetComponent<Image>().color = tmpColor;
		}
		else
		{
			tmpColor = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 0.3f);
			this.GetComponent<Image>().color = tmpColor;
		}
	}
}
