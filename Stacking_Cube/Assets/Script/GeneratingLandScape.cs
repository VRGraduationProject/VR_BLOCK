using UnityEngine;
using System.Collections;
using UnityEngine.VR;
using UnityEngine.SceneManagement;
public class Block
{
    public int type;
    public bool vis;
    public GameObject block;
    public int nearBlockCount;

    public Block(int t, bool v, GameObject b)
    {
        type = t;
        vis = v;
        block = b;
        nearBlockCount = 0;
    }
}



public class GeneratingLandScape : MonoBehaviour {

    private static int width = 3;
    private static int depth = 3;
    private static int width2 = 4;
    private static int depth2 = 4;
    private static int width3 = 5;
    private static int depth3 = 5;


    public static int height = 128;
    public int heightScale = 20;
    public int heightOffset = 100;
    public float detailScale = 25.0f;
    private int cubeCount = 0;

    private bool isClear1 = false;
    private bool isClear2 = false;
    private bool isClear3 = false;

    private int stage1Cube = 0;
    private int stage2Cube = 0;
    private int stage3Cube = 0;
    private bool stage1 = false;
    private bool stage2 = false;
    private bool stage3 = false;

    private GameObject[] tmpbox = new GameObject[] { };
    private GameObject[] expbox = new GameObject[] { };
    private GameObject stageWall;
    private GameObject stagewall2;
    private GameObject stagewall3;

    public GameObject grassBlock;
    public GameObject sandBlock;
    public GameObject snowBlock;
    public GameObject cloudBlock;
    public GameObject diamondBlock;
    public GameObject bridge1;
    public GameObject bridge2;

    public Camera oculusCamera;
    public GameObject explosion;
    public GameObject fireWork;

    private bool clickX = false;
    private bool clickB = false;

    
    


    Block[,,] worldBlocks = new Block[width, height, depth];
    bool[,,] blockInfo = new bool[3, 3, 3];
    bool[,,] blockInfo2 = new bool[4, 4, 4];
    bool[,,] blockInfo3 = new bool[5, 5, 5];
    // Use this for initialization

    void stage1Start()
    {
        int seed = (int)Network.time * 10;
        for (int z = 0; z < depth; z++)// depth , width = 가로 세로
        {
            for (int x = 0; x < width; x++)
            {
                // int y = (int)(Mathf.PerlinNoise((x + seed) / detailScale, (z + seed) / detailScale) * heightScale) + heightOffset;
                // y 는 파지는 땅의 깊이
                int y = 10;

                Vector3 blockPos = new Vector3(x, y, z);

                CreateBlock(y, blockPos, true);// 맵생성
                //while (y > 0)// 땅 파면 계속 들어가게 해줌
                //{
                //    y--;
                //    blockPos = new Vector3(x, y, z);
                //    CreateBlock(y, blockPos, false);
                //}
            }
        }
        for (int x = 0; x < 3; x++)
        {
            for (int z = 0; z < 3; z++)
            {
                for (int y = 11; y < 14; y++)
                {
                    blockInfo[x, y - 11, z] = false;
                }
            }
        }
    }

    void stage2Start()
    {
        int seed = (int)Network.time * 10;
        for (int z = -1; z < -1 + depth2; z++)// depth , width = 가로 세로
        {
            for (int x = 20; x < 20 + width2; x++)
            {
                // int y = (int)(Mathf.PerlinNoise((x + seed) / detailScale, (z + seed) / detailScale) * heightScale) + heightOffset;
                // y 는 파지는 땅의 깊이
                int y = 10;

                Vector3 blockPos = new Vector3(x, y, z);

                CreateBlock(y, blockPos, true);// 맵생성

            }
        }
        for (int x = 0; x < 4; x++)
        {
            for (int z = 0; z < 4; z++)
            {
                for (int y = 11; y < 15; y++)
                {
                    blockInfo2[x, y - 11, z] = false;
                }
            }
        }


    }
    void stage3Start()
    {
        int seed = (int)Network.time * 10;
        for (int z = -2; z < -2 + depth3; z++)// depth , width = 가로 세로
        {
            for (int x = 60; x < 60 + width3; x++)
            {
                // int y = (int)(Mathf.PerlinNoise((x + seed) / detailScale, (z + seed) / detailScale) * heightScale) + heightOffset;
                // y 는 파지는 땅의 깊이
                int y = 10;

                Vector3 blockPos = new Vector3(x, y, z);

                CreateBlock(y, blockPos, true);// 맵생성

            }
        }

    }
    void Start()
    {
        stage1Start();
        stage2Start();
        stage3Start();

    }

