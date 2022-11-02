using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static EnumSpaceScript;
using static DeckScript;
using static GM2;

public class HighlightScript : MonoBehaviour, IPointerClickHandler
{
    List<int> highlights;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        onHighlight += highlight;
        onRemoveHighlight +=removeHighlight;
    }

    void OnDisable()
    {
        onHighlight -= highlight;
        onRemoveHighlight -= removeHighlight;
    }

    void highlight(List<int> highlightSpaces)
    {
        for(int i = 0; i < highlightSpaces.Count; i++)
        {
            GameObject tempObject = Instantiate((GameObject)Resources.Load("Objects/Highlight/circle"), new Vector3(spaces.ElementAt(highlightSpaces.ElementAt(i)).posX + 960, spaces.ElementAt(highlightSpaces.ElementAt(i)).posY + 540, 0), Quaternion.identity);
            tempObject.transform.SetParent(GameObject.Find("HighlightDisplay").transform);
            tempObject.name = "highlight_"+ highlightSpaces.ElementAt(i).ToString();
        }
        highlights = highlightSpaces;
    }

    void removeHighlight(List<int> highlightSpaces)
    {
        for (int i = 0; i < highlightSpaces.Count; i++)
        {
            
            if (gameObject.transform.Find("highlight_"+ highlightSpaces.ElementAt(i).ToString()) != null)
            {

                GameObject tempObject = GameObject.Find("highlight_" + highlightSpaces.ElementAt(i).ToString());
                Destroy(tempObject.gameObject);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        
            removeHighlight(highlights);
        
        highlightSelected = int.Parse(eventData.pointerCurrentRaycast.gameObject.name.Substring(10));
        onHighlightSelected();
    }
}
