using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





/// <summary>
/// UIを管理する
/// </summary>
/// <remarks> このゲームのUIはすべてここからアクセスしてください </remarks>
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<Text> m_textLists = new List<Text>();

    [SerializeField]
    private List<Image> m_imageLists = new List<Image>();



    public void SetActiveTextAtIndex(int index)
    {
        m_textLists[index].gameObject.SetActive(false);
    }

    public void SetTextAtIndex(string str,int index)
    {
        try
        {
            m_textLists[index].text = str;
        }
        catch(UnityException e)
        {
            Debug.LogError(e.Message);
        }
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
