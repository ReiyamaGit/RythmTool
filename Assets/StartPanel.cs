using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    [SerializeField]
    private SceneDirector m_SceneDirector;

    public void NextScene()
    {
        Debug.Log("hit");
        m_SceneDirector.StartChangeToNextScene();
    }
	
}
