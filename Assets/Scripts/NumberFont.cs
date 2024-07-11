using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberFont : MonoBehaviour
{
    public Image[] numberImages;
    public Sprite[] numberSprites;

    public void SetNumber(int number)
    {
        // Convert the number to a string to process each digit
        string numberString = number.ToString();

        // Ensure the numberImages array has enough elements to display the number
        if (numberString.Length > numberImages.Length)
        {
            Debug.LogWarning("Not enough image components to display the number!");
            return;
        }

        // Loop through each digit in the number string
        for (int i = 0; i < numberString.Length; i++)
        {
            // Get the numeric value of the current digit
            int digit = int.Parse(numberString[i].ToString());

            // Set the corresponding sprite
            numberImages[i].sprite = numberSprites[digit];

            // Enable the image component (in case it was disabled)
            numberImages[i].enabled = true;
        }

        // Disable any extra image components
        for (int i = numberString.Length; i < numberImages.Length; i++)
        {
            numberImages[i].enabled = false;
        }
    }
}
