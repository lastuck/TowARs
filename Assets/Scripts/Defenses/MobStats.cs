using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MobStats : MonoBehaviour
{
    public int mobHP;
    
    [SerializeField]
    public int maxHP;

    [SerializeField] 
    public NavMeshAgent agent;

    [SerializeField]
    public Slider healthBar;
    
    [SerializeField]
    public int baseSpeed;

    public GameObject currentCamera;
    
    private void Start()
    {
        mobHP = maxHP;
        healthBar.value = 1;
        agent.speed = baseSpeed;
    }

    private void OnEnable()
    {
        mobHP = maxHP;
        healthBar.value = 1;
    }

    private void Update()
    {
        healthBar.transform.LookAt(currentCamera.transform);
    }

    public void Slow()
    {
        agent.speed = baseSpeed * 0.5f;
    }
}
