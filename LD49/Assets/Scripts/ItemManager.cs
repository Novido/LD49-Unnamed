using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item thisItem;
    public Vector2 mousePosition;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        thisItem = new Item();
        thisItem.Weight = 1;
        
    }

    private void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Dragging object!");

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }
}
