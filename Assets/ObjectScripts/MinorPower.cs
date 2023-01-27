using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnumSpaceScript;
using static DeckScript;

public class MinorPower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activate(int major, int minor)
    {

        GM2.resetMap();
        GM1.diplomacyState[major, minor] = 2;
        List<int> atWar = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            if (GM1.diplomacyState[i, minor] == 1)
            {
                atWar.Add(i);
                GM1.diplomacyState[i, minor] = 0;
            }
        }
        GM2.onChangeDip();
        //place square&hex control markers
        for (int i = 0; i < 134; i++)
        {
            if (spaces.ElementAt(i).homePower == (PowerType2)minor)
            {
                GM2.onRemoveSpace(i);
                spacesGM.ElementAt(i).controlPower = major;
                if (spaces.ElementAt(i).spaceType == (SpaceType)0 || spaces.ElementAt(i).spaceType == (SpaceType)1)
                {
                    if (GM1.religiousInfluence[i] == 0)
                    {
                        spacesGM.ElementAt(i).controlMarker = 1;
                    }
                    else
                    {
                        spacesGM.ElementAt(i).controlMarker = 2;
                    }

                }
                else
                {
                    if (GM1.religiousInfluence[i] == 0)
                    {
                        spacesGM.ElementAt(i).controlMarker = 3;
                    }
                    else
                    {
                        spacesGM.ElementAt(i).controlMarker = 4;
                    }
                    GM1.cardTracks[major]++;
                    GM1.updateVP();
                    GM2.onVP();
                }
                GM2.onAddSpace(i, major, spacesGM.ElementAt(i).controlMarker);
            }
        }

    }

    public void deactivate(int major, int minor)
    {
        GM2.resetMap();
        GM1.diplomacyState[major, minor] = 0;
        for (int i = 0; i < 134; i++)
        {
            if (spaces.ElementAt(i).homePower == (PowerType2)minor)
            {
                GM2.onRemoveSpace(i);
                spacesGM.ElementAt(i).controlPower = minor;
                if (spaces.ElementAt(i).spaceType == (SpaceType)0 || spaces.ElementAt(i).spaceType == (SpaceType)1)
                {
                    if (GM1.religiousInfluence[i] == 0)
                    {
                        spacesGM.ElementAt(i).controlMarker = 1;
                    }
                    else
                    {
                        spacesGM.ElementAt(i).controlMarker = 2;
                    }

                }
                else
                {
                    if (GM1.religiousInfluence[i] == 0)
                    {
                        spacesGM.ElementAt(i).controlMarker = 3;
                    }
                    else
                    {
                        spacesGM.ElementAt(i).controlMarker = 4;
                    }
                    GM1.cardTracks[major]--;
                    GM1.updateVP();
                    GM2.onVP();
                }
                GM2.onAddSpace(i, minor, spacesGM.ElementAt(i).controlMarker);
            }
        }
    }
}
