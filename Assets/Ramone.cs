using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramone : MonoBehaviour
{
    public Camera camera;
    private Vector3 targetPos;
    private Vector3 velocityVec;

    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody>();
        targetPos = transform.position - rb.velocity.normalized * 10.0f;
        velocityVec = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        var rb = GetComponent<Rigidbody>();

        Vector3 newVelocityVec = rb.velocity.normalized * 10.0f;
        velocityVec = Vector3.Slerp(velocityVec, newVelocityVec, Time.deltaTime * 10.0f);
        Vector3 heightOFfset = new Vector3(0, 5, 0);
        Vector3 newTargetPos = transform.position + heightOFfset - velocityVec;
        targetPos = Vector3.Slerp(targetPos, newTargetPos, Time.deltaTime * 10.0f);

        camera.transform.position = Vector3.Slerp(camera.transform.position, targetPos, Time.deltaTime * 10.0f);
        camera.transform.LookAt(transform);

        Quaternion camRot = camera.transform.rotation;
        camRot.eulerAngles.Set(camRot.eulerAngles.x, 0, camRot.eulerAngles.z);

        if (Input.GetKey(KeyCode.LeftArrow)) {
            rb.AddForce(camRot * new Vector3(-15,0,0));
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            rb.AddForce(camRot * new Vector3(15, 0, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            rb.AddForce(camRot * new Vector3(0, 0, 15));
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            rb.AddForce(camRot * new Vector3(0, 0, -15));
        }
    }
}
