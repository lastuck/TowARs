using System;
using UnityEngine;
using Vuforia;

namespace Defenses
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject gridCell;
        
        private GameObject map;

        [SerializeField] 
        private CheckSeen checkSeen;

        [SerializeField] 
        private ObjectsInScene objectsInScene;
        
        private GameObject currentCell;

        private bool hasBeenGenerated;

        private void Generate()
        {
            for (int i = -12; i < 12; i++)
            {
                for (int j = -22; j < 2; j++)
                {
                    currentCell = Instantiate(gridCell, new Vector3( i,  3f, j), Quaternion.Euler(90,0,0));
                    currentCell.transform.parent = objectsInScene.map.transform;
                }
            }
        }


        private void Update()
        {
            if (checkSeen.hasBeenSeen && !hasBeenGenerated)
            {
                hasBeenGenerated = true;
                Generate();
            }
        }
    }
}
