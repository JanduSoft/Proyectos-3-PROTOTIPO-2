using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonMenu : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    [SerializeField] Text Highlighted;
    void Update()
    { 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
            Highlighted.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
            Highlighted.gameObject.SetActive(false);
    }
    public void OnSelect(BaseEventData eventData)
    {
            Highlighted.gameObject.SetActive(true);
    }
    public void OnDeselect(BaseEventData eventData)
    {
            Highlighted.gameObject.SetActive(false);
    }
}