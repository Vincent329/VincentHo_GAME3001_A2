using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safeguard : MonoBehaviour
{
    [SerializeField]
    private GameObject m_barrier;
    [SerializeField]
    private Transform m_vLerpPos;
    [SerializeField]
    private bool m_bTrigger;

    private Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        m_bTrigger = false;
        newPos = m_vLerpPos.position;
        Debug.Log("NewPos: " + newPos);
    }

    // Update is called once per frame
    void Update()
    {
        PositionShift();
    }

    private void PositionShift()
    {
        if (m_bTrigger)
        {
            m_barrier.transform.position = Vector3.Lerp(m_barrier.transform.position, newPos, Time.deltaTime * 3);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("triggering mechanism");
        m_bTrigger = !m_bTrigger;
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.0f);
        m_bTrigger = !m_bTrigger;
    }
}
