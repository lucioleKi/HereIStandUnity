using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumSpaceScript : MonoBehaviour
{
    public enum SpaceType { Unfortified, Fortress, Capital, Key, Electorate, Home };
    public enum PowerType1 { Major, Minor};
    public enum PowerType2 { Ottoman, Hapsburgs, England, France, Papacy, Protestant, Genoa, HungaryBohemia, Scotland, Venice, Independent};
    public enum Religion { Catholic, Protestant, Other};
    public enum Language { English, French, German, Italian, Spanish, Other};
    public enum CardType { Home, Mandatory, Response, Combat, Event, Diplomacy};


}
