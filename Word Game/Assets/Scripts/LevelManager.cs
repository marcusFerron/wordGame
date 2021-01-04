using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
  
    [SerializeField] LetraButton letraButton;
    [SerializeField] GameObject botõesField;
    [SerializeField] List<Fase> fases;
    Fase faseAtual;
    [SerializeField] LetraButton[] botões;
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI mensagensText;
    [SerializeField] TextMeshProUGUI tempoText;
    [SerializeField] List<Receptáculo> receptáculos;
    string palavra;
    float timeModifier = 1;

   
    int pontos;
    List<string> listaDePalavras;
    List<string> listaDeLetras;
    float tempo = 20;
    float temporizador;
    bool timer = false;

    //essa é a bool que é ativada quando o player usa o escudo;
   [SerializeField] bool usandoEscudo = false;

    // Start is called before the first frame update
    void Start()
    {
        Timer(true);
        temporizador = tempo;
        LimpaTudo();
        mensagensText.gameObject.SetActive(false);
        faseAtual = fases[Random.Range(0, fases.Count)];
        listaDePalavras = faseAtual.palavras;

        for (int i = 0; i < faseAtual.letras.Count; i++)
        {
            var botão = Instantiate(letraButton, transform.position, Quaternion.identity);
            botão.Letra(faseAtual.letras[i]);
            botão.transform.SetParent(botõesField.transform);
        }
    }


    private void Update()
    {
        if (timer)
        {
            temporizador -= Time.deltaTime * timeModifier ;
            tempoText.text = temporizador.ToString("00");
            if (temporizador<=0)
            {
                temporizador = 0;
                StartCoroutine(FimDoTempo());
            }
            
        }
    }


   

    public void ProntoButton()
    {
       

        for (int i = 0; i < listaDePalavras.Count; i++)
        {
            if (textField.text == faseAtual.palavras[i])
            {
                ConfereSpecial();
                int contaPontos = textField.text.Length * 10;
                pontos += contaPontos;
                pointsText.text = pontos.ToString();
                LimpaTudo();
                NextStage();
                return;
            }
            else
            {
                if (i == listaDePalavras.Count - 1)
                {
                    MandaMensagem("palavra inexistente!");
                }
            }
        }               
    }

    public void ConfereSpecial()
    {

        foreach(Receptáculo rec in receptáculos)
        {
            if (rec.Especial()==true)
            {
                if (rec.LetraEspecial() == rec.LetraReceptáculo())
                {
                    pontos += 20;
                }

            }
        }
    }

    public void NextStage()
    {
        //desativando o escudo
        usandoEscudo = false;

        //removendo as letras atuais;
        fases.Remove(faseAtual);
       
 
        if (fases.Count == 0)
        {
            //se não há mais fases, fim do jogo;
            FimDoJogo();
        }
        else
        {
            //se ainda há fases:
            //timer reinicia;
            temporizador = tempo;
            Timer(true);
            //seleciona uma nova "fase";
            faseAtual = fases[Random.Range(0, fases.Count)];
           
            listaDePalavras = faseAtual.palavras;
            listaDeLetras = faseAtual.letras;

            //acessa o método que vai decidir se existirá uma letra especial nessa fase ou não;
            Especial();

            //instancia os botões de letra de acordo com a quantidade de letras da fase atual;
            for (int i = 0; i < listaDeLetras.Count; i++)
            {
               
                var botão = Instantiate(letraButton, transform.position, Quaternion.identity);
                botão.Letra(faseAtual.letras[i]);
                botão.transform.SetParent(botõesField.transform, true);
            }       
        }     
    }


    public void LimpaTudo()
    {
       
        textField.text = null;
        //destuindo os botões de letra;
        botões = FindObjectsOfType<LetraButton>();
        foreach (LetraButton botão in botões)
        {
            Destroy(botão.gameObject);
        }

        //chamando a função que limpa o campo de texto;
        CleanText();

        //removendo os especiais de cada campo
        foreach(Receptáculo rec in receptáculos)
        {
            rec.ChangeEspecial(false, null);

        }
       

    }

    //método que limpa o campo de texto;
    public void CleanText()
    {
        for (int i = 0; i < receptáculos.Count; i++)
        {

            receptáculos[i].Changetext(null);
            receptáculos[i].ChangeOcupada(false);
        }

    }

    //método que decide se vai haver recepáculo especial nessa fase. 
    public void Especial()
    {
        //um random que decide se vai haver ou não. Se der o número 3, vai ter especial na fase;
        int randomSpecial = Random.Range(0, 4);
        if (randomSpecial == 3)
        {
            //decidindo qual das palavras será a base para a letra do especial;
            int randomPalavra = Random.Range(0, faseAtual.palavras.Count);

            //decidino qual letra da palavra selecionada será utilizada como a letra especial;
            int randomLetra = Random.Range(0, faseAtual.palavras[randomPalavra].Length);

            char letraSpecial = faseAtual.palavras[randomPalavra][randomLetra];

            string letraX = letraSpecial.ToString();

            //colocando a letra no recepátulo devido, e enviado o valor da letra para o método ChangeEspecial do referido receptáculo
            receptáculos[randomLetra].ChangeEspecial(true, letraX);
        }

    }



    public void MandaMensagem(string mensagem)
    {
        mensagensText.gameObject.SetActive(true);
        mensagensText.text = mensagem;

        Invoke("LimpaMensagem", 3);
    }
 
    public void LimpaMensagem()
    {
        mensagensText.gameObject.SetActive(false);
    }

    public void FimDoJogo()
    {
        Timer(false);
        Debug.Log("FimDoJogo");
        MandaMensagem("Fim Do Jogo!");
        LimpaTudo();
    }

    public void Timer(bool tempo)
    {
        if (tempo)
        {
            timer = true;
        }
        else
        {
            timer = false;
        }
    }

    IEnumerator FimDoTempo()
    {
        
        Timer(false);
        LimpaTudo();
        MandaMensagem("Fim do Tempo");
        Debug.Log("Fim do Tempo");
        
        yield return new WaitForSecondsRealtime(4);
        NextStage();

    }


    
   public void InputLetter(string letra)
   {
        for (int i=0; i<receptáculos.Count; i++)
        {
            if (receptáculos[i].Ocupada()==false)
            {
                receptáculos[i].Changetext(letra);
                receptáculos[i].ChangeOcupada(true);
                return;
            }
           
           

        }


   }

    //método chamado quando é atingido por algemas;
    [ContextMenu("Algemado")]
     public void Algemado()
    {
        // se está usando escudo, toca animação mostrando o escudo quebrando (escudo só defende de um ataque) e não executa o resto do código.
        if (usandoEscudo)
        {
            MandaMensagem("Seu escudo o defendeu de Algemas");
            //AnimaçãoEscudoQuebrando
            usandoEscudo = false;
            return;

        }


        MandaMensagem("Você foi algemado");
        //cria nova array contendo todos os botões de letra da fase;
        LetraButton[] buttons;
        buttons = FindObjectsOfType<LetraButton>();
        //desativa todos eles;
        foreach(LetraButton but in buttons)
        {
            but.gameObject.SetActive(false);

        }
        //chama uma Corrotina pra esperar certo tempo até ativar os botões de novo;
        //envia pra corrotina o tempo e o array dos botões;
        StartCoroutine(ContagemAlgemado(buttons));

           
    }


    IEnumerator ContagemAlgemado(LetraButton[] letrasButons)
    {
        yield return new WaitForSecondsRealtime(10);
        //depois de passado o tempo, ativa os botões novamente;  
        foreach (LetraButton but in letrasButons)
        {
            but.gameObject.SetActive(true);

        }

    }


    //método chamado quando usa relógio;
    [ContextMenu("UsouRelógio")]
    public void UsouRelógio()
    {
        MandaMensagem("Você usou o Relógio");
        //mudando o timeModifier pra fazer o tempo correr mais devagar;
        timeModifier = 0.5f;
        //depois de 10 segundos com o tempo correndo mais devagar, invoca o método que reseta o time modifier;
        Invoke("ResetTimeModifier", 10);

    }


    //método chamado quando leva um míssil;
    [ContextMenu("TomouMíssil")]
    public void RecebeuMíssil()
    {
        // se está usando escudo, toca animação mostrando o escudo quebrando (escudo só defende de um ataque) e não executa o resto do código.
        if (usandoEscudo)
        {
            MandaMensagem("Seu escudo o defendeu de um Míssil");
            //AnimaçãoEscudoQuebrando
            usandoEscudo = false;
            return;

        }
        MandaMensagem("Você foi atingido por um Míssil");

        //cria nova array contendo todos os botões de letra da fase;
        LetraButton[] buttons;
        buttons = FindObjectsOfType<LetraButton>();

        //seleciona um deles aleatoriamente
        int botãoQueVaiExplodir = Random.Range(0, buttons.Length);

        //desativa o botão selecionado
        buttons[botãoQueVaiExplodir].gameObject.SetActive(false);
    }


    //método que reseta o timeModifier para seu valor inicial (1)
    public void ResetTimeModifier()
    {
        timeModifier = 1;
    }
    
    


    //método que ativa ou desativa o escudo;
    public void SetEscudo(bool escud)
    {
        usandoEscudo = escud;
    }


    [ContextMenu("LevouBomba")]
    public void LevouBomba()
    {
        // se está usando escudo, toca animação mostrando o escudo quebrando (escudo só defende de um ataque) e não executa o resto do código.
        if (usandoEscudo)
        {
            MandaMensagem("Seu escudo o defendeu de uma Bomba");
            //AnimaçãoEscudoQuebrando
            usandoEscudo = false;
            return;

        }
        MandaMensagem("Você foi atingido por uma Bomba!");
        //cria nova array contendo todos os botões de letra da fase;
        LetraButton[] buttons;
        buttons = FindObjectsOfType<LetraButton>();
        //desativa todos eles;
        foreach (LetraButton but in buttons)
        {
            but.gameObject.SetActive(false);

        }
        

    }



}


