using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

    private AudioClip m_backGroundMusic;
    private float m_endTime;


	// Use this for initialization
	void Start ()
    {
        m_backGroundMusic = GetComponent<AudioSource>().clip;
        m_endTime = Time.timeSinceLevelLoad + m_backGroundMusic.length;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(m_endTime < Time.timeSinceLevelLoad)
        {
            Debug.LogWarning("曲が終了しました。");
        }
	}
}
