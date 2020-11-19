using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CraftingSystem : MonoBehaviour
{

    public ItemDataBase itemDataBase = null;

    public ItemPlaceArea itemPlaceArea = null;

    public GameObject ProductsItem = null;

    public List<GameObject> CraftableItem = new List<GameObject>();

    public GameObject SpawnPoint = null;

    GameObject _parent;


    public List<Item> return2Item = new List<Item>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //isCraftable();
    }

    public void isCraftable()
    {

        CraftableItem.Clear();

        foreach (Item it in itemDataBase.GetItemLists())//アイテムデータベースを回す
        {

            if (CompareListCustom(it.Recipe, itemPlaceArea.PlacedItem))//あるアイテムのレシピと今のクラフト台の上が合致してる？？
            {

                //Debug.Log("確認1if\t" + it.ToString());

                CraftableItem.Add(it.GetgameObject());

                //ProductsItem = it.GetgameObject();

                //break;

            }
            else
            {
                //Debug.Log("クラフト不可");
                //Debug.Log("確認2else\t" + it.ToString());
                //ProductsItem = null;
            }
        }
    }


    public void Spawn()
    {
        if (CraftableItem?.Count > 0 && CraftableItem?.Count <= 1)
        {

            Debug.Log(CraftableItem.Count);

            ProductsItem = CraftableItem[0];

            Instantiate(ProductsItem, SpawnPoint.transform.position, Quaternion.identity);

            CraftableItem.Clear();

            ProductsItem = null;

            itemPlaceArea.ItemRemover();
        }
        else if (CraftableItem?.Count >= 2)
        {
            Debug.Log("レシピが重なってる！！治せ！！" + CraftableItem[0].name + CraftableItem[1].name);

            ProductsItem = CraftableItem[1];

            Instantiate(ProductsItem, SpawnPoint.transform.position, Quaternion.identity);

            CraftableItem.Clear();

            ProductsItem = null;

            itemPlaceArea.ItemRemover();
        }

        else
        {
            //Debug.Log("クラフト不可");
        }



    }

    //public List<Item> arrayGameobj2item()
    //{
    //    foreach(Item it in itemPlaceArea.PlacedItem)
    //    {

    //        return2Item.Add(it.GetComponent<ItemTypeChecker>().ItemType);

    //    }


    //    return return2Item;
    //}


    public Boolean CompareListCustom(List<Item> ary1, List<Item> ary2)//1がレシピ2が置いた奴
    {
        bool isEqual = true;

        List<Item> ary2Buffer = new List<Item>(ary2);

        if(ary1.Count != 0)
        {

            if (ary1.Count == ary2.Count)
            {
                foreach (Item item1 in ary1)
                {

                    if(ary2Buffer.Find(item2 => item2 == item1) == null)
                    {
                        //Debug.Log("確認3null\t" + item1.ToString() + "との比較により");

                        isEqual = false;

                        break;

                    }
                    else
                    {
                        ary2Buffer.Remove(ary2Buffer.Find(item2 => item2 == item1));
                    }


                    ////両方中身があるならなら次へ
                    //if ((ary1[i] != null) && (ary2[i] != null))
                    //{
                    //    //Debug.Log("両方中身あるパターン");

                    //    //ary1の要素のEqualsメソッドで、ary2の要素と等しいか調べる
                    //    if (!Equals(ary1[i] ,ary2[i]))
                    //    {
                    //        //1つでも等しくない要素があれば、同じではない
                    //        isEqual = false;
                    //        break;
                    //    }
                    //}

                    ////どっちも中身なければ同じだとしておく
                    //else if ((ary1[i] == null) && (ary2[i] == null))
                    //{
                    //    //Debug.Log("両方中身ないパターン");
                    //}

                    ////どっちか中身なければアウト同じではない
                    //else
                    //{
                    //    //Debug.Log("どっちか中身あるパターン");
                    //    isEqual = false;
                    //    break;
                    //}

                }
            }
            else
            {
                isEqual = false;
            }
        }
        else
        {
            isEqual = false;
        }








        return isEqual;
    }

    

}
