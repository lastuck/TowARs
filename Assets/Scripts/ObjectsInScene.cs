using System;
using System.Collections;
using System.Collections.Generic;
using Defenses;
using GarbageRoyale.Scripts;
using UnityEngine;

public class ObjectsInScene : MonoBehaviour
{
    [SerializeField] 
    public List<GameObject> mobsList;

    [SerializeField] 
    public GameObject map;
    
    [SerializeField] 
    public LevelReader levelReader;
    
    [SerializeField] 
    public CheckSeen checkSeen;

    [SerializeField] 
    public GameObject currentCamera;
    
    [SerializeField] 
    public GameObject attackController;
    
    [SerializeField] 
    public ObjectPooler objectPooler;
    
    [SerializeField] 
    public GameController gameController;
    
    [SerializeField] 
    public PlayerStats playerStats;

    private void Start()
    {
        mobsList = new List<GameObject>();
    }
}
