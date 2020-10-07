using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Collections;
using System.Collections.Generic;
//SIMS.SideQuest.ARKubb
namespace Lean.Touch {
    // This script allows you to transform the current GameObject with smoothing
    public class LeanTranslateSmooth2 : LeanTranslate{
        [Tooltip("How smoothly this object moves to its target position")]
        public float Dampening = 10.0f;
        // The position we still need to add
        [HideInInspector]
        public Vector3 RemainingDelta;

        protected virtual void LateUpdate(){
            var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime); // Get t value
            var newDelta = Vector3.Lerp(RemainingDelta, Vector3.zero, factor); // Dampen remainingDelta
            Vector3 moveMe = (RemainingDelta - newDelta); // Shift this transform by the change in delta(Atul's change)
            //moveMe.z = moveMe.y;
            moveMe.z = 0f;
            //transform.position += (RemainingDelta - newDelta);
            transform.position += moveMe;
            // Debug.Log("pos : "+transform.position);
            RemainingDelta = newDelta; // Update remainingDelta with the dampened value
        }

        protected void Translate(Vector2 screenDelta){
            var camera = LeanTouch.GetCamera(Camera, gameObject); // Make sure the camera exists
            if (camera != null){
                var oldPosition = transform.position; // Store old position
                var screenPosition = camera.WorldToScreenPoint(oldPosition); // Screen position of the transform
                screenPosition += (Vector3)screenDelta; // Add the deltaPosition
                var newPosition = camera.ScreenToWorldPoint(screenPosition); // Convert back to world space
                RemainingDelta += newPosition - oldPosition; // Add to delta
            }
        }
    }
}

