using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float moveSpeed = 5f;
    //public Rigidbody theRB;
    public float jumpForce = 4f;
    public bool isGrounded;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravScale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        //theRB = GetComponent<Rigidbody>();
        //controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.Translate(theRB.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, theRB.velocity.y, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime), Space.Self);

        //if (Input.GetButtonDown("Jump"))
        //{
        //    theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z) * Time.deltaTime;
        //}

        //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);


        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }


        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }

}
