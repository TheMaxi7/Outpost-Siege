using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementTime;
    [SerializeField] private float rotationAmount;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 zoomAmount;

    [SerializeField] private Vector3 newPosition;
    [SerializeField] private Quaternion newRotation;
    [SerializeField] private Vector3 newZoom;
    [SerializeField] private float zoomSpeed = 800f;

    [SerializeField] private Vector3 dragStartPosition;
    [SerializeField] private Vector3 dragCurrentPosition;
    [SerializeField] private Vector3 rotateStartPosition;
    [SerializeField] private Vector3 rotateCurrentPosition;

    [SerializeField] private AnimationCurve lerpCurve;

    [SerializeField] private float minX, minZ, minY;
    [SerializeField] private float maxX, maxZ, maxY;


    private void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleMouseInput();

        float clampedY = Mathf.Clamp(newZoom.y, minY, maxY);
        float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
        float clampedZ = Mathf.Clamp(newPosition.z, minZ, maxZ);
        newPosition = new Vector3(clampedX, clampedY, clampedZ);
        newZoom.y = clampedY;

    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, lerpCurve.Evaluate(Time.deltaTime * movementSpeed));
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, lerpCurve.Evaluate(Time.deltaTime * rotationAmount));
        
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        newZoom.y -= scroll * zoomSpeed * Time.deltaTime;
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, lerpCurve.Evaluate(Time.deltaTime * movementSpeed));


    }
}
