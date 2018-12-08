using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScript : MonoBehaviour {

    [SerializeField]
    private float m_toPlayerDist = 5.0f;

    private GameObject m_player;
    private GameController m_gameController;
    private ContactListener m_contactListener;

    public int PanelID{get;set;}
    

	// Use this for initialization
	void Start ()
    {
        m_player = FindObjectOfType<PlayerController>().gameObject;
        m_gameController = FindObjectOfType<GameController>();
        m_contactListener = FindObjectOfType<ContactListener>();
        if(!m_player)
        {
            Debug.LogWarning("Not Found Player");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Vector3 dist = m_player.transform.position - transform.position;
        
        //if(dist.z > m_toPlayerDist)
        //{
        //    m_gameController.DeletePanel(gameObject);
        //}
	}

    private void OnTriggerEnter(Collider other)
    {
        m_contactListener.RegisterContactObject(PanelID);
    }
}
