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
    private int[,] levelMap = new int[,]
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
        /*{
            {1, 2, 7, 7, 2 },
            {2, 5, 5, 5, 5 },
            {2, 5, 3, 3, 5 },
            {2, 5, 3, 3, 5 },
            {2, 5, 5, 5, 5 },
        };*/
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
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                //Outside Corner
                if (levelMap[i, j] == 1)
                {
                    List<int> rotations = new List<int>();
                    if (i != 0 && (levelMap[i - 1, j] == 1 || levelMap[i - 1, j] == 2))
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                            rotations.AddRange(rotationOrder(2));
                        else
                            rotations.AddRange(rotationOrder(-3));
                    }
                    else
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                            rotations.AddRange(rotationOrder(4));
                        else
                            rotations.AddRange(rotationOrder(-1));
                    }
                    mapList.Add(Instantiate(outsideCorner, new Vector3(j, -i, 0f), Quaternion.Euler(0, 0, rotations[0])));
                    mapList.Add(Instantiate(outsideCorner, new Vector3(width * 2 - j - 1, -i, 0f), Quaternion.Euler(0, 0, rotations[1])));
                    mapList.Add(Instantiate(outsideCorner, new Vector3(j, -(height * 2 - i - 2), 0f), Quaternion.Euler(0, 0, rotations[2])));
                    mapList.Add(Instantiate(outsideCorner, new Vector3(width * 2 - j - 1, -(height * 2 - i - 2), 0f), Quaternion.Euler(0, 0, rotations[3])));
                }
                //Outside Wall
                else if (levelMap[i, j] == 2)
                {
                    int[] test = adjacent(i, j);
                    if ((test[1] == test[2] || test[0] != test[3] || (test[0] == test[3] && test[0] == 0) || ((test[1] == 1 || test[1] == 3) && (test[2] == 2 || test[2] == 4 || test[2] == 3))) 
                        && (test[1] == 1 || test[2] == 1 || test[1] == 3))
                    {
                        rotation1 = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        rotation1 = new Vector3(0, 0, 0);
                    }
                    mapList.Add(Instantiate(outsideWall, new Vector3(j, -i, 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(outsideWall, new Vector3(width * 2 - j - 1, -i, 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(outsideWall, new Vector3(j, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(outsideWall, new Vector3(width * 2 - j - 1, -(height * 2 - i - 2), 0f), Quaternion.Euler(rotation1)));
                }
                //Inside Corner
                else if (levelMap[i, j] == 3)
                {
                    List<int> rotations = new List<int>();
                    if ((i != 0 && i < height - 1 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4) && (i != 0 && (levelMap[i + 1, j] == 3 || levelMap[i + 1, j] == 4))))
                    {
                        if (j != 0 && levelMap[i - 1, j - 1] != 3 && levelMap[i - 1, j - 1] != 4)
                            rotations.AddRange(rotationOrder(2));
                        else if (j != 0 && levelMap[i + 1, j - 1] != 3 && levelMap[i + 1, j - 1] != 4)
                            rotations.AddRange(rotationOrder(4));
                        else if (j != 0 && j < width - 1 && levelMap[i - 1, j + 1] != 3 && levelMap[i - 1, j + 1] != 4)
                            rotations.AddRange(rotationOrder(-3));
                        else
                            rotations.AddRange(rotationOrder(-1));
                    }
                    else if (i != 0 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4))
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                            rotations.AddRange(rotationOrder(2));
                        else
                            rotations.AddRange(rotationOrder(-3));
                    }
                    else
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                            rotations.AddRange(rotationOrder(4));
                        else
                            rotations.AddRange(rotationOrder(-1));
                    }
                    mapList.Add(Instantiate(insideCorner, new Vector3(j, -i, 0f), Quaternion.Euler(0, 0, rotations[0])));
                    mapList.Add(Instantiate(insideCorner, new Vector3(width * 2 - j - 1, -i, 0f), Quaternion.Euler(0, 0, rotations[1])));
                    mapList.Add(Instantiate(insideCorner, new Vector3(j, -(height * 2 - i - 2), 0f), Quaternion.Euler(0, 0, rotations[2])));
                    mapList.Add(Instantiate(insideCorner, new Vector3(width * 2 - j - 1, -(height * 2 - i - 2), 0f), Quaternion.Euler(0, 0, rotations[3])));
                }
                //Inside Wall
                else if (levelMap[i, j] == 4)
                {
                    int[] test = adjacent(i, j);
                    if ((test[1] == test[2] || test[0] != test[3] || (test[0] == test[3] && test[0] == 0)) && (test[1] == 2 || test[2] == 2))
                    {
                        rotation1 = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        rotation1 = new Vector3(0, 0, 0);
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
                    bool test = false;
                    if (i != 0 && (levelMap[i - 1, j] == 1 || levelMap[i - 1, j] == 2))
                    {
                        rotation1 = new Vector3(0, 0, 180);
                        rotation2 = new Vector3(0, 0, 0);
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                        {
                            test = true;
                        }
                    }
                    else if (i != 0 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4))
                    {
                        rotation1 = new Vector3(0, 0, 90);
                        rotation2 = new Vector3(0, 0, 270);
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                        {
                            test = true;
                        }
                    }
                    else if (i < height - 1 && (levelMap[i + 1, j] == 1 || levelMap[i + 1, j] == 2))
                    {
                        rotation1 = new Vector3(0, 0, 0);
                        rotation2 = new Vector3(0, 0, 180);
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                        {
                            test = true;
                        }
                    }
                    else
                    {
                        rotation1 = new Vector3(0, 0, 270);
                        rotation2 = new Vector3(0, 0, 90);
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                        {
                            test = true;
                        }
                    }
                    mapList.Add(Instantiate(junction, new Vector3(j, (float)(i - 0.4), 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(junction, new Vector3(width * 2 - j - 1, (float)(i - 0.4), 0f), Quaternion.Euler(rotation1)));
                    mapList.Add(Instantiate(junction, new Vector3(j, -(float)(height * 2 - i - 2 - 0.4), 0f), Quaternion.Euler(rotation2)));
                    mapList.Add(Instantiate(junction, new Vector3(width * 2 - j - 1, -(float)(height * 2 - i - 2 - 0.4), 0f), Quaternion.Euler(rotation2)));
                    junctionFlip(test);
                }
            }
        }
    }

    private int[] adjacent(int fromTop, int fromLeft)
    {
        int[] adjacents = new int[4];
        if (fromTop == 0)
            adjacents[0] = -1;
        else
            adjacents[0] = levelMap[fromTop - 1, fromLeft];
        if (fromLeft == 0)
            adjacents[1] = -1;
        else
            adjacents[1] = levelMap[fromTop, fromLeft - 1];
        if (fromLeft == width-1)
            adjacents[2] = -1;
        else
            adjacents[2] = levelMap[fromTop, fromLeft + 1];
        if (fromTop == height-1)
            adjacents[3] = -1;
        else
            adjacents[3] = levelMap[fromTop + 1, fromLeft];
        return wallType(adjacents);
    }

    private int[] wallType(int[] adjacents)
    {
        for (int i = 0; i < adjacents.GetLength(0); i++)
        {
            if (adjacents[i] == 1 || adjacents[i] == 2)
                adjacents[i] = 1;
            else if (adjacents[i] == 3 || adjacents[i] == 4)
                adjacents[i] = 2;
            else if (adjacents[i] == 7)
                adjacents[i] = 3;
            else if (adjacents[i] == -1)
                adjacents[i] = 4;
            else
                adjacents[i] = 0;
        }
        return adjacents;
    }

    private List<int> rotationOrder(int number)
    {
        List<int> order = new List<int>();
        while (order.Count < 4)
        {
            if (number == 1 || number == -1)
            {
                order.Add(0);
            }
            else if (number == 2 || number == -2)
            {
                order.Add(180);
            }
            else if (number == 3 || number == -3)
            {
                order.Add(90);
            }
            else
            {
                order.Add(270);
                if (number == 4)
                    number = 0;
                else
                    number = -4;
            }
            number++;
        }
        return order;
    }

    private void junctionFlip(bool indicator)
    {
        if (indicator)
        {
            mapList[mapList.Count - 3].transform.localScale = new Vector3(1, -1, 1);
            mapList[mapList.Count - 2].transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            mapList[mapList.Count - 4].transform.localScale = new Vector3(1, -1, 1);
            mapList[mapList.Count - 1].transform.localScale = new Vector3(1, -1, 1);
        }
    }
}
