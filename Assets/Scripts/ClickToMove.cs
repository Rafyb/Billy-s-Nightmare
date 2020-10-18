using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour
{

    [Header("Data")]
    public float speed;

    [Header("Components")]
    public Animator animator;
    public SpriteRenderer sprite;
    public MenuContext menu;
    public GameObject UI1;
    public GameObject UI2;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float timeStart;

    void Start()
    {
        timeStart = 0;
    }

    void Update()
    {
        if(timeStart < 2.3f)
        {
            timeStart += Time.deltaTime;
        } else
        {
            if (Input.GetMouseButton(0) && menu.getSelectedObject() == null)
            {
                SetTargetPosition();
            }

            if (isMoving)
            {
                Move();
                animator.SetBool("moving", true);
            }
            else
            {
                animator.SetBool("moving", false);
            }
        }

    }

    public void resetPlayerPosition()
    {
        transform.position = new Vector3(1,2,0);
        UI1.SetActive(false);
        UI2.SetActive(false);
        
    }

    public void resetPlayerAnimation()
    {
        animator.SetTrigger("sleep");
    }


    void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        isMoving = true;
    }

    void Move()
    {

        //Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed);         

        //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        
        
        if(transform.position == targetPosition)
        {
            // Fin du déplacmeent
            isMoving = false;
        } else {
            // Gestion de la rotation
            if(transform.position.x > targetPosition.x){
                sprite.flipX = true;
            } else {
                sprite.flipX = false;
            }
        }

        //transform.position = newPosition;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        isMoving = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isMoving = false;
    }
}
