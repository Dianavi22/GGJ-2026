using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject ButtonParts;
    public void OnSelect(BaseEventData eventData)
    {
        OnButtonSelected();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnButtonDeselected();
    }

    void OnButtonSelected()
    {
        ButtonParts.SetActive(true);
    }

    void OnButtonDeselected()
    {
        ButtonParts.SetActive(false);

    }
}
