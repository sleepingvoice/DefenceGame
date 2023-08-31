using System.Collections;
using System.Collections.Generic;
using Gu;
using UnityEngine;
using UnityEngine.UI;

public class CaptureImg : MonoBehaviour
{
	public byte[] Texbinary;

	public IEnumerator CaptureCam(Camera TargetCam,int resWidth,int resHeight)
	{
		yield return new WaitForEndOfFrame();

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
		Texbinary = temptex.EncodeToPNG();
	}
}
