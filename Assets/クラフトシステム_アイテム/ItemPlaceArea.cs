using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPlaceArea : MonoBehaviour
{


    public List<GameObject> PlacedItemasGameobj = new List<GameObject>();
    public List<Item> PlacedItem = new List<Item>();

    public UnityEvent PlaceItemChanged;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("atu");

        if (other.transform.parent.gameObject.GetComponent<ItemTypeChecker>())
        {

            PlacedItemasGameobj.Add(other.transform.parent.gameObject);
            PlacedItem.Add(other.transform.parent.gameObject.GetComponent<ItemTypeChecker>().ItemType);

            PlaceItemChanged.Invoke();

        }

        else
        {

        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject.GetComponent<ItemTypeChecker>())
        {

            PlacedItemasGameobj.Remove(other.transform.parent.gameObject);
            PlacedItem.Remove(other.transform.parent.gameObject.GetComponent<ItemTypeChecker>().ItemType);

            PlaceItemChanged.Invoke();
        }

        else
        {

        }
    }

    public void ItemRemover()
    {
        foreach(GameObject placedGO in PlacedItemasGameobj)
        {

            Destroy(placedGO);
            //PlacedItemasGameobj.Remove(placedGO);

        }

        PlacedItem.Clear();
        PlacedItemasGameobj.Clear();

        PlaceItemChanged.Invoke();

    }

}
