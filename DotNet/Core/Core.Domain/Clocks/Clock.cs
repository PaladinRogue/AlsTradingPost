using NodaTime;

namespace PaladinRogue.Libray.Core.Domain.Clocks
{
    public static class Clock
    {
        private static volatile IClock _clock;

        private static IClock ClockInstance
        {
            get => _clock;
            set => _clock = value;
        }

        public static void SetClock(this IClock clock)
        {
            if (ClockInstance == null)
            {
                ClockInstance = clock;
            }
        }

        public static Instant Now()
        {
            if (ClockInstance == null)
            {
                throw new ClockNotSetException();
            }

            return ClockInstance.GetCurrentInstant();
        }
    }
}