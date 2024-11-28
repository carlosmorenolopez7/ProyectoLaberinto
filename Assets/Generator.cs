using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public static Generator gen;
    public int xMax, zMax;
    public int limit;
    public GameObject piece, labyrinthPiece;
    public GameObject[,] map;

    // Start is called before the first frame update
    void Start()
    {
        gen = this;
        map = new GameObject[xMax, zMax];
        //StartCoroutine(GenMapBasic());
        //StartCoroutine(GenMapMedium(0, 0));
        GenerateFirstFloor();
    }

    public IEnumerator GenMapBasic()
    {
        for (int x = 0; x < xMax; x++)
        {
            for (int z = 0; z < zMax; z++)
            {
                if (Random.Range(0, 100) < 50)
                {
                    Instantiate(piece, new Vector3(x * 5, 0, z * 5), Quaternion.identity);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }

    public IEnumerator GenMapMedium(int x, int z)
    {
        limit--;
        Transform newPiece = Instantiate(piece, new Vector3(x, 0, z), Quaternion.identity).transform;
        yield return new WaitForEndOfFrame();
        if (limit > 0)
        {
            bool complete = false;
            int cont = 0;
            while (!complete && cont < 50)
            {
                int num = Random.Range(0, 100);
                if (num < 25 && !Physics.Raycast(newPiece.position, newPiece.forward, 6))
                    {StartCoroutine(GenMapMedium(x, z + 5)); complete = true;}
                else if (num < 50 && !Physics.Raycast(newPiece.position, newPiece.forward * -1, 6))
                    {StartCoroutine(GenMapMedium(x, z - 5)); complete = true;}
                else if (num < 75 && !Physics.Raycast(newPiece.position, newPiece.right, 6))
                    {StartCoroutine(GenMapMedium(x + 5, z)); complete = true;}
                else if(!Physics.Raycast(newPiece.position, newPiece.right * -1, 6))
                    {StartCoroutine(GenMapMedium(x - 5, z)); complete = true;}
            }
        }
    }

    public void GenerateFirstFloor()
    {
        LabyrinthPiece newPiece = Instantiate(labyrinthPiece, new Vector3((xMax / 2) * 5, 0, (zMax / 2) * 5), Quaternion.identity).GetComponent<LabyrinthPiece>();
        newPiece.n = true; newPiece.s = true; newPiece.e = true; newPiece.w = true;
        newPiece.x = xMax / 2; newPiece.z = zMax / 2;
        map[xMax / 2, zMax / 2] = newPiece.gameObject; 
    }

    public void GenerateNextPiece(int x, int z)
    {
        if (map[x, z] == null)
        {
            LabyrinthPiece newPiece = Instantiate(labyrinthPiece, new Vector3(x * 5, 0, z * 5), Quaternion.identity).GetComponent<LabyrinthPiece>();
            newPiece.x = x; newPiece.z = z;
            map[x, z] = newPiece.gameObject;
        }
    }
}
