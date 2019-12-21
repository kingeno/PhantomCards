using System;
using UnityEngine;

public class TileData
{
	public enum TileType
	{
		Wall,
		Water
	}

	private TileType m_Type;

	public TileData(TileType type)
	{
		m_Type = type;
	}
}

public class MapData
{
	private TileData[] m_Tiles;
	private Vector2Int m_Size;
	private Vector2Int m_Offset;

	public MapData(Vector2Int size, Vector2Int offset)
	{
		m_Size = size;
		m_Offset = offset;
		m_Tiles = new TileData[m_Size.x * m_Size.y];
	}

	public TileData GetTile(Vector2Int position)
	{
		int i = MapPositionToIndex(position);
		if(i >= 0 && i < m_Tiles.Length)
		{
			return m_Tiles[i];
		}
		else
		{
			return null;
		}
	}

	public void SetTile(TileData tileData, Vector2Int position)
	{
		int i = MapPositionToIndex(position);

		if(i < 0 || i >= m_Tiles.Length)
		{
			Debug.LogError($"Failed to set tile at position {position}: invalid position.");
			return;
		}

		if(m_Tiles[i] != null)
		{
			Debug.LogError($"Failed to set tile at position {position}: tile already exists.");
			return;
		}

		if(tileData == null)
		{
			Debug.LogWarning("Trying to set null tile");
		}

		m_Tiles[i] = tileData;
	}

	private int MapPositionToIndex(Vector2Int position)
	{
		Vector2Int localPosition = new Vector2Int(position.x - m_MapOffset.x, position.y - m_MapOffset.y);
		if(localPosition.x < 0 || localPosition.x >= m_MapSize.x)
		{
			return -1;
		}

		return localPosition.y * m_MapSize.x + localPosition.x;
	}
}
