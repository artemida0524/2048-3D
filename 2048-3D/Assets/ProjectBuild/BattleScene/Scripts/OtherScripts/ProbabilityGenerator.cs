using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProbabilityGenerator
{
    public static int GenerateWithProbability(int probability, int probably4, int probably2)
    {
        int chance = Random.Range(0, 100);

        if (chance < probability)
        {
            return probably4;
        }
        else
        {
            return probably2;
        }
    }

}
