﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed*Input.GetAxis("Horizontal") * Time.deltaTime, 0f, Input.GetAxis("Vertical")*Time.deltaTime);
    }
}
