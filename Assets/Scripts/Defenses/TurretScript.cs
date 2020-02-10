using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Defenses
{
    public class TurretScript : MonoBehaviour
    {
        [SerializeField] 
        private int type;

        private GameObject explosion;
        
        private ObjectsInScene objectsInScene;
        
        private GameObject selectedMob;
        private bool isAiming;

        public List<GameObject> seenMobs;

        private CardsDescription.Card card;
        void Start()
        {
            card = CardsDescription.GetCardStats(type);
            objectsInScene = GameObject.Find("Controller").GetComponent<ObjectsInScene>();
            seenMobs = new List<GameObject>();
            StartCoroutine(nameof(findEnemy));
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 10)
            {
                seenMobs.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            seenMobs.Remove(other.gameObject);
        }

        private IEnumerator findEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                //Debug.Log(isAiming);
                if (!isAiming && seenMobs.Count > 0)
                {
                    StartCoroutine(ShootEnemy(seenMobs[Random.Range(0,seenMobs.Count)]));
                }
            }
        }
        private IEnumerator ShootEnemy(GameObject enemy)
        {
            int cpt = 0;
            isAiming = true;
            MobStats currStats;
            while (cpt < card.firingSpeed)
            {
                yield return new WaitForSeconds(0.1f);
                cpt++;
                if (enemy.activeInHierarchy)
                {
                    transform.LookAt(enemy.transform);
                    transform.Rotate(-90,90,0);
                }

                if (cpt > card.firingSpeed-1)
                {
                    currStats = enemy.GetComponent<MobStats>();
                    switch (card.targetingType)
                    {
                        case 0:
                            currStats.mobHP -= card.damage;
                            currStats.healthBar.value = (float) currStats.mobHP / currStats.maxHP;
                            if (currStats.mobHP <= 0)
                            {
                                seenMobs.Remove(enemy);
                                objectsInScene.mobsList.Remove(enemy);
                                enemy.SetActive(false);
                            }
                            break;
                        case 1:
                            explosion = objectsInScene.objectPooler.GetPooledObject(0);
                            explosion.transform.localScale = new Vector3(4f,4f,4f);
                            explosion.transform.position = enemy.transform.position;
                            explosion.SetActive(true);
                            seenMobs.Remove(enemy);
                            break;
                        default:
                            break;
                    }

                    switch (card.otherEffect)
                    {
                        case 0:
                            break;
                        case 1:
                            currStats.Slow();
                            break;
                    }
                }
            }
            isAiming = false;
        }
        
        private IEnumerator ShootClosestEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                float minDist = 5000;
                foreach (var mob in objectsInScene.mobsList)
                {
                    float currentDist = Vector3.Distance(transform.position,mob.transform.position);
                    if (currentDist < minDist)
                    {
                        selectedMob = mob;
                        minDist = currentDist;
                    }
                }

                if (selectedMob != null)
                {
                    transform.LookAt(selectedMob.transform);
                    selectedMob.SetActive(false);
                }
            }
        }
    }
}
