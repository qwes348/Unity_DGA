using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{    
    public Transform target;
    public float rotationSpeed = 180f;
    public float zoomSpeed = 120f;
    public float cameraAngleX = 15f;

    float mouseX, mouseY;
    float zoom = -5f;

    // Start is called before the first frame update
    void Start()
    {
        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        zoom = Mathf.Clamp(zoom, -10f, -1f);
        transform.localPosition = new Vector3(0, 0, zoom);

        if(Input.GetMouseButton(0))  // 좌클릭 드래그로 카메라 이동
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        }
        mouseY = Mathf.Clamp(mouseY, -60f, 60f);
        target.localRotation = Quaternion.Euler(mouseY + cameraAngleX, mouseX, 0);
    }

    public void ResetCamera()
    {
        mouseX = mouseY = 0;
        transform.localPosition = new Vector3(0, 0, zoom);
        transform.LookAt(target);
        target.localRotation = Quaternion.Euler(mouseY + cameraAngleX, 0, 0);
    }
}
