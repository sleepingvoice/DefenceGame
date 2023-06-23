using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetUi : MonoBehaviour
{
	public TMP_Text Money;
	public TMP_Text UnityNum;
	public TMP_Text LimitTime;

	private void Awake()
	{
		MainGameInfo.Money.AddListener((value) => Money.text = value.ToString());
		MainGameInfo.EnmeyNum.AddListener((value) => UnityNum.text = value.ToString());
	}

	private void Start()
	{
		MainGameInfo.Money.SetValue(500);
		MainGameInfo.EnmeyNum.SetValue(0);
	}
}
