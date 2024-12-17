namespace AdventOfCode2024.Days;

public class Day9
{
    public static long CalculateChecksum(string input)
    {
        return new Disk(input).CompactBlocks().GetChecksum();
    }

    public static long CalculateChecksumViaFileMove(string input)
    {
        return new Disk(input).CompactBlocksViaFileMove().GetChecksum();
    }
}

public class Disk
{
    private List<Block> InitialBlocks { get; } = [];
    private List<Block> ExpandedBlocks { get; } = [];

    public Disk(string input)
    {
        var isFree = false;
        var id = 0;
        foreach (var c in input.ToCharArray())
        {
            InitialBlocks.Add(new Block(!isFree ? id++ : 0, int.Parse(c.ToString()), isFree));
            isFree = !isFree;
        }

        ExpandBlocks();
    }

    public Disk CompactBlocks()
    {
        var firstFreeIndex = ExpandedBlocks.FindIndex(x => x.IsFree);
        var lastNotFreeIndex = ExpandedBlocks.FindLastIndex(x => !x.IsFree);
        while (firstFreeIndex < lastNotFreeIndex)
        {
            (ExpandedBlocks[firstFreeIndex], ExpandedBlocks[lastNotFreeIndex]) = (ExpandedBlocks[lastNotFreeIndex], ExpandedBlocks[firstFreeIndex]);
            firstFreeIndex = ExpandedBlocks.FindIndex(x => x.IsFree);
            lastNotFreeIndex = ExpandedBlocks.FindLastIndex(x => !x.IsFree);
        }

        return this;
    }

    public Disk CompactBlocksViaFileMove()
    {
        var currentId = InitialBlocks.Max(x => x.Id);
        while (currentId > 0)
        {
            var fileBlockIndex = InitialBlocks.FindIndex(x => !x.IsFree && x.Id == currentId);
            var freeBlockIndex = InitialBlocks.FindIndex(x => x.IsFree && InitialBlocks[fileBlockIndex].Size <= x.Size);

            if (freeBlockIndex >= 0 && freeBlockIndex < fileBlockIndex)
            {
                var fileBlock = InitialBlocks[fileBlockIndex];

                InitialBlocks[fileBlockIndex] = new Block(0, fileBlock.Size, true);
                InitialBlocks[freeBlockIndex] = new Block(0, InitialBlocks[freeBlockIndex].Size - fileBlock.Size, true);
                InitialBlocks.Insert(freeBlockIndex, fileBlock);
            }

            currentId--;
        }

        ExpandBlocks();
        return this;
    }

    public long GetChecksum()
    {
        return ExpandedBlocks.Select((x, i) => (long)i * x.Id).Sum();
    }

    private void ExpandBlocks()
    {
        ExpandedBlocks.Clear();
        foreach (var block in InitialBlocks)
        {
            for (var i = 0; i < block.Size; i++)
            {
                ExpandedBlocks.Add(block with { Size = 1 });
            }
        }
    }
}

public record struct Block(int Id, int Size, bool IsFree);