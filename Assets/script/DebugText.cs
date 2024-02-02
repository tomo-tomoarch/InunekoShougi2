using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public TextMeshPro myText;
    string newText;
    int komaNum;
    int selectedPosNumber;

    void Start()
    {
        UpdateText();
    }

    public void GetKomaNum()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandler�X�N���v�g�̎擾

        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreator �X�N���v�g�̎擾

        selectedPosNumber = masHandler.masNumber;
        //�I�����ꂽ�}�X�̈ʒu�i���o�[���擾

        int rows = cellsCreator.banmen.GetLength(0);
        int cols = cellsCreator.banmen.GetLength(1);
        int row = selectedPosNumber / cols;
        int col = selectedPosNumber % cols;
        //�ʒu�i���o�[�����W�ɕϊ�

        komaNum = cellsCreator.banmen[row, col];
        //�Ֆʏ�̋�������ʂ���
    }
    public void UpdateText()
    {
        GetKomaNum();
        int number = komaNum;
        string numberAsString = "" + number;
        DebugTextUpdate(numberAsString);
    }

    public void DebugTextUpdate(string newText)
    {  
         myText.text = newText;
    }

}
