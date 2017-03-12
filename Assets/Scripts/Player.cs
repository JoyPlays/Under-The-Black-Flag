using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player {

    // Ship variables
    public static float hullHitpoints;
    public static float sailHitpoints;
    public static int gunPowderCount;
    public static int cannonBallCount;
	public static float shipSpeed;


	// All about guns
	public static bool gunsLoadedLeft;
    public static bool gunsLoadedRight;
	public static bool leftShot;
	public static bool rightShot;

	// City variables
	public static string enteredCity;
    
    // Crew variables
    public static int crewCount;
    public static float crewHitpoints;

}
