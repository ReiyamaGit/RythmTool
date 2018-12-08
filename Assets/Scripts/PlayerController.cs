using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_impulsePower = 2.0f;

    [SerializeField]
    private float m_gravity = 9.8f;

    [SerializeField]
    private GameController m_gameController;

    [SerializeField]
    private float m_arriveTime = 0.8f;

    [SerializeField]
    private Text m_gameOverText;

    [SerializeField]
    private float m_rotateSpeed = 1.0f;

    

    private float m_tempo = 0f;
    private float time = 0f;
    private float m_startTime = 0f;
    private bool m_isStart = false;
    private bool m_isGrounded = false;
    private Vector3 m_startPos;
    private Vector3 m_arraivePos;

    private bool m_isStop;

	// Use this for initialization
	void Start ()
    {
        m_isStop = false;
        m_tempo = m_gameController.m_tempo;
        StartCoroutine("WaitStart");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_isStart && !m_isStop)
        {
            AutoMove();

            if (Input.GetMouseButton(0))
            {
                MouseMove();
            }
        }
        else if(m_isStop)
        {
            m_gameController.ShowGameOverText();
        }
        
    }


    void MouseMove()
    {
        Vector3 objectPointInScreen = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 mousePointScreen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, objectPointInScreen.z);

        Vector3 mousePointWorld = Camera.main.ScreenToWorldPoint(mousePointScreen);

        mousePointWorld.y = transform.position.y;
        mousePointWorld.z = transform.position.z;
        transform.position = mousePointWorld;
    }

    void SetMovePoint()
    {
       if(m_isStop == true)
        {
            return;
        }

        m_startPos = transform.position;

        m_arraivePos = m_gameController.NextPanel() - new Vector3(0f, 0f, 0.6f) ;

        m_startTime = Time.timeSinceLevelLoad;

    }

    public void AutoMove()
    {
        time += Time.deltaTime;

        //yの移動計算
        float speed = m_impulsePower - m_gravity * m_tempo * time;
        float y = transform.position.y + speed ;

        //一定以下の数値になったら時間を初期化する
        if(y <= 0.4f && m_isGrounded)
        {
#if ENABLE_DEBUG
            Debug.Log(m_gameController.NortIndex + "：" + (m_gameController.NortTime - (Time.timeSinceLevelLoad - m_startTime)));
#endif 
            y = 0.4f;
            time = 0;
            m_isGrounded = false;
            m_tempo = m_gameController.NextTempo();

            if (m_tempo == -1)
            {
                m_isStop = true;
            }
        }

        //ｚの移動計算
        float diff = Time.timeSinceLevelLoad - m_startTime;
        float rate = diff / m_arriveTime * m_tempo;
        Vector3 forward = Vector3.Lerp(m_startPos, m_arraivePos, rate);

        //最終座標
        transform.position = new Vector3(transform.position.x, y, forward.z);

        //バウンドが終了していた場合強制的に終点に移動
        if(y == 0.4f)
        {
         
            transform.position = new Vector3(transform.position.x, y, Vector3.Lerp(m_startPos, m_arraivePos, 1.0f).z);
            SetMovePoint();
        }


        transform.Rotate(transform.right * m_rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_isGrounded = true;
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(3.0f);
        m_isStart = true;
        SetMovePoint();
        yield break;
    }

}
