using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    [Header("Components")]
    public DialogBox dialogBox;
    public Animator transtion;
    public ClickToMove player;

    [Header("Objets")]
    public GameObject porte;
    public GameObject lit;
    public GameObject biblio1;
    public GameObject biblio2;
    public GameObject armure1;
    public GameObject armure2;
    public GameObject cheminee;
    public GameObject table;
    public GameObject tapis;
    public GameObject chevet1;
    public GameObject chevet2;
    public GameObject bureau;
    public GameObject bar;
    public GameObject armoire;
    public GameObject commode;
    public GameObject boite;
    public GameObject fenetre1;
    public GameObject fenetre2;
    public GameObject clef;
    public GameObject calendrier;
    public GameObject tableau1;
    public GameObject tableau2;

    private int state;
    private int map;
    private bool locked = false;
    

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        map = 0;
        boite.SetActive(false);
    }

    IEnumerator incrementeNiveau()
    {
        map++;
        transtion.SetTrigger("blackScreenOn");
        yield return new WaitForSeconds(1.8f);
        player.resetPlayerPosition();
        updateMap();
        yield return new WaitForSeconds(0.2f);
        transtion.SetTrigger("blackScreenOff");
        dialogue();
        //player.resetPlayerAnimation();
        if(map == 1 || map == 2 || map == 3 || map == 4 || map == 9) locked = false;
        else locked = true;

        if(map==10) {
            yield return new WaitForSeconds(4);
            transtion.SetTrigger("blackScreenOn");
            yield return new WaitForSeconds(1.4f);
            SceneManager.LoadScene("EndScene");
        }
        
    }

    public void dialogue(){
        if(map == 1) dialogBox.writeText("A Voice: AH ! AH ! AH ! Did you really think you could get out of here so easily ?");
        if(map == 2) dialogBox.writeText("A Voice: Yes, it's still the same room, but don't you feel a difference ?!");
        if(map == 9) dialogBox.writeText("A Voice: Just the door... Try to open it...");
        if(map == 10) dialogBox.writeText("A Voice: I never told you that the loop can be break... MOUAHAHAH !!!");

    }

    public void emit(string name,int action){
        
        if(name=="porte") {
            porteAction(action);
        } else if(name=="clef"){
            clefAction(action);
        } else if(name=="armoire"){
            armoireAction(action);
        } else if(name=="boite"){
            boiteAction(action);
        } else if(name=="commode"){
            commodeAction(action);
        } else if(name=="bureau"){
            bureauAction(action);
        } else {
            dialogBox.writeText("Nothing here...");
        }
    }

    void bureauAction(int action){
        if(map==4 && action==1) {
                dialogBox.writeText("You find a code !");
                state = 2;
                return;
        }
        // Action par défaut
        if(action==1) dialogBox.writeText("Nothing here ...");
        else if(action==2) dialogBox.writeText("It doesn't seem to light up"); 
        else dialogBox.writeText("You like to go under the desk ? it's your choice !"); 
        
    }

    void porteAction(int action)
    {
        if(!locked)
        {
            StartCoroutine(incrementeNiveau());
            return; 
        }
        if(map == 7 && action==1){
            dialogBox.writeText("The handle has disappeared, it's impossible to open, it could have fallen and rolled");
            return;
        }
        // Action par défaut
        if(action==1) dialogBox.writeText("The door is locked, you just walk out...");
        else dialogBox.writeText("You leave the door closed and continue on your way."); 
        
    }

    void clefAction(int action)
    {
        if(map == 8 && action==1){
            dialogBox.writeText("You find keys and collect them.");
            locked = false;
            return;
        }
        if(map == 5 &&action==1){
            if(state == 1){
                dialogBox.writeText("You rebuild the key !");
                locked = false;
                return;
            } else {
                dialogBox.writeText("There is a half key, you can't use it");
                return;
            }
        }
        if( (map == 8 || map == 5) && action==2 ){
            dialogBox.writeText("You leave it like that ? Hmmm why not?"); 
            return;
        }
        // Action défaut
        if(action==1) dialogBox.writeText("Nothing here...");
        else if(action==2) dialogBox.writeText("You leave it..."); 
        
    }

    void armoireAction(int action)
    {
        if(map == 7 && action==2){
                dialogBox.writeText("You find the handle under the cabinet, you pick it up"); 
                locked = false;
                return;
        }
        if(map == 6 &&action==3){
                dialogBox.writeText("You look behind the cabinet... It's a W- wait... You find a code"); 
                state=1;
                return;
        }
        // Action par défaut
        if(action==1) dialogBox.writeText("Nothing here ...");
        else if(action==2) dialogBox.writeText("Nothing here ..."); 
        else if(action==3) dialogBox.writeText("You look behind the cabinet... It's a wall !"); 
        else dialogBox.writeText("Good try but you are too small..."); 
        
    }

    void boiteAction(int action){
            if(action==1){
                if(state==1){
                    dialogBox.writeText("You find a key inside, you take it");
                    locked = false;
                }
                else dialogBox.writeText("You need a code");
            } else {
                dialogBox.writeText("Can't find a master ball, too bad"); 
            }
    }

    void commodeAction(int action){
        if(map == 5 && action==2){
            dialogBox.writeText("You find a box... and a half key inside the box !"); 
            state=1;
            return;
        }
        // Action par défaut
        if(action==1) dialogBox.writeText("You are looking at an 18th century chest of drawers with a nice trim!");
        else if(action==2) dialogBox.writeText("Nothing here ..."); 
        else if(action==3) dialogBox.writeText("Oh my God ! a cockroach..."); 
        else dialogBox.writeText("Old furnitures does not interest you ?"); 
        
    }

    void updateMap(){
        if(map==1){
            calendrier.SetActive(false);
            chevet1.SetActive(false);
            bar.SetActive(false);
        } else if(map==2){
            biblio1.SetActive(false);
            tableau1.SetActive(false);
        } else if(map==3){
            tapis.SetActive(false);
            biblio2.SetActive(false);
        } else if(map==4){
            table.SetActive(false);
            fenetre1.SetActive(false);
            fenetre2.SetActive(false);
        } else if(map==5){
            bureau.SetActive(false);
            armure1.SetActive(false);
        } else if(map==6){
            commode.SetActive(false);
            cheminee.SetActive(false);
            // Ajout de boite
            boite.SetActive(true);
        } else if(map==7){
            // Retrait de boite
            boite.SetActive(false);
            lit.SetActive(false);
            tableau2.SetActive(false);
        } else if(map==8){
            armoire.SetActive(false);
            armure2.SetActive(false);
        } else if(map==9){
            chevet2.SetActive(false);
            clef.SetActive(false);
        }else if(map==10){
            porte.SetActive(false);
        }
    }
}
