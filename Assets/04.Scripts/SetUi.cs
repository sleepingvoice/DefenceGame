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
		MainGameData.Money.AddListener((value) => Money.text = value.ToString());
		MainGameData.EnemyNum.AddListener((value) => UnityNum.text = value.ToString());

		MainGameData.Money.SetValue(MainGameData.Money.Value);
		MainGameData.EnemyNum.SetValue(MainGameData.EnemyNum.Value);
	}

}
