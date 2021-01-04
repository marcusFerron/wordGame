using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Receptáculo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI letraReceptáculoText;
    [SerializeField] TextMeshProUGUI letraSpecial;
    [SerializeField] GameObject special;
    bool ocupada = false;
    string letraDoReceptáculo;
    bool especial = false;

    // Start is called before the first frame update

    private void Start()
    {
        special.SetActive(false);
        letraReceptáculoText.text = null;
    }
    public bool Ocupada()
    {
        return ocupada;

    }

    public void ChangeOcupada(bool change)
    {
        ocupada = change;

    }

    public void Changetext(string txt)
    {
        letraReceptáculoText.text = txt;
        letraDoReceptáculo = txt;
    }

    public string LetraReceptáculo()
    {
        return letraDoReceptáculo;
    }

  
    public bool Especial()
    {
        return especial;
    }

    public void ChangeEspecial(bool spc, string lettra)
    {
        if (spc == true)
        {
            special.SetActive(true);
            especial = spc;
            letraSpecial.text = lettra;
        }

        else
        {
            especial = spc;
            special.SetActive(false);
            letraSpecial.text = null;
        }

    }
    public string LetraEspecial()
    {
        return letraSpecial.text;
    }
        



}
