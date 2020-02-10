using System;
using UnityEngine;

namespace Defenses
{
    public class GridColorChanger : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;

        public bool validArea;
        
        [SerializeField]
        private LayerMask wallMask;

        private void Start()
        {
            ray = new Ray(transform.position,new Vector3(0f,-1f,0f));
            if (Physics.Raycast(ray, out hit, 10f, wallMask))
            {
                validArea = true;
            }
        }

        public void ChangeColorHover()
        {
            
            if (validArea)
            {
                GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
        }

        public void ChangeColorToCyan()
        {
            GetComponent<Renderer>().material.color = new Color(0,1,1,0.0f);
        }
    }
}
