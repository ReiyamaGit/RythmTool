using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactListener : MonoBehaviour
{

    [SerializeField]
    private List<int> m_contactLists = new List<int>();



    public void RegisterContactObject(int panelID)
    {
        int registerID = panelID % 3;
        m_contactLists.Add(registerID);
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
