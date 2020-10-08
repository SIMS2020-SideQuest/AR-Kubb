using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Collections;
using System.Collections.Generic;
//SIMS.SideQuest.ARKubb
namespace Lean.Touch{
	public class LeanTranslateMove : MonoBehaviour{
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
            Vector3 moveMe = (RemainingDelta - newDelta); //Shift this transform by the change in delta
            moveMe.y = 0f;//Turns off movment control in the Y-axis
            transform.position += moveMe;
			RemainingDelta = newDelta;// Update remainingDelta with the dampened value
		}
		
		protected virtual void TranslateUI(Vector2 screenDelta){
            var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera, transform.position); // Screen position of the transform
            screenPoint += screenDelta; // Add the deltaPosition
            var worldPoint = default(Vector3);// Convert back to world space
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, Camera, out worldPoint) == true){
                transform.position = worldPoint;
            }
        }

        protected virtual void Translate(Vector2 screenDelta){
            var camera = LeanTouch.GetCamera(Camera, gameObject); // Make sure the camera exists
			if (camera != null){
                var oldPosition = transform.position; // Store old position
                var screenPosition = camera.WorldToScreenPoint(oldPosition); // Transforms position from world space into viewport space.
                screenPosition += (Vector3)screenDelta; // Add the screen delta
                var newPosition = camera.ScreenToWorldPoint(screenPosition); // Make the camera render with shader replacement, to world space
                RemainingDelta += newPosition - oldPosition; // Add to Remaining delta
            }
			else{
				Debug.LogError("Failed to find camera.", this);
			}
        }
	}
}