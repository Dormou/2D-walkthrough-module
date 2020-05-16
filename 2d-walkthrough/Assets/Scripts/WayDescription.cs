using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using UnityEngine;

[System.Serializable]
public static class WayDescription
{
    // Идентификатор сцены
    public static string SpaceLabel { get; set; }

    // Набор точек для построения маршрута
    public static HashSet<string> Waypoints { get; set; }

    // Построенный маршрут
    public static List<GameObject> Path { get; set; }

    // Построенный маршрут
    public static List<WalkPartModellingResult> PathDescription { get; set; }

    // Длина текущего участка пути
    public static float CurrentPathPartLength { get; set; }

    // Оптимальная длина текущего участка пути
    public static float CurrentOptimalPathPartLength { get; set; }

    // Длина пути
    public static float PathLength { get; set; }
    
    // Оптимальная длина пути
    public static float OptimalPathLength { get; set; }

    // Флаг завершения прохождения
    public static bool IsPathCompleted { get; set; }

    // Время начала прохождения
    public static DateTime BeginningTime { get; set; }

    // Время окончания прохождения
    public static DateTime EndingTime { get; set; }

    // Время начала прохождения текущего участка
    public static DateTime PathPartBeginningTime { get; set; }

    public static void SaveResult()
    {
        var resultDescription = new WalkModellingResult();
        var path = string.Format("{0}//ModellingResults//result({1}).json", Application.dataPath, WayDescription.BeginningTime.ToString("dd-MM-yy HH-mm-ss"));
        try
        {
            var fs = File.Create(path);
            var text = JsonUtility.ToJson(resultDescription, true);            
            var textToWrite = new UTF8Encoding(true).GetBytes(text);
            fs.Write(textToWrite, 0, textToWrite.Length);
        }
        catch (System.Exception ex)
        {
            Debug.Log("Error while save result: " + ex);
        }
        
    }

    public static void AddPartDescription(string currentWaypoint)
    {
        PathDescription.Add(new WalkPartModellingResult(currentWaypoint));
    }
}

