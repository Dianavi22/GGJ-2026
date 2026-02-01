using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
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
        _audioManager.PlayButtonSelect();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonParts.ActivePartsButton();
        _audioManager.PlayButtonSelect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonParts.DisablePartsButton();
    }
}
