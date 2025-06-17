using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IndicatorFactory
{
    public static GameObject LoadIndicator(eINDICATOR type)
    {
        return type switch
        {
            eINDICATOR.CIRCLE => Resources.Load<GameObject>("Prefabs/Indicator/Indicator_Rectangle"),
            eINDICATOR.RECTANGLE => Resources.Load<GameObject>("Prefabs/Indicator/Indicator_Rectangle"),
            eINDICATOR.FAN => Resources.Load<GameObject>("Prefabs/Indicator/Indicator_Fan"),
            _ => null
        };
    }        
}
