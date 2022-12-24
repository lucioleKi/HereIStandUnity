using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using static EnumSpaceScript;
using static DeckScript;
using static GM1;
using static GM2;

public class GraphUtils
{
    public static List<int> highlightReformation()
    {
        //todo: make port
        List<int> highlightReformations = new List<int>();
        for (int i = 0; i < spaces.Count(); i++)
        {
            if ((int)religiousInfluence[i] == 1)
            {
                continue;
            }
            for (int j = 0; j < spaces.ElementAt(i).adjacent.Count(); j++)
            {

                if (religiousInfluence[spaces.ElementAt(i).adjacent.ElementAt(j) - 1] == (Religion)1)
                {

                    highlightReformations.Add(i);
                    break;
                }
            }
            if (!highlightReformations.Contains(i))
            {
                for (int j = 0; i < spaces.ElementAt(i).pass.Count(); j++)
                {
                    if (religiousInfluence[spaces.ElementAt(i).pass.ElementAt(j)] == (Religion)1)
                    {
                        highlightReformations.Add(i);
                        break;
                    }
                }
            }
            if (!highlightReformations.Contains(i))
            {
                for (int j = 0; j < activeReformers.Count(); j++)
                {
                    if (activeReformers.ElementAt(j).space == i)
                    {
                        highlightReformations.Add(i);
                        break;
                    }
                }
            }


        }
        return highlightReformations;
    }

    public static List<int> highlightCReformation()
    {
        //todo: make port
        List<int> highlightCReformations = new List<int>();
        for (int i = 0; i < spaces.Count(); i++)
        {
            if ((int)religiousInfluence[i] == 0)
            {
                continue;
            }
            for (int j = 0; j < spaces.ElementAt(i).adjacent.Count(); j++)
            {

                if (religiousInfluence[spaces.ElementAt(i).adjacent.ElementAt(j) - 1] == (Religion)0)
                {

                    highlightCReformations.Add(i);
                    break;
                }
            }
            if (!highlightCReformations.Contains(i))
            {
                for (int j = 0; i < spaces.ElementAt(i).pass.Count(); j++)
                {
                    if (religiousInfluence[spaces.ElementAt(i).pass.ElementAt(j)] == (Religion)0)
                    {
                        highlightCReformations.Add(i);
                        break;
                    }
                }
            }



        }
        return highlightCReformations;
    }

    public static List<int> dietSpaces(bool isProtestant)
    {
        List<int> highlightSpaces = new List<int>();
        for (int i = 0; i < DeckScript.spaces.Count(); i++)
        {

            if ((int)religiousInfluence[i] == 1 && isProtestant || (int)religiousInfluence[i] == 0 && !isProtestant)
            {
                continue;
            }
            for (int j = 0; j < spaces.ElementAt(i).adjacent.Count(); j++)
            {

                if (religiousInfluence[spaces.ElementAt(i).adjacent.ElementAt(j) - 1] == (Religion)0 && !isProtestant && DeckScript.spaces.ElementAt(i).language == (Language)2 || religiousInfluence[spaces.ElementAt(i).adjacent.ElementAt(j) - 1] == (Religion)1 && isProtestant && DeckScript.spaces.ElementAt(i).language == (Language)2)
                {

                    highlightSpaces.Add(i);
                    break;
                }
            }
        }
        return highlightSpaces;
    }

