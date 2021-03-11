using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement speed")]
    public float movementSpeed = 5f;

    [Header("Rigid body")]
    public Rigidbody2D rigidbody;
    
    private Vector2 movement;

    [Header("Animators")]
    public Animator animator;

    //--------------------------------

    private void Start()
    {
        //Instantiate(Resources.Load("prefab") as GameObject, 
        //    new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), Quaternion.identity);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.sqrMagnitude > 0.0001f)
        {
            var info = animator.GetCurrentAnimatorClipInfo(0);

            switch (info[0].clip.name)
            {
                case "Player_Alex_Run_Front":
                    animator.SetFloat("Direction_Number", 0);
                    break;
                case "Player_Alex_Run_Right":
                    animator.SetFloat("Direction_Number", 1);
                    break;
                case "Player_Alex_Run_Back":
                    animator.SetFloat("Direction_Number", 2);
                    break;
                case "Player_Alex_Run_Left":
                    animator.SetFloat("Direction_Number", 3);
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}



