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

	[Header("Shop functionality")]
	public GameObject shop;
	public Animator shopAnimator;

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
        SetReloadUI();
    }

    public void SetDeadUI()
    {
        float hpLeft;
        hpLeft = Player.hullHitpoints / maxHitpoints - 1;
        Color c = lowHitpoints.color;
        c.a = Mathf.Abs(hpLeft);
        lowHitpoints.color = c;
    }

    public void SetReloadUI()
    {
        if (!Player.gunsLoadedLeft)
        {
            Color c = leftNotReady.color;
            c.a = 255f;
            leftNotReady.color = c;
        }
        else
        {
            Color c = leftNotReady.color;
            c.a = 0f;
            leftNotReady.color = c;
        }
        if (!Player.gunsLoadedRight)
        {
            Color c = rightNotReady.color;
            c.a = 255f;
            rightNotReady.color = c;
        }
        else
        {
            Color c = rightNotReady.color;
            c.a = 0f;
            leftNotReady.color = c;
        }
    }

	public void DisableShop()
	{
		StartCoroutine("DisableAfterTime");
	}

	 IEnumerator DisableAfterTime()
	{
		shopAnimator.SetBool("Exit", true);
		yield return new WaitForSeconds(2f);
		shopAnimator.SetBool("Exit", false);
		shop.SetActive(false);
	}
}