    public static List<int> findTrace(int playerIndex)
    {
        bool[] traceable = new bool[134];
        Array.Clear(traceable, 0, 134);
        List<int> searchIndex = new List<int>();
        List<int> trace = new List<int>();
        switch (playerIndex)
        {
            case 0:
                searchIndex.Add(98);
                break;
            case 1:
                searchIndex.Add(84);
                searchIndex.Add(22);
                break;
            case 2:
                searchIndex.Add(28);
                break;
            case 3:
                searchIndex.Add(42);
                break;
            case 4:
                searchIndex.Add(66);
                break;
            default:
                break;
        }
        while (searchIndex.Count() > 0)
        {

            traceable[searchIndex.ElementAt(0) - 1] = true;
            //UnityEngine.Debug.Log(spaces.ElementAt(searchIndex.ElementAt(0) - 1).name);
            for (int j = 0; j < spaces.ElementAt(searchIndex.ElementAt(0) - 1).adjacent.Count(); j++)
            {

                if (!spacesGM.ElementAt(spaces.ElementAt(searchIndex.ElementAt(0) - 1).adjacent[j] - 1).unrest && !traceable[spaces.ElementAt(searchIndex.ElementAt(0) - 1).adjacent[j] - 1] && spacesGM.ElementAt(searchIndex.ElementAt(0) - 1).controlPower == spacesGM.ElementAt(spaces.ElementAt(searchIndex.ElementAt(0) - 1).adjacent[j] - 1).controlPower)
                {
                    searchIndex.Add(spaces.ElementAt(searchIndex.ElementAt(0) - 1).adjacent[j]);
                }


            }
            searchIndex.RemoveAt(0);

        }

        traceable[97] = false;
        traceable[83] = false;
        traceable[21] = false;
        traceable[27] = false;
        traceable[41] = false;
        traceable[65] = false;
        for (int i = 0; i < traceable.Length; i++)
        {
            if (traceable[i])
            {
                trace.Add(i);
            }
        }
        return trace;
    }

    public static List<int> improveTrace(List<int> trace)
    {
        switch (GM1.player)
        {
            case 0:
                trace.Add(97);
                break;
            case 1:
                trace.Add(83);
                trace.Add(21);
                break;
            case 2:
                trace.Add(27);
                break;
            case 3:
                trace.Add(41);
                break;
            case 4:
                trace.Add(65);
                break;

        }
        return trace;
    }

    public static List<int> findUnfortified(int playerIndex)
    {
        //need to debug more
        List<int> searchIndex = new List<int>();
        List<int> trace = findTrace(playerIndex);
        for (int i = 0; i < 134; i++)
        {
            //removing unrest
            if (spacesGM.ElementAt(i).unrest && spacesGM.ElementAt(i).regular > 0 && spacesGM.ElementAt(i).controlPower == playerIndex)
            {
                bool friendUnits = false;
                bool enemyUnits = false;
                for (int j = 0; j < spaces.ElementAt(i).adjacent.Count(); j++)
                {
                    if (spacesGM.ElementAt(spaces.ElementAt(i).adjacent[j]).regular > 0 && spacesGM.ElementAt(spaces.ElementAt(i).adjacent[j]).controlPower == playerIndex)
                    {
                        friendUnits = true;
                        break;
                    }
                    if (spacesGM.ElementAt(spaces.ElementAt(i).adjacent[j]).regular > 0 && spacesGM.ElementAt(spaces.ElementAt(i).adjacent[j]).controlPower != playerIndex)
                    {
                        enemyUnits = true;
                        break;
                    }
                }
                if (friendUnits && !enemyUnits)
                {
                    searchIndex.Add(i);
                }
                continue;
            }
            //not under unrest
            if (spacesGM.ElementAt(i).controlPower == playerIndex)
            {
                continue;
            }
            //the space is unfortified
            if (spaces.ElementAt(i).spaceType != (SpaceType)0)
            {
                continue;
            }
            //the space is independent or controlled by an enemy power
            if (spacesGM.ElementAt(i).controlPower != 10 && diplomacyState[playerIndex, spacesGM.ElementAt(i).controlPower] != 1)
            {
                continue;
            }
            //the power has an LOC to the space, and regular troops
            bool LOC = false;
            for (int j = 0; j < spaces.ElementAt(i).adjacent.Count(); j++)
            {

                if (trace.Contains(spaces.ElementAt(i).adjacent[j]))// && spacesGM.ElementAt(spaces.ElementAt(i).adjacent[j]).regular>0
                {
                    LOC = true;
                    break;
                }
            }
            if (!LOC)//&& spacesGM.ElementAt(i).regular==0
            {
                //UnityEngine.Debug.Log("filter out "+i.ToString());
                continue;
            }
            //not occupied by land units from another power, unless allies
            if (spacesGM.ElementAt(i).regular > 0 && diplomacyState[playerIndex, spacesGM.ElementAt(i).controlPower] != 2)
            {
                //UnityEngine.Debug.Log("land units " + i.ToString());
                continue;
            }
            searchIndex.Add(i);
        }

        return searchIndex;
    }

