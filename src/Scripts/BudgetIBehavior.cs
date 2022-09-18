using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BudgetIBehavior : MonoBehaviour
{
    public Text budgetText;
    public Text itemText;

    private ArrayList budgetItems = new ArrayList();

    private float budget;

    private int requiredItems = 0;
    public float GetBudget()
    {
        return budget;
    }
    public int GetRequiredItems()
    {
        return requiredItems;
    }
    public void ResetItemCounter()
    {
        requiredItems = 0;
    }
    public void RandomizeBudget(float[] prices)
    {
        budget = ChoosePricesNItems(prices);
        SetItemText();
        budgetText.text = "Budget: $" + budget.ToString("F2");
    }
    public ArrayList GetBudgetItems()
	{
        return budgetItems;
	}
    public void ClearBudgetItems()
	{
        budgetItems.Clear();
	}
    /// <summary>
    /// Flips a coin for what items are chosen.
    /// </summary>
    /// <param name="prices"></param>
    /// <returns>Total for items chosen</returns>
    private float ChoosePricesNItems(float[] prices)
    {
        float priceTotal = 0;

        for (int i = 0; i < prices.Length; i++)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                requiredItems++;
                priceTotal += prices[i];
                budgetItems.Add(i);
            }
        }
        //Makes sure an item is picked if none are picked
        if (requiredItems <= 0)
        {
            requiredItems = 1;
            int chosenItem = UnityEngine.Random.Range(0, prices.Length);
            priceTotal = prices[chosenItem];
            budgetItems.Add(chosenItem);
        }

        return priceTotal;
    }
    private void SetItemText()
    {
        itemText.text = "Required Items: " + requiredItems;
    }
}
