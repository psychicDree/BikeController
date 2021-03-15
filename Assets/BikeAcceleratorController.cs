using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BikeAcceleratorController : MonoBehaviour
{
    public Rigidbody rb;
    public float rotateSpeed;
    public float forwardForce;
    public float sidewaysForce;
    public bool isBikeGrounded;
    public bool isBikeBoosted;
    public Transform bikeHolder;
    //public float boostForce;
    public float gravity = 0;
    public GameObject BikeCamera;

    private float constantSpeed;
    private Touch touch;
    private Vector2 initialPosition;
    private void Start()
    {
        constantSpeed = forwardForce;
    }
    private void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal")*sidewaysForce*100, 0, Input.GetAxis("Vertical")*forwardForce*100);
        //MoveBike(movement);
        if (isBikeGrounded)
        {
            forwardForce = constantSpeed;
        }
    }
    private void FixedUpdate()
    {
        if(isBikeGrounded == true)
        {
            rb.useGravity = false;
            bikeHolder.transform.rotation = Quaternion.identity;
            //BikeCamera.GetComponent<CameraFollow>().isFirstPersonSelected = true;
        }
        else
        {
            rb.useGravity = true;
            
            //isBikeBoosted = false;
        }
        
#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                initialPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                var direction = touch.position - initialPosition;

                var signedDirection = Mathf.Sign(direction.x);

                if (!isBikeGrounded)
                {
                    bikeHolder.transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime, Space.Self);
                    signedDirection = 0;
                }
                else
                {
                    BikeCamera.GetComponent<CameraFollow>().isFirstPersonSelected = true;
                }
                rb.AddForce((Vector3.forward * forwardForce) + (Vector3.right* sidewaysForce * signedDirection)+(Vector3.down*gravity));
                
            }else if(touch.phase == TouchPhase.Ended)
            {
                rb.AddForce((-Vector3.forward * forwardForce/2) + (Vector3.down * gravity));
            }
        }
#endif
    }
#if UNITY_EDITOR
    void MoveBike(Vector3 direction)
    {
        rb.velocity = direction * Time.deltaTime;
    }
#endif
    public void BoostSpeed(float force)
    {
        rb.AddForce(Vector3.up * force ,ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        isBikeGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isBikeGrounded = false;
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
