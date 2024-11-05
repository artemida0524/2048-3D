using System.Collections.Generic;

public interface ITable
{
    ThrowingPlatformBase Platform { get; }
    List<WallBase> Walls { get; }
}