    void CreateBlock(int y, Vector3 blockPos, bool create)
    {
        GameObject newBlock = null;

            if (create)
                newBlock = (GameObject)Instantiate(diamondBlock, blockPos, Quaternion.identity);
          //  worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(5, create, newBlock);
        
    }

    void DrawBlock(Vector3 blockPos)
    {
        //if it is outside the map
        if (blockPos.x < 0 || blockPos.x > width - 1 ||
            blockPos.y < 0 || blockPos.y > height - 1 ||
            blockPos.z < 0 || blockPos.z > depth - 1) return;

        if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] == null) return;

        if (!worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].vis)
        {
            GameObject newBlock = null;
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].vis = true;

            if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].type == 1)
                newBlock = (GameObject)Instantiate(snowBlock, blockPos, Quaternion.identity);

            else if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].type == 2)
                newBlock = (GameObject)Instantiate(grassBlock, blockPos, Quaternion.identity);

            else if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].type == 3)
                newBlock = (GameObject)Instantiate(sandBlock, blockPos, Quaternion.identity);

            else if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].type == 5)
                newBlock = (GameObject)Instantiate(diamondBlock, blockPos, Quaternion.identity);

            else
                worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].vis = false;

            if (newBlock != null)
                worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].block = newBlock;
        }
    }

    // Update is called once per frame
    bool checkClear()
    {//나머지 어레이 값들은 false가 되어야 함 
        if (blockInfo[1, 0, 0] == true && blockInfo[1, 0, 1] == true && blockInfo[0, 0, 1] == true && blockInfo[0, 1, 1] == true
            && blockInfo[1, 1, 1] == true && blockInfo[2, 0, 1] == true && blockInfo[1, 2, 1] == true && stage1 == false)
        {
            stage1 = true;
            return true;
        }
        else
            return false;
    }
    bool checkClear2()
    {//나머지 어레이 값들은 false가 되어야 함 
        if (blockInfo2[1, 0, 0] == true && blockInfo2[2, 0, 0] == true && blockInfo2[0,0,1] == true && blockInfo2[1, 0, 1] == true
            && blockInfo2[2,0,1] == true && blockInfo2[2, 1, 1] == true && blockInfo2[3,0,1] == true 
            && blockInfo2[1,0,2] == true && blockInfo2[1,1,2] == true &&  blockInfo2[2,0,2] == true && blockInfo2[2, 1, 2] == true && blockInfo2[2, 2, 2] == true
            && blockInfo2[1,0,3] == true && stage2 == false)
        {
            stage2 = true;
            return true;
        }else if (blockInfo2[1, 0, 0] == true && blockInfo2[2, 0, 0] == true && blockInfo2[0, 0, 1] == true && blockInfo2[1, 0, 1] == true
            && blockInfo2[2, 0, 1] == true && blockInfo2[2, 1, 1] == true && blockInfo2[3, 0, 1] == true
            && blockInfo2[1, 0, 2] == true && blockInfo2[1, 1, 2] == true && blockInfo2[2, 0, 2] == true && blockInfo2[2, 1, 2] == true && blockInfo2[2, 2, 2] == true
            && blockInfo2[1, 0, 3] == true && blockInfo2[1,1,1] == true && stage2== false)
        {
            stage2 = true;
            return true;
        }
        else
            return false;
    }
    bool checkClear3()
    {//나머지 어레이 값들은 false가 되어야 함 
        if (blockInfo3[1, 0, 0] == true && blockInfo3[4, 0, 0] == true &&
            blockInfo3[1, 0, 1] == true && blockInfo3[2, 0, 1] == true && blockInfo3[2, 1, 1] == true && blockInfo3[4, 0, 1] == true &&
            blockInfo3[0, 0, 2] == true && blockInfo3[1, 0, 2] == true && blockInfo3[2, 0, 2] == true && blockInfo3[3, 0, 2] == true &&
            blockInfo3[4, 0, 2] == true && blockInfo3[4, 1, 2] == true && blockInfo3[4, 2, 2] == true && blockInfo3[4, 3, 2] == true &&
            blockInfo3[4, 4, 2] == true && blockInfo3[3, 0, 3] == true && blockInfo3[4, 0, 3] == true &&
            blockInfo3[3, 0, 4] == true && blockInfo3[3, 1, 4] == true && blockInfo3[2, 1, 4] == true && blockInfo3[3, 2, 4] == true &&
            blockInfo3[2, 2, 4] == true && blockInfo3[1, 2, 4] == true && blockInfo3[4, 3, 1] == true && blockInfo3[3, 2, 2] == true &&
            blockInfo3[3, 3, 2] == true && stage3 == false)
        {
            stage3 = true;
            return true;
        }
        else
            return false;
    }
    void stage1complete()
    {
        tmpbox = GameObject.FindGameObjectsWithTag("snow");
        expbox = GameObject.FindGameObjectsWithTag("explosion");
        for(int i=0;i<5;i++)
        Instantiate(fireWork, new Vector3(1.46f, 30.0f, -30.0f), Quaternion.identity);
        Debug.Log("없어질 큐브개수 : " + tmpbox.Length);
        Debug.Log(" Stage 1 Clear !!");
        for (int i = 0; i < tmpbox.Length; i++)
        {
            Destroy(tmpbox[i], 5.0f);
            DestroyImmediate(expbox[i]);

        }
        cubeCount = 0;
        GameObject newBridge1 = (GameObject)Instantiate(bridge2, new Vector3(11.1f, 8.9f, 0.4f), Quaternion.identity);// 브릿지 생성
        stageWall = GameObject.FindGameObjectWithTag("stageWall");
        Destroy(stageWall, 5.0f);

        isClear1 = false;
    }
    void stage2complete()
    {
        tmpbox = GameObject.FindGameObjectsWithTag("snow");
        expbox = GameObject.FindGameObjectsWithTag("explosion");
        for(int i=0; i<5; i++)
        Instantiate(fireWork, new Vector3(21.46f, 30.0f, -30.0f), Quaternion.identity);
        Debug.Log("없어질 큐브개수 : " + tmpbox.Length);
        Debug.Log(" Stage 2 Clear !!");
        for (int i = 0; i < tmpbox.Length; i++)
        {
            Destroy(tmpbox[i], 5.0f);
            DestroyImmediate(expbox[i]);

        }
        cubeCount = 0;
        GameObject newBridge2 = (GameObject)Instantiate(bridge2, new Vector3(39.6f, 9.0f, 0.5f), Quaternion.identity);
        stagewall2 = GameObject.FindGameObjectWithTag("stageWall2");
        Destroy(stagewall2, 5.0f);

        isClear2 = false;
    }
    void stage3complete()
    {
        tmpbox = GameObject.FindGameObjectsWithTag("snow");
        expbox = GameObject.FindGameObjectsWithTag("explosion");
        for(int i=0;i<5;i++)
        Instantiate(fireWork, new Vector3(61.46f, 30.0f, -30.0f), Quaternion.identity);
        Debug.Log("없어질 큐브개수 : " + tmpbox.Length);
        Debug.Log(" Stage 3 Clear !!");
        for (int i = 0; i < tmpbox.Length; i++)
        {
            Destroy(tmpbox[i], 5.0f);
            DestroyImmediate(expbox[i]);
        }
        cubeCount = 0;
        //stagewall3 = GameObject.FindGameObjectWithTag("stageWall3");
        //Destroy(stagewall3, 5.0f);
        

        isClear3 = false;
    }
 
    void Update() {
        if (Input.GetButton("Xiaomi button X") || Input.GetMouseButtonDown(1))
        {
            if (!clickX)
            {
                clickX = true;
                RaycastHit hit;
                //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0)); //(x,y,z)
                if (Physics.Raycast(new Ray(oculusCamera.transform.position,
                              oculusCamera.transform.rotation * Vector3.forward), out hit))
                {
                    Vector3 blockPos = hit.transform.position;

                    //this is the bottom block. Can't delete it.

                    if ((int)blockPos.y == 0) return;

                    //worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = null;
                    if (blockPos.x >= 0 && blockPos.x < 3 && blockPos.y >= 10 && blockPos.y < 14 && blockPos.z >= 0 && blockPos.z < 3
                          && stage1 == false)
                    {
                        if (hit.transform.tag.Equals("snow"))////////////////// 클릭해서 생성된 블록만 제거
                        {
                            Destroy(hit.transform.gameObject);
                            blockInfo[(int)blockPos.x, (int)blockPos.y - 11, (int)blockPos.z] = false;
                            cubeCount--;
                            stage1Cube--;
                        }
                        if (stage1Cube == 7) isClear1 = checkClear();
                        else return;

                        if (isClear1) stage1complete();

                    }
                    else if (blockPos.x >= 20 && blockPos.x < 24 && blockPos.y >= 10 && blockPos.y < 15 && blockPos.z >= -1 && blockPos.z < 3
                           && stage2 == false)
                    {
                        if (hit.transform.tag.Equals("snow"))////////////////// 클릭해서 생성된 블록만 제거
                        {
                            Destroy(hit.transform.gameObject);
                            blockInfo2[(int)blockPos.x - 20, (int)blockPos.y - 11, (int)blockPos.z + 1] = false;
                            cubeCount--;
                            stage2Cube--;
                        }
                        if (stage2Cube == 13) isClear2 = checkClear2();
                        else return;

                        if (isClear2) stage2complete();

                    }
                    else if (blockPos.x >= 60 && blockPos.x < 65 && blockPos.y >= 10 && blockPos.y < 16 && blockPos.z >= -2 && blockPos.z < 3
                           && stage3 == false)
                    {
                        if (hit.transform.tag.Equals("snow"))////////////////// 클릭해서 생성된 블록만 제거
                        {
                            Destroy(hit.transform.gameObject);
                            blockInfo3[(int)blockPos.x - 60, (int)blockPos.y - 11, (int)blockPos.z + 2] = false;
                            cubeCount--;
                            stage3Cube--;
                        }
                        Debug.Log("stage3Cube = " + stage3Cube);
                        if (stage3Cube == 26) { isClear3 = checkClear3(); Debug.Log("isClear3 값 = " + isClear3); }
                        else return;


                        if (isClear3) stage3complete();

                    }
                    else
                        return;

                    for (int x = -1; x <= 1; x++)
                        for (int y = -1; y <= 1; y++)
                            for (int z = -1; z <= 1; z++)
                            {
                                if (!(x == 0 && y == 0 && z == 0))
                                {
                                    Vector3 neighbour = new Vector3(blockPos.x + x, blockPos.y + y, blockPos.z + z);
                                    DrawBlock(neighbour);
                                }
                            }
                }
            }
        } // press button
        else if (Input.GetButton("Xiaomi button B") || Input.GetMouseButtonDown(0))
        {
            if (!clickB)
            {
                clickB = true;
                RaycastHit hit;// ---> 광선으로 쏴서 맞은 오브젝트의 정보를 가져온다

                if (Physics.Raycast(new Ray(oculusCamera.transform.position,
                               oculusCamera.transform.rotation * Vector3.forward), out hit))
                {
                    Vector3 blockPos = hit.transform.position;
                    Vector3 hitVector = blockPos - hit.point;

                    hitVector.x = Mathf.Abs(hitVector.x);
                    hitVector.y = Mathf.Abs(hitVector.y);
                    hitVector.z = Mathf.Abs(hitVector.z);

                    Debug.Log("히트 X = " + hitVector.x);
                    Debug.Log("히트 Y = " + hitVector.y);
                    Debug.Log("히트 Z = " + hitVector.z);

                    Debug.Log("블락 position x = " + (int)blockPos.x);
                    Debug.Log("블락 position y = " + (float)blockPos.y);
                    Debug.Log("블락 position z = " + (int)blockPos.z);

                    Debug.Log(hit.transform.gameObject.name);


                    //------------- 위에만 쌓기 ---------------
                    blockPos.y += 1;

                    GameObject newBlock = null;
                    if (blockPos.x >= 0 && blockPos.x < 3 && blockPos.y >= 10 && blockPos.y < 14 && blockPos.z >= 0 && blockPos.z < 3
                        && stage1 == false)
                    {
                        if (blockInfo[(int)blockPos.x, (int)blockPos.y - 11, (int)blockPos.z] == false)
                        {
                            newBlock = (GameObject)Instantiate(snowBlock, blockPos, Quaternion.identity);// 블럭 생성
                            blockInfo[(int)blockPos.x, (int)blockPos.y - 11, (int)blockPos.z] = true;
                            Instantiate(explosion, blockPos, Quaternion.identity);
                            cubeCount++;
                            stage1Cube++;
                            Debug.Log((int)blockPos.x);
                            Debug.Log((int)blockPos.y - 11);
                            Debug.Log((int)blockPos.z);
                            Debug.Log("블록생성");
                            Debug.Log("큐브 총 개수" + cubeCount);
                            Debug.Log("stage1Cube" + stage1Cube);

                        }

                        else
                            return;

                        if (stage1Cube == 7) isClear1 = checkClear();
                        else return;


                        if (isClear1) stage1complete();

                    }//stage1 쪽

                    //satge2 시작
                    if (blockPos.x >= 20 && blockPos.x < 24 && blockPos.y >= 10 && blockPos.y < 15 && blockPos.z >= -1 && blockPos.z < 3
                            && stage2 == false)
                    {
                        if (blockInfo2[(int)blockPos.x - 20, (int)blockPos.y - 11, (int)blockPos.z + 1] == false)//해당위치에 블럭이 있는지
                        {
                            newBlock = (GameObject)Instantiate(snowBlock, blockPos, Quaternion.identity);// 블럭 생성
                            blockInfo2[(int)blockPos.x - 20, (int)blockPos.y - 11, (int)blockPos.z + 1] = true;
                            Instantiate(explosion, blockPos, Quaternion.identity);
                            cubeCount++;
                            stage2Cube++;
                            Debug.Log((int)blockPos.x - 20);
                            Debug.Log((int)blockPos.y - 11);
                            Debug.Log((int)blockPos.z + 1);
                            Debug.Log("블록생성");
                            Debug.Log("큐브 총 개수" + cubeCount);
                            Debug.Log("stage2Cube" + stage2Cube);

                        }

                        else
                            return;

                        //-----------------------------
                        if (stage2Cube == 13 || stage2Cube == 14) isClear2 = checkClear2();
                        else return;


                        if (isClear2) stage2complete();

                    }//stage2 끝

                    //stage3
                    if (blockPos.x >= 60 && blockPos.x < 65 && blockPos.y >= 10 && blockPos.y < 16 && blockPos.z >= -2 && blockPos.z < 3
                         && stage3 == false)
                    {
                        if (blockInfo3[(int)blockPos.x - 60, (int)blockPos.y - 11, (int)blockPos.z + 2] == false)//해당위치에 블럭이 있는지
                        {
                            newBlock = (GameObject)Instantiate(snowBlock, blockPos, Quaternion.identity);// 블럭 생성
                            blockInfo3[(int)blockPos.x - 60, (int)blockPos.y - 11, (int)blockPos.z + 2] = true;
                            Instantiate(explosion, blockPos, Quaternion.identity);
                            cubeCount++;
                            stage3Cube++;
                            Debug.Log((int)blockPos.x - 60);
                            Debug.Log((int)blockPos.y - 11);
                            Debug.Log((int)blockPos.z + 2);
                            Debug.Log("블록생성");
                            Debug.Log("큐브 총 개수" + cubeCount);
                            Debug.Log("stage3Cube" + stage3Cube);

                        }

                        else
                            return;


                        //worldBlocks[(int)blockPos.x, (int)blockPos.y+1, (int)blockPos.z] = new Block(5, true, newBlock);
                        //-----------------------------
                        if (stage3Cube == 26) isClear3 = checkClear3();
                        else return;


                        if (isClear3) stage3complete();

                    }

                }
            }
        }
        else if (Input.GetButton("Xiaomi button Restart"))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            clickX = false;
            clickB = false;
        }
    }
}

