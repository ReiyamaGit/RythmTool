using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGround : MonoBehaviour {

    [SerializeField]
    private GameObject m_player;

    [SerializeField]
    private float m_toDistance = 100;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, m_player.transform.position.z + m_toDistance);
	}
}
