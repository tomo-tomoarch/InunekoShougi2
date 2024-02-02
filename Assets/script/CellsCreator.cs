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

    GameObject clickedGameObject;
    //�Z���N�g�����^�[�Q�b�g�}�X�̃Q�[���I�u�W�F�N�g�i�[�p

    public int[,] masu = new int[n, n];
    //masu�񎟌��z��̏�����

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




        /*
        ////////////////////////////////////////
        foreach (int a in masu)   //�Ֆʂɋ��u��
        {
            int number = a;
            int i = number / 15;
            int j = number % 15;


            if (banmen[j, i] > 0)
            {
                GameObject obj = (GameObject)Instantiate(koma, new Vector3(j - (n / 2), 1, i - (n / 2)), Quaternion.identity);
                //���z�u

                KomaInfo komaInfo = obj.GetComponent<KomaInfo>();
                //��̏����Q�b�g

                komaInfo.CangeKomColor(255, 255, 0);
                //��ɐF��t����
            }
        };
        /////////////////////////////////////////
        */


        /*
       /////////////////////////////////////////
       GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
       //�p���o��������i�f�o�b�O�p�j
       MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
       //�p�X�N���v�g�̎擾
       CurrentMasuNum = 112;
       //�p�̌��݈ʒu�}�X�ԍ�
        ////////////////////////////////////////
       */



    }

  
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectDestinationMas();
        }


            /*
          if (Input.GetButtonDown("Jump"))
          {
              UnSelectMas();
              //�p���\�b�h�̌Ăяo��
          }
          if (Input.GetKeyDown(KeyCode.LeftArrow))
          {
              UnSelectMas();

              GameObject gameObject = GameObject.FindWithTag("Koma");
              Destroy(gameObject);
              //��x������Z�b�g

              GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
              //�p���o��������i�f�o�b�O�p�j
              MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
              //�p�X�N���v�g�̎擾

              int nextKaku = CurrentMasuNum - 1;
              int i = nextKaku / 15;
              int j = nextKaku % 15;
              CurrentMasuNum = nextKaku;
              //���̃}�X���v�Z����

              moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
              //�p�𓮂���

              CheckKaku();
          }
          if (Input.GetKeyDown(KeyCode.RightArrow))
          {
              UnSelectMas();

              GameObject gameObject = GameObject.FindWithTag("Koma");
              Destroy(gameObject);
              //��x������Z�b�g

              GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
              //�p���o��������i�f�o�b�O�p�j
              MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
              //�p�X�N���v�g�̎擾

              int nextKaku = CurrentMasuNum + 1;
              int i = nextKaku / 15;
              int j = nextKaku % 15;
              CurrentMasuNum = nextKaku;
              //���̃}�X���v�Z����

              moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
              //�p�𓮂���

              CheckKaku();
          }
          if (Input.GetKeyDown(KeyCode.UpArrow))
          {
              UnSelectMas();

              GameObject gameObject = GameObject.FindWithTag("Koma");
              Destroy(gameObject);
              //��x������Z�b�g

              GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
              //�p���o��������i�f�o�b�O�p�j
              MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
              //�p�X�N���v�g�̎擾

              int nextKaku = CurrentMasuNum - 15;
              int i = nextKaku / 15;
              int j = nextKaku % 15;
              CurrentMasuNum = nextKaku;
              //���̃}�X���v�Z����

              moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
              //�p�𓮂���

              CheckKaku();
          }
          if (Input.GetKeyDown(KeyCode.DownArrow))
          {
              UnSelectMas();

              GameObject gameObject = GameObject.FindWithTag("Koma");
              Destroy(gameObject);
              //��x������Z�b�g

              GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
              //�p���o��������i�f�o�b�O�p�j
              MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
              //�p�X�N���v�g�̎擾

              int nextKaku = CurrentMasuNum + 15;
              int i = nextKaku / 15;
              int j = nextKaku % 15;
              CurrentMasuNum = nextKaku;
              //���̃}�X���v�Z����

              moveKaku.MoveKakuNext(i - (n / 2), j - (n / 2));
              //�p�𓮂���

              CheckKaku();
          }
              */

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

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾

            int k = masHandler.masNumber;
            //�}�X�̔ԍ����擾����

            if (4 < k / 15 && k / 15 < 10)
            {
                if (4 < k % 15 && k % 15 < 10)
                {
                    masHandler.CangeMasColor(180, 180, 180);
                    //�t�B�[���h���n�C���C�g
                }
                else
                {
                    masHandler.CangeMasColor(122, 122, 122);
                    //�F��߂�
                }
            }
            else
            {
                masHandler.CangeMasColor(122, 122, 122);
                //�F��߂�
            }



        }
    }

    public void HilightField()�@//�����z�u�ł���2�񂾂����n�C���C�g
    {
        foreach (GameObject obj in masuGameObject)
        {
            //�}�X�I�u�W�F�N�g���ꂼ��ɏ������s��

            MasHandler masHandler = obj.GetComponent<MasHandler>();
            //masHandler�X�N���v�g�̎擾
            int k = masHandler.masNumber;
            //�}�X�̔ԍ����擾����

            if (7 < k / 15 && k / 15 < 10)
            {
                if (4 < k % 15 && k % 15 < 10)
                {
                    masHandler.CangeMasColor(255, 255, 255);
                    //�t�B�[���h���n�C���C�g
                }
                else
                {
                    masHandler.CangeMasColor(122, 122, 122);
                    //�F��߂�
                }
            }
            else
            {
                masHandler.CangeMasColor(122, 122, 122);
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

            if (7 < k / 15 && k / 15 < 10)
            {
                if (4 < k % 15 && k % 15 < 10)
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

    void SelectDestinationMas() //������̃}�X�I�u�W�F�N�g���擾����
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            clickedGameObject = hit.collider.gameObject;
            MasHandler masHandler = clickedGameObject.GetComponent<MasHandler>();
            int a = masHandler.masNumber;

            CellsSelector cellsSelector = clickedGameObject.GetComponent<CellsSelector>();
            //CellsSelector�X�N���v�g�̎擾

            if (7 < a / 15 && a / 15 < 10)
            {
                if (4 < a % 15 && a % 15 < 10)
                {
                    DestinationMasNum = a;
                    //�������̃}�X�ɂ����ԍ�

                    MasuInfo masuInfo = GameObject.FindWithTag("GameController").GetComponent<MasuInfo>();
                    //masuInfo �X�N���v�g�̎擾
                    DestinationKomaNum = masuInfo.GetKomaNum(DestinationMasNum);
                    //��������̋�̔ԍ����擾����

                    UnTargetField();
                    //�}�X�^�[�Q�b�g�I�t
                    masHandler.CangeMasColor(255, 255, 255);
                    //�t�B�[���h���n�C���C�g
                    SwitchKomaNum();
                    //����X�C�b�`����
                }
                else
                {
                    
                }
            }
            else
            {
               
            }

            //Debug.Log(clickedGameObject.name);//�Q�[���I�u�W�F�N�g�̖��O���o��
            //Destroy(clickedGameObject);//�Q�[���I�u�W�F�N�g��j��
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
    }
 
}
