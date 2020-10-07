using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnElement : MonoBehaviour
{

    [Header("Data")]
    public string name;
    public string[] actions; 

    [Header("Components")]
    public GameObject player;
    public MenuContext menu;
    public QuestManager questManager;
    private SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void emitEvent(int num)
    {
        questManager.emit(name,num);
    }

    public string[] getActions()
    {
        return actions;
    }

    public string getName()
    {
        return name;
    }

    void OnMouseOver()
    {
        //Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dist = Mathf.Abs(Vector3.Distance(transform.position, player.transform.position));
        renderer.color = new Color(1, 0, 0, 1);
        if(dist < 2){
            // Mettre en surbrillance
            renderer.color = new Color(0, 1, 0, 1);
            if(Input.GetMouseButtonDown(1)){
                menu.setSelectedObject(this);
                menu.showOn(transform.position);
            }
        }
    }

    void OnMouseExit()
    {
        renderer.color = new Color(1, 1, 1, 1);
    }


}
