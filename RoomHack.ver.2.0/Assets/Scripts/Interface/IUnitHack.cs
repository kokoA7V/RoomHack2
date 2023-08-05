public interface IUnitHack
{
    public bool hacked { get; set; }
    void StatusDisp();
    enum PuzzleGimmick
    {
        atomicHearts,
        dbd,
        osu,
        valt,
    }
}