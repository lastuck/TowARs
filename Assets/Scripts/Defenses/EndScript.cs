using System;
using UnityEngine;

namespace Defenses
{
    public class EndScript : MonoBehaviour
    {
        private PlayerStats playerStats;
        
        private ObjectsInScene objectsInScene;
        
        private GameController gameController;

        private void Start()
        {
            objectsInScene = GameObject.Find("Controller").GetComponent<ObjectsInScene>();
            playerStats = objectsInScene.playerStats;
            gameController = objectsInScene.gameController;
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject mob  = other.gameObject;
            mob.SetActive(false);
            objectsInScene.mobsList.Remove(mob);
            playerStats.playerHP -= 1;
            if (playerStats.playerHP < 1)
            {
                gameController.LostGame();
            }
        }
    }
}
