using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ScenarioObject : ScriptableObject
{
    public new string name;
    public int turnStart;
    public int turnEnd;
    public int phaseStart;
    public int[] VPs;
    public int protestantSpaces;
    public int englishSpaces;
    //todo diplomacy status
    public CitySetup[] cities;
}
