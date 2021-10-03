using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadingContainerManager : MonoBehaviour, IDropHandler
{

    public float TotalWeight;
    public GameObject UI_Packaging;

    public Vector2 NewCenterOfMassGizmo;

    private Rigidbody2D rb2d;
    private List<ItemManager> ListOfItems;

    private ShipManager shipManager;
    private float multiple;
    

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ListOfItems = new List<ItemManager>();
        
        GameObject ship;
        ship = GameObject.Find("Ship");

        //shipManager = new ShipManager();
        shipManager = ship.GetComponent<ShipManager>();        
    }

    public void LateUpdate()
    {
        //Debug.Log("Total Weight: " + TotalWeight);
        TotalWeight = CalculateTotalWeight(ListOfItems);
        //Debug.Log("Items in list: " + ListOfItems.Count);

        //rb2d.centerOfMass = UpdateCenterOfMass();
        
        //Debug.Log("Center of mass: " + rb2d.centerOfMass);
    }

    public void OnDrop(PointerEventData eventData)
    {
        rb2d.centerOfMass = UpdateCenterOfMass();
        NewCenterOfMassGizmo = rb2d.centerOfMass;
        //Debug.Log("OnDrop in loading container");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        ListOfItems.Add(collision.gameObject.GetComponent<ItemManager>());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollisionExit2D");
        ListOfItems.Remove(collision.gameObject.GetComponent<ItemManager>());
    }

    public float CalculateTotalWeight(List<ItemManager> items)
    {
        //Debug.Log("Calculate weight");
        float totalWeight = 0;

        foreach (var item in items)
        {
            //Debug.Log("Weight of item: " + item.Weight);
            totalWeight += item.Weight;
            //Debug.Log("Total weight when calc: " + totalWeight);
        }

        return totalWeight;
    }

    public void SendLoadingInformation()
    {
        

        if (TotalWeight != 0)
        { 
            shipManager.FreightWeight = TotalWeight;
            shipManager.UpdateMass();
            //rb2d.centerOfMass = UpdateCenterOfMass();

            
            shipManager.GetComponent<Rigidbody2D>().centerOfMass = UpdateCenterOfMass();
        }

        UI_Packaging.SetActive(false);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * UpdateCenterOfMass(), 5f);
    }

    private void DebugGizmo(Vector3 vector)
    {
        Gizmos.color = Color.blue;
        float radius = 5f;
        Gizmos.DrawWireSphere(vector, radius);
    }
    

    private Vector2 UpdateCenterOfMass()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        Vector2 size = col.size;
        Debug.Log("size: " + size);
        float width = col.size.x;
        Debug.Log("width: " + width);
        //Vector2 centerPoint = col.bounds.center;
        Vector2 worldPos = transform.TransformPoint(col.bounds.center);
        Debug.Log("worldPos: " + worldPos);
        float leftColliderWorldSpace = worldPos.x - (size.x / 2f);
        Debug.Log("leftColliderBorder: " + leftColliderWorldSpace);        
        float rightColliderWorldSpace = worldPos.x + (size.x / 2f);
        
        float offset = leftColliderWorldSpace + (width/2);
        Debug.Log("offset: " + offset);
        float leftColliderLocalSpace = leftColliderWorldSpace - offset;
        Debug.Log("leftColliderLocalSpace: " + leftColliderLocalSpace);
        float rightColliderLocalSpace = rightColliderWorldSpace - offset;
        //Debug.Log("leftColliderWorldSpace (" + leftColliderWorldSpace + ") - offset (" + offset + ")");

        Vector2 newCenterOfMassWorldSpace = new Vector2();
        Vector2 newCenterOfMassLocalSpace = new Vector2();
        float sumWeightAndPosition = 0;
        float sumOfAllWeights = 0;
        
        GameObject[] units;
        units = GameObject.FindGameObjectsWithTag("Package");

        if (units.Length > 0)
        {
            foreach (var item in units)
            {
                var position = item.GetComponent<ItemManager>().BoxWorldPosition().x;
                var weight = item.GetComponent<ItemManager>().Weight;

                sumWeightAndPosition += position * weight;
                sumOfAllWeights += weight;
                //Debug.Log("World position of package: " + item.GetComponent<ItemManager>().BoxWorldPosition());
            }
        }

        Debug.Log("Sum of Weight and Position: " + sumWeightAndPosition);
        Debug.Log("Sum of Weight: " + sumOfAllWeights);

        newCenterOfMassWorldSpace.x = sumWeightAndPosition / sumOfAllWeights;
        Debug.Log("com world space: " + newCenterOfMassWorldSpace);

        newCenterOfMassLocalSpace.x = newCenterOfMassWorldSpace.x - offset;
        Debug.Log("com local space: " + newCenterOfMassLocalSpace);

        //update multiple
        multiple = rightColliderLocalSpace / newCenterOfMassLocalSpace.x;

        // get ship collision size
        float shipWidth = shipManager.GetComponent<BoxCollider2D>().size.x;
        float shipCenterOfMass = shipWidth * multiple;
        Vector2 newCenterOfMassShip = new Vector2(shipCenterOfMass, -0.05f);

        return newCenterOfMassShip;
    }


}
