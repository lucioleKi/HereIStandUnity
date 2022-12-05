using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSize : MonoBehaviour
{
    float currentX;
    float currentY;
    // Start is called before the first frame update
    void Start()
    {
        currentX = 1536f;
        currentY = 993.75f;
    }

    // Update is called once per frame
    void Update()
    {
        //todo: fix the refresh or put a hard stop when the mouse zooms out too much
        float factor = Mathf.Clamp(gameObject.transform.localScale.x + Input.GetAxis("Mouse ScrollWheel"), 0.25f, 4f);
        if (factor < 1 && currentY<1000 || factor>1&&currentX>=6144&&currentY>=3975)
        {
            return;
        }
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, factor * currentX);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, factor * currentY);
        currentX = factor * currentX;
        currentY = factor * currentY;
    }
}
