using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsCreator : MonoBehaviour
{
   
    const int n = 15;
    //�}�X�̐��@
    public GameObject originObject;
    //�}�X�R���|�[�l���g
    public GameObject kaku;
    //�p�R���|�[�l���g
    public GameObject koma;
    //��R���|�[�l���g

    public int CurrentMasuNum;
    //��̌��݈ʒu�̃}�X�̐���

    public int CurrentKomaNum;
    //���݃Z���N�g����Ă����̐���

    public int DestinationMasNum;
    //�������̃}�X�ԍ�

    public int DestinationKomaNum;
    //�������̃}�X�ɂ����ԍ�

    public List<int> MovableAreaNums = new List<int>();
    //��������̃}�X


    GameObject clickedGameObject;
    //�Z���N�g�����^�[�Q�b�g�}�X�̃Q�[���I�u�W�F�N�g�i�[�p

    public int[,] masu = new int[n, n];
    //masu�񎟌��z��̏�����

    List<int> onetoTwotwofive = new List<int>();
    //movablearea�v�Z�p�񎟌��z��̏�����

    public int[,] banmen = new int[n, n]{
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,35,34,33,32,31,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,1,2,3,4,5,0,11,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
    //��̏����z�u2�����z��̐錾

    List<GameObject> masuGameObject = new List<GameObject>();
    //�}�X���Q�[���I�u�W�F�N�g�Ƃ��Ċi�[���郊�X�g�̐錾


    // 0 = empty, 1 = Gyoku fuse, 2 = Kin fuse, 3 = hisha fuse, 4 = kaku fuse, 5 =keima fuse,
    //            6 = Gyoku Omote, 7= Kin Omote, 8 = Hisha Omote, 9= Kaku Omote, 10= Keima Omote,
    //            11= Matatabi,                  13= Hisha Nari, 14= Kaku Nari, 15= Keima Nari
    //                            17= Kin Uti  , 18= Hisha Uti,  19= Kaku Uti,  20 =Keima Uti
    //                                           23= Hisha Uti Nari, 24=Kaku Uti Nari, 25 Keima Uti Nari,
    //                                          28= Hisha bare, 29= kaku bare,  30= keima bare,
    // 0 = empty, 31 = Gyoku fuse, 32 = Kin fuse, 33 = hisha fuse, 34 = kaku fuse, 35 =keima fuse,
    //            36 = Gyoku Omote, 37= Kin Omote, 38 = Hisha Omote, 39= Kaku Omote, 40= Keima Omote,
    //            41= Hone,                       43= Hisha Nari, 44= Kaku Nari, 45= Keima Nari            
    //                            47= Kin Uti  , 48= Hisha Uti,  49= Kaku Uti,  50 =Keima Uti
    //                                           53= Hisha Uti Nari, 54=Kaku Uti Nari, 55 Keima Uti Nari,
    //                                          58= Hisha bare, 59= kaku bare,  60= keima bare,


    void Start()
    {
        DrawBanmen();
        UnSelectMas();

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectNewMasuOrDestination();
        }
          
    }

    void DrawBanmen() //�Ֆʂ�`��
    {
        int k;


        for (k = 0; k < masu.GetLength(1); k++)
        {
            int i;

            for (i = 0; i < masu.GetLength(1); i++)
            {
                masu[k, i] = (k * masu.GetLength(1)) + i;
                //�񎟌��z��}�X�̃i���o�����O1-224

                //Debug.Log(masu[j, i]);�@//�f�o�b�O�p

                GameObject obj = (GameObject)Instantiate(originObject, new Vector3(k - (n / 2), 0, i - (n / 2)), Quaternion.identity);
                //�}�X�����_���S�ɕ`�悷��

                MasHandler masHandler = obj.GetComponent<MasHandler>();
                //�`�悵���}�X��MasHandler�X�N���v�g�̎擾

                masHandler.MasNumber(masu[k, i]);
                //MasNumber���\�b�h�̎��s1-224�̃i���o�����O���}�X���̂ɕt����

                masuGameObject.Add(obj);
                onetoTwotwofive.Add(masu[k, i]);
                //���X�g�Ƀ}�X���̂������}�X�Q�[���I�u�W�F�N�g�̃��X�g�����

            }
        }
    }

    void ResetBanmen()�@//�Ֆʂ��X�V����
    {
        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��
            DebugText debugText = obj.GetComponent<DebugText>();

            debugText.UpdateText();
            //�}�X�����ׂčX�V����
        }
    }

    public void GetOriginalPosition(int masNum) //��������̌��̈ʒu���擾����
    {
        CurrentMasuNum = masNum;
    }

    public void GetCurrentKomaNum() //�������Ă����̔ԍ����擾����
    {
        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo �X�N���v�g�̎擾
        CurrentKomaNum = masuInfo.GetKomaNum(CurrentMasuNum);
        //�Ֆʏ�̋�������ʂ���
    }

    public void CheckKaku()
    {
        List<int> HilightMasuNum = new List<int>();
        //�p�����n�C���C�g������}�X���i�[���郊�X�g

        int i;
        int masNum;

        for(i=-10;i<10;i++ )
        {
            masNum = CurrentMasuNum + 14 * i;
            //�p�����̂P

            if (masNum > 224 || masNum< 0 ){ continue; }
            //0-224�ȊO�͔�΂�

            HilightMasuNum.Add(masNum); 
            //�}�X�i���o�[�̊i�[
        }

        for (i = -10; i < 10; i++)
        {
            masNum = CurrentMasuNum + 16 * i;
            //�p�����̂Q

            if (masNum > 224 || masNum < 0) { continue; }
            //0-224�ȊO�͔�΂�

            HilightMasuNum.Add(masNum);
            //�}�X�i���o�[�̊i�[
        }

        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            int k = masHandler.masNumber;
            //�}�X�̔ԍ����擾����

            foreach (int num in HilightMasuNum)
            {
                //�p���̃}�X�ԍ����ꂼ��ɏ������s��

                if (k == num)
                {
                    //�}�X�ԍ��Ɗp���̔ԍ�����v����ꍇ

                    masHandler.CangeMasColor(255,0,0);
                    //�F��ς���
                }
            }
           
        }
       
    }

    public void UnSelectMas() //�t�B�[���h�S�Ă��n�C���C�g
    {
        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            CellsHilighter cellsHilighter = obj.GetComponent<CellsHilighter>();
            //masHandler�X�N���v�g�̎擾

            cellsHilighter.HilightDefault();
        }
    }

    public void HilightField()�@//�����z�u�ł���2�񂾂����n�C���C�g
    {
        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            CellsHilighter cellsHilighter = obj.GetComponent<CellsHilighter>();
            //masHandler�X�N���v�g�̎擾

            int k = masHandler.masNumber;
            //�}�X�̔ԍ����擾����

            if (7 < k / n && k / n < 10)
            {
                if (4 < k % n && k % n < 10)
                {
                    cellsHilighter.HilightWhite();
                    //�t�B�[���h���n�C���C�g
                    cellsHilighter.HilightMyKoma();
                }
                else
                {
                    cellsHilighter.HilightDefault();
                    //�F��߂�
                }
            }
            else
            {
                cellsHilighter.HilightDefault();
                //�F��߂�
            }
        }

    }
    public void HilightAllField()�@//�����z�u�ł���2�񂾂����n�C���C�g
    {
        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            CellsHilighter cellsHilighter = obj.GetComponent<CellsHilighter>();
            //masHandler�X�N���v�g�̎擾

            int k = masHandler.masNumber;
            //�}�X�̔ԍ����擾����

            if (4 < k / n && k / n < 10)
            {
                if (4 < k % n && k % n < 10)
                {
                    cellsHilighter.HilightWhite();
                    //�t�B�[���h���n�C���C�g
                    cellsHilighter.HilightMyKoma();
                }
                else
                {
                    cellsHilighter.HilightDefault();
                    //�F��߂�
                }
            }
            else
            {
                cellsHilighter.HilightDefault();
                //�F��߂�
            }
        }
    }


    public void UnLockMas()�@//�S�Ẵ}�X���b�N����
    {
        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            CellsSelector cellsSelector = obj.GetComponent<CellsSelector>();
            cellsSelector.masLock = false;
            //�}�X���b�N����

        }
    }

    public void UnlockField()�@//�����z�u�ł���2�񂾂����}�X�^�[�Q�b�g�I��
    {
        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            CellsSelector cellsSelector = obj.GetComponent<CellsSelector>();
            //CellsSelector�X�N���v�g�̎擾

            int k = masHandler.masNumber;
            //�}�X�̔ԍ����擾����

            if (7 < k / n && k / n < 10)
            {
                if (4 < k % n && k % n < 10)
                {
                    cellsSelector.masTarget = true;
                }
                else
                {
                   
                }
            }
            else
            {
                
            }
        }
    }

    public void UnTargetField()�@//�S���}�X�^�[�Q�b�g�I�t
    {
        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            CellsSelector cellsSelector = obj.GetComponent<CellsSelector>();
            //CellsSelector�X�N���v�g�̎擾
            cellsSelector.masTarget = false;
        }
    }

    (int,int) Selectmasu()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            clickedGameObject = hit.collider.gameObject;
            MasHandler masHandler = clickedGameObject.GetComponent<MasHandler>();
            int a = masHandler.masNumber;
            

            MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
            //masuInfo �X�N���v�g�̎擾

            int b = masuInfo.GetKomaNum(a);

            return (a,b); // masuNum a , KomaNum b ���^�v���ŕԂ�
        } else
        {
            Debug.Log("error");
            return (0,0); // 0,0���^�v���ŕԂ�
        }
    }
    void SelectNewMasuOrDestination() //�I�������}�X�I�u�W�F�N�g���擾����
    {

        var c = Selectmasu();
        //�^�v���l�����󂯂�

        int a = c.Item1;
        //masuNum
        //Debug.Log(a+"A");

        int b = c.Item2;
        //komaNum
        //Debug.Log(b+"B");
                 
        if (0 < b && b < 31)
        {
            CurrentMasuNum = a;

            CurrentKomaNum = b;

            MovableAreaNums = CalcInitMovableArea(a, b);
            //��̓�����G���A�̃}�X�ԍ������X�g�ŃQ�b�g
        }
        else if (MovableAreaNums.Contains(a))//��̃��X�g�Ɋ܂܂�Ă���ꍇ
        {
            CellsHilighter cellsHilighter = clickedGameObject.GetComponent<CellsHilighter>();
            //masHandler�X�N���v�g�̎擾

            DestinationMasNum = a;
            //�������̃}�X�ɂ����ԍ�

            MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
            //masuInfo �X�N���v�g�̎擾
            DestinationKomaNum = masuInfo.GetKomaNum(DestinationMasNum);
            //��������̋�̔ԍ����擾����

            UnTargetField();
            //�}�X�^�[�Q�b�g�I�t
            cellsHilighter.HilightWhite();
            //�t�B�[���h���n�C���C�g
            SwitchKomaNum();
            //����X�C�b�`����
              
          
        }
    }

    public List<int> CalcInitMovableArea(int a, int b)
    {
        List<int> MovableArea = new List<int>();
        //�p�����n�C���C�g������}�X���i�[���郊�X�g

       
      
        if (a >184 && a < 192)
        {
            if(b == 11)
            {
                foreach (int x in onetoTwotwofive)
                {
                    if (4 < x / n && x / n < 10)
                    {
                        if (4 < x % n && x % n < 10)
                        {
                            MovableArea.Add(x);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }

                }

                return MovableArea;
            }
            else if (0 < b && b < 31)
            {
                foreach (int x in onetoTwotwofive)
                {
                    if (7 < x / n && x / n < 10)
                    {
                        if (4 < x % n && x % n < 10)
                        {
                            MovableArea.Add(x);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }

                }
                return MovableArea;
            }
            else
            {
                return MovableArea;
            }
        }
        else
        {
            return MovableArea;
        }

    }



    public void SwitchKomaNum()//��ԍ��̌������s��
    {
        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo �X�N���v�g�̎擾

        masuInfo.GetCoordinate(CurrentMasuNum);

        int xc = masuInfo.ColumnNumber;
        int yc = masuInfo.LowNumber;

        banmen[xc, yc] = DestinationKomaNum;

        masuInfo.GetCoordinate(DestinationMasNum);

        int xd = masuInfo.ColumnNumber;
        int yd = masuInfo.LowNumber;

        banmen[xd, yd] = CurrentKomaNum;

        //��ԍ��̌������s��

        ResetBanmen();
        DrawBanmen();
        UnSelectMas();
        UnTargetField();
        CurrentMasuNum = 0;
        CurrentKomaNum = 0;
        DestinationKomaNum = 0;
        DestinationMasNum = 0;
        MovableAreaNums = new List<int>();
    }

}
