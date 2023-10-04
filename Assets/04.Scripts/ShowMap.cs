using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour
{
	public Material AreaMat;
	public Color NormalColor;
	[HideInInspector] public Material NormalMat;

	[Header("설치 가능 구역")]
	public Color BuildColor;
	[HideInInspector] public Material BuildMat;

	[Header("꼭짓점")]
	public List<Color> CodinateColor;
	[HideInInspector] public List<Material> CodinateMat;

	private void Start()
	{
		MatReturn();
	}

	private void MatReturn()
	{
		NormalMat = Instantiate(AreaMat);
		NormalMat.SetColor("_OutlineColor", NormalColor);

		BuildMat = Instantiate(AreaMat);
		BuildMat.SetColor("_OutlineColor", BuildColor);

		foreach (var color in CodinateColor)
		{
			var mat = Instantiate(AreaMat);
			mat.SetColor("_OutlineColor", color);
			CodinateMat.Add(mat);
		}
	}

	public void SetMapColor(AreaInfo info)
	{
		if (info.Notmove)
			info.OutLineObj.GetComponent<MeshRenderer>().material = BuildMat;
		else
			info.OutLineObj.GetComponent<MeshRenderer>().material = NormalMat;
	}

	public void SetCodinateColor(List<AreaInfo> Codinate)
	{
		for (int i = 0; i < Codinate.Count; i++)
		{
			Codinate[i].OutLineObj.GetComponent<MeshRenderer>().material = CodinateMat[i];
		}
	}
}
