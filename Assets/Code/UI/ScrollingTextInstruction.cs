using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class ScrollingTextInstruction : MonoBehaviour
{
    [SerializeField]
    [TextArea] private string[] instructions;

    [SerializeField] private float textSpeed = 0.01f;
    [SerializeField] private TextMeshProUGUI instructionText;
    private int currentDisplayingText = 0;

    private void Start()
    {
        ActivateText();
    }
    public void ActivateText()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        for (int i = 0; i < instructions[currentDisplayingText].Length + 1; i++)
        {
            instructionText.text = instructions[currentDisplayingText].Substring(0,i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
    
}
