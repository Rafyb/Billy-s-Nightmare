using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{

    private TextMeshProUGUI texte;
    private float typingTime;
    private float typingSpeed;
    private int typingIdx;
    private char[] waitingTexte;

    void Start()
    {
        texte = GetComponentInChildren<TextMeshProUGUI>();
        texte.text = "";
        typingTime = 0;
        typingIdx = 0;
        typingSpeed = 0.05f;
        gameObject.SetActive(false);
    }


    void Update()
    {
        typingTime += Time.deltaTime;
        if(typingTime >= typingSpeed){
            typingTime = 0;
            if(waitingTexte != null)
            {
                if(typingIdx >= waitingTexte.Length )
                {
                    waitingTexte = null;
                    typingIdx = 0;
                    typingSpeed = 1.5f;
                } else {
                    texte.text += waitingTexte[typingIdx++];
                }
            } else {
                texte.text = "";
                typingSpeed = 0.05f;
                gameObject.SetActive(false);
            }
        }
    }

    public void writeText(string str)
    {
        gameObject.SetActive(true);
        waitingTexte = str.ToCharArray();
        texte.text = "";
        typingSpeed = 0.05f;
    }
}
