using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class WalkModellingResult
{
    // Идентификатор пространства, в котором генерировался маршрут
    public string SpaceLabel;

    // Время начала прохождения
    public string BeginningTime;

    // Время окончания прохождения
    public string EndingTime;

    // Построенный маршрут
    public List<string> Path;

    // Длина пути, пройденного пользователем
    public float PathLength;
    
    // Оптимальная длина пути
    public float OptimalPathLength;

    // Флаг завершения прохождения
    public bool IsPathCompleted;

    public WalkModellingResult()
    {
        SpaceLabel = WayDescription.SpaceLabel;
        BeginningTime = WayDescription.BeginningTime.ToString();
        EndingTime = WayDescription.EndingTime.ToString();
        Path = WayDescription.PathDescription;
        PathLength = WayDescription.PathLength;
        OptimalPathLength = WayDescription.OptimalPathLength;
        IsPathCompleted = WayDescription.IsPathCompleted;
    }
}
