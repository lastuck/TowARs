using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ObjectsInScene objectsInScene;
    void Start()
    {
        objectsInScene = GameObject.Find("Controller").GetComponent<ObjectsInScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            MobStats mobStats = other.GetComponent<MobStats>();
            mobStats.mobHP -= 2;
            if (mobStats.mobHP <= 0)
            {
                objectsInScene.mobsList.Remove(other.gameObject);
                other.gameObject.SetActive(false);
            }
        }
        waitABit();
        gameObject.SetActive(false);
    }

    private IEnumerator waitABit()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
