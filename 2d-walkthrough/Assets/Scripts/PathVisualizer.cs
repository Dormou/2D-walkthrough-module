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

    void Start()
    {
        movementButtons = GameObject.Find("MovementButtons");
        informationPanel = GameObject.Find("InformationPanel");
        informationPanel.SetActive(false);

        currentPointIndex = 0;
        WayDescription.PathLength = 0;
        WayDescription.CurrentPathPartLength = 0;
        WayDescription.IsPathCompleted = false;
        WayDescription.BeginningTime = DateTime.Now;
        pathfinder = GetComponent<PathFinder>();
       
        // ????
        player = GameObject.Find("Player");

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
            WayDescription.CurrentOptimalPathPartLength = pathfinder.GetPathLength(path);
            WayDescription.PathLength += WayDescription.CurrentPathPartLength;
            WayDescription.CurrentPathPartLength = 0;
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

            // Поворот персонажа по направлению линии!!!

            return true;
        }
        return false;
    }

    public void OnWaypointArrivalEvent(GameObject waypoint)
    {
        if(waypoint == partEndPoint)
        {
            movementButtons.SetActive(false);
            informationPanel.SetActive(true);
            SetPartResultInformation();
            
            if(!DrawNextPathPart())
            {
                Debug.Log("Path ended");
            }
        }
    }

    public void OnExitArrivalEvent(GameObject exit)
    {
        if(exit == partEndPoint)
        {
            WayDescription.IsPathCompleted = true;
            WayDescription.EndingTime = DateTime.Now;
            SceneManager.LoadScene("Result");
        }
    }

    private void SetPartResultInformation()
    {
        GameObject.Find("OptimalPathValue").GetComponent<TextMeshProUGUI>()?.SetText(string.Format("{0:N2}", WayDescription.CurrentOptimalPathPartLength));
        GameObject.Find("ResultPathValue").GetComponent<TextMeshProUGUI>()?.SetText(string.Format("{0:N2}", WayDescription.CurrentPathPartLength));
        var relation = WayDescription.CurrentPathPartLength / WayDescription.CurrentOptimalPathPartLength;
        var ratingManager = GameObject.Find("Rating").GetComponent<RatingManager>();

        if(relation <= 1.3)
        {
            ratingManager.SetRating(3);
        }
        else if(relation <= 1.6)
        {
            ratingManager.SetRating(2);
        }
        else if(relation <= 2)
        {
            ratingManager.SetRating(1);
        }
        else
        {
            ratingManager.SetRating(0);
        }

    }
}
