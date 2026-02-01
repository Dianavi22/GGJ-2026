using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] AudioManager _audioManager;
    [SerializeField] PlayButtonPart ButtonParts;

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
        ButtonParts.ActivePartsButton();
    }

    void OnButtonDeselected()
    {
        ButtonParts.DisablePartsButton();
        _audioManager.ButtonSelect();

    }
}
