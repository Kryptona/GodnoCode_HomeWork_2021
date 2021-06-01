using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    public class Car : MonoBehaviour
    {

        [SerializeField] 
        [Range(0.0f, 100f)]
        private float m_Mass;
        
        [SerializeField] 
        [Multiline]
        private string m_ModelName;       
        [SerializeField] private float m_EnginePower;
        
        [Range(0, 10)]
        [SerializeField] private int m_NumSteeringWheels;
        
        [HideInInspector]
        [SerializeField] private Color m_Color;
        
        [SerializeField] private Vector3 m_Pos;
        
        [SerializeField] private Quaternion m_Rotation;
        
        [SerializeField] private Transform m_WheelA;
        [SerializeField] private Transform m_WheelB;
        
        // [SerializeField] private CustomBehavior m_Custom;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

