using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

namespace Gu
{
	public static class utility
	{
		private static Dictionary<bool, Queue<int>> UiFingerState = new Dictionary<bool, Queue<int>>();

		public static T DeepCopy<T>(T listToCopy)
		{
			string json = JsonUtility.ToJson(listToCopy);
			return JsonUtility.FromJson<T>(json);
		}

		public static Vector3 SetPosWorld(Camera targetCam, Vector2 screenPos, LayerMask Mask)
		{
			Ray ray = targetCam.ScreenPointToRay(screenPos);

			if (Physics.Raycast(ray, out RaycastHit hit, 1000, Mask))
			{
				return hit.point;
			}
			return Vector3.zero;
		}

		public static void MobileTouch_One(Action<Touch> UITouchAct = null, Action<Touch> TouchAct = null, Action NoTouchAct = null)
		{

			if (Input.touchCount != 0)
			{
				if (IsPointerOverUIObject(Input.GetTouch(0).position))
					TouchAct.Invoke(Input.GetTouch(0)); // UI ��ġ ���Ѱ�
				else
					UITouchAct.Invoke(Input.GetTouch(0));
			}
			else
				NoTouchAct.Invoke();
		}

		public static void MobileTouch_Two(Action<Touch> OneTouch = null, Action<List<Touch>> TwoTouch = null, Action NoTouch = null)
		{
			int Count = Input.touchCount;
			Queue<int> NoTouchQueue = CheckTouchNum(Count);

			if (NoTouchQueue.Count > 0)
			{
				if (NoTouchQueue.Count >= 2) // 2�� �̻� UI�� ��ġ���ϰ�������
				{
					List<Touch> TouchList = new List<Touch>();
					TouchList.Add(Input.GetTouch(NoTouchQueue.Dequeue()));
					TouchList.Add(Input.GetTouch(NoTouchQueue.Dequeue()));
					TwoTouch.Invoke(TouchList);
				}
				else if (NoTouchQueue.Count == 1) // Ui�� ��ġ�����ʾҰ� �ϳ��� Ŭ���ϰ�������
				{
					OneTouch.Invoke(Input.GetTouch(NoTouchQueue.Dequeue()));
				}
			}
			else // UI�� Ŭ���ϰų� ��ġ�� ����������
			{
				NoTouch.Invoke();
			}
		}

		public static Queue<int> CheckTouchNum(int Count)
		{
			if (UiFingerState.Count == 0)
			{
				UiFingerState[true] = new Queue<int>();
				UiFingerState[false] = new Queue<int>();
			}

			for (int i = 0; i < Count; i++)
			{
				if (Input.GetTouch(i).phase == UnityEngine.TouchPhase.Began) // ó�� ��ġ�Ѱ�
				{
					int id = Input.GetTouch(i).fingerId;
					if (IsPointerOverUIObject(Input.GetTouch(i).position)) // Ui������ ó�� ��ġ�Ҷ�
					{
						UiFingerState[false].Enqueue(id); // UI ���� ��ġ�Ѱ�
					}
					else
					{
						UiFingerState[true].Enqueue(id); // UI ��ġ ���Ѱ�
					}
				}
			}

			Queue<int> NoTouch = new Queue<int>();
			var tmpQueue = new Queue<int>(UiFingerState[true]);

			//���� �����ִ��� üũ
			for (int i = 0; i < UiFingerState[true].Count; i++)
			{
				int fingerId = tmpQueue.Dequeue();
				for (int j = 0; j < Count; j++)
				{
					if (fingerId == Input.GetTouch(j).fingerId)
					{
						NoTouch.Enqueue(j);
						break;
					}
				}
			}

			return NoTouch;
		}

		public static bool IsPointerOverUIObject(Vector2 touchPosition)
		{
			var eventData = new PointerEventData(EventSystem.current) { position = touchPosition };
			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventData, results);
			return results.Count > 0;
		}

		public static Texture2D textureFromSprite(Sprite sprite)
		{
			if (sprite.rect.width != sprite.texture.width)
			{
				Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
				Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
															 (int)sprite.textureRect.y,
															 (int)sprite.textureRect.width,
															 (int)sprite.textureRect.height);
				newText.SetPixels(newColors);
				newText.Apply();
				return newText;
			}
			else
				return sprite.texture;
		}

		public static IEnumerator GetJson(string url, Action<string> success = null, Action<UnityWebRequest> fail = null, Action complete = null, Action<float> progress = null)
		{
			using (UnityWebRequest www = UnityWebRequest.Get(url))
			{
				yield return DownloadProgress(www.SendWebRequest(), progress);
				if (www.result != UnityWebRequest.Result.Success) //�ҷ����� ���� ��
				{
					Debug.Log("Error : " + www.result + www.error + url);
					fail?.Invoke(www);
				}
				else
				{
					success?.Invoke(System.Text.Encoding.UTF8.GetString(www.downloadHandler.data));
				}
				complete?.Invoke();
			}
		}

		public static IEnumerator DownloadProgress(UnityWebRequestAsyncOperation operation, Action<float> Progress, UnityWebRequest Test = null)
		{
			while (!operation.isDone)
			{
				Progress?.Invoke(operation.progress);
				yield return null;
			}
		}
	}
}