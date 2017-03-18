using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIMessageType
{
	Message, Error
}

public class GameUI : MonoBehaviour
{
	public static GameUI Instance;

	public Text moneyText;

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

	[Header("Game Map")]
	public GameObject map;
	private bool mapOpen = false;

	[Header("Shop functionality")]
	public GameObject shop;
	public Animator shopAnimator;

	[Header("Esc button")]
	public GameObject quitToMenu;
	private bool quitMenuOpened;

	[Header("Message")]
	public Color messageColor;
	public Color errorColor;
	public Image messageImage;
	public Text messageText;
	private bool messageBreak;

	float tempSpeed;
    float maxHitpoints;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
			maxHitpoints = Player.hullHitpoints;
	}

	public static void ShowMessage(UIMessageType type, string message)
	{
		if (!Instance) return;

		Instance.StartCoroutine(Instance.DispalayMessage(type, message));
	}

    // Update is called once per frame
	void Update()
	{
		if (moneyText) moneyText.text = "Money: " + Player.money;

		// Updating UI speed text 
		tempSpeed = Mathf.Round(Player.shipSpeed * 100.0f) / 100.0f;
		shipSpeed.text = tempSpeed.ToString() + " m/s";

		// Updating UI cannonammount & gunpowder text 
		cannonBallCount.text = Player.cannonBallCount.ToString();
		gunPowderCount.text = Player.gunPowderCount.ToString();

		// Updating hitpoint UI
		SetDeadUI();
		SetReloadUI();

		// Open Game Map
		if (Input.GetKeyDown(KeyCode.M))
		{
			OpenMap();
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ReturnToMainMenu();
		}
		
	}

	public void OpenMap()
	{
		if (!mapOpen)
		{
			Debug.Log("Opening Map");
			map.SetActive(true);
			mapOpen = true;
		}
		else
		{ 
			Debug.Log("Closing Map");
			map.SetActive(false);
			mapOpen = false;
		}
	}

    public void SetDeadUI()
    {
        float hpLeft;
        hpLeft = Player.hullHitpoints / maxHitpoints - 1;
        Color c = lowHitpoints.color;
        c.a = Mathf.Abs(hpLeft);
        lowHitpoints.color = c;
    }

	public void ReturnToMainMenu()
	{
		if (!quitMenuOpened)
		{
			quitToMenu.SetActive(true);
			quitMenuOpened = true;
		}
		else
		{
			quitToMenu.SetActive(false);
			quitMenuOpened = false;
		}
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

	IEnumerator DispalayMessage(UIMessageType type, string message)
	{

		if (messageImage.gameObject.activeSelf)
		{
			messageBreak = true;
			while (messageBreak) yield return null;
		}

		messageImage.color = type == UIMessageType.Error ? errorColor : messageColor;
		messageText.text = message;

		Color m = messageText.color;
		m.a = 1;
		messageText.color = m;

		messageImage.gameObject.SetActive(true);

		float timeOut = 5;
		while (timeOut > 0)
		{
			if (messageBreak)
			{
				messageBreak = false;
				yield break;
			}

			m.a = timeOut/5f;
			messageText.color = m;

			timeOut -= Time.deltaTime;
			yield return null;
		}

		messageImage.gameObject.SetActive(false);

		
	}
}