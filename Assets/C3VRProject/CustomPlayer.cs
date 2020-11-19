//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Player interface used to query HMD transforms and VR hands
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	// Singleton representing the local VR player/user, with methods for getting
	// the player's hands, head, tracking origin, and guesses for various properties.
	//-------------------------------------------------------------------------
	public class CustomPlayer : MonoBehaviour
	{
		[Tooltip( "Virtual transform corresponding to the meatspace tracking origin. Devices are tracked relative to this." )]
		public Transform trackingOriginTransform;

		[Tooltip( "List of possible transforms for the head/HMD, including the no-SteamVR fallback camera." )]
		public Transform[] hmdTransforms;




		[Tooltip( "These objects are enabled when SteamVR is available" )]
		public GameObject rigSteamVR;

		[Tooltip( "The audio listener for this player" )]
		public Transform audioListener;

        [Tooltip("This action lets you know when the player has placed the headset on their head")]
        public SteamVR_Action_Boolean headsetOnHead = SteamVR_Input.GetBooleanAction("HeadsetOnHead");

		public bool allowToggleTo2D = true;



        //-------------------------------------------------
        // Get Player scale. Assumes it is scaled equally on all axes.
        //-------------------------------------------------

        public float scale
        {
            get
            {
                return transform.lossyScale.x;
            }
        }


        //-------------------------------------------------
        // Get the HMD transform. This might return the fallback camera transform if SteamVR is unavailable or disabled.
        //-------------------------------------------------
        public Transform hmdTransform
		{
			get
			{
                if (hmdTransforms != null)
                {
                    for (int i = 0; i < hmdTransforms.Length; i++)
                    {
                        if (hmdTransforms[i].gameObject.activeInHierarchy)
                            return hmdTransforms[i];
                    }
                }
				return null;
			}
		}


		//-------------------------------------------------
		// Height of the eyes above the ground - useful for estimating player height.
		//-------------------------------------------------
		public float eyeHeight
		{
			get
			{
				Transform hmd = hmdTransform;
				if ( hmd )
				{
					Vector3 eyeOffset = Vector3.Project( hmd.position - trackingOriginTransform.position, trackingOriginTransform.up );
					return eyeOffset.magnitude / trackingOriginTransform.lossyScale.x;
				}
				return 0.0f;
			}
		}


		//-------------------------------------------------
		// Guess for the world-space position of the player's feet, directly beneath the HMD.
		//-------------------------------------------------
		public Vector3 feetPositionGuess
		{
			get
			{
				Transform hmd = hmdTransform;
				if ( hmd )
				{
					return trackingOriginTransform.position + Vector3.ProjectOnPlane( hmd.position - trackingOriginTransform.position, trackingOriginTransform.up );
				}
				return trackingOriginTransform.position;
			}
		}


		//-------------------------------------------------
		// Guess for the world-space direction of the player's hips/torso. This is effectively just the gaze direction projected onto the floor plane.
		//-------------------------------------------------
		public Vector3 bodyDirectionGuess
		{
			get
			{
				Transform hmd = hmdTransform;
				if ( hmd )
				{
					Vector3 direction = Vector3.ProjectOnPlane( hmd.forward, trackingOriginTransform.up );
					if ( Vector3.Dot( hmd.up, trackingOriginTransform.up ) < 0.0f )
					{
						// The HMD is upside-down. Either
						// -The player is bending over backwards
						// -The player is bent over looking through their legs
						direction = -direction;
					}
					return direction;
				}
				return trackingOriginTransform.forward;
			}
		}


		//-------------------------------------------------
		private void Awake()
		{
			if ( trackingOriginTransform == null )
			{
				trackingOriginTransform = this.transform;
			}

#if OPENVR_XR_API && UNITY_LEGACY_INPUT_HELPERS
			if (hmdTransforms != null)
			{
				foreach (var hmd in hmdTransforms)
				{
					if (hmd.GetComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>() == null)
						hmd.gameObject.AddComponent<UnityEngine.SpatialTracking.TrackedPoseDriver>();
				}
			}
#endif
		}




        protected virtual void Update()
        {
            if (SteamVR.initializedState != SteamVR.InitializedStates.InitializeSuccess)
                return;

            if (headsetOnHead != null)
            {
                if (headsetOnHead.GetStateDown(SteamVR_Input_Sources.Head))
                {
                    Debug.Log("<b>SteamVR Interaction System</b> Headset placed on head");
                }
                else if (headsetOnHead.GetStateUp(SteamVR_Input_Sources.Head))
                {
                    Debug.Log("<b>SteamVR Interaction System</b> Headset removed");
                }
            }
        }





		//-------------------------------------------------
		public void PlayerShotSelf()
		{
			//Do something appropriate here
		}
	}
}
