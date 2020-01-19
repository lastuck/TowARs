using UnityEngine;
using Vuforia;

namespace Defenses
{
    public class GridGenerator : MonoBehaviour, ITrackableEventHandler
    {
        [SerializeField]
        private GameObject gridCell;

        [SerializeField] 
        private GameObject map;

        private GameObject currentCell;

        private bool hasBeenSeen;
        
        private TrackableBehaviour mTrackableBehaviour;

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
                //Debug.Log("yes");
            }
        }
        
        private void Generate()
        {
            for (int i = -35; i < 34; i+=2)
            {
                for (int j = -35; j < 34; j+=2)
                {
                    currentCell = Instantiate(gridCell, new Vector3( i/20.0f,  1f, j/20.0f), Quaternion.Euler(90,0,0));
                    currentCell.transform.parent = map.transform;
                }
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
                    Generate();
                }
            }
        }
    }
}
