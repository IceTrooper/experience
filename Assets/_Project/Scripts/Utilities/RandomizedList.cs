public abstract class RandomizedList
{

}

public abstract class RandomizedList<T> : RandomizedList, IRandomizedList<T>
{
    public abstract T GetRandomItem();
}

public interface IRandomizedList<T>
{
    public abstract T GetRandomItem();
}