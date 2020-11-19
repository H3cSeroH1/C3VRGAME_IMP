using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class Climber : MonoBehaviour
{
    public ClimberHand RightHand;
    public ClimberHand LeftHand;
    public SteamVR_Action_Boolean ToggleGripButton;
    public ConfigurableJoint ClimberHandle;

    private bool Climbing;
    private ClimberHand ActiveHand;
    private Rigidbody movedRigidbody;

    //public MovementVR movementvr = null;

    private void Start()
    {
        movedRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (!RightHand)
        {
            RightHand = transform.Find("HandColliderRight(Clone)").gameObject.GetComponent<ClimberHand>();
        }
        if (!LeftHand)
        {
            LeftHand = transform.Find("HandColliderLeft(Clone)").gameObject.GetComponent<ClimberHand>();
        }


        updateHand(RightHand);
        updateHand(LeftHand);
        if (Climbing)
        {
            ClimberHandle.targetPosition =  -(ActiveHand.transform.position-transform.position);
        }
    }

    void updateHand(ClimberHand Hand)
    {
        if (Climbing && Hand == ActiveHand)
        {

            //movementvr.CalculateMovement(true);
            movedRigidbody.AddForce(100f * (new Vector3(0f, 0f, 0f) - movedRigidbody.velocity), ForceMode.Force);


            if (ToggleGripButton.GetStateUp(Hand.Hand))
            {
                ClimberHandle.connectedBody = null;
                Climbing = false;

                movedRigidbody.useGravity = true;

                movedRigidbody.AddForce(movedRigidbody.velocity, ForceMode.Impulse); //position.GetVelocity(Hand)

            }





        }
        else
        {
            if (ToggleGripButton.GetStateDown (Hand.Hand)||Hand.grabbing)
            {
                Hand.grabbing = true;
                if (Hand.TouchedCount > 0)
                {
                    ActiveHand = Hand;
                    Climbing = true;
                    ClimberHandle.transform.position = Hand.transform.position;



                    movedRigidbody.useGravity = false;
                    ClimberHandle.connectedBody = movedRigidbody;
                    Hand.grabbing = false;

                    
                }
            }
        }




    }
}
