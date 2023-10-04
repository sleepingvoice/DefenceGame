using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CaptureImg : MonoBehaviour
{
	[HideInInspector] public byte[] Texbinary;

	public Camera TargetCam;
	public RawImage TargetImg;
	[HideInInspector]public Texture2D NowShowTex = null;

	public IEnumerator CaptureCam()
	{
		TargetCam.gameObject.SetActive(true);
		if (NowShowTex != null)
			DestroyImmediate(NowShowTex);

		NowShowTex = null;
		yield return new WaitForEndOfFrame();
		int resWidth = Screen.width;
		int resHeight = Screen.height;

		RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
		TargetCam.targetTexture = rt;
		TargetCam.Render();

		NowShowTex = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
		RenderTexture.active = rt;

		NowShowTex.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
		NowShowTex.Apply();

		TargetCam.targetTexture = null;
		RenderTexture.active = null;
		Destroy(rt);

		TargetImg.texture = NowShowTex;
		TargetCam.gameObject.SetActive(false);
	}
}
