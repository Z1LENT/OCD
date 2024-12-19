using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class GrabbHaptics : MonoBehaviour
{
    public float vibrationIntensity = 0.5f;   
    public float vibrationDuration = 0.075f;                

    private OVRInput.Controller controllerInUse;
    bool heldByLeftHand = false;
    public GrabInteractable grabbable;

    public void StartPullingLever()
    {
        DetectActiveController();
    }

    public void StopPullingLever()
    {
        OVRInput.SetControllerVibration(0, 0, controllerInUse);  
    }

    private void TriggerHaptics()
    {
        if (controllerInUse != OVRInput.Controller.None)
        {
            OVRInput.SetControllerVibration(0.8f, vibrationIntensity, controllerInUse);  
            StartCoroutine(StopHapticsAfterDuration(vibrationDuration)); 
        }
    }

    private IEnumerator StopHapticsAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controllerInUse); 
    }
    
    public void DetectActiveController()
    {
        foreach (var interactor in grabbable.SelectingInteractors)
        {
            GetHand getHand = interactor.GetComponent<GetHand>();

            if (getHand != null)
            {
                heldByLeftHand = getHand.hand == GetHand.Hand.Left;
                if (!heldByLeftHand)
                {
                    controllerInUse = OVRInput.Controller.RTouch;
                }
                else 
                {
                    controllerInUse = OVRInput.Controller.LTouch; 
                }
                if (controllerInUse != OVRInput.Controller.None)
                {
                    TriggerHaptics();
                }
            }
            else
            {
                controllerInUse = OVRInput.Controller.None;
                Debug.LogWarning("No GetHand script found on interactor.");
            }
        }
    }
}