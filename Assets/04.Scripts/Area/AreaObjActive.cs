using System.Collections.Generic;
using UnityEngine;

public class AreaObjActive : MonoBehaviour
{
	public List<GameProgress> ShowProgress;
	private bool _activeCheck = false;

	private void Awake()
	{

		MainGameData.s_progressMainGame.AddListener((value) =>
		{
			_activeCheck = false;
			foreach (var progress in ShowProgress)
			{
				if (value == progress)
					_activeCheck = true;
			}
			if (_activeCheck)
				this.gameObject.SetActive(true);
			else
				this.gameObject.SetActive(false);
		});
	}
}
