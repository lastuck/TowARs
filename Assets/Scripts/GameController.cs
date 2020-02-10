using System;
using System.Collections;
using System.Collections.Generic;
using Defenses;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] 
    private GameObject attackController;
    
    [SerializeField] 
    private GameObject defenseController;

    [SerializeField] 
    private Button skipDefenseButton;
    
    [SerializeField] 
    private Button nextLevelButton;

    [SerializeField] 
    private Button retryButton;
    
    [SerializeField] 
    private Button mainMenuButtonWin;
    
    [SerializeField] 
    private Button quitButtonWin;
    
    [SerializeField] 
    private Button mainMenuButtonLost;
    
    [SerializeField] 
    private Button quitButtonLost;
    
    [SerializeField] 
    private ObjectsInScene objectsInScene;

    [SerializeField] 
    private GameObject[] levelContainers;
    
    public bool attackRunning;
    
    [SerializeField] 
    private LevelReader levelReader;

    [SerializeField] 
    private GameObject winningPanel;
    
    [SerializeField] 
    private GameObject loosingPanel;

    [SerializeField] 
    private GameObject mainTracker;

    [SerializeField] 
    private DeckListReader deckListReader;
    private void Start()
    {
        SceneParams.lastWave = false;
        objectsInScene.map = Instantiate(levelContainers[SceneParams.level - 1],mainTracker.transform);
        objectsInScene.map.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        objectsInScene.map.GetComponent<NavMeshSurface>().BuildNavMesh();
        defenseController.SetActive(true);
        
        skipDefenseButton.onClick.AddListener(delegate
        {
            defenseController.SetActive(false);
            attackController.SetActive(true);
            levelReader.StartWave();
        });
        nextLevelButton.onClick.AddListener(delegate
        {
            SceneParams.lastWave = false;
            SceneParams.level += 1;
            SceneManager.LoadScene("SampleScene");
        });
        retryButton.onClick.AddListener(delegate
        {
            SceneParams.lastWave = false;
            SceneManager.LoadScene("SampleScene");
        });
        
        mainMenuButtonWin.onClick.AddListener(delegate
        {
            SceneParams.lastWave = false;
            SceneManager.LoadScene("MenuScene");
        });
        
        quitButtonWin.onClick.AddListener(Application.Quit);
        
        mainMenuButtonLost.onClick.AddListener(delegate
        {
            SceneParams.lastWave = false;
            SceneManager.LoadScene("MenuScene");
        });
        
        quitButtonLost.onClick.AddListener(Application.Quit);
        
    }

    private void Update()
    {
        if (objectsInScene.mobsList.Count > 0)
        {
            attackRunning = true;
        }
        if (attackRunning)
        {
            if (objectsInScene.mobsList.Count == 0)
            {
                attackController.SetActive(false);
                attackRunning = false;
                if (!SceneParams.lastWave)
                {
                    defenseController.SetActive(true);
                    deckListReader.FillHand();
                    objectsInScene.playerStats.playerMaxRP += 1;
                    objectsInScene.playerStats.playerRP = objectsInScene.playerStats.playerMaxRP;
                }
                else
                {
                    winningPanel.SetActive(true);
                }
            }
        }
    }

    public void LostGame()
    {
        loosingPanel.SetActive(true);
    }
}
