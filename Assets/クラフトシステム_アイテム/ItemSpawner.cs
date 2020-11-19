using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    public ItemDataBase itemDataBase = null;
    [SerializeField]
    public List<Item> items = new List<Item>();
    [SerializeField]
    public GameObject SpawnPoint = null;
    [SerializeField]
    int Succecer;    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {

        if (SpawnPoint)
        {
            Instantiate(items[Succecer].GetgameObject(), SpawnPoint.transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(items[Succecer].GetgameObject(), this.transform.position, Quaternion.identity);

        }

    }


#if UNITY_EDITOR
    //-------------------------------------------------------------------------
    [UnityEditor.CustomEditor(typeof(ItemSpawner))]
    public class ItemSpawnerEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            ItemSpawner itemSpawner = target as ItemSpawner;

            itemSpawner.itemDataBase = EditorGUILayout.ObjectField("アイテムデータベース", itemSpawner.itemDataBase, typeof(ItemDataBase), true) as ItemDataBase;

            itemSpawner.SpawnPoint = EditorGUILayout.ObjectField("アイテムの出現位置", itemSpawner.SpawnPoint, typeof(GameObject), true) as GameObject;



            if (itemSpawner.itemDataBase)
            {
                string[] src = {};

                itemSpawner.items = itemSpawner.itemDataBase.GetItemLists();

                int i = 0;

                foreach (Item it in itemSpawner.items)
                {

                    Array.Resize(ref src, src.Length + 1);
                    src[src.Length - 1] = it.GetItemName();



                }

                itemSpawner.Succecer = EditorGUILayout.Popup("omg", itemSpawner.Succecer, src);
                EditorGUILayout.LabelField("Popup = ", itemSpawner.Succecer.ToString());

            }




            //popup = EditorGUILayout.Popup("Popup", itemDataBase_editor.GetItemLists());
            //EditorGUILayout.LabelField("Popup = ", popup.ToString());



        }
    }
#endif

}
