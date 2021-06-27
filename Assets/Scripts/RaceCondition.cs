using UnityEngine;


namespace Race
{
    public abstract class RaceCondition : MonoBehaviour
    {
        public bool isTriggered { get; protected set; }

        public virtual void OnRaceStart()
        {
            
        } 
        public virtual void OnRaceEnd()
        {
            
        }
    }
}