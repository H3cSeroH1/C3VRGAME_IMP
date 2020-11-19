using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerHolder : MonoBehaviour
{
    public Player player = null;
    public new CapsuleCollider collider = null;
    public GameObject Head = null;

    public float BeltRadius = 0.4f;
    public float HolderRadiusSpeed = 1.5f;

    public GameObject ItemholderHost = null;
    public GameObject Itemholder_Right = null;
    public GameObject Itemholder_Back = null;
    public GameObject Itemholder_Left = null;
    public GameObject Itemholder_Chest = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        collider = GetComponent<CapsuleCollider>();

        
    }

    // Update is called once per frame
    void Update()
    {
        ItemholderController();
    }

    public void ItemholderController()
    {
        Itemholder_Back.transform.localPosition = new Vector3(0, player.eyeHeight / 2.5f, -BeltRadius);


        ItemholderHost.transform.position = new Vector3(player.hmdTransform.position.x, player.trackingOriginTransform.position.y + player.eyeHeight/2f, player.hmdTransform.position.z);
        //Debug.Log(player.bodyDirectionGuess);

        //ItemholderHost.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3 (0, (player.leftHand.gameObject.transform.rotation * player.rightHand.gameObject.transform.rotation).y, 0));

        //ItemholderHost.transform.rotation = Quaternion.FromToRotation(Vector3.forward, player.bodyDirectionGuess);

        //ItemholderHost.transform.rotation = Quaternion.FromToRotation(Vector3.forward, player.bodyDirectionGuess) ;//!!!最強卍!!!

        ItemholderHost.transform.rotation = Quaternion.Slerp(ItemholderHost.transform.rotation, Quaternion.FromToRotation(Vector3.forward, player.bodyDirectionGuess), HolderRadiusSpeed * Time.deltaTime);


    }
}
