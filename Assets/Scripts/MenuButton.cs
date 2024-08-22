using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private TextMeshProUGUI theText;
    private Image image;
    public Sprite sp1;
    public Sprite sp2;
    private void Start()
    {
        if (gameObject.name.Equals("pauseGame"))
        {
            image=GetComponent<Image>();
        }
        else
        {
            theText = GetComponent<TextMeshProUGUI>();
        }
            
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (theText != null)
        {
            theText.color = Color.yellow;
        }

        if (image != null)
        {
            image.sprite =sp2 ;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (theText != null)
        {
            theText.color = Color.white;
        }

        if (image != null)
        {
            image.sprite = sp1;
        }
    }
}