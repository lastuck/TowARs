using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CheckSeen : MonoBehaviour,ITrackableEventHandler
{
    [SerializeField]
    private TrackableBehaviour mTrackableBehaviour;

    public bool hasBeenSeen;
    
    public bool seenNow;

    [SerializeField]
    public bool noARScene;
    
    [SerializeField]
    private GameObject warningPanel;
    void Start()
    {
        if (mTrackableBehaviour && !noARScene)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
            //Debug.Log("yes");
        }
        else if(noARScene)
        {
            hasBeenSeen = true;
            seenNow = true;
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            if (!hasBeenSeen)
            {
                hasBeenSeen = true;
            }

            seenNow = true;
        }
        else
        {
            seenNow = false;
        }
    }

    private void Update()
    {
        if (hasBeenSeen)
        {
            warningPanel.SetActive(false);
        }
        else
        {
            warningPanel.SetActive(true);
        }
    }
}
