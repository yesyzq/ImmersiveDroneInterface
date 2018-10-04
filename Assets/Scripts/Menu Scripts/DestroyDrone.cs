namespace ISAACS
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DestroyDrone : MonoBehaviour {

        public GameObject referenceDrone;

        //This is a temporary flag until how multi-drone deletion should be handled.
        public bool multiDroneBehavior = false;

        public void OnClick()
        {
            if (multiDroneBehavior)
            {
                foreach (KeyValuePair<string, Drone> entry in WorldProperties.selectedDrones)
                {
                    Destroy(entry.Value.gameObjectPointer);
                }
            } else
            {
                Destroy(referenceDrone);
            }
        }
    }
}
