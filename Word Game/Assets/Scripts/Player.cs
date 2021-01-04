using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerTime;

    //bomba � o item especial mais forte. Faz o inimigo sair fora da rodada atual.
    int bomba = 0;
    //rel�gio faz o tempo do player passar mais devagar.
    int rel�gio = 0;
    //escudo protege o player de qualquer tipo de ataque;
    int escudo = 0;
    //m�ssil remove uma das letras do inimigo selecionado
    int m�ssil = 0;
    //mira pode ser usado em qualquer um dos especiais, menos no rel�gio.
    //todos os items s�o atirados em um alvo aleat�rio. Se acoplar a mira ao item, voc� pode escolher o alvo.
    int mira = 0;
    //algemas imobiliza o inimigo por determinado tempo;
    int algemas = 0;


    public float PlayerTime()
    {

        return playerTime;
    }

    public void ChangePlayerTime(float time)
    {
        playerTime -= time;



    }
}
