using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsCreator : MonoBehaviour
{
    // Start is called before the first frame update

    const int n = 15;
    //�}�X�̐��@
    public GameObject originObject;
    //�}�X�R���|�[�l���g
    public GameObject kaku;
    //�p�R���|�[�l���g
    public GameObject koma;

    public int CurrentMasuNum;
    //��̌��݈ʒu�̃}�X�̐���

    int[,] masu = new int[n, n];
    //�񎟌��z��}�X�̃i���o�����O1-224

    int[,] banmen = new int[n, n]{
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            { 0,0,0,0,0,35,340,33,32,31,0,0,0,0,0},
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


    void Start()
    {

        int k;
    

        for (k = 0; k < masu.GetLength(1); k++)
        {
            int i;

            for (i = 0; i < masu.GetLength(1); i++)
            {
                masu[k, i] = (k * masu.GetLength(1)) + i;

                //Debug.Log(masu[j, i]);�@//�f�o�b�O�p

                GameObject obj = (GameObject)Instantiate(originObject, new Vector3(k - (n / 2), 0, i - (n / 2)), Quaternion.identity);
                //�}�X�����_���S�ɕ`�悷��

                MasHandler masHandler = obj.GetComponent<MasHandler>();
                //�`�悵���}�X��MasHandler�X�N���v�g�̎擾

                masHandler.MasNumber(masu[k, i]);
                //MasNumber���\�b�h�̎��s1-224�̃i���o�����O���}�X���̂ɕt����

                masuGameObject.Add(obj);
            �@�@//���X�g�Ƀ}�X���̂������}�X�Q�[���I�u�W�F�N�g�̃��X�g�����

            }
        }
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
       
        foreach (int a in masu)
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

        //�Ֆʂɋ��u��





        GameObject kakuobj = (GameObject)Instantiate(kaku, new Vector3(0, 1, 0), Quaternion.identity);
        //�p���o��������i�f�o�b�O�p�j
        MoveKaku moveKaku = kakuobj.GetComponent<MoveKaku>();
        //�p�X�N���v�g�̎擾
        CurrentMasuNum = 112;
        //�p�̌��݈ʒu�}�X�ԍ�
        
    }

    // Update is called once per frame
    void Update()
    {

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

    public void UnSelectMas()
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

    public void HilightField()
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
                    masHandler.CangeMasColor(122, 122, 255);
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
}
