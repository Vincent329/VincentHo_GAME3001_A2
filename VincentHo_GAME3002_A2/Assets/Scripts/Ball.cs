/*
 ----------- Developer's Notes ------------
 The only purpose of this script is to keep track of the velocity of the previous frame
 This velocity is then used
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_rb; 
    private Vector3 oldVelocity; // We use the old velocity to 
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        oldVelocity = m_rb.velocity;
    }

    public Vector3 GetOldVelocity()
    {
        return oldVelocity;
    }
}
