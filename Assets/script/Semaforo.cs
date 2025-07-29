using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Semaforo : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_RendererGreen;
    [SerializeField] private MeshRenderer m_RendererYellow;
    [SerializeField] private MeshRenderer m_RendererRed;
    [SerializeField] private TextMeshProUGUI TextMeshProUGUI;

    [SerializeField] private Image image;

    private Color colorOriginal;
    private float time;
    private float alpha = 1;
    private bool isAplhaOne;
    private bool isAplhaGo;

    private void Start()
    {
        colorOriginal = m_RendererGreen.material.color;
        StartCoroutine(Semaforone());

        
    }

    private void Update()
    {
        if(time > 0) time -= Time.deltaTime;
        TextMeshProUGUI.text = "0.0" + (int)time;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isAplhaOne = !isAplhaOne;
            isAplhaGo = true;
            StartCoroutine(ChangeAplaha());
        }
    }

    private IEnumerator Semaforone()
    {
        while (true)
        {
            m_RendererYellow.material.color = colorOriginal;
            m_RendererRed.material.color = colorOriginal;

            m_RendererGreen.material.color = Color.green;
            time = 3.5f;
            yield return new WaitForSeconds(3);
            m_RendererGreen.material.color = colorOriginal;
            m_RendererRed.material.color = colorOriginal;

            m_RendererYellow.material.color = Color.yellow;
            time = 3.5f;
            yield return new WaitForSeconds(3);
            m_RendererYellow.material.color = colorOriginal;
            m_RendererGreen.material.color = colorOriginal;

            m_RendererRed.material.color = Color.red;
            time = 3.5f;
            yield return new WaitForSeconds(3);
        }
    }

    private IEnumerator ChangeAplaha()
    {
        while(alpha <= 1.5 && alpha >= -0.5f && isAplhaGo)
        {
            alpha += isAplhaOne ? -0.01f : 1f * Time.deltaTime;

            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            Debug.Log(alpha);

            if (alpha <= 1 && alpha >= 0) isAplhaGo = true;
            else isAplhaGo = false;
            yield return null;
        }
    }
}
