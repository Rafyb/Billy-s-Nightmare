using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MenuContext : MonoBehaviour
{   

    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;

    private TextMeshProUGUI b1text;
    private TextMeshProUGUI b2text;
    private TextMeshProUGUI b3text;
    private TextMeshProUGUI b4text;

    private MonoBehaviour selectedObject;

    void Start()
    {
        gameObject.SetActive(false);

        btn1.onClick.AddListener(()=>ClickAction(1));
        b1text = btn1.GetComponentInChildren<TextMeshProUGUI>();

        btn2.onClick.AddListener(()=>ClickAction(2));
        b2text = btn2.GetComponentInChildren<TextMeshProUGUI>();

        btn3.onClick.AddListener(()=>ClickAction(3));
        b3text = btn3.GetComponentInChildren<TextMeshProUGUI>();

        btn4.onClick.AddListener(()=>ClickAction(4));
        b4text = btn4.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {

    } 

    public void setSelectedObject(MonoBehaviour obj)
    {
        this.selectedObject = obj;
    }

    public void showOn(Vector3 pos)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = pos ;

        string[] actions =  ((ClickOnElement)this.selectedObject).getActions();
        int nb = actions.Length;
        
        if(nb < 4) {
            btn4.interactable = false;
            b4text.text = "";
        } else {
            btn4.interactable = true;
            b4text.text = actions[3];
        }
        if(nb < 3) {
            btn3.interactable = false;
            b3text.text = "";
        } else {
            btn3.interactable = true;
            b3text.text = actions[2];
        }
        if(nb < 2) {
            btn2.interactable = false;
            b2text.text = "";
        } else {
            btn2.interactable = true;
            b2text.text = actions[1];
        }
        if(nb < 1) {
            btn1.interactable = false;
            b1text.text = "";
        } else {
            btn1.interactable = true;
            b1text.text = actions[0];
        }

        if(nb <= 0) closeMenu();
        else gameObject.SetActive(true);
    }

    void ClickAction(int num)
    {
        ((ClickOnElement)this.selectedObject).emitEvent(num);
        closeMenu();
    }

    public void closeMenu()
    {
        gameObject.SetActive(false);
        this.selectedObject = null;
    }

    public MonoBehaviour getSelectedObject()
    {
        return this.selectedObject;
    }


}
