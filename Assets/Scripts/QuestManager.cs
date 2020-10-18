using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

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
    private int[] docs;
    private string[] pwd;
    private bool locked = false;
    

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        map = 0;
        docs = new int[4];
        pwd = new string[4];
        boite.SetActive(false);
        initDocs();
    }

    void initDocs()
    {
        for(int i = 0; i < 4; i++)
        {
            int value = UnityEngine.Random.Range(0, 9);
            docs[i] = value;
        }
        pwd[0] = "" + docs[1] + docs[2] + docs[0] + docs[3];
        pwd[1] = "" + docs[3] + docs[2] + docs[0] + docs[1];
        pwd[2] = "" + docs[0] + docs[2] + docs[3] + docs[1];
        pwd[3] = "" + docs[1] + docs[2] + docs[3] + docs[0];

    }

    IEnumerator incrementeNiveau()
    {
        map++;
        state = 0;
        if (map == 0 || map == 9) locked = false;
        else locked = true;

        transtion.SetTrigger("blackScreenOn");
        yield return new WaitForSeconds(1.8f);
        player.resetPlayerPosition();
        updateMap();
        yield return new WaitForSeconds(0.2f);
        transtion.SetTrigger("blackScreenOff");
        dialogue();
        //player.resetPlayerAnimation();
        
        if(map==10) {
            yield return new WaitForSeconds(4);
            transtion.SetTrigger("blackScreenOn");
            yield return new WaitForSeconds(1.4f);
            SceneManager.LoadScene("EndScene");
        }
        
    }

    public void dialogue(){
        if (map == 1) dialogBox.writeText("A Voice: AH ! AH ! AH ! Did you really think you could get out of here so easily ?");
        if (map == 2) dialogBox.writeText("A Voice: Yes, it's still the same room, but don't you feel a difference ?!");
        if (map == 3) dialogBox.writeText("A Voice: If you have some time, can you sort my papers?");
        if (map == 4) dialogBox.writeText("A Voice: No more paper ! I hope you have enough to spend the winter here.");
        if (map == 5) dialogBox.writeText("A Voice: ok ok ... I see ... Without a code, what are you going to do?");
        if (map == 6) dialogBox.writeText("A Voice: I didn't know you were capable of repairing a key, what a talent!");
        if (map == 7) dialogBox.writeText("A Voice: No handle? What a piece of bad luck!");
        if (map == 8) dialogBox.writeText("A Voice: All right, I give up. You win.");
        if (map == 9) dialogBox.writeText("A Voice: Just the door... Try to open it...");
        if (map == 10) dialogBox.writeText("A Voice: I never told you that the loop can be break... MOUAHAHAH !!!");

    }

    public void emit(string name,int action){
        // Sur la première carte on peut sortir direct
        if(map == 0)
        {
            if (name == "porte")
            {
                porteAction(action);
                return;
            } else
            {
                dialogBox.writeText("Billy : Where am I ? I need to go out to find some help !");
                return;
            }
        }
        // Sur les autres cartes    
        if (name == "porte")
        {
            porteAction(action);
        }
        else if (name == "clef")
        {
            clefAction(action);
        }
        else if (name == "armoire")
        {
            armoireAction(action);
        }
        else if (name == "boite")
        {
            boiteAction(action);
        }
        else if (name == "commode")
        {
            commodeAction(action);
        }
        else if (name == "bureau")
        {
            bureauAction(action);
        }
        else if (name == "cheminee")
        {
            fireplaceAction(action);
        }
        else if (name == "chevet1")
        {
            chevet1Action(action);
        }
        else if (name == "chevet2")
        {
            chevet2Action(action);
        }
        else if (name == "table")
        {
            tableAction(action);
        }
        else if (name == "tapis")
        {
            tapisAction(action);
        }
        else if (name == "lit")
        {
            litAction(action);
        }
        else if (name == "bibliotheque2")
        {
            biblioAction(action);
        }
        else if (name == "armure")
        {
            dialogBox.writeText("A Voice : I Know what you're thinking, you can't.");
            armure1.SetActive(false);
            armure2.SetActive(false);
        }
        else
        {
            dialogBox.writeText("Nothing here...");
        }
    }

    void litAction(int action)
    {
        if(map == 2 && action == 2 && state == 2)
        {
            dialogBox.writeText("You found page 47 of the book the Krosmoz Vol.2");
            state = 3;
            return;
        }
        if (map == 1 && action == 2)
        {
            if(state == 0)
            {
                dialogBox.writeText("You can't see nothing.");
                return;
            }
            if (state == 1)
            {
                dialogBox.writeText("You find a note with something that looks like a password.");
                state = 2;
                return;
            }

            state = 3;
            return;
        }
        if (action == 1) dialogBox.writeText("I'm not sure it's the right time.");
        else dialogBox.writeText("No monsters under the bed : checked !");
    }

    void biblioAction(int action)
    {
        if(map == 2 && action == 2)
        {
            if(state == 1)
            {
                dialogBox.writeText("You take Krosmoz Vol.1 and replace the page, Vol.2 has also lost its page 47");
                state = 2;
                return;
            }
            if (state == 2)
            {
                dialogBox.writeText("Billy: Hmmm I need to find this page the story is incredible...");
                return;
            }
            if (state == 3)
            {
                dialogBox.writeText("You take Krosmoz Vol.2 and replace the page, Vol.3 has a writing code on its page 47");
                state = 4;
                return;
            }
            dialogBox.writeText("Which book to take...");
            return;
        }
        if (map == 1 && action == 2 && state == 3)
        {
            dialogBox.writeText("You take a book named \"Pokedex\", num 491... I will have to keep this number in case.");
            state = 4;
            return;
        }
        // Action par défaut
        if (action == 1) dialogBox.writeText("The books are incredibly well organized, there's a lot of them.");
        else if (action == 2) dialogBox.writeText("The books seem relatively heavy to you, you should have done sports instead of playing video games");
        else dialogBox.writeText("A little mouse passes you in front of your eyes, so cute");
    }

    void bureauAction(int action){
        if(map==4 && action==1) {
            dialogBox.writeText("It's write 123, wow nice code !");
            state = 1;
            return;
        }
        if (map == 3 && action == 2 && state == 0)
        {
            dialogBox.writeText("The computer is starting, please enter the code :");
            ClickOnElement bureauCmp = bureau.GetComponent<ClickOnElement>();
            for(int i = 0; i < 4; i++) bureauCmp.actions[i] = pwd[i];  
            state = 1;
            return;
        }
        if (map == 3 && state == 1)
        {
            if(action == 3)
            {
                dialogBox.writeText("You hear a 'Click' coming from the left.");
                state = 2;
                ClickOnElement bureauCmp = bureau.GetComponent<ClickOnElement>();
                bureauCmp.actions[0] = "Look at";
                bureauCmp.actions[1] = "Use computer";
                bureauCmp.actions[2] = "Look under";
                bureauCmp.actions[3] = "Leave";
                return;
            } else
            {
                dialogBox.writeText("Incorrect Password");
                initDocs();
                ClickOnElement bureauCmp = bureau.GetComponent<ClickOnElement>();
                for (int i = 0; i < 4; i++) bureauCmp.actions[i] = pwd[i];
                return;
            }
        }
        if (map == 1 && action == 2)
        {
            if (state < 2)
            {
                dialogBox.writeText("A password is required");
                return;
            }
            if (state == 2)
            {
                dialogBox.writeText("You enter the code and you find a list of Pokemon, a number is missing...");
                state = 3;
                return;
            }
            if (state == 3)
            {
                dialogBox.writeText("Billy: Maybe a book can help me...");
                return;
            }
        }
        // Action par défaut
        if (action==1) dialogBox.writeText("Nothing here ...");
        else if(action==2) dialogBox.writeText("It doesn't seem to light up"); 
        else if(action==3) dialogBox.writeText("You like to go under the desk ? it's your choice !"); 
        else dialogBox.writeText("No other LoL games ?");

    }

    void tableAction(int action)
    {
        if(action == 1)
        {
            dialogBox.writeText("Report 04-16-1996 : The patient survived "+docs[0]+" days");
        }
        if (action == 2)
        {
            dialogBox.writeText("Report 02-04-2002 : The patient survived "+docs[1]+" days");
        }
        if (action == 3)
        {
            dialogBox.writeText("Report 07-02-1999 : The patient survived " + docs[2] + " days");
        }
        if (action == 4)
        {
            dialogBox.writeText("Report 07-15-1999 : The patient survived " + docs[3] + " days");
        }
    }

    void porteAction(int action)
    {
        if(!locked && action == 1)
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
        if(map == 8 && action==1 && locked == true)
        {
            dialogBox.writeText("You find keys and collect them.");
            locked = false;
            return;
        }
        if(map == 5 && action==1 && locked == true){
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
        if (map == 2 && action == 1)
        {
            if(state == 4)
            {
                dialogBox.writeText("You remove the padlock and retrieve the key.");
                locked = false;
            } else
            {
                dialogBox.writeText("The key is secured with a coded padlock.");
            }
            return;
        }
        if (map == 1 && action == 1)
        {
            if (state == 4)
            {
                dialogBox.writeText("You remove the padlock and retrieve the key.");
                locked = false;
            }
            else
            {
                dialogBox.writeText("The key is secured with a coded padlock.");
            }
            return;
        }
        // Action défaut
        if (action==1) dialogBox.writeText("Nothing here...");
        else if(action==2) dialogBox.writeText("You leave it..."); 
        
    }

    void chevet1Action(int action)
    {
        if (map == 1 && action == 2 && state == 0)
        {
            dialogBox.writeText("You turn on the lamp");
            state = 1;
            return;
        }
        if (map == 1 && action == 2 && state == 1)
        {
            dialogBox.writeText("You turn off the lamp");
            state = 0;
            return;
        }
        // Action défaut
        if (action == 1) dialogBox.writeText("Nothing here...");
        else if (action == 2) dialogBox.writeText("Lamp refuses to turn on");
        else if (action == 3) dialogBox.writeText("Lamp is already off");
    }
    
    void chevet2Action(int action)
    {
        if(map == 4 && action == 2 && state == 1)
        {
            dialogBox.writeText("You find some matches with your current numbers");
            state = 2;
            return;
        }
        // Action défaut
        if (action==1) dialogBox.writeText("Nothing here...");
        else if(action == 2) dialogBox.writeText("Weird book with a lot of numbers");
        else if(action==3) dialogBox.writeText("You leave it..."); 
    }

    void tapisAction(int action)
    {
        Debug.Log(state);
        Debug.Log(map);
        if (map == 2 && state == 0)
        {
            dialogBox.writeText("You found page 47 of the book the Krosmoz Vol.1");
            state = 1;
            return;
        }
        // Action par défaut
        dialogBox.writeText("Nothing but I think you can sell it to Toom Nook");
    }

    void armoireAction(int action)
    {
        if(map == 7 && action==2 && locked == true){
                dialogBox.writeText("You find the handle under the cabinet, you pick it up"); 
                locked = false;
                return;
        }
        if(map == 6 &&action==3){
                dialogBox.writeText("You look behind the cabinet... It's a W- wait... You find a code"); 
                state=1;
                return;
        }
        if (map == 3 && action == 1)
        {
            if(state < 2)
            {
                dialogBox.writeText("It's locked");
                return;
            }
            if (state == 2)
            {
                dialogBox.writeText("You find the key !");
                state = 3;
                locked = false;
                return;
            }
        }
        // Action par défaut
        if (action==1) dialogBox.writeText("Aaaah a ghost !!! oh... it was just a sheet.");
        else if(action==2) dialogBox.writeText("Nothing here ..."); 
        else if(action==3) dialogBox.writeText("You look behind the cabinet... It's a wall !"); 
        else dialogBox.writeText("Good try but you are too small..."); 
        
    }

    void fireplaceAction(int action)
    {
        if ( map == 4 && action == 1)
        {
            if (state == 3)
            {
                dialogBox.writeText("You light the fire");
                cheminee.GetComponent<ClickOnElement>().actions[0] = "Wait until the fire goes out";
                state = 4;
                return;
            }
            if(state == 4)
            {
                dialogBox.writeText("You find a key among the ashes");
                locked = false;
                return;
            }
        }
        // Action défaut
        if (action == 1) dialogBox.writeText("You can't roasting shamallows for the moment");
        else if (action == 2) dialogBox.writeText("You leave it...");
    }

    void boiteAction(int action){
            if(action==1 && locked == true){
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
        if (map == 4 && action == 2)
        {
            if (state == 0)
            {
                dialogBox.writeText("You find a box, you need a code to open it");
                return;
            }
            else if (state == 1)
            {
                dialogBox.writeText("You find a box, you enter the code but nothing happens...");
                return;
            }
            else if (state == 2)
            {
                dialogBox.writeText("You find a box, you enter the code translated and you find a firelighter !");
                state = 3;
                return;
            }

        }
        // Action par défaut
        if (action==1) dialogBox.writeText("You are looking at an 18th century chest of drawers with a nice trim!");
        else if(action==2) dialogBox.writeText("Nothing here ..."); 
        else if(action==3) dialogBox.writeText("Oh my God ! a cockroach..."); 
        else dialogBox.writeText("Old furnitures does not interest you ?"); 
        
    }

    void updateMap(){
        if(map==1){
            tableau1.SetActive(false);
            bar.SetActive(false);
        } else if(map==2){
            calendrier.SetActive(false);
            chevet1.SetActive(false);
            biblio1.SetActive(false);
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
