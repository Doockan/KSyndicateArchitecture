using CodeBase.Infrastructure.Services;

namespace CodeBase.Services.Randomizer
{
  public interface IRandomService : IService
  {
    int Next(int minValue, int maxValue);
  }
}