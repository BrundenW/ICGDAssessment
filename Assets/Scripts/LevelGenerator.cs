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
    public GameObject parent;
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
    private List<GameObject> segmentsList = new List<GameObject>();
    private float width;
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        oldMap.SetActive(false);
        height = levelMap.GetLength(0);
        width = levelMap.GetLength(1);
        Vector3 rotation;
        for (int i = 0; i < height; i++)
        {
            if (i == height - 1)
            {
                segmentsList.Add(Instantiate(parent, new Vector3(0, -(2*height-2), 0f), Quaternion.identity));
                segmentsList.Add(Instantiate(parent, new Vector3((2*width - 1), -(2*height-2), 0f), Quaternion.identity));
                segmentsList[0].transform.localScale = new Vector3(1, -1, 1);
                segmentsList[1].transform.localScale = new Vector3(-1, -1, 1);
            }
            for (int j = 0; j < width; j++)
            {
                //Outside Corner
                if (levelMap[i, j] == 1)
                {
                    if (i != 0 && (levelMap[i - 1, j] == 1 || levelMap[i - 1, j] == 2))
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                            rotation = new Vector3(0, 0, 180);
                        else
                            rotation = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                            rotation = new Vector3(0, 0, 270);
                        else
                            rotation = new Vector3(0, 0, 0);
                    }
                    duplicate(outsideCorner, new Vector3(j, -i, 0f), rotation, true);
                }
                //Outside Wall
                else if (levelMap[i, j] == 2)
                {
                    int[] test = adjacent(i, j);
                    if ((test[1] == test[2] || test[0] != test[3] || (test[0] == test[3] && test[0] == 0) || ((test[1] == 1 || test[1] == 3) && (test[2] == 2 || test[2] == 4 || test[2] == 3)))
                        && (test[1] == 1 || test[2] == 1 || test[1] == 3))
                    {
                        rotation = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        rotation = new Vector3(0, 0, 0);
                    }
                    duplicate(outsideWall, new Vector3(j, -i, 0f), rotation, true);
                }
                //Inside Corner
                else if (levelMap[i, j] == 3)
                {
                    if ((i != 0 && i < height - 1 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4) && (i != 0 && (levelMap[i + 1, j] == 3 || levelMap[i + 1, j] == 4))))
                    {
                        if (j != 0 && levelMap[i - 1, j - 1] != 3 && levelMap[i - 1, j - 1] != 4)
                            rotation = new Vector3(0, 0, 180);
                        else if (j != 0 && levelMap[i + 1, j - 1] != 3 && levelMap[i + 1, j - 1] != 4)
                            rotation = new Vector3(0, 0, 270);
                        else if (j != 0 && j < width - 1 && levelMap[i - 1, j + 1] != 3 && levelMap[i - 1, j + 1] != 4)
                            rotation = new Vector3(0, 0, 90);
                        else
                            rotation = new Vector3(0, 0, 0);
                    }
                    else if (i != 0 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4))
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                            rotation = new Vector3(0, 0, 180);
                        else
                            rotation = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                            rotation = new Vector3(0, 0, 270);
                        else
                            rotation = new Vector3(0, 0, 0);
                    }
                    duplicate(insideCorner, new Vector3(j, -i, 0f), rotation, true);

                }
                //Inside Wall
                else if (levelMap[i, j] == 4)
                {
                    int[] test = adjacent(i, j);
                    if ((test[1] == test[2] || test[0] != test[3] || (test[0] == test[3] && test[0] == 0)) && (test[1] == 2 || test[2] == 2))
                    {
                        rotation = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        rotation = new Vector3(0, 0, 0);
                    }
                    duplicate(insideWall, new Vector3(j, -i, 0f), rotation, true);
                }
                //Normal Pellet
                else if (levelMap[i, j] == 5)
                {
                    duplicate(normalPellet, new Vector3(j, -i, 0f), new Vector3(0, 0, 0), true);
                }
                //Power Pellet
                else if (levelMap[i, j] == 6)
                {
                    duplicate(powerPellet, new Vector3(j, -i, 0f), new Vector3(0, 0, 0), true);
                }
                //Junction
                else if (levelMap[i, j] == 7)
                {
                    bool test = false;
                    if (i != 0 && (levelMap[i - 1, j] == 1 || levelMap[i - 1, j] == 2))
                    {
                        rotation = new Vector3(0, 0, 180);
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                            test = true;
                    }
                    else if (i != 0 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4))
                    {
                        rotation = new Vector3(0, 0, 90);
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                            test = true;
                    }
                    else if (i < height - 1 && (levelMap[i + 1, j] == 1 || levelMap[i + 1, j] == 2))
                    {
                        rotation = new Vector3(0, 0, 0);
                        if (j != 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                            test = true;
                    }
                    else
                    {
                        rotation = new Vector3(0, 0, 270);
                        if (j != 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                            test = true;
                    }
                    duplicate(junction, new Vector3(j, -(float)(i+0.4), 0f), rotation, test);
                }
            }
        }
        segmentsList.Add(Instantiate(parent, new Vector3((2*width - 1), 0f, 0f), Quaternion.identity));
        segmentsList[2].transform.localScale = new Vector3(-1, 1, 1);
    }

    private void duplicate(GameObject segment, Vector3 position, Vector3 rotation, bool junctionFlip)
    {
        mapList.Add(Instantiate(segment, position, Quaternion.Euler(rotation)));
        if (!junctionFlip)
            mapList[mapList.Count - 1].transform.localScale = new Vector3(1, -1, 1);
        mapList[mapList.Count - 1].transform.parent = parent.transform;
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
}
