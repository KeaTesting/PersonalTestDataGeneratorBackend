namespace PersonalTestDataGeneratorBackend.Generators
{
    public interface IRandomGenerator
    {
        int Next(int minValue, int maxValue);
        int Next(int maxValue);
    }
}
