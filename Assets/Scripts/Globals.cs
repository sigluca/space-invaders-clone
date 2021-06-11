using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Globals 
{
    public const int HORDE_GOING_RIGHT = 1;
    public const int HORDE_GOING_LEFT = -1;
    
    public static int hordeDirection = HORDE_GOING_RIGHT;
    public static int score = 0;
    public static int highScore = 0;
    public static int lives = 3; 

    public static int barrierMaxHealth = 5;
}
