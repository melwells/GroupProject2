﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

  public CharacterController controller; //calls on controller attached to Player
  public float speed = 12f;
  public float gravity = -9.81f;
  public float jumpHeight = 3f;

  public AudioSource footsteps;

//checking for whether or not player is touching the ground
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;

  Vector3 originalPos;
  Vector3 velocity; //help with gravity
  bool isGrounded; //are we on the ground

  public int lives;

  void Start()
  {
    originalPos = this.transform.position;
    lives = 3;
  }

    // Update is called once per frame
    void FixedUpdate()
    {
      //checking for if we are on ground.
      isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

      if (isGrounded && velocity.y <0)
      {
        velocity.y = -2f;
        footsteps.Play();
      }


      //controlling movement
      float x = Input.GetAxis("Horizontal"); //x axis
      float z = Input.GetAxis("Vertical"); //z axis

      Vector3 move = transform.right * x + transform.forward * z;

      controller.Move(move * speed * Time.deltaTime);

      //jump
      if(Input.GetButtonDown("Jump") && isGrounded)
      {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
      }

      //fall with gravity
      velocity.y += gravity * Time.deltaTime;

      controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
      //if you get the key, win the game
      if (other.tag == "Macguffin")
      {
        WinGame();
        Debug.Log("did it");
      }

      //if you walk into AI, lose a life and reset pos
      if (other.tag == "Enemy")
      {
        //lose lives

        //reset position
        Respawn();
        Debug.Log("got got");
      }
    }

    void WinGame()
    {
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
      SceneManager.LoadScene("Win");
    }

    void Respawn()
    {
      //transform.localPosition = new Vector3 (26, 2, -1);
      this.transform.position = originalPos;
      lives = lives - 1;

      if (lives <= 0)
      {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Lose");
      }
    }
}
