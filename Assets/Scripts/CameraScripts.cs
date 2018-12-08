using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour {

    [SerializeField]
    private GameObject m_target;

    [SerializeField]
    private float m_toDist;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, m_target.transform.position.z - m_toDist);
		
	}
}
