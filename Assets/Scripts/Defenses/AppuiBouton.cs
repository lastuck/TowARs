using UnityEngine;
using Vuforia;

namespace Defenses
{
    public class AppuiBouton : MonoBehaviour,IVirtualButtonEventHandler
    {
        [SerializeField] 
        private GameObject button;

        [SerializeField] 
        private GameObject targetObject;

        [SerializeField]
        private HoverDetector hoverDetector;

        private bool hasBeenPressed;
        private void Start()
        {
            button.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        }

        public void OnButtonPressed(VirtualButtonBehaviour vb)
        {
            if (!hasBeenPressed && hoverDetector.oldHover != null)
            {
                targetObject.transform.parent = hoverDetector.oldHover.transform;
                targetObject.transform.position = hoverDetector.oldHover.transform.position + Vector3.up;
                targetObject.transform.localScale = new Vector3(1f,1f,1f);
                hasBeenPressed = true;
            }
        }

        public void OnButtonReleased(VirtualButtonBehaviour vb)
        {
            //targetObject.transform.position -= Vector3.up;
        }
    }
}
