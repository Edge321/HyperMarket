using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemsBehavior : MonoBehaviour
{
	public GameObject[] items;

	public Sprite[] itemSprites;

	public Text time;
	public Text score;

	public CanvasBehavior canvasBehavior;

	public AudioClip submitSound;

	public int timeNumber = 60;

	public bool GameActive { get; set; } = false;

	private float[] prices;
	private float lowRandom = 3.0f;
	private float highRandom = 8.0f;

	private BudgetIBehavior budgetBehavior;
	private ChooseItemBehavior choiceBehavior;

	private int tempTotalTimeNumber;
	private int totalTimeNumber;

	private int minTotalTime = 5;

	private int scoreNumber = 0;
	private int highScoreNumber = 0;
	private void Start()
	{
		prices = new float[items.Length];
		totalTimeNumber = timeNumber;
		tempTotalTimeNumber = timeNumber;

		budgetBehavior = GetComponent<BudgetIBehavior>();
		choiceBehavior = GetComponent<ChooseItemBehavior>();

		RandomizePrices();
		RandomizeItems();
		RandomizeBudget();
	}
	private void Update()
	{
		if (tempTotalTimeNumber <= 0)
			GameOver();
		SetScoreText();

		if (GameActive)
		{
			GameActive = false;
			InvokeRepeating("DecreaseTimer", 0, 1.0f);
		}
	}
	public void OnSubmit()
	{
		AudioBehavior.Instance.PlaySound(submitSound);
		if (CheckChoices())
		{
			RefreshItemsNPrices();
			IncreaseScore();
			ResetNDecreaseTimer();
		}
		else
		{
			GameOver();
		}
	}
	private void DecreaseTimer()
    {
		tempTotalTimeNumber--;

		time.text = "Time: " + tempTotalTimeNumber;
    }
	/// <summary>
	/// Decreases time as player progresses
	/// </summary>
	private void ResetNDecreaseTimer()
    {
		totalTimeNumber = Mathf.Clamp(totalTimeNumber - 1, minTotalTime, timeNumber); ;
		tempTotalTimeNumber = totalTimeNumber;
    }
	/// <summary>
	/// <list type="bullet">
	/// <item>Checks if the total from the items the player chose is lower than their budget</item>
	/// <item>Checks if the total items chosen are the required items</item>
	/// <item>Checks if the items chosen are what the budget required</item>
	/// </list>
	/// </summary>
	/// <returns></returns>
	private bool CheckChoices()
    {
		float budget = budgetBehavior.GetBudget();
		int requiredItems = budgetBehavior.GetRequiredItems();
		ArrayList budgetItemsArray = budgetBehavior.GetBudgetItems();

		float selectedPriceTotal = choiceBehavior.GetSelectedItemPrices();
		int chosenItems = choiceBehavior.GetItemsSelected();
		ArrayList chosenItemsArray = choiceBehavior.GetChosenItems();

		if (selectedPriceTotal > budget)
        {
			return false;
        }
		else if (requiredItems != chosenItems)
        {
			return false;
        }
		
		for (int i = 0; i < budgetItemsArray.Count; i++)
		{
			if (!chosenItemsArray.Contains(budgetItemsArray[i]))
				return false;
		}

		return true;
    }
	private void IncreaseScore()
    {
		scoreNumber++;
    }
	private void SetScoreText()
    {
		score.text = "Score: " + scoreNumber +
					"\nHighscore: " + highScoreNumber;
    }
	private void RandomizePrices()
    {
		Text text;

		for (int i = 0; i < items.Length; i++)
		{
			prices[i] = (float) Math.Round(GetRandomPrice(), 2);
			text = items[i].GetComponentInChildren<Text>();
			text.text = "$" + prices[i].ToString("F2");
		}
	}
	private void RandomizeBudget()
	{
		budgetBehavior.RandomizeBudget(prices);
	}
	/// <summary>
	/// Chooses random item sprites to put in the game
	/// </summary>
	private void RandomizeItems()
    {
		for (int i = 0; i < items.Length; i++)
        {
			items[i].GetComponent<Image>().sprite = itemSprites[UnityEngine.Random.Range(0, itemSprites.Length)];
		}
    }
	private float GetRandomPrice()
	{
		return UnityEngine.Random.Range(lowRandom, highRandom);
	}
	/// <summary>
	/// Sets highscore if a highscore was made.
	/// Resets score, items, prices.
	/// </summary>
	private void GameOver()
    {
		if (scoreNumber >= highScoreNumber)
			highScoreNumber = scoreNumber;

		scoreNumber = 0;
		totalTimeNumber = timeNumber;
		tempTotalTimeNumber = timeNumber;

		CancelInvoke();

		RefreshItemsNPrices();

		canvasBehavior.GameOver();
	}
	private void RefreshItemsNPrices()
	{
		budgetBehavior.ClearBudgetItems();
		choiceBehavior.ClearChosenItems();
		budgetBehavior.ResetItemCounter();
		choiceBehavior.ResetItemsChosen();
		RandomizeItems();
		RandomizePrices();
		RandomizeBudget();
	}
}
