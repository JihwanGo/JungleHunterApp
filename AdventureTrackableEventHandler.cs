using Insol.JungleHunter.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Vuforia;
using Insol.Adventur.Manager;
using DG.Tweening;

namespace Insol
{
    public class AdventureTrackableEventHandler : MonoBehaviour, 
     ITrackableEventHandler
    {
        public AnimalData AnimalInfo;
        private float HuntTime = 30;
        private TrackableBehaviour mTrackableBehaviour;


        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
            if (mTrackableBehaviour != null)
            {
                AnimalInfo.AnimalName = mTrackableBehaviour.TrackableName;
            }
        }
        void Update()
        {
            HuntTime -= Time.deltaTime;
        }

        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,TrackableBehaviour.Status newStatus)
        {
            if (AdventurManager.Instance == null)
                return;

            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                if (AdventurManager.Instance.CurrentAnimal == null)
                {
                    AdventurManager.Instance.OnTrackingFound(AnimalInfo, gameObject.transform);
                 

                }
                else
                {
                    if (AdventurManager.Instance.CurrentAnimal == null && HuntTime > 0 )
                    {
                        Debug.Log("Adsfasdf");
                    }
                   
                }
                HuntTime = 30;
            }
            else //동물이 화면 가운데 부분으로 넘어 오는곳.
            {
                Debug.Log("AS");
                transform.DOKill();
                transform.DOMove(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1000)), 2f);
                transform.DOLocalRotate(new Vector3(45, -30f, 0), 2f);
            }

  
        }

        public void OnTrackingLost(bool checkChance = true)
        {

            Debug.Log("bbbbbbbbbb");
            if (AdventurManager.Instance == false)
                return;         
        }

    }
}
