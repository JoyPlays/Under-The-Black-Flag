using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [Header("Ship game UI variables")]
    public Text shipSpeed;
    public Image lowHitpoints;
    public Image fullHitpoints;
    public Image directionArrow;

    [Header("Ship gun items")]
    public Text cannonBallCount;
    public Text gunPowderCount;

    [Header("Reload indicators")]
    public Image leftReady;
    public Image leftNotReady;
    public Image rightReady;
    public Image rightNotReady;

    float tempSpeed;
    float maxHitpoints;

    void Start()
    {
        maxHitpoints = Player.hullHitpoints;
    }

    // Update is called once per frame
    void Update()
    {
        // Updating UI speed text 
        tempSpeed = Mathf.Round(Player.shipSpeed * 100.0f) / 100.0f;
        shipSpeed.text = tempSpeed.ToString() + " m/s";

        // Updating UI cannonammount & gunpowder text 
        cannonBallCount.text = Player.cannonBallCount.ToString();
        gunPowderCount.text = Player.gunPowderCount.ToString();

        // Updating hitpoint UI
        SetDeadUI();
    }

    public void SetDeadUI()
    {
        Debug.Log("Overall Hitpoints left: " + Player.hullHitpoints);
        float hpLeft;
        hpLeft = Player.hullHitpoints / maxHitpoints - 1;
        Debug.Log("This is HP left amount: " + hpLeft);
        Color c = lowHitpoints.color;
        c.a = Mathf.Abs(hpLeft);
        lowHitpoints.color = c;
    }
}