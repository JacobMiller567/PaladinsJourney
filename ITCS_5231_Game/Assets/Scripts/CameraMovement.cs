using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  [SerializeField] private PlayerMovement player;
  [SerializeField] private Transform playerTransform;
  [SerializeField] private Transform cameraTransform;
  private Vector3 cameraOffset; // distance and height of camera
  //float rotationSpeed = 8f;


    private void Start()
    {
        cameraOffset = new Vector3(0f, 6f, -7f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
      Vector3 cameraEndPosition = player.playerTransform.position + cameraOffset;

      cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraEndPosition, Time.deltaTime);

      /*
      Vector3 cameraDirection = playerTransform.position - new Vector3(cameraTransform.position.x, playerTransform.position.y, cameraTransform.position.z);
      cameraTransform.forward = cameraDirection.normalized;


      float horizontal = Input.GetAxis("Mouse X"); //* mouseSensitivity * Time.deltaTime;
      float vertical = Input.GetAxis("Mouse Y"); //* mouseSensitivity * Time.deltaTime;

      Vector3 directionInput = cameraTransform.forward * horizontal + cameraTransform.right * vertical;

      if (directionInput != Vector3.zero)
      {
        playerTransform.forward = Vector3.Slerp(playerTransform.forward, directionInput.normalized, rotationSpeed * Time.deltaTime);
      }
      */
    }
}
