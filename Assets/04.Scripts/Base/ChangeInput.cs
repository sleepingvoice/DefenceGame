using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class ChangeInput : MonoBehaviour
{
	void Start()
	{
		this.GetComponent<TMP_InputField>().onSelect.AddListener((str) => this.GetComponent<TMP_InputField>().text = "");
	}
}
