﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AgentController : MonoBehaviour
{
    private GameObject[] waypoints;
    private GameObject[] enters;
    private GameObject[] exits;

    // Start is called before the first frame update
    void Start()
    {
        if (waypoints == null)
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        }
        if (enters == null)
        {
            enters = GameObject.FindGameObjectsWithTag("Enter");
        }
        if (exits == null)
        {
            exits = GameObject.FindGameObjectsWithTag("Exit");
        }

        var ajacencyList = GetAdjacencyList(waypoints, enters, exits);
        var optimalPath = GetOptimalPathByWaypoints(waypoints, ajacencyList);
        Debug.Log(optimalPath);
        Debug.Log(optimalPath.Count);
    }

    static bool GetPath(NavMeshPath path, Vector3 fromPosition, Vector3 toPosition, int mask)
    {
        path.ClearCorners();

        NavMesh.CalculatePath(fromPosition, toPosition, mask, path);

        if(path.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }

        return true;
    }

    static float GetPathLength(NavMeshPath path)
    {
        float length = 0.0f;

        if(path.status != NavMeshPathStatus.PathInvalid && path.corners.Length > 1)
        {
            for(int i = 1; i < path.corners.Length; i++)
            {
                length += Vector3.Distance(path.corners[i-1], path.corners[i]);
            }
        }

        return length;
    }

    static Dictionary<GameObject, Dictionary<GameObject, float>> GetAdjacencyList(GameObject[] waypoints, GameObject[] enters, GameObject[] exits)
    {
        var path = new NavMeshPath();
        var result = new Dictionary<GameObject, Dictionary<GameObject, float>>();
        float minPath;
        float currentPathLength;
        GameObject nearestPoint = new GameObject();

        foreach (var waypoint in waypoints)
        {
            result.Add(waypoint, new Dictionary<GameObject, float>());
        }
        
        for (int i = 0; i < waypoints.Length; i++)
        {
            minPath = float.PositiveInfinity;
            for (int j = 0; j < enters.Length; j++)
            {
                if(GetPath(path, enters[j].transform.position, waypoints[i].transform.position, NavMesh.AllAreas))
                {
                    currentPathLength = GetPathLength(path);
                    if(currentPathLength < minPath)
                    {
                        minPath = currentPathLength;
                        nearestPoint = enters[j];
                    }
                }
            }
            result[waypoints[i]].Add(nearestPoint, minPath);

            minPath = float.PositiveInfinity;
            for (int j = 0; j < exits.Length; j++)
            {
                if(GetPath(path, waypoints[i].transform.position, exits[j].transform.position, NavMesh.AllAreas))
                {
                    currentPathLength = GetPathLength(path);
                    if(currentPathLength < minPath)
                    {
                        minPath = currentPathLength;
                        nearestPoint = exits[j];
                    }
                }
            }
            result[waypoints[i]].Add(nearestPoint, minPath);

            for (int j = i + 1; j < waypoints.Length; j++)
            {
                if(GetPath(path, waypoints[i].transform.position, waypoints[j].transform.position, NavMesh.AllAreas))
                {
                    currentPathLength = GetPathLength(path);
                    result[waypoints[i]].Add(waypoints[j], currentPathLength);
                    result[waypoints[j]].Add(waypoints[i], currentPathLength);
                }
            }
        }

        return result;
    }

    static List<GameObject> GetOptimalPathByWaypoints(GameObject[] waypoints, Dictionary<GameObject, Dictionary<GameObject, float>> ajacencyList)
    {
        var resultPath = new List<GameObject>();
        var currentPath = new List<GameObject>();
        var resultPathLength = float.PositiveInfinity;
        float currentPathLength;

        foreach (var waypoint in waypoints)
        {
            currentPath.Clear();
            currentPathLength = 0.0f;

            var pair = ajacencyList[waypoint].FirstOrDefault(t => t.Key.tag == "Enter");
            currentPath.Add(pair.Key);
            currentPathLength += pair.Value;
            currentPath.Add(waypoint);

            var currentPoint = waypoint;

            while(waypoints.Where(t => !currentPath.Contains(t)).Count() > 0)
            {
                pair = ajacencyList[currentPoint]
                .Where(t => t.Key.tag == "Waypoint" && !currentPath.Contains(t.Key))
                .OrderBy(t => t.Value)
                .First();

                currentPath.Add(pair.Key);
                currentPathLength += pair.Value;

                currentPoint = pair.Key;
            }

            pair = ajacencyList[waypoint].FirstOrDefault(t => t.Key.tag == "Exit");
            currentPath.Add(pair.Key);
            currentPathLength += pair.Value;

            if(currentPathLength < resultPathLength)
            {
                resultPathLength = currentPathLength;
                resultPath = currentPath;
            }
        }
        Debug.Log(resultPathLength);
        return resultPath;
    }
}
