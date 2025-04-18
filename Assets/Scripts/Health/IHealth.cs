// IHealth.cs
public interface IHealth
{
    float Current { get; }
    void Decrease(float value);
    void Increase(float value);
}