using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour {

    const int GENERATE_POINT = 3;

    public float m_tempo = 1.0f;
    public int m_nortIndex = 1;
    public List<GameObject> m_panels = new List<GameObject>();
    private Vector3 m_prevPanelPosition;

    [SerializeField]
    GameObject[] m_generatePoints = new GameObject[GENERATE_POINT];

    [SerializeField]
    GameObject m_panel;

    [SerializeField]
    float   m_panelStep = 5f;

    [SerializeField]
    int m_startPanelCount = 5;

    [SerializeField]
    int     m_panelCount = 30;

    [SerializeField]
    private List<float> m_nortTimes = new List<float>();

    [SerializeField]
    private Text m_gameOverText;



    float   m_time;
    float   m_currentTempo = 0f;
    int     m_pointsIdx;
    int     m_panelIdx = 0;
    bool    m_isGameOver;

    Generator[] m_generators = new Generator[3];

   public bool IsGameOver { get { return m_isGameOver; } }
    public float NortTime { get { return m_nortTimes[m_nortIndex - 1]; } }
    public float NortIndex { get { return m_nortIndex - 1; } }

    public void StartGenerate()
    {
        Vector3 stdPos = transform.position;
        for(int i = 0; i < m_panelCount; i++)
        {
            //int x = Random.value > 0.5f ? 1 : -1;
            m_panels.Add(Instantiate(m_panel, stdPos + /*Quaternion.Euler(0f, 20f,0)*/ transform.forward  * m_panelStep, transform.rotation, transform));
            stdPos = m_panels[m_panels.Count - 1].transform.position;
        }

        print(m_panels.Count);
    }


    public Vector3 NextPanel()
    {
        Vector3 result = m_panels[m_panelIdx].transform.position;
        m_panelIdx++;

        //もう参照できるパネルがない
        if (m_panelIdx > m_panels.Count - 1)
            m_isGameOver = true;

        return result;
    }


    public void AddPanel(GameObject panel)
    {
        panel.GetComponent<PanelScript>().PanelID = m_panels.Count;
        m_panels.Add(panel);
    }

    public void DeletePanel(GameObject panel)
    {

       for(int i= 0; i< m_panels.Count; i++)
        {
            if(m_panels[i] == panel)
            {
                m_panels[i] = null;
                Destroy(m_panels[i].gameObject);
                break;
            }
        }
    }

    public float NextTempo()
    {
        if(m_nortIndex < m_nortTimes.Count)
        {
            return m_tempo / m_nortTimes[m_nortIndex++];
        }

        return -1;
    }


    public void ShowGameOverText()
    {
        m_gameOverText.gameObject.SetActive(true);
    }

    void SetGenerator()
    {
        if(transform.childCount < 2)
        {
            Debug.LogError("Add Generator as GameControllerChild");
        }

        m_generators[0] = transform.GetChild(0).GetComponent<Generator>();
        m_generators[1] = transform.GetChild(1).GetComponent<Generator>();
        m_generators[2] = transform.GetChild(2).GetComponent<Generator>();


        for (int i = 0; i < m_nortTimes.Count; i++)
        {
            //for(int x = 0; x<3; x++)
            //{
            //    m_generators[x].GenerateStep = m_panelStep / (m_tempo / m_nortTimes[i]);
            //    m_generators[x].GenerateItem = m_panel;
            //    m_generators[x].GameController = this;
            //    m_generators[x].Generate(i);
            //}
            int random = Random.Range(0, 3);
            Vector3 startPos = new Vector3(m_generatePoints[random].transform.position.x, m_generatePoints[random].transform.position.y, m_prevPanelPosition.z);
            m_generators[random].GenerateStep = m_panelStep / (m_tempo / m_nortTimes[i]);
            m_generators[random].GenerateItem = m_panel;
            m_generators[random].GameController = this;
            
            m_prevPanelPosition = m_generators[random].Generate(i,startPos);
        }

    }

    void LoadData()
    {
        //for(int i = 0; i< PlayerPrefs.GetInt("Count"); i++)
        //{
        //    m_nortTimes.Add(PlayerPrefs.GetFloat(i.ToString()));
        //}

        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/StageData/StageData.csv");
        string str ;
        while ((str = reader.ReadLine())  != null)
        {
            m_nortTimes.Add(float.Parse(str));

            #if ENABLE_DEBUG
                 Debug.Log(m_nortTimes[m_nortTimes.Count - 1]);
            #endif

        }
      

    }

    // Use this for initialization
    void Awake()
    {
        LoadData();
        //StartGenerate();
        SetGenerator();
        m_currentTempo = m_tempo;

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 40;
        GUI.TextField(new Rect(0f, 0f, 100f, 100f), Time.time.ToString(), style);
    }
}
