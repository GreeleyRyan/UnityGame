using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonController : MonoBehaviour
{
    public GameObject cannonBall;
    public TextMeshProUGUI currentPowerText;
    public TextMeshProUGUI cannonReadyText;
    public TextMeshProUGUI currentAngleText;
    public Transform muzzle;
    //public ParticleSystem cannonSmoke;

    private float minPower = 0f;
    private float maxPower = 100.0f;
    private float currentPower;
    private int currentAngle;
    private bool isCannonReady;
    private Rigidbody cannonBallRB;
    // Start is called before the first frame update
    void Start()
    {
        isCannonReady = true;
        currentPowerText.text = "Power: " + minPower;
        cannonReadyText.text = "Cannon is Ready!";
        currentAngleText.text = "Current Angle: " + currentAngle;
        cannonBallRB = cannonBall.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && isCannonReady)
        {
            CalculateAngle();
            CalculatePower();
        } 
        else if (Input.GetMouseButtonUp(0) && isCannonReady)
        {
            FireCannon((int) currentPower * 500);
            StartCoroutine(CannonCooldown());
        }
    }

    void FireCannon(int power)
    {
        Instantiate(cannonBall, muzzle.transform.position + new Vector3(1f,2f,3f), muzzle.transform.rotation);
        cannonBallRB.AddForce(transform.right * power, ForceMode.Impulse);
        isCannonReady = false;
        cannonReadyText.text = "Hang on!";
    }

    IEnumerator CannonCooldown()
    {
        yield return new WaitForSeconds(3);
        isCannonReady = true;
        cannonReadyText.text = "Cannon is Ready!";
    }

    void CalculateAngle()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        UpdateAngle((int) angle);
    }

    void CalculatePower()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector3.Distance(mousePosition, transform.position);
        UpdatePower((int) distance);
    }

    void UpdatePower(int amount)
    {
        currentPower = Mathf.Clamp(amount, minPower, maxPower);
        currentPowerText.text = "Power: " + currentPower;
    }

    void UpdateAngle(int angle)
    {
        currentAngle = angle;
        currentAngleText.text = "Current Angle: " + currentAngle;
    }
}
