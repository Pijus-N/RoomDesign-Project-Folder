using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Transform player;

    [SerializeField] private Manager manager;

    float xRotation = 0f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


        player.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown("escape"))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
            manager.EnterPreviewMode();
            manager.EnterSceneView();
        }

    }
}
