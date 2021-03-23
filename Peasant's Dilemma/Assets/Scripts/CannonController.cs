using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject cannonBall;
    //public ParticleSystem cannonSmoke;

    private float rotationSpeed = 50.0f;
    private bool isCannonReady;
    private Rigidbody cannonRB;
    // Start is called before the first frame update
    void Start()
    {
        isCannonReady = true;
        cannonRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        float verticalInput = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.right, verticalInput * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isCannonReady)
        {
            FireCannon();
            StartCoroutine(CannonCooldown());
        }
    }

    void FireCannon()
    {
        Instantiate(cannonBall, new Vector3(0f,3.0f,1.9f), cannonBall.transform.rotation);
        isCannonReady = false;
    }

    IEnumerator CannonCooldown()
    {
        yield return new WaitForSeconds(3);
        isCannonReady = true;
    }
}
