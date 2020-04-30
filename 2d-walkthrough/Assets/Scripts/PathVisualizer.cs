using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathVisualizer : MonoBehaviour
{
    private int currentPointIndex;
    private GameObject partEndPoint;
    private PathFinder pathfinder;

    void Start()
    {
        currentPointIndex = 0;
        pathfinder = GetComponent<PathFinder>();

        if(!DrawNextPathPart())
        {
            Debug.Log("Error while visualize first path part");
        };
    }

    private bool DrawNextPathPart()
    {
        if (currentPointIndex >= pathfinder.optimalPath.Count - 1)
        {
            return false;
        }

        var path = new NavMeshPath();
        var line = GetComponent<LineRenderer>();
        var currentPoint = pathfinder.optimalPath[currentPointIndex];
        partEndPoint = pathfinder.optimalPath[currentPointIndex + 1];

        if(pathfinder.GetPath(path, currentPoint, partEndPoint))
        {
            currentPointIndex++;
            if(line == null)
            {
                line = gameObject.AddComponent<LineRenderer>();
                line.material = new Material(Shader.Find("Sprites/Default"))
                {
                    color = Color.yellow
                };
                line.startWidth = line.endWidth = 0.5f;
                line.startColor = line.endColor = Color.yellow;
            }

            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);

            return true;
        }
        return false;
    }

    public void OnWaypointArrivalEvent(GameObject waypoint)
    {
        if(waypoint == partEndPoint)
        {
            if(!DrawNextPathPart())
            {
                Debug.Log("Path ended");
            }
        }
    }
}
