using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class WayDescription
{
    public static string SpaceLabel { get; set; }
    public static HashSet<string> Waypoints { get; set; }
    public static float CurrentPathPartLength { get; set; }
    public static float PathLength { get; set; }
    public static float CurrentOptimalPathPartLength { get; set; }
    public static float OptimalPathLength { get; set; }
}