    public static HashSet<int> findAllPorts()
    {
        HashSet<int> ports = new HashSet<int>();
        for (int i = 0; i < seas.Count(); i++)
        {
            for (int j = 0; j < seas.ElementAt(i).ports.Count(); j++)
            {
                ports.Add(seas.ElementAt(i).ports[j]);
            }
        }
        return ports;
    }

    public static List<int> findPorts(int playerIndex)
    {

        List<int> trace = findTrace(playerIndex);
        trace = improveTrace(trace);
        HashSet<int> ports = findAllPorts();
        for (int i = trace.Count() - 1; i >= 0; i--)
        {
            if (!ports.Contains(trace[i] + 1))
            {
                trace.RemoveAt(i);

            }
            else
            {
                UnityEngine.Debug.Log(trace[i]);
            }
        }
        return trace;
    }
    public static List<int> findClearFormation(int playerIndex)
    {
        List<int> trace = new List<int>();
        for (int i = 0; i < 134; i++)
        {
            if (DeckScript.spacesGM.ElementAt(i).regularPower == playerIndex && (DeckScript.spacesGM.ElementAt(i).regular > 0 || DeckScript.spacesGM.ElementAt(i).merc > 0) && (!DeckScript.spacesGM.ElementAt(i).sieged))
            {
                trace.Add(i);
            }
        }
        return trace;
    }

    public static List<int> findPassFormation(int playerIndex)
    {
        List<int> trace = new List<int>();
        for (int i = 0; i < 134; i++)
        {
            if (DeckScript.spacesGM.ElementAt(i).regularPower == playerIndex && (DeckScript.spacesGM.ElementAt(i).regular > 0 || DeckScript.spacesGM.ElementAt(i).merc > 0) && spaces.ElementAt(i).pass.Count() > 0)
            {
                trace.Add(i);
            }
        }

        return trace;
    }

    public static List<int> findClearDest(List<int> trace)
    {
        HashSet<int> adjacents = new HashSet<int>();
        for (int i = 0; i < trace.Count(); i++)
        {
            for (int j = 0; j < spaces.ElementAt(trace[i] - 1).adjacent.Count; j++)
            {
                adjacents.Add(spaces.ElementAt(trace[i] - 1).adjacent[j]);

            }

        }
        return adjacents.ToList();

    }

    public static List<int> findPassDest(List<int> trace)
    {
        HashSet<int> adjacents = new HashSet<int>();
        for (int i = 0; i < trace.Count(); i++)
        {
            for (int j = 0; j < spaces.ElementAt(trace[i] - 1).pass.Count; j++)
            {
                adjacents.Add(spaces.ElementAt(trace[i] - 1).pass[j]);

            }

        }
        return adjacents.ToList();
    }

    public static List<int> checkSiege(int playerIndex)
    {
        List<int> siegedSpaces = new List<int>();
        for (int i = 0; i < 134; i++)
        {
            if (DeckScript.spacesGM.ElementAt(i).sieged)
            {
                siegedSpaces.Add(i);
                UnityEngine.Debug.Log("sieged " + i.ToString());
            }
        }
        if (siegedSpaces.Count() == 0) { return siegedSpaces; }
        for(int i=0; i < siegedSpaces.Count(); i++)
        {
            int siegedPower = spacesGM.ElementAt(siegedSpaces.ElementAt(i)).controlPower;
            
            if (GM1.diplomacyState[GM1.player, siegedPower] != 1 && GM1.diplomacyState[siegedPower, GM1.player] != 1)
            {
                siegedSpaces.Remove(siegedSpaces.ElementAt(i));
                continue;
            }
            bool canSiege = false;
            for (int j = 0; j < spaces.ElementAt(siegedSpaces.ElementAt(i)).adjacent.Count; j++)
            {
                if(spacesGM.ElementAt(spaces.ElementAt(siegedSpaces.ElementAt(i)).adjacent[j]).regularPower == playerIndex)
                {
                    UnityEngine.Debug.Log("sieged power" + spaces.ElementAt(siegedSpaces.ElementAt(i)).adjacent[j].ToString());
                    canSiege = true;
                }
            }
            if (!canSiege)
            {
                siegedSpaces.Remove(siegedSpaces.ElementAt(i));
            }
        }
        return siegedSpaces;
    }
}
