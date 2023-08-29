using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class ChangeInput : MonoBehaviour
{
    private EventSystem _system;

    void Start()
    {
        _system = EventSystem.current;

        this.GetComponent<TMP_InputField>().onSelect.AddListener((str) => this.GetComponent<TMP_InputField>().text = "");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = _system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
                next.Select();
        }
    }
}
