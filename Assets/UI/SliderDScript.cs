using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDScript : MonoBehaviour
{
    public Slider s;
    // Start is called before the first frame update
    void Start()
    {
        s = gameObject.GetComponent<Slider>();
        s.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        GM2.onHighlight += post;
        GM2.onRemoveHighlight += reset;
    }

    void OnDisable()
    {
        GM2.onHighlight -= post;
        GM2.onRemoveHighlight -= reset;
    }

    public void OnValueChanged()
    {
        UnityEngine.Debug.Log("here");
        GameObject.Find("Darken").GetComponent<CanvasGroup>().alpha = s.value;
    }

    public void post(List<int> index)
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<Slider>().value = 1;
    }

    public void reset()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<Slider>().value = 1;
    }
}
