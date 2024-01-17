namespace StringsCountAlgorithm;

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
            get
            {
                return maxCount.HasValue ? (int)maxCount : throw new Exception("MaxCount has no value");
            }
            set
            {
                maxCount = value;
            }
        }

        private int? minCount = null;
        private int MinCount
        {
            get
            {
                return minCount.HasValue ? (int)minCount : throw new Exception("MinCount has no value");
            }
            set
            {
                minCount = value;
            }
        }

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
                    // give MaxCount and MinCount their initial values
                    MaxCount = 1;
                    MinCount = 1;
                }
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

        // "It is guaranteed that key exists in the data structure before the decrement."
        private void Dec(string key)
        {
            int value = StringToCountDict[key] - 1;
            if (value == 0)
            {
                StringToCountDict.Remove(key);
                CountToStringDictRemoveKey(key, 1);
            }
            else
            {
                StringToCountDict[key] = value;
                CountToStringDictAddKey(key, value);
                CountToStringDictRemoveKey(key, value + 1);
            }
        }

        private string? GetMaxKey() => CountToStringDict[MaxCount].FirstOrDefault();
        private string? GetMinKey() => CountToStringDict[MinCount].FirstOrDefault();
    }
}
