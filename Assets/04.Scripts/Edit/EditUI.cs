using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditUI : MonoBehaviour
{
	public EditProgrss progress;

	private void Awake()
	{
		MainGameData.s_editProgress.AddListener((value) => this.gameObject.SetActive(value == progress));
	}
}
