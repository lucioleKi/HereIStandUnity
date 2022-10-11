using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static EnumSpaceScript;
using System.Linq;

public class ImportSpaces : MonoBehaviour
{
    // read spaces.csv and produce space objects
    List<SpaceObject> importSpaces()
    {
        List<SpaceObject> spaces = new List<SpaceObject>();
        string[,] adjacentArray = new string[133, 6];
        string[,] passArray = new string[133, 2];
        int row = 0;
        using (var reader = new StreamReader("spaces.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                SpaceObject temp = new SpaceObject();
                temp.name = values[1];
                temp.id = int.Parse(values[0]);
                temp.posX = int.Parse(values[2]);
                temp.posY = int.Parse(values[3]);
                temp.spaceType = (SpaceType)int.Parse(values[4]);
                temp.homePower = (PowerType2)int.Parse(values[5]);
                temp.language = (Language)int.Parse(values[6]);
                temp.matching = values[16];
                for (int j = 7; j <= 12; j++)
                {
                    if (values[j] != null && values[j] != "")
                    {
                        adjacentArray[row, j - 7] = values[j];
                    }
                    else
                    {
                        adjacentArray[row, j - 7] = "";
                    }
                }
                for (int j = 13; j <= 14; j++)
                {
                    if (values[j] != null && values[j] != "")
                    {
                        passArray[row, j - 13] = values[j];
                    }
                    else
                    {
                        passArray[row, j - 13] = "";
                    }
                }
                spaces.Add(temp);
                row++;
            }
            for (int i = 1; i <= spaces.Count; i++)
            {
                List<SpaceObject> tempAdjacent = new List<SpaceObject>();
                List<SpaceObject> tempPass = new List<SpaceObject>();
                for (int j = 0; j < 6; j++)
                {
                    if (adjacentArray[i, j] != "")
                    {
                        tempAdjacent.Add(spaces.Find(k => k.Equals(adjacentArray[i, j])));
                    }
                    else
                    {
                        break;
                    }
                }
                spaces.ElementAt(i).adjacent = tempAdjacent;
                for (int j = 0; j < 2; j++)
                {
                    if (passArray[i, j] != "")
                    {
                        tempPass.Add(spaces.Find(k => k.Equals(passArray[i, j])));
                    }
                    else
                    {
                        break;
                    }
                }
                spaces.ElementAt(i).pass = tempPass;
            }

        }
        return spaces;
    }


}
