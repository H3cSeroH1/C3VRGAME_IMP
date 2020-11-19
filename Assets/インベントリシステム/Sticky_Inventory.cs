using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
using System;

public class Sticky_Inventory : MonoBehaviour
{
    GameObject _parent;
    public GameObject HoldItem = null;

    public bool KinematicHold = false;
    public bool MinisizeHold = true;

    public float minScalePer = 0.1f;
    public float HoldItemvolume = 0;

    private bool Handling;

    //public bool ChangeedHoldItem=false;

    //public CraftingSystem craftingSystem = null;


    //[EnumFlags]
    public Item.KindOfItem allowItemType;

    void Start()
    {

    }

    void Update()
    {

        if (HoldItem)
        {




            if (HoldItem.GetComponent<CustomInteractible>())
            {
                if (HoldItem.GetComponent<CustomInteractible>().attachedToHand)
                {
                    HoldItem.transform.parent = null;
                    HoldItem.GetComponent<Rigidbody>().isKinematic = false;
                    if (MinisizeHold)
                    {
                        HoldItem.transform.localScale = (HoldItem.transform.localScale / (1 - HoldItemvolume)) / minScalePer;
                    }
                    //SetLayerRecursively(HoldItem, 0);
                    HoldItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;


                    HoldItem = null;
                    //ChangeedHoldItem = true;

                    //craftingSystem.canCraft();
                    //SetLayerRecursively(_parent, 14);


                }
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {

        if (!HoldItem)
        {


            if (other.transform.parent.gameObject.GetComponent<CustomInteractible>())
            {
                _parent = other.transform.parent.gameObject;

                Debug.Log(_parent.GetComponent<CustomInteractible>().rightHand);

                

                if (!_parent.GetComponent<CustomInteractible>().attachedToHand)// || (_parent.GetComponent<ItemTypeChecker>().ItemType.GetKindOfItem() == allowItemType)
                {



                    Renderer objRenderer = other.GetComponent<Renderer>();
                    Bounds objBounds = objRenderer.bounds;

                    HoldItemvolume = objBounds.size.sqrMagnitude;

                    _parent.transform.parent = transform.parent;


                    _parent.transform.position = this.transform.position;

                    _parent.transform.rotation = this.transform.rotation;


                    if (MinisizeHold)
                    {
                        _parent.transform.localScale = (_parent.transform.localScale * (1 - HoldItemvolume)) * minScalePer;
                    }

                    //_parent.GetComponent<Rigidbody>().isKinematic = true;


                    _parent.GetComponent<Rigidbody>().isKinematic = KinematicHold;

                    _parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;




                    _parent.GetComponent<Rigidbody>().angularVelocity = new Vector3(5, 5, 5);

                    HoldItem = _parent;

                    //ChangeedHoldItem = true;

                    //craftingSystem.canCraft();

                    //SetLayerRecursively(_parent, 10);

                }

                else
                {
                    Debug.Log("ばななまん");
                }

            }
        }
        


    }


    //追加コード
    /// <summary>
    /// 自分自身を”含まない”すべての子オブジェクトのレイヤーを設定します
    /// </summary>
    public static void SetLayerRecursively(
        GameObject self,
        int layer
    )
    {
        self.layer = layer;

        foreach (Transform n in self.transform)
        {
            SetLayerRecursively(n.gameObject, layer);
        }
    }

}
