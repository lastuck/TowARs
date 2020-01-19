using UnityEngine;

namespace Defenses
{
    public class GridColorChanger : MonoBehaviour
    {
        public void ChangeColorToGreen()
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        public void ChangeColorToCyan()
        {
            GetComponent<Renderer>().material.color = new Color(0,1,1,0.8f);
        }
    }
}
