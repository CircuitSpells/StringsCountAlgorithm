﻿namespace StringsCountAlgorithm;

class Program
{
    static void Main(string[] args)
    {
        
    }

    private class MinMaxHash
    {
        private Dictionary<string, int> StringToCountDict { get; set; } = [];
        private Dictionary<int, HashSet<string>> CountToStringDict { get; set; } = [];

        private int? maxCount = null;
        private int MaxCount
        {
            get => maxCount.HasValue ? (int)maxCount : throw new Exception("MaxCount has no value");
            set => maxCount = value;
        }

        private int? minCount = null;
        private int MinCount
        {
            get => minCount.HasValue ? (int)minCount : throw new Exception("MinCount has no value");
            set => minCount = value;
        }

        private void ResetCount()
        {
            maxCount = null;
            minCount = null;
        }

        private void InitCount()
        {
            maxCount = 1;
            minCount = 1;
        }

        private string? GetMaxKey() => CountToStringDict[MaxCount].FirstOrDefault();
        private string? GetMinKey() => CountToStringDict[MinCount].FirstOrDefault();

        private void Inc(string key)
        {
            if (StringToCountDict.ContainsKey(key))
            {
                int value = StringToCountDict[key] + 1;
                StringToCountDict[key] = value;
                CountToStringDictAddKey(key, value);
                CountToStringDictRemoveKey(key, value - 1);

                MaxCount = Math.Max(MaxCount, value);
            }
            else
            {
                int value = 1;
                StringToCountDict.Add(key, value);
                CountToStringDictAddKey(key, value);

                if (StringToCountDict.Count == 1)
                {
                    InitCount();
                }
            }
        }

        // "It is guaranteed that key exists in the data structure before the decrement."
        private void Dec(string key)
        {
            int value = StringToCountDict[key] - 1;
            if (value == 0)
            {
                StringToCountDict.Remove(key);
                CountToStringDictRemoveKey(key, 1);

                if (StringToCountDict.Count == 0)
                {
                    ResetCount();
                }
            }
            else
            {
                StringToCountDict[key] = value;
                CountToStringDictAddKey(key, value);
                CountToStringDictRemoveKey(key, value + 1);

                MinCount = Math.Min(MinCount, value);
            }
        }

        private void CountToStringDictAddKey(string key, int value)
        {
            if (CountToStringDict.ContainsKey(value))
            {
                CountToStringDict[value].Add(key);
            }
            else
            {
                CountToStringDict.Add(value, new HashSet<string>([key]));
            }
        }

        private void CountToStringDictRemoveKey(string key, int value)
        {
            // assume that the value exists
            CountToStringDict[value].Remove(key);
        }
    }
}
