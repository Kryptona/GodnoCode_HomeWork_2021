using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float maxSpeed;
    public Transform endPoint;
    public Vector3 rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(transform.up, 15 * Time.deltaTime, Space.Self);
        // Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, endPoint.rotation, maxSpeed * Time.deltaTime);
        // transform.rotation *= Quaternion.Euler(rotationSpeed * Time.deltaTime);
        // transform.rotation = newRotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        
        Vector3 localPos = transform.TransformPoint(endPoint.position);
        localPos += new Vector3(0, 18, 0);
        Vector3 globalPos = transform.InverseTransformPoint(localPos);
        endPoint.position = globalPos;
    }
}
