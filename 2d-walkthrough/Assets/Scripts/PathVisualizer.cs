using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PathVisualizer : MonoBehaviour
{
    private int currentPointIndex;
    private GameObject partEndPoint;
    private PathFinder pathfinder;
    private GameObject player;
    private GameObject movementButtons;
    private GameObject informationPanel;
    private RatingManager ratingManager;

    void Start()
    {
        movementButtons = GameObject.Find("MovementButtons");
        informationPanel = GameObject.Find("InformationPanel");
        ratingManager = GameObject.Find("Rating").GetComponent<RatingManager>();
        informationPanel.SetActive(false);

        currentPointIndex = 0;
        WayDescription.PathLength = 0;
        WayDescription.CurrentPathPartLength = 0;
        WayDescription.IsPathCompleted = false;
        WayDescription.BeginningTime = DateTime.Now;
        pathfinder = GetComponent<PathFinder>();
       
        player = GameObject.Find("Player");

        if(!DrawNextPathPart())
        {
            Debug.Log("Error while visualize first path part");
        };
    }

    private bool DrawNextPathPart()
    {
        if (currentPointIndex >= WayDescription.Path.Count - 1)
        {
            return false;
        }

        var path = new NavMeshPath();
        var line = GetComponent<LineRenderer>();
        var currentPoint = WayDescription.Path[currentPointIndex];
        partEndPoint = WayDescription.Path[currentPointIndex + 1];


        WayDescription.PathLength += WayDescription.CurrentPathPartLength;
        WayDescription.CurrentPathPartLength = 0;

        if(pathfinder.GetPath(path, currentPoint, partEndPoint))
        {
            WayDescription.PathPartBeginningTime = DateTime.Now;
            WayDescription.CurrentOptimalPathPartLength = pathfinder.GetPathLength(path);
            
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

            Debug.Log(currentPoint.tag);
            Debug.Log(partEndPoint.tag);
            currentPoint.SetActive(false);
            partEndPoint.SetActive(true);

            return true;
        }
        return false;
    }

    public void OnWaypointArrivalEvent(GameObject waypoint)
    {
        if(waypoint == partEndPoint)
        {
            WayDescription.AddPartDescription(waypoint.GetComponent<WaypointController>().Label);
            movementButtons.SetActive(false);
            informationPanel.SetActive(true);
            ratingManager.SetRating();  
            
            if(!DrawNextPathPart())
            {
                if (waypoint.tag == "Exit")
                {
                    WayDescription.IsPathCompleted = true;
                    WayDescription.EndingTime = DateTime.Now;
                    WayDescription.SaveResult();
                    SceneManager.LoadScene("Result");
                }
                else
                {
                    Debug.Log("Error while path visualizing");
                }           
            }
        }
    }
}
