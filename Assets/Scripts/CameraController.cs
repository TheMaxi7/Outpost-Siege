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

    public static Vector3 newPosition;
    [SerializeField] private Quaternion newRotation;
    public static Vector3 newZoom;
    [SerializeField] private float zoomSpeed = 800f;

    [SerializeField] private Vector3 dragStartPosition;
    [SerializeField] private Vector3 dragCurrentPosition;
    [SerializeField] private Vector3 rotateStartPosition;
    [SerializeField] private Vector3 rotateCurrentPosition;

    [SerializeField] private AnimationCurve lerpCurve;

    [SerializeField] private float minX, minZ, minY;
    [SerializeField] private float maxX, maxZ, maxY;

    public static Vector3 oldCameraPosition;
    public static float startingCameraPositionX;
    public static float startingCameraPositionZ;
    public static Vector3 oldZoom;
    public static bool isBuilding;
    public static bool isManagingTurret;
    public static Vector3 whereImWatching;

    private void Start()
    {
        isBuilding = false;
        isManagingTurret = false;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        newZoom.y = 10f;
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
        whereImWatching = cameraTransform.forward;
        //Debug.Log(whereImWatching);
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
        if (Input.GetMouseButtonDown(2))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(2))
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

    public static void ZoomBase(Base baseSelected)
    {
        if (!baseSelected.isOccupied)
            isBuilding = true;
        else
            isManagingTurret = true;
        //Debug.Log(baseSelected.transform.position);
        oldCameraPosition = newPosition;
        startingCameraPositionX = oldCameraPosition.x;
        startingCameraPositionZ = oldCameraPosition.z;
        oldZoom = newZoom;
        Vector3 zoomedPosition = new Vector3(baseSelected.transform.position.x, newPosition.y, baseSelected.transform.position.z) - whereImWatching * 6;
        newPosition = new Vector3(zoomedPosition.x, newPosition.y, zoomedPosition.z);
        newZoom.y -= 15.0f;

    }

    public static void UpdateZoomBase(Base baseSelected)
    {
        oldCameraPosition = newPosition;
        Vector3 zoomedPosition = new Vector3(baseSelected.transform.position.x, newPosition.y, baseSelected.transform.position.z) - whereImWatching * 6;
        newPosition = new Vector3(zoomedPosition.x, newPosition.y, zoomedPosition.z);
    }

    public static void UnZoomBase()
    {
        Vector3 unZoomedPosition = new Vector3(startingCameraPositionX, newPosition.y, startingCameraPositionZ);
        newPosition = new Vector3(unZoomedPosition.x, newPosition.y, unZoomedPosition.z);
        newZoom = oldZoom;
        isBuilding = false;
        isManagingTurret = false;
    }
}
