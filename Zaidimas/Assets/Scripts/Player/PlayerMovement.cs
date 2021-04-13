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

    public enum PlayerStates
    {
        Walking,
        Mobile
    }

    public PlayerStates State;

    public PhoneUIManager PhoneUIManager;


    // gynimui nenaudoti
    bool dishes;
    public ParticleSystem particles;
    public GameObject soap;
    // gynimui nenaudoti

    //--------------------------------

    private void Start()
    {
        // gynimui
        //dishes = false;

        State = PlayerStates.Walking;

        animator.SetBool("Phone_TakeOut", false);
        animator.SetBool("Phone_PutAway", false);

        // For first defense
        //Instantiate(Resources.Load("prefab") as GameObject, 
        //    new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z), Quaternion.identity);
    }

    void Update()
    {
        HandleInputs();
    }

    void FixedUpdate()
    {
        if (State == PlayerStates.Walking)
        {
            rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape) &&
            animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Player_Alex_Phone_TakeOut" &&
            animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Player_Alex_Phone_PutAway")
        {
            Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);

            switch (State)
            {
                case PlayerStates.Walking:
                    PhoneUIManager.TakeOutPhone();
                    movement = new Vector2(0, 0);
                    animator.SetFloat("Horizontal", 0);
                    animator.SetFloat("Vertical", 0);
                    animator.SetFloat("Speed", 0);

                    animator.SetFloat("Direction_Number", 0);
                    animator.SetBool("Phone_PutAway", false);
                    animator.SetBool("Phone_TakeOut", true);

                    State = PlayerStates.Mobile;
                    break;
                case PlayerStates.Mobile:
                    PhoneUIManager.PutAwayPhone();
                    animator.SetBool("Phone_TakeOut", false);
                    animator.SetBool("Phone_PutAway", true);

                    State = PlayerStates.Walking;
                    break;
            }
        }
        else
        {
            if (State == PlayerStates.Walking)
            {
                HandleMovement();
            }
        }
    }

    private void HandleMovement()
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


    // gynimui
    //public void StartDoingDishes()
    //{
    //    if (dishes)
    //    {
    //        animator.SetBool("Dishes", true);
    //        soap.SetActive(true);
    //    }
    //}

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name == "Dishes obj")
    //    {
    //        dishes = true;
    //    }
    //}

    //public void OnCollisionExit2D(Collision2D collision)
    //{
    //    dishes = false;
    //    soap.SetActive(false);
    //    if (animator.GetBool("Dishes"))
    //    {
    //        particles.gameObject.SetActive(true);
    //        particles.Play();
    //    }
    //    animator.SetBool("Dishes", false);
    //}
}



