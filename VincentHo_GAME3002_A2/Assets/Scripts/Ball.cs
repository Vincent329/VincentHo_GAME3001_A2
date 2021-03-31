using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_rb;

    private Vector3 oldVelocity;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position - m_rb.velocity.normalized);
        oldVelocity = m_rb.velocity;
    }

    public Vector3 GetOldVelocity()
    {
        return oldVelocity;
    }
}
