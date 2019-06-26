using NodaTime;

namespace Common.Domain.Clocks
{
    public class Clock
    {
        private static volatile IClock _clock;

        protected Clock()
        {
        }

        protected static IClock ClockInstance
        {
            get => _clock;
            set => _clock = value;
        }

        public static void SetClock(IClock clock)
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