using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    [SerializeField] 
    private GameObject Cylinder;
    
    private void OnCollisionEnter(Collision other)
    {
        //Instantiate(Cylinder, transform.position, Quaternion.identity);
        Target target = other.gameObject.GetComponent<Target>();

        Debug.Log("wesh");
        //If there was a Target script on the thing we hit, tell it we hit it
        if ( target != null )
            target.Hit();
    }
}
