using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public Item thisItem;
    public float Weight;
    public Vector2 mousePosition;

    public Vector2 worldPosition;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] Canvas canvas;

    private BoxCollider2D boxCollider2D;



    // Start is called before the first frame update
    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //thisItem = new Item();
        //thisItem.Weight = 1;
        Weight = 1f;

        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size.Set(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);

    }

    private void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        // When dragging we want to block raycast on the package to see where we are dropping it.
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Package")
        {
            Debug.Log("Cannot be placed on another package");
        }
    }

    public Vector2 BoxWorldPosition()
    {
        //Vector2 size = boxCollider2D.size;
        //Vector2 centerPoint = new Vector2(boxCollider2D.bounds.center.x, boxCollider2D.bounds.center.y);
        Vector2 worldPos = transform.TransformPoint(boxCollider2D.bounds.center);

        return worldPos;
    }
}
    

