using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    //　アイテム情報のスロットプレハブ
    [SerializeField]
    public GameObject slot;

    public int slotqty = 9;

    public int ChangeLine = 3;

    public float slotGap = 0;

    [SerializeField]
    bool useX = true, useY, useZ;

    private void Start()
    {

        CreateSlot();

        notDisplayItemnotGrab();

    }

    //　アイテムスロットの作成
    public void CreateSlot() {


        Collider objCollider = slot.GetComponent<Collider>();
        Bounds objBounds = objCollider.bounds;
        Vector3 objPos = slot.transform.position;
        
        for (int i = 1 ; i < slotqty; i++)
        {
            useY = false;

            if (i%ChangeLine == 0)
            {
                useY = true;
                objPos.x += -((objBounds.size.x + slotGap) * ChangeLine);
            }

            //Debug.Log("でたああ");
            if (useX) objPos.x += objBounds.size.x + slotGap;

            if (useY) objPos.y -= objBounds.size.y + slotGap;

            if (useZ) objPos.z += objBounds.size.z + slotGap;

            GameObject tmpObj = Instantiate(slot, objPos, slot.transform.rotation);
            tmpObj.transform.parent = slot.transform.parent;

            //　スロットゲームオブジェクトの名前を設定
            tmpObj.name = slot.name + i;
            //　Scaleを設定しないと0になるので設定

        }

    }

    //アイテムスロットの親を持ってるときだけ表示
    public void notDisplayItemnotGrab()
    {
        slot.transform.parent.gameObject.SetActive(false);
    }

    public void DisplayIteminGrab()
    {
        slot.transform.parent.gameObject.SetActive(true);
    }


}
