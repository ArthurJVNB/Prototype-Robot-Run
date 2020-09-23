using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeat : MonoBehaviour
{
    GameObject player;
    Vector3 myHalfSize;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myHalfSize = GetComponentInChildren<Collider>().bounds.extents;
    }

    private void Update()
    {
        if (Mathf.Abs(player.transform.position.z) > Mathf.Abs(transform.position.z + myHalfSize.z))
        {
            transform.position += new Vector3(0, 0, myHalfSize.z/2);
        }
    }
}
