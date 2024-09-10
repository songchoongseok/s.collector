using Listener.Domains.Rules.ValueObjects;

namespace Listener.Domains.Rules;

public sealed class Rule(long id, ScheduleType scheduleType, int scheduleTime, DateTime lastWorkingTime)
{
    public long Id { get; } = id;
    public ScheduleType ScheduleType { get; } = scheduleType;
    public int ScheduleTime { get; } = scheduleTime;
    public DateTime LastWorkingTime { get; } = lastWorkingTime;

    public DateTime NextWorkingTime()
        => ScheduleType switch
        {
            ScheduleType.Second => LastWorkingTime.AddSeconds(ScheduleTime),
            ScheduleType.Minute => LastWorkingTime.AddMinutes(ScheduleTime),
            ScheduleType.Hour => LastWorkingTime.AddHours(ScheduleTime),
            ScheduleType.Day => LastWorkingTime.AddDays(ScheduleTime),
            _ => throw new ArgumentException(nameof(ScheduleType)),
        };
}