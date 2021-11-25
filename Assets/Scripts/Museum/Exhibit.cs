﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhibit : MonoBehaviour
{
    HashSet<Material> m_materials = new HashSet<Material>();
    Dictionary<Material, Color32> originalColors = new Dictionary<Material, Color32>();
 
    Shader TransparentShader;
    Shader OriginalShader;

    Transform m_center;

    public Color32 m_Color;

    public int exhibitId;
    public string exhibitName;
    public string content;
    public string helpmessage;

    public string year;
    public string producer;

    public int m_MinRotate;
    public int m_MaxRotate;

    public float m_MidRigHeight;
    public float m_TopRigHeight;

    public int m_minFOV;
    public int m_maxFOV;


    void Awake()
    {
        m_minFOV = 20;
        m_maxFOV = 40;
        if (gameObject.CompareTag("Exhibit"))
        {
            m_Color = new Color32(255, 0, 0, 100);
        }
        if (gameObject.CompareTag("GuestBook"))
        {
            m_Color = new Color32(0, 0, 255, 100);
        }
        if (gameObject.CompareTag("Game"))
        {
            m_Color = new Color32(255, 0, 0, 100);
        }
        
        m_center = transform.GetChild(transform.childCount - 1);
        
        MeshRenderer[] childMeshs = GetComponentsInChildren<MeshRenderer>();
        
        for (int i = 0; i < childMeshs.Length; i++)
        {
            Material[] materials = childMeshs[i].materials;
            for (int j = 0; j < materials.Length; j++)
            {
                Material material = materials[j];
                if (m_materials.Add(material))
                {
                    m_materials.Add(material);
                    OriginalShader = material.shader;
                }
            }
        }

        foreach (Material material in m_materials)
        {
            originalColors.Add(material, material.color);
        }

        TransparentShader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
    }
     

    public Transform GetCenter()
    {
        return m_center;
    }
    public void StartBlink()
    {
        StartCoroutine("BlinkObject");
    }
    public void StopBlink()
    {
        StopCoroutine("BlinkObject");
        FillColor();
    }
    void BlurredColor()
    {
        foreach (Material material in m_materials)
        {
            material.shader = TransparentShader;
            material.color = m_Color;
        }
    }
    void FillColor()
    {
        foreach (Material material in m_materials)
        {
            material.shader = OriginalShader;
            Color32 originalColor = originalColors[material];
            material.color = new Color32(originalColor[0], originalColor[1], originalColor[2], originalColor[3]);
        } 
    }
    IEnumerator BlinkObject()
    {
        yield return null;
        while (true)
        {
            BlurredColor();
            yield return new WaitForSeconds(0.5f);

            FillColor();
            yield return new WaitForSeconds(0.5f);
        }
    }
    public virtual void OpenRankBoard()
    {
    }
    public virtual void StartGame()
    {
    }

    public virtual void CloseRankBoard()
    { 
    }
    public virtual void RequestGameRank()
    {

    }
}
