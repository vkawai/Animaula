﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Tutorial_Controller : MonoBehaviour {

    public Animator board;
    public Animator student;
    public Animator glove;
    public Animator student2;
    public Text text;
    public GameObject board_object;

    public GameObject puff;
    public GameObject item;

    //Nem vou fazer um arquivo de strings :)
    private string[] texts = {"OS ALUNOS ESTÃO DESCONTROLADOS!"
                             ,"UM DOS ALUNOS FICOU VERMELHO!\nELE DEVE ESTAR QUERENDO APRONTAR!"
                             ,"ELE PEGOU SEM PEDIR UM OBJETO DO OUTRO ALUNO, DEVOLVA O OBJETO!"
                             ,"AGORA VAMOS A AULA!"};

    public int text_stage = 0;
    private float time = 0;
    private float animTime = 0;
    private bool isAnimating = true;

	void Start () 
    {
        board.SetInteger("State", 1);
        student2.SetInteger("type", 2);
        ChangeText(0);
	}
	
	void Update () 
    {
        //Só checar se ta rolando animação no quadro.
        animTime += Time.deltaTime;
        if (animTime > board.GetCurrentAnimatorStateInfo(0).length)
        {
            isAnimating = false;
        }


        if (text_stage == 1)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                if (student.GetCurrentAnimatorStateInfo(0).IsName("Corrupted-Dolphin"))
                {
                    ChangeText(0);
                    AnimationBoardChange(true);
                    time = 0;
                }
            }
        }
        else if (text_stage == 3)
        {
            time += Time.deltaTime;
            if (time >= 1)
            {
                ChangeText(1);
                AnimationBoardChange(true);
            }
        }
        else if (text_stage == 4)
        {
            if (glove.GetCurrentAnimatorStateInfo(0).IsName("Repeat_Animation"))
            {
                glove.gameObject.SetActive(false);
                AnimationBoardChange(true);
                ChangeText(2);
            }
        }
	}

    private void ChangeText(int mod)
    {
        text.text = texts[text_stage-mod];
    }

    public void AnimationBoardChange(bool increment)
    {
        if (board.GetCurrentAnimatorStateInfo(0).IsName("Going_Down"))
            board.SetInteger("State", 1);
        else
            board.SetInteger("State", 2);


        if(increment)
            text_stage++;

        isAnimating = true;
        animTime = 0;
    }

    public void ButtonClicked()
    {
        if (isAnimating)
        {
            return;
        }

        switch (text_stage)
        {
            case 0:
                student.SetInteger("status", 1);
                text_stage++;
                break;

            case 2:
                puff.SetActive(true);
                item.SetActive(true);
                student2.SetInteger("status", 3);
                text_stage++;
                break;

            case 4:
                glove.SetInteger("State", 1);
                break;

            case 5:
                SceneManager.LoadScene("Level");
                break;
        }

        AnimationBoardChange(false);
    }
}
