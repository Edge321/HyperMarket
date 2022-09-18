using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChooseItemBehavior : MonoBehaviour
{
    public Color notSelected = Color.white;
    public Color selected = new Color(255, 0, 0, 255);

    public Image[] itemImages;
    public Text[] itemPrices;

    public AudioClip selectNoise;
    public AudioClip deSelectNoise;

    private bool[] itemsChosen;

    private int itemsSelected;

    private ArrayList chosenItems = new ArrayList();
    void Awake()
    {
        itemsChosen = new bool[itemImages.Length];
    }
    void Update()
    {
        itemsSelected = SetItemColorNSelectedItems();
    }
    public void SetItemColor(int itemNumber)
    {
        if (itemsChosen[itemNumber])
		{
            itemsChosen[itemNumber] = false;
            AudioBehavior.Instance.PlaySound(deSelectNoise);
        }
        else
		{
            itemsChosen[itemNumber] = true;
            AudioBehavior.Instance.PlaySound(selectNoise);
        }
            
    }
    public void ResetItemsChosen()
    {
        for (int i = 0; i < itemsChosen.Length; i++)
        {
            itemsChosen[i] = false;
        }
    }
    public int GetItemsSelected()
    {
        return itemsSelected;
    }
    public ArrayList GetChosenItems()
	{
        return chosenItems;
	}
    public void ClearChosenItems()
	{
        chosenItems.Clear();
	}
    /// <summary>
    /// Scans items the player currently chose and adds up the corresponding prices
    /// </summary>
    /// <returns>Total of selected item prices</returns>
    public float GetSelectedItemPrices()
    {
        float totalPrice = 0;
        string priceString;

        for (int i = 0; i < itemsChosen.Length; i++)
        {
            if (itemsChosen[i])
            {
                priceString = itemPrices[i].text;
                priceString = priceString.Substring(1, priceString.Length - 1);
                totalPrice += float.Parse(priceString);
            }
        }
        return totalPrice;
    }
    private int SetItemColorNSelectedItems()
    {
        int totalItemsSelected = 0;
        for (int i = 0; i < itemsChosen.Length; i++)
        {
            if (itemsChosen[i])
            {
                totalItemsSelected++;
                itemImages[i].color = selected;
                chosenItems.Add(i);
            }
            else
			{
                itemImages[i].color = notSelected;
                chosenItems.Remove(i);
            }
        }
        return totalItemsSelected;
    }
}
