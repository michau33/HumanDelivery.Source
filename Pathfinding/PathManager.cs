using UnityEngine;
using System.Collections.Generic;

public class PathManager : MonoBehaviour 
{
    [SerializeField] public List<Waypoint> waypoints = new List<Waypoint>();
    [SerializeField] public Color handleColor = Color.green;
    [SerializeField] public Color lineColor = Color.red;

    void OnDrawGizmos()
    {
 #if UNITY_EDITOR        
        // draw only if there are any waypoints
        if (waypoints.Count > 0) {
            for (int i = 1; i < waypoints.Count; i++)
            {
                Gizmos.color = lineColor;
                Gizmos.DrawLine(waypoints[i].position + transform.position, waypoints[i-1].position + transform.position);
                Gizmos.color = handleColor;
                Gizmos.DrawSphere(waypoints[i].position + transform.position, .4f);
            }
        }
#endif
    }
}


[System.Serializable]
public class Waypoint 
{
    [SerializeField] public Vector3 position;
}
