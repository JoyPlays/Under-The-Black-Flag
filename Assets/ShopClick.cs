﻿using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ShopClick : MonoBehaviour
{


    public bool GunShop = false;
    public bool GoodShop = false;

	[Header("Shop objects")]
	public GameObject shop;
	public Animator shopAnimator;

	[Header("Main Canvas")]
	public Animator mainCanvas;

	void OnMouseDown()
	{
		if (GunShop)
		{
			shop.SetActive(true);
			mainCanvas.SetTrigger("FadeOut");

			shopAnimator.SetTrigger("Start");
			shopAnimator.SetTrigger("Guns");
		}

		if (GoodShop)
		{
			shop.SetActive(true);
			mainCanvas.SetTrigger("FadeOut");

			shopAnimator.SetTrigger("Start");
			shopAnimator.SetTrigger("Goods");
		}
	}
}