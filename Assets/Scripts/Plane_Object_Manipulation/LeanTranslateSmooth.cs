using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Collections;
using System.Collections.Generic;
//SIMS.SideQuest.ARKubb
namespace Lean.Touch{
	// This script allows you to transform the current GameObject with smoothing
	public class LeanTranslateSmooth : MonoBehaviour{
		[Tooltip("Ignore fingers with StartedOverGui?")]
        public bool IgnoreIfStartedOverGui = false;
 
        [Tooltip("Ignore fingers with IsOverGui?")]
        public bool IgnoreIfOverGui;
 
        [Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
        public int RequiredFingerCount;
 
        [Tooltip("Does translation require an object to be selected?")]
        public LeanSelectable RequiredSelectable;
		
		[Tooltip("The camera the translation will be calculated using (None = MainCamera)")]
        public Camera Camera;
		
		[Tooltip("How smoothly this object moves to its target position")]
		public float Dampening = 10.0f;
		
		// The position we still need to add
		[HideInInspector]
		public Vector3 RemainingDelta;

	
		#if UNITY_EDITOR
        protected virtual void Reset(){
            if (RequiredSelectable == null){
                RequiredSelectable = GetComponent<LeanSelectable>();
            }
        }
        #endif
		
        protected virtual void Update(){ 
            var fingers = LeanSelectable.GetFingers(IgnoreIfStartedOverGui, IgnoreIfOverGui, RequiredFingerCount, RequiredSelectable); // Get the fingers we want to use
            var screenDelta = LeanGesture.GetScreenDelta(fingers); // Calculate the screenDelta value based on these fingers
            if (screenDelta != Vector2.zero){ // Perform the translation
                if (transform is RectTransform){
                    TranslateUI(screenDelta);
                }
                else{
                    Translate(screenDelta);
                }
            }
        }

		protected virtual void LateUpdate(){ // Get t value
			var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime); //The framerate independent damping factor (-1 = instant)
			//var factor = 1.0f - Mathf.Exp(-Smoothness * Time.deltaTime); //The framerate independent damping factor (-1 = instant)
			var newDelta = Vector3.Lerp(RemainingDelta, Vector3.zero, factor); //Linearly interpolates between two vectors
            Vector3 moveMe = (RemainingDelta - newDelta); //Shift this transform by the change in delta(Atul's change)
            moveMe.y = 0f;
            transform.position += moveMe;
			RemainingDelta = newDelta;// Update remainingDelta with the dampened value
		}
		
		protected virtual void TranslateUI(Vector2 screenDelta){// Screen position of the transform
            var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera, transform.position); // Add the deltaPosition
            screenPoint += screenDelta; // Convert back to world space
            var worldPoint = default(Vector3);
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, Camera, out worldPoint) == true){
                transform.position = worldPoint;
            }
        }

        protected virtual void Translate(Vector2 screenDelta){
            var camera = LeanTouch.GetCamera(Camera, gameObject); // Make sure the camera exists, // If camera is null, try and get the main camera, return true if a camera was found
            //LeanTouch.GetCamera(Camera) == true
			if (camera != null){
                var oldPosition = transform.position; // Store old position
                var screenPosition = camera.WorldToScreenPoint(oldPosition); // Screen position of the transform
                screenPosition += (Vector3)screenDelta; // Add the deltaPosition
                var newPosition = camera.ScreenToWorldPoint(screenPosition); // Convert back to world space
                RemainingDelta += newPosition - oldPosition; // Add to delta
            }
			else{
				Debug.LogError("Failed to find camera.", this);
			}
        }
	}
}