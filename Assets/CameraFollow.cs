using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public CinemachineVirtualCamera firstPersonCamera;
    public CinemachineVirtualCamera thirdPersonCamera;
    public bool isFirstPersonSelected;
    public float smoothOffset=0.01f;
    public float speed;
    public Vector3 velocity = Vector3.zero;
    public bool CheckSelectedCamera()
    {
        return isFirstPersonSelected;
    }
    private void FixedUpdate()
    {
        ////float step = speed * Time.deltaTime; // calculate distance to move
        ////transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);

        ////this.transform.position = playerTransform.position;
        //Vector3 desiredPos= Vector3.Lerp(transform.position, playerTransform.position,smoothOffset*Time.fixedDeltaTime);
        //transform.position = desiredPos;

        transform.LookAt(playerTransform);
        if (isFirstPersonSelected)
        {
            firstPersonCamera.Priority = 1;
            thirdPersonCamera.Priority = 0;
            firstPersonCamera.gameObject.SetActive(true);
            
            thirdPersonCamera.gameObject.SetActive(false);

        }
        else
        {
            firstPersonCamera.Priority = 0;
            thirdPersonCamera.Priority = 1;
            firstPersonCamera.gameObject.SetActive(false);
            thirdPersonCamera.gameObject.SetActive(true);

        }
    }
    public void SelectCamera()
    {
        isFirstPersonSelected = !isFirstPersonSelected;
    }
}
