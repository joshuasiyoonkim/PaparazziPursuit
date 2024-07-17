using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberFont : MonoBehaviour
{
    public Sprite[] numberSprites;  // Array of sprites for digits 0-9
    public GameObject digitPrefab;  // Prefab with a SpriteRenderer component
    public float spacing = 0.5f;    // Spacing between digits
    public Vector3 offset = new Vector3(0, 5, 0);  // Offset from the camera

    private List<GameObject> digits = new List<GameObject>();

    void LateUpdate()
    {
        // Position the score display relative to the camera
        transform.position = Camera.main.transform.position + offset;
        transform.rotation = Camera.main.transform.rotation;
    }

    public void SetNumber(int number)
    {
        // Clear previous digits
        foreach (var digit in digits)
        {
            Destroy(digit);
        }
        digits.Clear();

        // Convert the number to a string to process each digit
        string numberString = number.ToString();

        // Loop through each digit in the number string
        for (int i = 0; i < numberString.Length; i++)
        {
            // Get the numeric value of the current digit
            int digitValue = int.Parse(numberString[i].ToString());

            // Instantiate a new digit prefab
            GameObject digitObject = Instantiate(digitPrefab, transform);

            // Set the sprite of the digit
            SpriteRenderer spriteRenderer = digitObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = numberSprites[digitValue];

            // Position the digit based on its index
            digitObject.transform.localPosition = new Vector3(i * spacing, 0, 0);

            // Add the digit to the list for future clearing
            digits.Add(digitObject);
        }
    }
}
