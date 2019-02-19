using OpenSpaceImplementation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GlobalController.InputManager.CheckSequenceJustEntered("hello")) {
            this.transform.position += new Vector3(0, 0.5f, 0);
        }
    }
}
