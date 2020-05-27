using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    List<List<List<Block>>> chunk = new List<List<List<Block>>>();

    CentralRenderController CRC;

    public GameObject[] blockTypes;
    
    // Start is called before the first frame update
    void Start()
    {
        CRC = FindObjectOfType<CentralRenderController>();
        GenerateChunk();
        InvokeRepeating("CheckDistance", 1, 0.11f);
    }


    void CheckDistance()
    {
        GameObject playerObj = FindObjectOfType<Player>().gameObject;

        if (Vector3.Distance(playerObj.transform.position, new Vector3(this.transform.position.x, playerObj.transform.position.y, this.transform.position.z)) < CRC.maxRender)
        {
            for (int i = 0; i < chunk.Count; i++)
            {
                for (int x = 0; x < chunk[i].Count; x++)
                {
                    for (int j = 0; j < chunk[i][x].Count; j++)
                    {
                        if (Vector3.Distance(chunk[i][x][j].blockObject.transform.position, playerObj.transform.position)
                            < CRC.maxRender && Vector3.Distance(new Vector3(0, chunk[i][x][j].blockObject.transform.position.y, 0),
                            new Vector3(0, playerObj.transform.position.y, 0)) < CRC.maxVerticalRender)
                        {
                            chunk[i][x][j].blockObject.SetActive(true);
                        }
                        else
                        {
                            chunk[i][x][j].blockObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    void GenerateChunk()
    {
        List<List<List<Block>>> tmpChunk = new List<List<List<Block>>>();
        for (int x = 0; x < 16; x++)
        {
            List<List<Block>> chunket = new List<List<Block>>();
            tmpChunk.Add(chunket);
            for (int z = 0; z < 16; z++)
            {
                List<Block> chunketo = new List<Block>();
                tmpChunk[x].Add(chunketo);

                for (int y = 0; y < 30; y++)
                {
                    Block thisBlock = new Block();
                    thisBlock.blockType =  Mathf.FloorToInt(UnityEngine.Random.Range(1, 3));

                    tmpChunk[x][z].Add(thisBlock);
                    thisBlock.blockObject = Instantiate(blockTypes[thisBlock.blockType], this.transform.position + new Vector3(x * 1, y * 1, z * 1), this.transform.rotation);
                }
            }
        }

        chunk = tmpChunk;
        
    }
}
