using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            // contact
            Debug.Log("Contact");
            Rigidbody refBody = other.gameObject.GetComponent<Rigidbody>();
            refBody.AddForce(Vector3.right * 5);
        }
    }
}
