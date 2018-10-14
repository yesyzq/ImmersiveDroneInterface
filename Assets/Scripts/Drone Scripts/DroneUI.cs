namespace ISAACS
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DroneUI : MonoBehaviour {

        public UnityEngine.UI.Text droneID;
        public UnityEngine.UI.Text droneWaypoints;

        // Update is called once per frame
        void Update() {
            droneID.text = WorldProperties.selectedDrone.id.ToString();
            droneWaypoints.text = WorldProperties.selectedDrone.waypoints.Count.ToString();
    
    }
    }
}