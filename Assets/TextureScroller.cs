using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public Vector2 Scroll = new Vector2(0.05f, 0.05f);
    Renderer m_Renderer;
    Vector2 m_Offset = new Vector2(0f, 0f);

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Offset += Scroll * Time.deltaTime;
        m_Renderer.sharedMaterial.SetTextureOffset("_BaseMap", m_Offset);
    }
}
