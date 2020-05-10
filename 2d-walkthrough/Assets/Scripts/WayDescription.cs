using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public static class WayDescription
{
    // Идентификатор сцены
    public static string SpaceLabel { get; set; }

    // Набор точек для построения маршрута
    public static HashSet<string> Waypoints { get; set; }

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
}

