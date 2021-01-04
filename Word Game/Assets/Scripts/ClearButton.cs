using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearButton : MonoBehaviour
{
    TextMeshProUGUI textField;
    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("TextField").GetComponent<TextMeshProUGUI>();
    }

    
    public void CleanText()
    {
        textField.text = null;

    }

}
