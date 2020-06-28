using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject InitialBlock;
    public GameObject[] TerrainBlocks;

    private List<GameObject> CurrentBlocks = new List<GameObject>();
    private int BlockIndex = 0;

	void Start ()
    {
		for(int i=0; i<4; i++)
        {
            GenerateBlock();
        }
	}

    void GenerateBlock()
    {
        var index = Random.Range(0, TerrainBlocks.Length);
        var prefab = TerrainBlocks[index];

        if (BlockIndex < 2)
            prefab = InitialBlock;

        var block = Instantiate(prefab);
        CurrentBlocks.Add(block);

        block.transform.position = Vector2.right * BlockIndex * 8f;
        GetComponent<BoxCollider2D>().transform.position = Vector2.right * (BlockIndex-2) * 8f;
        BlockIndex++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GenerateBlock();

        var block = CurrentBlocks.First();
        Destroy(block);
        CurrentBlocks.RemoveAt(0);
    }
}
