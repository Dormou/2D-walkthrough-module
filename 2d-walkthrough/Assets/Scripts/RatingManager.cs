using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RatingManager : MonoBehaviour
{
    [SerializeField] private GameObject rating;
    [SerializeField] private Sprite thumbUp;
    [SerializeField] private Sprite thumbDown;
    private Image image;
    private GameObject imageList;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        imageList = GameObject.Find("ItemsList");
        
        if(imageList != null)
        {
            foreach (var item in WayDescription.PathDescription)
            {
                var ratingItem = Instantiate(rating, imageList.transform);
                var relation = item.PathPartLength / item.OptimalPathPartLength;
                if(relation <= 1.5)
                {
                    ratingItem.GetComponent<Image>().sprite = thumbUp;
                }
                else
                {
                    ratingItem.GetComponent<Image>().sprite = thumbDown;
                }
            }
        }
    }

    public void SetRating()
    {
        if(image != null)
        {
            var relation = WayDescription.CurrentPathPartLength / WayDescription.CurrentOptimalPathPartLength;
            if(relation <= 1.5)
            {
                image.sprite = thumbUp;
            }
            else
            {
                image.sprite = thumbDown;
            }
        }
    }
}
