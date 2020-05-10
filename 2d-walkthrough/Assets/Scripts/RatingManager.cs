using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RatingManager : MonoBehaviour
{
    [SerializeField] private Sprite fullStar;
    [SerializeField] private Sprite emptyStar;
    private Image firstStar;
    private Image secondStar;
    private Image thirdStar;

    void Start()
    {
        firstStar = GameObject.Find("FirstStar").GetComponent<Image>();
        secondStar = GameObject.Find("SecondStar").GetComponent<Image>();
        thirdStar = GameObject.Find("ThirdStar").GetComponent<Image>();

        if(WayDescription.IsPathCompleted)
        {
            GameObject.Find("ResultPathValue").GetComponent<TextMeshProUGUI>()?.SetText(string.Format("{0:N2}", WayDescription.PathLength));
            var time = WayDescription.EndingTime - WayDescription.BeginningTime;
            GameObject.Find("ResultTimeValue").GetComponent<TextMeshProUGUI>()?.SetText("{0} min {1} sec", time.Minutes, time.Seconds);

            var relation = WayDescription.PathLength / WayDescription.OptimalPathLength;

            if(relation <= 1.3)
            {
                SetRating(3);
            }
            else if(relation <= 1.6)
            {
                SetRating(2);
            }
            else if(relation <= 2)
            {
                SetRating(1);
            }
            else
            {
                SetRating(0);
            }
        }
    }

    public void SetRating(int rating)
    {
        firstStar.sprite = rating < 1 ? emptyStar : fullStar;
        secondStar.sprite = rating < 2 ? emptyStar : fullStar;
        thirdStar.sprite = rating < 3 ? emptyStar : fullStar;
    }
}
