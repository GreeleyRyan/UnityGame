using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float speed = 50.0f;
    private Rigidbody cannonBallRB;
    // Start is called before the first frame update
    void Start()
    {
        cannonBallRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        cannonBallRB.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Impulse);
    }
}
