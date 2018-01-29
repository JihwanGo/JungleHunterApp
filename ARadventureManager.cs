using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vuforia;


public class ARadventureManager : MonoBehaviour,
                                            ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES

    private TrackableBehaviour mTrackableBehaviour;

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            
        }
        //else
        //{
        //    OnTrackingLost();
        //}
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    public void OnTrackingLost(bool checkChance = true)
    {

        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        var audio = GetComponentInChildren<AudioSource>();

        if (audio != null)
            audio.Stop();

        // 한번 더 찬스 걸렸으면
        // 다시 찬스 뽑기 하지 않도록 처리
        /*if (HunterManager.Instance.IsChanceTime)
        {
            HunterManager.Instance.IsChanceTime = false;
            checkChance = false;
        }

        if (checkChance)
        {
            HunterManager.Instance.HuntingUI.ResetHuntingUI();

            // 한번 더 찬스 10% 확률
            var oneMoreChance = UnityEngine.Random.Range(0, 100);
            if (oneMoreChance <= 10)
            {
                Debug.Log("Get chance");
                HunterManager.Instance.ChanceUI.SetActive(true);
                HunterManager.Instance.IsChanceTime = true;
            }
            else
                Debug.Log("lose chance - " + oneMoreChance);

            Destroy(HunterManager.Instance.CurrentAnimal, 0.5f);
        }*/

        //HunterManager.Instance.HuntingUI.ResetHuntingUI();
        //HunterManager.Instance.IsHuntingTime = false;

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

    #endregion // PRIVATE_METHODS
}
