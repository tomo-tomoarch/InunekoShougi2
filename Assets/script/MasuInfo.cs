using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasuInfo : MonoBehaviour
{
    public int ColumnNumber { get; private set; }
    public int LowNumber { get; private set; }
    public int MasuNumber { get; private set; }

   

    public void GetCoordinate(int x)//�ʒu�i���o�[�����W�ɕϊ�
    {
        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreator �X�N���v�g�̎擾

        int rows = cellsCreator.banmen.GetLength(0);
        int cols = cellsCreator.banmen.GetLength(1);
        int row = x / cols;
        int col = x % cols;
        //�ʒu�i���o�[�����W�ɕϊ�

        ColumnNumber = row;
        LowNumber = col;

    }
    public void GetMasuNum(int x, int y)//���W���ʒu�i���o�[�ɕϊ�
    {
        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreator �X�N���v�g�̎擾

        MasuNumber = cellsCreator.masu[x, y];
        //���W���ʒu�i���o�[�ɕϊ�
    }
    public int GetKomaNum(int x)//�ʒu�i���o�[����i���o�[�ɕϊ�
    {
        GetCoordinate(x);

        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreator �X�N���v�g�̎擾
        return cellsCreator.banmen[ColumnNumber, LowNumber];
    }
}
