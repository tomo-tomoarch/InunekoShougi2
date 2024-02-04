using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsSelector : MonoBehaviour
{

    public bool masLock = false;
    public bool masTarget = false;
    public int komaNum;
    int selectedPosNumber;

    void Start()
    {
       

    }
    

    void OnMouseOver()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandler�X�N���v�g�̎擾

        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo �X�N���v�g�̎擾

        selectedPosNumber = masHandler.masNumber;
        //�I�����ꂽ�}�X�̈ʒu�i���o�[���擾

        komaNum = masuInfo.GetKomaNum(selectedPosNumber);
        //�Ֆʏ�̋�������ʂ���

        CellsHilighter cellsHilighter = GetComponent<CellsHilighter>();
        //masHandler�X�N���v�g�̎擾

        if (masLock == false && 0 < komaNum && komaNum< 31)�@//�}�X���b�N���|�����ĂȂ��A���A����̏ꍇ
        {
            cellsHilighter.HilightWhite();
            //�L���[�u���n�C���C�g
        }
        else
        {
            cellsHilighter.HilightDefault();
        }

        if (masTarget == true)
        {
            cellsHilighter.HilightRed();
        }
    }

    void OnMouseExit()
    {
        CellsHilighter cellsHilighter = GetComponent<CellsHilighter>();
        //masHandler�X�N���v�g�̎擾

        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        //CellsCreator�X�N���v�g�̎擾

        if (masLock == false && 0 < komaNum && komaNum < 31)�@//�}�X���b�N���|�����ĂȂ��A���A����̏ꍇ
        {
            cellsHilighter.HilightDefault();
            //�����}�X�����n�C���C�g
        }
        if (masTarget == true)
        {
            //cellsCreator.HilightKomaExist();
            //�����}�X�����n�C���C�g
        }
    }

    void OnMouseDown()
    {
        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        cellsCreator.UnLockMas();
        //�S�Ẵ}�X���A�����b�N

        if ( 0 < komaNum && komaNum < 31)//�}�X���b�N���|�����ĂȂ��A���A����̏ꍇ
        {
           

            cellsCreator.UnSelectMas();
            //�}�X��S�ăf�t�H���g�J���[�ɃN���A

            CellsHilighter cellsHilighter = GetComponent<CellsHilighter>();
            cellsHilighter.HilightMyField();
            //��������ꏊ���n�C���C�g

            cellsHilighter.HilightBlue();
            //�}�X��Ƀn�C���C�g

            masLock = true;
            //�}�X�����b�N

            cellsCreator.UnlockField();
            //�}�X�^�[�Q�b�g�A�����b�N�t�B�[���h��
        }
    }
}
