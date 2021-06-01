using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour
{
    [Range(0, 1)] 
    public Vector3 speed;
    
    public Vector3 newPosition;
    
    public Transform m_EndPoint;
    
    public float maxSpeed;

    public Vector3 rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        // для большей стабильности, трансформ это вектор,
        // который показывает положение относительно системы координат
         // transform.position += speed * Time.deltaTime;


        // это аналогичный верхнему способ
        transform.Translate(speed * Time.deltaTime);

        //линейная интерполяция
        // transform.position = Vector3.Lerp(Vector3.zero, newPosition, speed); 

        //сферическая интерполяция
        // transform.position = Vector3.Slerp(Vector3.zero, newPosition, speed); 

        // Vector3 distance = transform.position - m_EndPoint.position;
        // float kDistance = distance.magnitude;
        // Vector3 movement = speed * Time.deltaTime * kDistance;
        // movement = Vector3.ClampMagnitude(movement, maxSpeed);
        // transform.Translate(movement);
        
        transform.position = Vector3.MoveTowards(transform.position, m_EndPoint.position, maxSpeed * Time.deltaTime);
    }
}