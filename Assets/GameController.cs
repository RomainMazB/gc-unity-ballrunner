using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnableBlocksList;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private List<GameObject> blocksList;
    public IEnumerable<GameObject> BlocksList => blocksList;
    private float _lastAnalysedX;
    public const float MinY = -1;
    private const float MaxY = 2;
    private const float BlocksBefore = 3;
    private const float BlocksAfter = 3;
    private const float BlocksSize = 3;

    private void Update()
    {
        SpawnBlockIfNeeded();
    }

    private void LateUpdate()
    {
        RemoveInvisibleBlock();
    }

    /// <summary>
    /// Removes blocks that go out of the screen
    /// </summary>
    private void RemoveInvisibleBlock()
    {
        var firstBlock = blocksList.First();

        if (firstBlock && firstBlock.transform.position.x > characterTransform.position.x - BlocksAfter * BlocksSize) return;

        Destroy(firstBlock);
        blocksList.RemoveAt(0);
    }

    /// <summary>
    /// Spawn a block whenever it's needed, meaning when BlocksBefore is not respected anymore
    /// </summary>
    private void SpawnBlockIfNeeded()
    {
        var lastSpawnedBlockPosition = blocksList.Last().transform.position;
        var characterXPosition = characterTransform.position.x;

        var distanceToTheEnd = Mathf.Floor(characterXPosition + BlocksBefore * BlocksSize);
        // Delay : Run the analyze only once the character position is [BlocksBefore] blocks far from the last analyze  
        if (_lastAnalysedX + BlocksSize >= distanceToTheEnd) return;

        _lastAnalysedX = distanceToTheEnd;

        // Abort if we want an empty space but assuring we don't have 2 spaces
        var spawningBlockX = (!(lastSpawnedBlockPosition.x + BlocksSize < _lastAnalysedX - 1) && Random.Range(0, 2) != 1) ? distanceToTheEnd : distanceToTheEnd + 1;

        // Get a random block from the prefabs' list
        var blockToSpawn = spawnableBlocksList.ElementAt(Random.Range(0, spawnableBlocksList.Count));

        var spawningBlockYPosition = CalculateNextBlockYPosition(lastSpawnedBlockPosition.y);
        var spawningBlock = Instantiate(blockToSpawn, new Vector3(spawningBlockX, spawningBlockYPosition, 0), Quaternion.identity);
        blocksList.Add(spawningBlock);
    }

    private static float CalculateNextBlockYPosition(float lastBlockYPosition)
    {
        // There is 1/2 chance the next block Y position is the same as the previous one
        if (Random.Range(0, 2) != 1)
            return lastBlockYPosition;

        // Calculate a ratio between the y range space to the last spawned block position
        // The more the block is high, the more the next block as a chance to go down
        var ratio = (lastBlockYPosition - MinY) / (MaxY - MinY);
        var sign = Random.Range(0f, 1f) < ratio ? -1 : +1;

        return lastBlockYPosition + (Random.Range(0, 2) != 1 ? 1f : .5f) * sign;
    }
}
