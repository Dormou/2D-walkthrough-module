using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void SetRating(int rating)
    {
        Debug.Log("I'm working! RM");
        firstStar.sprite = rating < 1 ? emptyStar : fullStar;
        secondStar.sprite = rating < 2 ? emptyStar : fullStar;
        thirdStar.sprite = rating < 3 ? emptyStar : fullStar;
    }
}
