using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PDScript : MonoBehaviour
{
    public Image thisImage;
    int player;
    // Start is called before the first frame update
    void Start()
    {

       player = 0;
       thisImage = gameObject.GetComponent<Image>();
       thisImage.sprite = Resources.Load<Sprite>("Sprites/power"+player.ToString());
        
        ///     //Set this in the Inspector
        ///     public Sprite m_Sprite;
        ///
        ///     void Start()
        ///     {
        ///         //Fetch the Image from the GameObject
        ///         
        ///     }
        ///
        ///     void Update()
        ///     {
        ///         //Press space to change the Sprite of the Image
        ///         if (Input.GetKey(KeyCode.Space))
        ///         {
        ///             m_Image.sprite = m_Sprite;
        ///         }
        ///     }
        /// }
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player = (player - 1) % 6;
            if (player == -1)
            {
                player = 5;
            }
            thisImage = gameObject.GetComponent<Image>();
            thisImage.sprite = Resources.Load<Sprite>("Sprites/power" + player.ToString());
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            player = (player + 1) % 6;
            thisImage = gameObject.GetComponent<Image>();
            thisImage.sprite = Resources.Load<Sprite>("Sprites/power" + player.ToString());
        }

    }
}
