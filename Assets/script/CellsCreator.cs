using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public List<int> BlockedMovableAreaNums = new List<int>();

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

    //�育�ܖ��̑ł胊�X�g
    public List<int> CurrentOpponentMovableFieldGyoku = new List<int>();
    public List<int> CurrentOpponentMovableFieldKinn = new List<int>();
    public List<int> CurrentOpponentMovableFieldHisha = new List<int>();
    public List<int> CurrentOpponentMovableFieldKaku = new List<int>();
    public List<int> CurrentOpponentMovableFieldKeima = new List<int>();
    //�育�ܖ��̑ł�|�C���g���X�g
    public List<int> GyokuHandPoint = new List<int>();
    public List<int> KinnHandPoint = new List<int>();
    public List<int> HishaHandPoint = new List<int>();
    public List<int> KakuHandPoint = new List<int>();
    public List<int> KeimaHandPoint = new List<int>();

    public List<int> HandPointComparison = new List<int>();

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

    public void HilightKikimichi()
    {

        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            int a = masHandler.masNumber;
            //�}�X�̔ԍ����擾����

            CellsHilighter cellsHilighter = obj.GetComponent<CellsHilighter>();
            //masHandler�X�N���v�g�̎擾


            if (MovableAreaNums.Contains(a))
            {
                cellsHilighter.HilightWhite();
                //�t�B�[���h���n�C���C�g
                cellsHilighter.HilightMyKoma();
       
            }
            else
            {

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

            MovableAreaNums = CheckBlockingKoma(a,b,MovableAreaNums);

            HilightKikimichi();
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

           
            if (DestinationKomaNum>30 && DestinationKomaNum < 60)
            {
                TakeKoma();
            }
            else
            {
                UnTargetField();
                //�}�X�^�[�Q�b�g�I�t
                cellsHilighter.HilightWhite();
                //�t�B�[���h���n�C���C�g
                SwitchKomaNum();
                //����X�C�b�`����                
            }

            StartCoroutine(WaitOneSecond());
        }

    }

    IEnumerator WaitOneSecond()
    {
        //Debug.Log("Wait start");

        // 1�b�ҋ@
        yield return new WaitForSeconds(1);

        OpponentTurnBegin();
    }

    void OpponentTurnBegin()
    {
        List<int> CurrentOpponentKomaNum = new List<int>();
        List<int> CurrentOpponentMovableField = new List<int>();

        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();

        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            int a = masHandler.masNumber;

            
            if (34 < a && a < 40 || 49 < a && a < 55)
            {
                //�GAI�̎�����̂���}�X�m�F
            
                int b = masuInfo.GetKomaNum(a);

                if (b > 30)
                {
                    //�GAI�̎�����̎�ފm�F
                    CurrentOpponentKomaNum.Add(b);
                }
             
            }
        }

        //������ꍇ
        if (CurrentOpponentKomaNum.Count > 0)
        {
            // ���X�g���烉���_���ɗv�f��I��
            int komaindex = UnityEngine.Random.Range(0, CurrentOpponentKomaNum.Count);
            CurrentKomaNum = CurrentOpponentKomaNum[komaindex];
            //��ԍ��̎擾

            foreach (GameObject obj in masuGameObject)
            {
                //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

                MasHandler masHandler = obj.GetComponent<MasHandler>();
                //masHandler�X�N���v�g�̎擾
                int a = masHandler.masNumber;

                int b = masuInfo.GetKomaNum(a);

                if (b == CurrentKomaNum)
                {
                    CurrentMasuNum = a;
                    //�}�X�ԍ��̎擾
                }
            }

            //�łĂ�͈͂��烉���_���Ƀ}�X��I��
            CurrentOpponentMovableField = MovableAreaEnemyField();
            int masuindex = UnityEngine.Random.Range(0, CurrentOpponentMovableField.Count);

            DestinationMasNum = CurrentOpponentMovableField[masuindex];
            //�ł���擾

            DestinationKomaNum = masuInfo.GetKomaNum(DestinationMasNum);
            //�ł�̋�ԍ�
        }
        else�@//���Ȃ��ꍇ
        {
            foreach (GameObject obj in masuGameObject)
            {
                //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

                MasHandler masHandler = obj.GetComponent<MasHandler>();
                //masHandler�X�N���v�g�̎擾

                int a = masHandler.masNumber;


                if (4 < a / n && a / n < 10)
                {
                    if (4 < a % n && a % n < 10)
                    {
                        //�GAI�̎�����̂���}�X�m�F

                        int b = masuInfo.GetKomaNum(a);

                        if (b > 30)
                        {
                            //�GAI�̔Ֆʂ̋�m�F
                            CurrentOpponentKomaNum.Add(b);
                        }
                    }
                }
            }

            // ���X�g���烉���_���ɗv�f��I��
            //int komaindex = UnityEngine.Random.Range(0, CurrentOpponentKomaNum.Count);
            //CurrentKomaNum = CurrentOpponentKomaNum[komaindex];
            //��ԍ��̎擾

            int GyokuCurrentMasuNum = 0;
            int KinnCurrentMasuNum = 0;
            int HishaCurrentMasuNum = 0;
            int KakuCurrentMasuNum = 0;
            int KeimaCurrentMasuNum = 0;

            foreach (int komaindex in CurrentOpponentKomaNum)
            {
                foreach (GameObject obj in masuGameObject)
                {
                    //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

                    MasHandler masHandler = obj.GetComponent<MasHandler>();
                    //masHandler�X�N���v�g�̎擾
                    int a = masHandler.masNumber;

                    int b = masuInfo.GetKomaNum(a);

                    if (b == komaindex)
                    {
                        CurrentMasuNum = a;
                        //�}�X�ԍ��̎擾
                    }
                }

                if (komaindex == 31)
                {
                    GyokuCurrentMasuNum = CurrentMasuNum;
                    CurrentOpponentMovableFieldGyoku = MovableAreaGyoku(CurrentMasuNum);
                    CurrentOpponentMovableFieldGyoku = CheckBlockingKoma(CurrentMasuNum, komaindex, CurrentOpponentMovableFieldGyoku);
                }
                else if (komaindex == 32)
                {
                    KinnCurrentMasuNum = CurrentMasuNum;
                    CurrentOpponentMovableFieldKinn = MovableAreaEnemyKin(CurrentMasuNum);
                    CurrentOpponentMovableFieldKinn = CheckBlockingKoma(CurrentMasuNum, komaindex, CurrentOpponentMovableFieldKinn);
                }
                else if (komaindex == 33)
                {
                    HishaCurrentMasuNum = CurrentMasuNum;
                    CurrentOpponentMovableFieldHisha = MovableAreaHisha(CurrentMasuNum);
                    CurrentOpponentMovableFieldHisha = CheckBlockingKoma(CurrentMasuNum, komaindex, CurrentOpponentMovableFieldHisha);
                }
                else if (komaindex == 34)
                {
                    KakuCurrentMasuNum = CurrentMasuNum;
                    CurrentOpponentMovableFieldKaku = MovableAreaKaku(CurrentMasuNum);
                    CurrentOpponentMovableFieldKaku = CheckBlockingKoma(CurrentMasuNum, komaindex, CurrentOpponentMovableFieldKaku);
                }
                else if (komaindex == 35)
                {
                    KeimaCurrentMasuNum = CurrentMasuNum;
                    CurrentOpponentMovableFieldKeima = MovableAreaEnemyKeima(CurrentMasuNum);
                    CurrentOpponentMovableFieldKeima = CheckBlockingKoma(CurrentMasuNum, komaindex, CurrentOpponentMovableFieldKeima);
                }
            }

            int i;

            GyokuHandPoint = new List<int>();
            KinnHandPoint = new List<int>();
            HishaHandPoint = new List<int>();
            KakuHandPoint = new List<int>();
            KeimaHandPoint = new List<int>();

            for (i = 0; i < CurrentOpponentMovableFieldGyoku.Count; i++)
            {
                int k = CurrentOpponentMovableFieldGyoku[i];
                int l = masuInfo.GetKomaNum(k);
                int m = CalcMovableHandPoint(k, l);

                GyokuHandPoint.Add(m);
            }
            for (i = 0; i < CurrentOpponentMovableFieldKinn.Count; i++)
            {
                int k = CurrentOpponentMovableFieldKinn[i];
                int l = masuInfo.GetKomaNum(k);
                int m = CalcMovableHandPoint(k, l);

                KinnHandPoint.Add(m);
            }
            for (i = 0; i < CurrentOpponentMovableFieldHisha.Count; i++)
            {
                int k = CurrentOpponentMovableFieldHisha[i];
                int l = masuInfo.GetKomaNum(k);
                int m = CalcMovableHandPoint(k, l);

                HishaHandPoint.Add(m);
            }
            for (i = 0; i < CurrentOpponentMovableFieldKaku.Count; i++)
            {
                int k = CurrentOpponentMovableFieldKaku[i];
                int l = masuInfo.GetKomaNum(k);
                int m = CalcMovableHandPoint(k, l);

                KakuHandPoint.Add(m);
            }
            for (i = 0; i < CurrentOpponentMovableFieldKeima.Count; i++)
            {
                int k = CurrentOpponentMovableFieldKeima[i];
                int l = masuInfo.GetKomaNum(k);
                int m = CalcMovableHandPoint(k, l);

                KeimaHandPoint.Add(m);
            }

            List<int> HandPointComparison = new List<int>();

            if(GyokuHandPoint.Count != 0)
            {
                HandPointComparison.Add(GyokuHandPoint.Max());
            }
            else
            {
                GyokuHandPoint.Add(0);
            }
            
            if(KinnHandPoint.Count != 0)
            {
                HandPointComparison.Add(KinnHandPoint.Max());
            }
            else
            {
                KinnHandPoint.Add(0);
            }
            
            if(HishaHandPoint.Count != 0)
            {
                HandPointComparison.Add(HishaHandPoint.Max());
            }else
            {
                HishaHandPoint.Add(0);
            }
            
            if(KakuHandPoint.Count != 0)
            {
                HandPointComparison.Add(KakuHandPoint.Max());
            }
            else
            {
                KakuHandPoint.Add(0);
            }
           
            if(KeimaHandPoint.Count != 0)
            {
                HandPointComparison.Add(KeimaHandPoint.Max());
            }
            else
            {
                KeimaHandPoint.Add(0);
            }
            

            int BestPointIndex = HandPointComparison.IndexOf(HandPointComparison.Max());
            

            if(BestPointIndex == 0)
            {
                int GyokuBestHandIndex = GyokuHandPoint.IndexOf(GyokuHandPoint.Max());
                DestinationMasNum = CurrentOpponentMovableFieldGyoku[GyokuBestHandIndex];
                CurrentMasuNum = GyokuCurrentMasuNum;
                CurrentKomaNum = 31;
            }
            else if(BestPointIndex == 1)
            {
                int KinnBestHandIndex = KinnHandPoint.IndexOf(KinnHandPoint.Max());
                DestinationMasNum = CurrentOpponentMovableFieldKinn[KinnBestHandIndex];
                CurrentMasuNum = KinnCurrentMasuNum;
                CurrentKomaNum = 32;
            }
            else if (BestPointIndex == 2)
            {
                int HishaBestHandIndex = HishaHandPoint.IndexOf(HishaHandPoint.Max());
                DestinationMasNum = CurrentOpponentMovableFieldHisha[HishaBestHandIndex];
                CurrentMasuNum = HishaCurrentMasuNum;
                CurrentKomaNum = 33;
            }
            else if (BestPointIndex == 3)
            {
                int KakuBestHandIndex = KakuHandPoint.IndexOf(KakuHandPoint.Max());
                DestinationMasNum = CurrentOpponentMovableFieldKaku[KakuBestHandIndex];
                CurrentMasuNum = KakuCurrentMasuNum;
                CurrentKomaNum = 34;
            }
            else{
                int KeimaBestHandIndex = KeimaHandPoint.IndexOf(KeimaHandPoint.Max());
                DestinationMasNum = CurrentOpponentMovableFieldKeima[KeimaBestHandIndex];
                CurrentMasuNum = KeimaCurrentMasuNum;
                CurrentKomaNum = 35;
            }
  
            //�łĂ�͈͂��烉���_���Ƀ}�X��I��
            //int masuindex = UnityEngine.Random.Range(0, CurrentOpponentMovableField.Count);
            //DestinationMasNum = CurrentOpponentMovableField[masuindex];
            //�ł���擾
             DestinationKomaNum = masuInfo.GetKomaNum(DestinationMasNum);
            //�ł�̋�ԍ�
        }

        if( DestinationKomaNum >0 && DestinationKomaNum < 31)
        {

            TakeKoma();

        }else if (DestinationKomaNum > 30)
        {
            Debug.Log(DestinationKomaNum);
        }
        else
        {
            SwitchKomaNum();
        }
        
    }

    public int CalcMovableHandPoint(int a, int b)
    {

        if (b == 11) {

            return 10;

        } else if (b % 5 == 1 && b<31)
        {

            return 9;

        }
        else if (b % 5 == 2 && b < 31)
        {
            return 6;

        }
        else if (b % 5 == 3 && b < 31)
        {
            return 8;
        }
        else if (b % 5 == 4 && b < 31)
        {
            return 7;
        }
        else if (b % 5 == 5 && b < 31)
        {
            return 5;
        }
        else
        {
            return 1;
        }
    }

    public List<int> CalcInitMovableArea(int a, int b)
    {
        List<int> MovableAreas = new List<int>();
        //�}�X���i�[���郊�X�g

       
      
        if (a >184 && a < 192)
        {
            //�ł���̏ꍇ

            if(b == 11)
            {
                //�}�^�^�r�̏ꍇ
                MovableAreas = MovableAreaAllField();
                return MovableAreas;
            }
            else if (0 < b && b < 15)
            {
                //����̎���̏ꍇ
                MovableAreas = MovableAreaMyField();
                return MovableAreas;
            }
            else if (16 < b && b < 31)
            {
                //�擾�����ł���̏ꍇ
                MovableAreas = MovableAreaAllField();
                return MovableAreas;
            }else
            {
                return MovableAreas;
            }
        }
        else if(4 < a / n && a / n < 10 && 4 < a % n && a % n < 10)
        {
            //�Տ�̋�̏ꍇ
            if (b == 1)
            {
                //1 = Gyoku fuse�̏ꍇ
                MovableAreas = MovableAreaGyoku(a);
                return MovableAreas;
            }
            else if (b == 2)
            {
                //2 = Kin fuse�̏ꍇ
                MovableAreas = MovableAreaKin(a);
                return MovableAreas;
            }
            else if (b == 3)
            {
                //3 = hisha fuse�̏ꍇ
                MovableAreas = MovableAreaHisha(a);
                return MovableAreas;
            }
            else if (b == 4)
            {
                //4 = kaku fuse�̏ꍇ
                MovableAreas = MovableAreaKaku(a);
                return MovableAreas;
            }
            else if (b == 5)
            {
                //5 = keima fuse�̏ꍇ
                MovableAreas = MovableAreaKeima(a);
                return MovableAreas;
            }
            else
            {
                return MovableAreas;
            }            
        }else
        {
            return MovableAreas;
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
        UnSelectMas();
        UnTargetField();
        CurrentMasuNum = 0;
        CurrentKomaNum = 0;
        DestinationKomaNum = 0;
        DestinationMasNum = 0;
        MovableAreaNums = new List<int>();
    }

    public void TakeKoma()
    {
        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo �X�N���v�g�̎擾

        masuInfo.GetCoordinate(CurrentMasuNum);

        int xc = masuInfo.ColumnNumber;
        int yc = masuInfo.LowNumber;

        banmen[xc, yc] = 0;


        if (DestinationKomaNum <= 30)
        {
            if (DestinationKomaNum == 11)
            {
                banmen[2, 11] = 41;
            }
            else if (DestinationKomaNum % 5 == 0)
            {
                banmen[3, 6] = 50;

            }
            else if (DestinationKomaNum % 5 == 1)
            {
                Debug.Log("game end");
            }
            else if (DestinationKomaNum % 5 == 2)
            {
                banmen[3, 9] = 47;
            }
            else if (DestinationKomaNum % 5 == 3)
            {
                banmen[3, 8] = 48;
            }
            else if (DestinationKomaNum % 5 == 4)
            {
                banmen[3, 7] = 49;
            }
            else
            {

            }
            
        }
        else
        {
            if (DestinationKomaNum == 11)
            {
                banmen[12, 12] = 11;
            }
            else if (DestinationKomaNum % 5 == 0)
            {
                banmen[11, 9] = 20;

            }
            else if (DestinationKomaNum % 5 == 1)
            {
                Debug.Log("game end");
            }
            else if (DestinationKomaNum % 5 == 2)
            {
                banmen[11, 6] = 17;
            }
            else if (DestinationKomaNum % 5 == 3)
            {
                banmen[11, 7] = 18;
            }
            else if (DestinationKomaNum % 5 == 4)
            {
                banmen[11, 8] = 19;
            }
            else
            {

            }

        }
        

            masuInfo.GetCoordinate(DestinationMasNum);

        int xd = masuInfo.ColumnNumber;
        int yd = masuInfo.LowNumber;

        banmen[xd, yd] = CurrentKomaNum;

        //��ԍ��̈ړ����s��

        ResetBanmen();
        UnSelectMas();
        UnTargetField();
        CurrentMasuNum = 0;
        CurrentKomaNum = 0;
        DestinationKomaNum = 0;
        DestinationMasNum = 0;
        MovableAreaNums = new List<int>();
    }

    List<int> CheckBlockingKoma(int a,int b, List<int>currentlist)
    {

        MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
        //masuInfo �X�N���v�g�̎擾

        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            int c = masHandler.masNumber;
            int d = masuInfo.GetKomaNum(c);

            if (currentlist.Contains(c) && d >0)
            {
                if (b % 5 == 3)
                {
                    

                    if (a/ n == c / n && c > a)
                    {
                        currentlist.Remove(c + 1);
                        currentlist.Remove(c + 2);
                        currentlist.Remove(c + 3);
                    }
                    else if (a % n == c % n && a > c)
                    {

                        currentlist.Remove(c - n);
                        currentlist.Remove(c - (2 * n));
                        currentlist.Remove(c - (3 * n));
                    }
                    else if (a/ n == c / n && a > c)
                    {

                        currentlist.Remove(c - 1);
                        currentlist.Remove(c - 2);
                        currentlist.Remove(c - 3);

                    }
                    else if (a % n == c % n && c > a)
                    {

                        currentlist.Remove(c + n);
                        currentlist.Remove(c + (2 * n));
                        currentlist.Remove(c + (3 * n));

                    }
                }else if(b % 5 == 4)
                {
                    if (0 == (a - c) % (n + 1) && c > a)
                    {

                        currentlist.Remove(c+(n+1));
                        currentlist.Remove(c + (n + 1)*2);
                        currentlist.Remove(c + (n + 1) * 3);
                    }
                    else if (0 == (a - c) % (n - 1) && a > c)
                    {
                        currentlist.Remove(c - (n - 1));
                        currentlist.Remove(c - (n - 1) * 2);
                        currentlist.Remove(c - (n - 1) * 3);
                    }
                    else if (0 == (a - c) % (n + 1) && a > c)
                    {
                        currentlist.Remove(c - (n + 1));
                        currentlist.Remove(c - (n + 1) * 2);
                        currentlist.Remove(c - (n + 1) * 3);
                    }
                    else if (0 == (a - c) % (n - 1) && c > a)
                    {
                        currentlist.Remove(c + (n - 1));
                        currentlist.Remove(c + (n - 1) * 2);
                        currentlist.Remove(c + (n - 1) * 3);
                    }

                }
   
            }
            
        }

        return currentlist;

    }

    public List<int> MovableAreaAllField()
    {
        List<int> MovableArea = new List<int>();

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

    public List<int> MovableAreaMyField()
    {
        List<int> MovableArea = new List<int>();

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

    public List<int> MovableAreaEnemyField()
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 7)
            {
                if (4 < x % n && x % n < 10)
                {
                    MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
                    //masuInfo �X�N���v�g�̎擾

                    int b = masuInfo.GetKomaNum(x);

                    if(b == 0)
                    {
                        MovableArea.Add(x);
                    }
                                        
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

    public List<int> MovableAreaGyoku(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if ( x == a + n - 1 || x == a + n + 1 || x == a + n || x == a + 1 || x == a - 1 || x == a - n - 1 || x == a - n + 1 || x == a - n )
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }


    public List<int> MovableAreaKin(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (x == a - n - 1 || x == a - n + 1 || x == a + n || x == a + 1 || x == a - 1 || x == a - n)
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }

    public List<int> MovableAreaHisha(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if ( x % n == a % n || x / n == a / n )
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }

    public List<int> MovableAreaKaku(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (0 == (a - x) % (n + 1) || 0 == (a - x) % (n - 1))
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }

    public List<int> MovableAreaKeima(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (x == a - 2*n - 1 || x == a - 2*n + 1 )
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }


    public List<int> MovableAreaEnemyKin(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (x == a + n - 1 || x == a + n + 1 || x == a + n || x == a + 1 || x == a - 1 || x == a - n)
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }

    public List<int> MovableAreaEnemyKeima(int a)
    {
        List<int> MovableArea = new List<int>();

        foreach (int x in onetoTwotwofive)
        {
            if (4 < x / n && x / n < 10)
            {
                if (4 < x % n && x % n < 10)
                {
                    if (x == a + 2 * n - 1 || x == a + 2 * n + 1)
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
            else
            {
                continue;
            }

        }
        return MovableArea;
    }
}
