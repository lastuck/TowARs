using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Defenses;
using UnityEngine;

public class LevelReader : MonoBehaviour
{
    public int numberOfWaves;
    public int[][] Waves;
    public bool hasBeenRead;
    
    public MobSpawner mobSpawner;
    
    [SerializeField] 
    private GameController gameController;
    public void StartWave()
    {
        if (!hasBeenRead)
        {
            ReadLevel();
            hasBeenRead = true;
        }

        mobSpawner.shouldBeGenerated = true;
        //gameController.attackRunning = true;
    }
    
    private void ReadLevel()
    {
        string path = Application.persistentDataPath + "/level_1.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "6\n0\n1\n0;0\n1;0\n1;1;1\n2");
        }
        
        path = Application.persistentDataPath + "/level_2.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "4\n0\n0;0\n0;0\n");
        }
        try 
        {
            using (StreamReader sr = new StreamReader(Application.persistentDataPath + "/level_"+ SceneParams.level +".txt")) 
            {
                string line;
                numberOfWaves = int.Parse(sr.ReadLine());
                /*while ((line = sr.ReadLine()) != null) 
                {
                    Debug.Log(line);
                }*/
                Waves = new int[numberOfWaves][];
                for (int i = 0; i < numberOfWaves; i++)
                {
                    line = sr.ReadLine();
                    string [] WaveString = line.Split(';');
                    Waves[i] = new int[WaveString.Length];
                    for (int j = 0; j < WaveString.Length; j++)
                    {
                        Waves[i][j] = int.Parse(WaveString[j]);
                        Debug.Log(Waves[i][j]);
                    }
                }
            }
        }
        catch (Exception e) 
        {
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
