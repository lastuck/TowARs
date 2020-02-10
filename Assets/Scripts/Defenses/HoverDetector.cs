using UnityEngine;

namespace Defenses
{
    public class HoverDetector : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        
        private LayerMask mouseMask;
        public GameObject oldHover;

        [SerializeField] 
        private int cost;

        private PlayerStats playerStats;

        [SerializeField] 
        private TurretScript turretScript;
        private void Start()
        {
            playerStats = GameObject.Find("Controller").GetComponent<PlayerStats>();
            mouseMask = LayerMask.GetMask("Grid");
        }

        void Update()
        {
            ray = new Ray(transform.position,transform.forward*-1);
            if (Physics.Raycast(ray, out hit, 100f, mouseMask))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.gameObject != oldHover)
                {
                    hit.collider.gameObject.GetComponent<GridColorChanger>().ChangeColorHover();
                    if (oldHover)
                    {
                        oldHover.GetComponent<GridColorChanger>().ChangeColorToCyan();
                    }
                    oldHover = hit.collider.gameObject;
                    
                    if (oldHover.GetComponent<GridColorChanger>().validArea && Vector3.Distance(oldHover.transform.position,transform.position) < 1 && playerStats.playerRP >= cost)
                    {
                        transform.SetParent(oldHover.transform);
                        transform.position = oldHover.transform.position+Vector3.down*0.8f;
                        transform.localScale = new Vector3(0.3f,0.3f,0.3f);
                        transform.rotation = Quaternion.identity;
                        transform.Rotate(-90,0,0);
                        playerStats.playerRP -= cost;
                        turretScript.enabled = true;
                        enabled = false;
                    }
                }
            }
            else
            {
                if (oldHover)
                {
                    oldHover.GetComponent<GridColorChanger>().ChangeColorToCyan();
                    oldHover = null;
                }
            }

            /*if (hasSelectedCard)
        {
            //keep track of the mouse position
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z); 
            card.transform.position = defenseCam.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0f,0f,10f);
        }*/
        }
        /*void OnMouseOver()
    {
        Debug.Log("On End");
    }

    void OnMouseExit()
    {
        Debug.Log("Exit End");
    }*/
    }
}
