using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetraButton : MonoBehaviour
{
    [SerializeField] string letra;
    TextMeshProUGUI textField;
    [SerializeField] TextMeshProUGUI letraButton;
    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("TextField").GetComponent<TextMeshProUGUI>();
        letraButton.text = letra;
        levelManager = FindObjectOfType<LevelManager>();

        
    }

    public void InputLetter()
    {
        textField.text = textField.text + letra;
        levelManager.InputLetter(letra);
        
    }


    public void Letra(string letter)
    {
        letra = letter;
        letraButton.text = letra;


    }


}
