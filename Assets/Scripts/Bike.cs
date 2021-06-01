using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Race
{
    /// <summary>
    /// Data model
    /// </summary>
    [System.Serializable]
    public class BikeParameters
    {
        [Range(0.0f, 10.0f)]
        public float mass;
        
        [Range(0.0f, 100.0f)]
        public float thrust;
        
        [Range(0.0f, 100.0f)]
        public float agility;
        public float maxSpeed;

        public bool afterburner;

        public GameObject engineModel;
        public GameObject hullModel;
    }
    
    /// <summary>
    /// Controller
    /// </summary>
    public class Bike : MonoBehaviour
    {
        [SerializeField] private BikeParameters m_BikeParameters;

        [SerializeField] private BikeViewController m_VisualController;

        private GameObject CreateNewPrefInstance(GameObject sourcePrefab)
        {
            return Instantiate(sourcePrefab);
        }

        [SerializeField] private GameObject m_Prefab;
        private void Update()
        {
            //сделать какое-либо управление вертолетом
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreateNewPrefInstance(m_Prefab);
            }
        }
    }
}

