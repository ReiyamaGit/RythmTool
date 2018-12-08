using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    [SerializeField]
    private int m_nextScene;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	
    public void StartChangeToNextScene()
    {
        SceneManager.LoadScene(0);
    }
}
