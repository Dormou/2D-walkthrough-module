using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathVisualizer : MonoBehaviour
{
    private int currentPointIndex;
    private PathFinder pathfinder;

    void Start()
    {
        currentPointIndex = 0;
        pathfinder = GetComponent<PathFinder>();
        if(!DrawNextPathPart(currentPointIndex))
        {
            Debug.Log("Error while visualize first path part");
        };
    }

    private bool DrawNextPathPart(int currentPointIndex)
    {
        var path = new NavMeshPath();
        var line = GetComponent<LineRenderer>();

        if (currentPointIndex >= pathfinder.optimalPath.Count - 1)
        {
            return false;
        }

        var currentPoint = pathfinder.optimalPath[currentPointIndex];
        var nextPoint = pathfinder.optimalPath[currentPointIndex + 1];

        if(pathfinder.GetPath(path, currentPoint, nextPoint))
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
}
