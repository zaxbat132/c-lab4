using System;

namespace Lab4.TimeApp
{
    public class Time
    {
        private byte _hours;
        private byte _minutes;

        public byte Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                if (value > 23)
                {
                    throw new ArgumentOutOfRangeException("value", "Часы должны быть от 0 до 23.");
                }

                _hours = value;
            }
        }

        public byte Minutes
        {
            get
            {
                return _minutes;
            }
            set
            {
                if (value > 59)
                {
                    throw new ArgumentOutOfRangeException("value", "Минуты должны быть от 0 до 59.");
                }

                _minutes = value;
            }
        }

        public Time() : this(0, 0)
        {
        }

        public Time(byte hours, byte minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }

        public Time(Time other)
        {
            CheckNotNull(other);
            Hours = other.Hours;
            Minutes = other.Minutes;
        }

        public Time AddMinutes(uint minutes)
        {
            // long позволяет прибавить даже uint.MaxValue без переполнения.
            return FromTotalMinutes(GetTotalMinutes() + (long)minutes);
        }

        public override string ToString()
        {
            return Hours.ToString("D2") + ":" + Minutes.ToString("D2");
        }

        public static Time operator ++(Time time)
        {
            CheckNotNull(time);
            return time.AddMinutes(1);
        }

        public static Time operator --(Time time)
        {
            CheckNotNull(time);
            return FromTotalMinutes(time.GetTotalMinutes() - 1);
        }

        public static explicit operator byte(Time time)
        {
            CheckNotNull(time);
            return time.Hours;
        }

        public static implicit operator bool(Time time)
        {
            CheckNotNull(time);
            // Буквальное условие: оба поля должны быть ненулевыми.
            return time.Hours != 0 && time.Minutes != 0;
        }

        public static Time operator +(Time time, uint minutes)
        {
            CheckNotNull(time);
            return time.AddMinutes(minutes);
        }

        public static Time operator +(uint minutes, Time time)
        {
            return time + minutes;
        }

        public static Time operator -(Time time, uint minutes)
        {
            CheckNotNull(time);
            return FromTotalMinutes(time.GetTotalMinutes() - (long)minutes);
        }

        public static Time operator -(uint minutes, Time time)
        {
            CheckNotNull(time);
            // Левое число трактуется как количество минут от полуночи.
            return FromTotalMinutes((long)minutes - time.GetTotalMinutes());
        }

        private int GetTotalMinutes()
        {
            return Hours * 60 + Minutes;
        }

        private static Time FromTotalMinutes(long totalMinutes)
        {
            const int MinutesPerDay = 24 * 60;
            long normalized = totalMinutes % MinutesPerDay;
            if (normalized < 0)
            {
                normalized += MinutesPerDay;
            }

            return new Time((byte)(normalized / 60), (byte)(normalized % 60));
        }

        private static void CheckNotNull(Time time)
        {
            if (ReferenceEquals(time, null))
            {
                throw new ArgumentNullException("time");
            }
        }
    }
}