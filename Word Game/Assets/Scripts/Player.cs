using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerTime;

    //bomba é o item especial mais forte. Faz o inimigo sair fora da rodada atual.
    int bomba = 0;
    //relógio faz o tempo do player passar mais devagar.
    int relógio = 0;
    //escudo protege o player de qualquer tipo de ataque;
    int escudo = 0;
    //míssil remove uma das letras do inimigo selecionado
    int míssil = 0;
    //mira pode ser usado em qualquer um dos especiais, menos no relógio.
    //todos os items são atirados em um alvo aleatório. Se acoplar a mira ao item, você pode escolher o alvo.
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
