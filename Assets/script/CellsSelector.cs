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

        if (masLock == false && 0 < komaNum && komaNum< 31)�@//�}�X���b�N���|�����ĂȂ��A���A����̏ꍇ
        {
            masHandler.CangeMasColor(255, 255, 255);
            //�L���[�u���n�C���C�g
            
        }

        if (masTarget == true)
        {
            masHandler.CangeMasColor(255, 122, 122);
        }
    }

    void OnMouseExit()
    {
        MasHandler masHandler = GetComponent<MasHandler>();
        //masHandler�X�N���v�g�̎擾

        if (masLock == false && 0 < komaNum && komaNum < 31)�@//�}�X���b�N���|�����ĂȂ��A���A����̏ꍇ
        {
            masHandler.CangeMasColor(180, 180, 180);
            //�L���[�u�̐F��߂�
        }
        if (masTarget == true)
        {
            masHandler.CangeMasColor(180, 180, 180);
            //�L���[�u�̐F��߂�
        }
    }

    void OnMouseDown()
    {
        CellsCreator cellsCreator = GameObject.FindWithTag("GameController").GetComponent<CellsCreator>();
        cellsCreator.UnLockMas();
        //�S�Ẵ}�X���A�����b�N

        if ( 0 < komaNum && komaNum < 31)//�}�X���b�N���|�����ĂȂ��A���A����̏ꍇ
        {
            

            MasHandler masHandler = GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            cellsCreator.GetOriginalPosition(masHandler.masNumber);
            //masHandler�̃}�X�ԍ���GetOriginalPosition���\�b�h�ɑ�������������̃}�X�ԍ��̔c��

            cellsCreator.GetCurrentKomaNum();
            //���N���b�N�����}�X�ɂ����Ă��铮�������������

            cellsCreator.UnSelectMas();
            //�}�X����x�N���A

            CellsHilighter cellsHilighter = GetComponent<CellsHilighter>();
            cellsHilighter.HilightCells();
            //��������ꏊ���n�C���C�g

            masHandler.CangeMasColor(122, 122, 255);
            //�}�X��Ƀn�C���C�g
            masLock = true;
            //�}�X�����b�N
            cellsCreator.UnlockField();
            //�}�X�^�[�Q�b�g�A�����b�N�t�B�[���h��
        }
    }
}
