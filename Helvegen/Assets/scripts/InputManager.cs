using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public int selectedListIndex;

    public float panSpeed;
    public float rotateSpeed;
    public float rotateAmount;

    private Quaternion rotation;

    private float minHeight = 10f;
    private float maxHeight = 100f;

    public GameObject selectedObj;
   
    bool isDragging;
    
    
    private ObjectInfo selectedInfo;


    // Start is called before the first frame update
    void Start()
    {
        rotation = Camera.main.transform.rotation;

    }


    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = rotation;
        }

         }

    void MoveCamera()
    {
        float moveX = Camera.main.transform.position.x;
        float moveY = Camera.main.transform.position.y;
        float moveZ  = Camera.main.transform.position.z;

        float xPos = Input.mousePosition.x;
        float yPos = Input.mousePosition.y;

        if (Input.GetKey(KeyCode.A))
        {
            moveX -= panSpeed;
        } else
        if (Input.GetKey(KeyCode.D))
        {
            moveX += panSpeed;
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            moveZ += panSpeed;
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            moveZ -= panSpeed;
        }

        moveY += Input.GetAxis("Mouse ScrollWheel") * (panSpeed * 20);

        moveY = Mathf.Clamp(moveY, minHeight, maxHeight);

        Vector3 newPos = new Vector3(moveX, moveY, moveZ);

        Camera.main.transform.position = newPos;
    }


    void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        if (Input.GetMouseButton(2))
        {
            destination.x -= Input.GetAxis("Mouse Y") * rotateAmount;
            destination.y += Input.GetAxis("Mouse X") * rotateAmount;

        }
        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * rotateSpeed);

        }
        

        
    }

   
}
