using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{

    [SerializeField]
    Material m_Material;

    [SerializeField]
    private float m_time = 0;

    [SerializeField]
    private Color m_Color;

    private int m_ShaderPass;
    private bool m_isFade;
    private bool m_isFadeOut;
    private bool m_isFadeIn;
    private float m_startTime;

    public Color BackGroundColor { get { return m_Color; } set { m_Color = value; } }

    public void StartFadeIn()
    {
        m_isFade = true;
        m_startTime = Time.timeSinceLevelLoad;
        m_ShaderPass = 1;
    }


    public void StartFadeOut()
    {
        m_isFade = true;
        m_startTime = Time.timeSinceLevelLoad;
        m_ShaderPass = 0;
    }

    Color GetColor(RenderTexture src)
    {
        Texture2D tex = new Texture2D(src.width, src.height);
        tex.ReadPixels(new Rect(0, 0, 1, 1), 0, 0);
        tex.Apply();
        Color color = tex.GetPixel(0, 0);

        Debug.Log(color);

        return color;
    }



    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Debug.Log(Mathf.Sin(Time.timeSinceLevelLoad));

        //postProcess.Srcをdestにコピー。その過程でMaterialをRenderTextureに適用してく
        if (m_isFade)
        {
            Graphics.Blit(src, dest, m_Material, m_ShaderPass);
        }
        

        Color color = GetColor(src);

        if (m_isFade && m_ShaderPass == 0 && color == Color.black)
        {
            Debug.Log("Fade out");
            m_isFadeOut = true;
            m_isFadeIn = false;
            m_isFade = false;
        }
        else if (m_isFade && m_ShaderPass == 1 && color == Color.white)
        {
            Debug.Log("FadeIn");
            m_isFadeIn = true;
            m_isFadeOut = false;
            m_isFade = false;

        }
    }

    private void Start()
    {
        //フェードイン
        StartFadeIn();
    }
}
