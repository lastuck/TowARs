using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button playButton;

        [SerializeField]
        private Button quitButton;

        [SerializeField] 
        private Button decksButton;
        
        private void Start()
        {
            playButton.onClick.AddListener(delegate
            {
                SceneManager.LoadScene("SampleScene");
                SceneParams.buildingDeck = false;
            });
            
            decksButton.onClick.AddListener(delegate
            {
                SceneManager.LoadScene("ScrollDecks");
                SceneParams.buildingDeck = true;
            });

        
            quitButton.onClick.AddListener(Application.Quit);
        }
    }
}
