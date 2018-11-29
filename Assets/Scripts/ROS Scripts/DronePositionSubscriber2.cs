using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.interface_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using ISAACS;
using System.IO;
using UnityEditor;

public class DronePositionSubscriber2 : ROSBridgeSubscriber
{

    public new static string GetMessageTopic()
    {
        return "drone2/state/position_velocity";
        // return "/tf";

    }

    public new static string GetMessageType()
    {
        return "crazyflie_msgs/PositionVelocityStateStamped";
        // return "tf2_msgs/TFMessage";
    }

    public new static ROSBridgeMsg ParseMessage(JSONNode msg)
    {
        return new DronePositionMsg(msg);
    }

    public new static void CallBack(ROSBridgeMsg msg)
    {
        // Debug.Log("Drone Position Callback");
        DronePositionMsg poseMsg = (DronePositionMsg)msg;
        // Debug.Log("pose msg");
        // Debug.Log(poseMsg);
        string numID = "C";
        Drone currentDrone = null;
        if (!WorldProperties.dronesDict.ContainsKey(numID))
        {
            // Debug.Log("Made drone with id: " + numID);
            GameObject world = GameObject.FindWithTag("World");
            currentDrone = new Drone(WorldProperties.RosSpaceToWorldSpace(poseMsg._x, poseMsg._y, poseMsg._z), numID);
            WorldProperties.dronesDict[numID] = currentDrone;
        }

        else
        {
            // Debug.Log("Update drone" + numID);
            currentDrone = WorldProperties.dronesDict[numID];
        }

        currentDrone.gameObjectPointer.transform.localPosition
            = WorldProperties.RosSpaceToWorldSpace(poseMsg._x, poseMsg._y, poseMsg._z);
    }

    //WriteData will write the location of the closest Obstacle passed to it to a text file
    //[MenuItem("Tools/Write file")]
    /*static void WriteData()
    {
        //Find the closest obstacle from the selected drone and its distance

        WorldProperties.FindClosestObstacleAndDist(); //if no obstacles exist, closestDist is -1, and closestObstacle is null       
        string path = "Assets/Results/obstacles.txt";
        Debug.Log("hi");
        if (WorldProperties.closestObstacle != null)
        {
           // Debug.Log(closestObstacle.name + ": " + closestDist);

            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(WorldProperties.closestObstacle.name + ": " + WorldProperties.closestDist);
            writer.Close();

            //Re-import the file to update the reference in the editor
            AssetDatabase.ImportAsset(path);
            

            //Print the text from the file
            //Debug.Log("Text " + WorldProperties.asset.text);
        }
    }
        */
    /// <summary>
    /// Finds and keeps track of all the closest obstacle distancecs as strings in a list 
    /// </summary>
    static void SaveData()
    {
        //WorldProperties.FindClosestObstacleAndDist();
        WorldProperties.obstacleDistsToPrint.Add(WorldProperties.closestDist.ToString());
    }



}