public interface IGrowable
{
    abstract void IncrementProgress(int t);
    abstract bool IsMature();
}