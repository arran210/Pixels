using System;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
  public Chunk[,] chunkMap = new Chunk[chunkMapWidth, chunkMapHeight];
  private const int chunkMapWidth = 1024;
  private const int chunkMapHeight = 1024;
  private const int XZeroIndex = 512;
  private const int YZeroIndex = 512;


  public void CreateChunk(int x, int y)
  {
    chunkMap[x + XZeroIndex, y + YZeroIndex] = new GameObject($"chunk {x}, {y}").AddComponent<Chunk>();
    chunkMap[x + XZeroIndex, y + YZeroIndex].Init(x, y);
  }

  public Chunk GetChunk(int x, int y)
  {
    if (x + XZeroIndex < 0 || x + XZeroIndex >= chunkMapWidth || y + YZeroIndex < 0 ||
        y + YZeroIndex >= chunkMapHeight) return null;
    return chunkMap[x + XZeroIndex, y + YZeroIndex];
  }

  public void DestroyWorld()
  {
    for (int x = 0; x < chunkMapWidth; x++)
    {
      for (int y = 0; y < chunkMapHeight; y++)
      {
        if (chunkMap[x, y])
        {
          Destroy(chunkMap[x, y].gameObject);
          chunkMap[x, y] = null;
        }
      }
    }
  }
}