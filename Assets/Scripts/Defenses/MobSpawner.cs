using System.Collections;
using System.Linq;
using GarbageRoyale.Scripts;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;

namespace Defenses
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] 
        private GameObject mob;
        
        private ObjectsInScene objectsInScene;
        
        private ObjectPooler objectPooler;
        
        private GameObject currentMob;
        
        private NavMeshAgent agent;

        [SerializeField] 
        private GameObject endZone;
        
        private CheckSeen checkSeen;
        
        private bool noARScene;
        
        public bool shouldBeGenerated;
        
        private GameObject currentCamera;
        
        private GameObject attackController;
        
        private LevelReader levelReader;

        public int currentWave;
        
        void Start()
        {
            objectsInScene = GameObject.Find("Controller").GetComponent<ObjectsInScene>();
            levelReader = objectsInScene.levelReader;
            currentCamera = objectsInScene.currentCamera;
            attackController = objectsInScene.attackController;
            checkSeen = objectsInScene.checkSeen;
            objectPooler = objectsInScene.objectPooler;

            levelReader.mobSpawner = this;
            
            noARScene = SceneParams.noARScene;
            currentWave = 0;
            if (noARScene && attackController.activeInHierarchy)
            {
                StartCoroutine(nameof(SpawnMobs));
            }
        }


        IEnumerator SpawnMobs()
        {
            foreach (var mob in levelReader.Waves[currentWave])
            {
                yield return new WaitForSeconds(1f);
                currentMob = objectPooler.GetPooledObject(mob+1);
                currentMob.transform.parent = objectsInScene.map.transform;
                currentMob.transform.position = transform.position;
                currentMob.GetComponent<MobStats>().currentCamera = currentCamera;
                currentMob.SetActive(true);
                objectsInScene.mobsList.Add(currentMob);
                agent = currentMob.GetComponent<NavMeshAgent>();
                agent.SetDestination(endZone.transform.position);
            }

            currentWave += 1;
        }

        private void Update()
        {
            if (!noARScene && checkSeen.hasBeenSeen && shouldBeGenerated && currentWave < levelReader.Waves.Length)
            {
                shouldBeGenerated = false;
                StartCoroutine(nameof(SpawnMobs));
                if (currentWave == levelReader.Waves.Length - 1)
                {
                    SceneParams.lastWave = true;
                }
            }
            else if(noARScene && shouldBeGenerated && currentWave < levelReader.Waves.Length)
            {
                shouldBeGenerated = false;
                StartCoroutine(nameof(SpawnMobs));
                if (currentWave == levelReader.Waves.Length - 1)
                {
                    SceneParams.lastWave = true;
                }
            }
        }
    }
}
