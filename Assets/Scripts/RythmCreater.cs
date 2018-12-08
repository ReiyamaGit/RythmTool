using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RythmCreater : MonoBehaviour
{
    [SerializeField]
    private List<float> m_NortsTimes = new List<float>();

    [SerializeField]
    private AudioSource m_music;

    [SerializeField]
    private RawImage[] m_ButtonImage = new RawImage[2];

    [SerializeField,Tooltip("Assetフォルダより下の階層からファイル名を含めたパス")]
    private string m_SavePass = " /StageData/StageData.csv";

    private float m_prevTime = 0f;
    private bool m_isRecord = false;





    /// <summary>
    /// 譜面作成開始
    /// </summary>
    void StartRecord()
    {
        m_music.Play();
        m_prevTime = Time.timeSinceLevelLoad;
        m_isRecord = true;
    }





    /// <summary>
    /// 譜面作成終了
    /// </summary>
    void EndRecord()
    {
        m_music.Stop();
        m_isRecord = false;
        SaveByCSV();
    }





    /// <summary>
    /// CSVファイルの出力
    /// </summary>
    void SaveByCSV()
    {
        StreamWriter writer;
        FileInfo fInfo;
        fInfo = new FileInfo(Application.dataPath + m_SavePass);
        writer = fInfo.AppendText();

        for(int i= 0; i< m_NortsTimes.Count; i++)
        {
            writer.WriteLine(m_NortsTimes[i]);
        }
        writer.Flush();
        writer.Close();
    }





    /// <summary>
    /// ボタンを押した際の時間を記録する
    /// </summary>
    void Generate()
    {
        if (!m_isRecord)
            return;

        if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.D))
        {
            m_NortsTimes.Add(Time.timeSinceLevelLoad - m_prevTime);
            m_prevTime = Time.timeSinceLevelLoad;
        }
    }




    /// <summary>
    /// ボタンを押した際対応しているImageの色を変える
    /// </summary>
    void ChangeButtonColor()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            m_ButtonImage[0].color = new Color(m_ButtonImage[0].color.r / 2.0f, m_ButtonImage[0].color.g / 2.0f, m_ButtonImage[0].color.b / 2.0f);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            m_ButtonImage[0].color = new Color(m_ButtonImage[0].color.r * 2.0f, m_ButtonImage[0].color.g * 2.0f, m_ButtonImage[0].color.b * 2.0f);
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            m_ButtonImage[1].color = new Color(m_ButtonImage[0].color.r / 2.0f, m_ButtonImage[0].color.g / 2.0f, m_ButtonImage[0].color.b / 2.0f);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_ButtonImage[1].color  = new Color(m_ButtonImage[0].color.r * 2.0f, m_ButtonImage[0].color.g * 2.0f, m_ButtonImage[0].color.b * 2.0f);
        }
    }





   
    void Start ()
    {
        m_prevTime = Time.timeSinceLevelLoad;
	}





    void Update()
    {
        //FキーとDキーを押したとき
        Generate();

        //Imageの色変化
        //ChangeButtonColor();
    }




    /// <summary>
    /// uGUI
    /// </summary>
    private void OnGUI()
    {

        //-----レコードの開始-----
        if(!m_isRecord && GUI.Button(new Rect(50, 200, 150,50),"StartRecord"))
        {
            StartRecord();
        }


        //-----レコードの終了-----
        if (GUI.Button(new Rect(0,50,150,50),"EndRecord"))
        {
            EndRecord();
        }


        //-----レコードの停止-----
        if (GUI.Button(new Rect(0,100,150,50),"StopRecord"))
        {
            m_music.Stop();
            m_isRecord = false;
        }

    }
}
