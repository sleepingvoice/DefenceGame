using System;
using System.Collections;
using Gu;
using UnityEngine;
using UnityEngine.UI;

public class CaptureImg : MonoBehaviour
{
	[HideInInspector]public byte[] Texbinary;
	public Camera TargetCam;

	public int num;
	public RawImage Img;

	public void Set()
	{
		StartCoroutine(CaptureCam((value) => Debug.Log(value)));
	}

	public void Get()
	{
		StartCoroutine(LoadImg());
	}

	public IEnumerator CaptureCam(Action<int> actMapID)
	{
		yield return new WaitForEndOfFrame();
		int resWidth = Screen.width;
		int resHeight = Screen.height;

		RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
		TargetCam.targetTexture = rt;
		TargetCam.Render();

		Texture2D tempTex = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
		RenderTexture.active = rt;

		tempTex.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
		tempTex.Apply();

		TargetCam.targetTexture = null;
		RenderTexture.active = null;
		Destroy(rt);

		var tempSprite = Sprite.Create(tempTex, new Rect(0, 0, resWidth, resHeight), Vector2.one * 0.5f);
		var temptex = utility.textureFromSprite(tempSprite);
		var data = temptex.EncodeToPNG();

		yield return Socket.ins.PostImg(data, actMapID);
	}

	public IEnumerator LoadImg()
	{
		yield return Socket.ins.GetImg(num, (tex) => Img.texture = tex);
	}
}
