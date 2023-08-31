using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class ChangeInput : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<TMP_InputField>().onSelect.AddListener((str) => this.GetComponent<TMP_InputField>().text = "");
    }
}
