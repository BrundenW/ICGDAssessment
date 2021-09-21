using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject oldMap;
    [SerializeField]
    private GameObject outsideCorner;
    public GameObject insideCorner;
    public GameObject normalPellet;
    public GameObject powerPellet;
    public GameObject outsideWall;
    public GameObject insideWall;
    public GameObject junction;
    private int[,] levelMap = new int [,]
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
        };
    private List<GameObject> mapList = new List<GameObject>();
    private float width;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        oldMap.SetActive(false);
        height = levelMap.GetLength(0);
        width = levelMap.GetLength(1);
        Vector3 rotation1;
        Vector3 rotation2;
        Vector3 rotation3;
        Vector3 rotation4;
        for (int i = 0; i < height - 1; i++)
        {
            for (int j = 0; j < width; j++)
            {
                //Outside Corner
                if (levelMap[i, j] == 1)
                {
                    if (i != 0 && (levelMap[i - 1, j] == 1 || levelMap[i - 1, j] == 2))
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                        {
                            rotation1 = new Vector3(0, 0, 180);
                            rotation2 = new Vector3(0, 0, 90);
                            rotation3 = new Vector3(0, 0, 270);
                            rotation4 = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 90);
                            rotation2 = new Vector3(0, 0, 180);
                            rotation3 = new Vector3(0, 0, 0);
                            rotation4 = new Vector3(0, 0, 270);
                        }
                    }
                    else
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                        {
                            rotation1 = new Vector3(0, 0, 270);
                            rotation2 = new Vector3(0, 0, 0);
                            rotation3 = new Vector3(0, 0, 180);
                            rotation4 = new Vector3(0, 0, 90);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 0);
                            rotation2 = new Vector3(0, 0, 270);
                            rotation3 = new Vector3(0, 0, 90);
                            rotation4 = new Vector3(0, 0, 180);
                        }
                    }
                    mapList.Add(Instantiate(outsideCorner, new Vector3(j, -i, 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(outsideCorner, new Vector3(width * 2 - j - 1, -i, 0f), Quaternion.Euler(rotation2)));
                    mapList.Add(Instantiate(outsideCorner, new Vector3(j, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation3)));
                    mapList.Add(Instantiate(outsideCorner, new Vector3(width * 2 - j - 1, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation4)));
                }
                //Outside Wall
                else if (levelMap[i, j] == 2)
                {
                    if (j == 0)
                    {
                        if (levelMap[i, j + 1] == 1 || levelMap[i, j + 1] == 2 || levelMap[i, j + 1] == 7)
                        {
                            rotation1 = new Vector3(0, 0, 90);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 0);
                        }
                    }
                    else
                    {
                        if (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2 || levelMap[i, j - 1] == 7)
                        {
                            rotation1 = new Vector3(0, 0, 90);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 0);
                        }
                    }
                    mapList.Add(Instantiate(outsideWall, new Vector3(j, -i, 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(outsideWall, new Vector3(width * 2 - j - 1, -i, 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(outsideWall, new Vector3(j, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(outsideWall, new Vector3(width * 2 - j - 1, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation1)));
                }
                //Inside Corner
                else if (levelMap[i, j] == 3)
                {
                    if ((i != 0 && i < height - 1 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4) && (i != 0 && (levelMap[i + 1, j] == 3 || levelMap[i + 1, j] == 4))))
                    {
                        Debug.Log("test");
                        if (j != 0 && levelMap[i - 1, j - 1] != 3 && levelMap[i - 1, j - 1] != 4)
                        {
                            Debug.Log("test1");
                            rotation1 = new Vector3(0, 0, 180);
                            rotation2 = new Vector3(0, 0, 90);
                            rotation3 = new Vector3(0, 0, 270);
                            rotation4 = new Vector3(0, 0, 0);
                        }
                        else if (j != 0 && levelMap[i + 1, j - 1] != 3 && levelMap[i + 1, j - 1] != 4)
                        {
                            rotation1 = new Vector3(0, 0, 270);
                            rotation2 = new Vector3(0, 0, 0);
                            rotation3 = new Vector3(0, 0, 180);
                            rotation4 = new Vector3(0, 0, 90);
                        }
                        else if (j != 0 && j < width - 1 && levelMap[i - 1, j + 1] != 3 && levelMap[i - 1, j + 1] != 4)
                        {
                            rotation1 = new Vector3(0, 0, 90);
                            rotation2 = new Vector3(0, 0, 180);
                            rotation3 = new Vector3(0, 0, 0);
                            rotation4 = new Vector3(0, 0, 270);
                        }
                        else
                        {
                            Debug.Log("test4");
                            rotation1 = new Vector3(0, 0, 0);
                            rotation2 = new Vector3(0, 0, 270);
                            rotation3 = new Vector3(0, 0, 90);
                            rotation4 = new Vector3(0, 0, 180);
                        }
                    }
                    else if (i != 0 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4))
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                        {
                            rotation1 = new Vector3(0, 0, 180);
                            rotation2 = new Vector3(0, 0, 90);
                            rotation3 = new Vector3(0, 0, 270);
                            rotation4 = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 90);
                            rotation2 = new Vector3(0, 0, 180);
                            rotation3 = new Vector3(0, 0, 0);
                            rotation4 = new Vector3(0, 0, 270);
                        }
                    }
                    else
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                        {

                            rotation1 = new Vector3(0, 0, 270);
                            rotation2 = new Vector3(0, 0, 0);
                            rotation3 = new Vector3(0, 0, 180);
                            rotation4 = new Vector3(0, 0, 90);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 0);
                            rotation2 = new Vector3(0, 0, 270);
                            rotation3 = new Vector3(0, 0, 90);
                            rotation4 = new Vector3(0, 0, 180);
                        }
                    }
                    mapList.Add(Instantiate(insideCorner, new Vector3(j, -i, 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(insideCorner, new Vector3(width * 2 - j - 1, -i, 0f), Quaternion.Euler(rotation2)));
                    mapList.Add(Instantiate(insideCorner, new Vector3(j, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation3)));
                    mapList.Add(Instantiate(insideCorner, new Vector3(width * 2 - j - 1, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation4)));
                }
                //Inside Wall
                else if (levelMap[i, j] == 4)
                {
                    if (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4 || levelMap[i - 1, j] == 7)
                    {
                        if (levelMap[i, j - 1] != 3 && levelMap[i, j - 1] != 4 && levelMap[i, j - 1] != 7)
                        {
                            rotation1 = new Vector3(0, 0, 0);
                        }
                        else if (j == width - 1)
                        {
                            rotation1 = new Vector3(0, 0, 90);
                        }
                        else if (levelMap[i, j + 1] != 3 && levelMap[i, j + 1] != 4 && levelMap[i, j + 1] != 7)
                        {
                            rotation1 = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 90);
                        }
                    }
                    else
                    {
                        rotation1 = new Vector3(0, 0, 90);
                    }
                    mapList.Add(Instantiate(insideWall, new Vector3(j, -i, 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(insideWall, new Vector3(width * 2 - j - 1, -i, 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(insideWall, new Vector3(j, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(insideWall, new Vector3(width * 2 - j - 1, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation1)));
                }
                //Normal Pellet
                else if (levelMap[i, j] == 5)
                {
                    mapList.Add(Instantiate(normalPellet, new Vector3(j, -i, 0f), Quaternion.identity));
                    mapList.Add(Instantiate(normalPellet, new Vector3(width * 2 - j - 1, -i, 0f), Quaternion.identity));
                    mapList.Add(Instantiate(normalPellet, new Vector3(j, -(height * 2 - i - 2), 0f), Quaternion.identity));
                    mapList.Add(Instantiate(normalPellet, new Vector3(width * 2 - j - 1, -(height * 2 - i - 2), 0f), Quaternion.identity));
                }
                //Power Pellet
                else if (levelMap[i, j] == 6)
                {
                    mapList.Add(Instantiate(powerPellet, new Vector3(j, -i, 0f), Quaternion.identity));
                    mapList.Add(Instantiate(powerPellet, new Vector3(width * 2 - j - 1, -i, 0f), Quaternion.identity));
                    mapList.Add(Instantiate(powerPellet, new Vector3(j, -(height * 2 - i - 2), 0f), Quaternion.identity));
                    mapList.Add(Instantiate(powerPellet, new Vector3(width * 2 - j - 1, -(height * 2 - i - 2), 0f), Quaternion.identity));
                }
                //Junction
                else if (levelMap[i, j] == 7)
                {
                    if (i != 0 && (levelMap[i - 1, j] == 1 || levelMap[i - 1, j] == 2))
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                        {
                            rotation1 = new Vector3(0, 0, 180);
                            rotation2 = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 180);
                            rotation2 = new Vector3(0, 0, 0);
                        }
                    }
                    else if (i != 0 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4))
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                        {
                            rotation1 = new Vector3(0, 0, 90);
                            rotation2 = new Vector3(0, 0, 270);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 90);
                            rotation2 = new Vector3(0, 0, 270);
                        }
                    }
                    else if (i < height - 1 && (levelMap[i + 1, j] == 1 || levelMap[i + 1, j] == 2))
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                        {
                            rotation1 = new Vector3(0, 0, 0);
                            rotation2 = new Vector3(0, 0, 180);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 0);
                            rotation2 = new Vector3(0, 0, 180);
                        }
                    }
                    else
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                        {
                            rotation1 = new Vector3(0, 0, 270);
                            rotation2 = new Vector3(0, 0, 90);
                        }
                        else
                        {
                            rotation1 = new Vector3(0, 0, 270);
                            rotation2 = new Vector3(0, 0, 90);
                        }
                    }
                    mapList.Add(Instantiate(junction, new Vector3(j, (float)(i - 0.4), 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(junction, new Vector3(width * 2 - j - 1, (float)(i - 0.4), 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(junction, new Vector3(j, -(float)(height * 2 - i - 2 - 0.4), 0f), Quaternion.Euler(rotation2)));
                    mapList.Add(Instantiate(junction, new Vector3(width * 2 - j - 1, -(float)(height * 2 - i - 2 - 0.4), 0f), Quaternion.Euler(rotation2)));
                    mapList[mapList.Count - 3].transform.localScale = new Vector3(1, -1, 1);
                    mapList[mapList.Count - 2].transform.localScale = new Vector3(1, -1, 1);
                }
            }
        }
    }

    void rotation()
    {

    }
}
