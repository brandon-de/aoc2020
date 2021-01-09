using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace AOC_10
{
    class Program
    {
        private static int[] _nums;

        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"AOC10.txt");
            _nums = new int[lines.Length + 1];

            for (var i = 0; i < lines.Length; i++)
            {
                _nums[i+ 1] = Convert.ToInt32(lines[i]);
            }

            Array.Sort(_nums);


            Part1();
            //Console.ReadLine();

            var cache = new Dictionary<string, int>();
            var x = Splits(_nums.ToList(), cache);
            //Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            var numDiffOfThree = 1;
            var numDiffOfOne = 0;
            for (var i = 0; i < _nums.Length; i++)
            {
                if (i == _nums.Length - 1)
                {
                    continue;
                }

                int diff = _nums[i + 1] - _nums[i];

                switch (diff)
                {
                    case 1:
                        numDiffOfOne++;
                        break;
                    case 3:
                        numDiffOfThree++;
                        break;
                }
            }

            Console.WriteLine($"Part 1 answer: {numDiffOfOne * numDiffOfThree}");
        }

        private static void Part2()
        {
            var masterList= new List<List<int>>();
            var currentList = new List<int>();

            Part2_BuildList(currentList, 0, masterList);
        }

        private static int Combos(int index, Dictionary<int, int> cache)
        {
            if (index == _nums.Length - 1)
            {
                return 1;
            }

            if (cache.ContainsKey(index))
            {
                return cache[index];
            }

            int ans = 0;
            for (int i = index + 1; i < _nums.Length; i++)
            {
                var currentNum = _nums[index];
                var nextNum = _nums[i];
                if (_nums[i] - _nums[index] <= 3)
                {
                    ans += Combos(i, cache);
                }
            }

            cache.Add(index, ans);
            return ans;
        }

        private static int Splits(List<int> nums, Dictionary<string, int> cache)
        {
            var key = String.Join(',', nums);

            if (cache.ContainsKey(key))
            {
                return cache[key];
            }

            var result = 1;
            for (var i = 1; i < nums.Count - 1; i++)
            {
                var lowerBound= nums[i - 1];
                var higherBound = nums[i + 1];
                var toBeRemoved = nums[i];
                if (nums[i + 1] - nums[i - 1] <= 3)
                {
                    List<int> splits = new List<int>() {nums[i - 1]}.Concat(nums.Skip(i + 1)).ToList();
                    result += Splits(splits, cache);
                }
            }

            cache.Add(key, result);
            return result;
        }

        private static void Part2_BuildList(List<int> currentList, int index, List<List<int>> masterList)
        {
            for (var i = index; i < _nums.Length; i++)
            {
                // if end break loop and add to list
                if (i == _nums.Length - 1)
                {
                    if (_nums[^1] - currentList[^1] <= 3 && _nums[^1] - currentList[^1] >= 1)
                    {
                        currentList.Add(_nums[^1]);
                        masterList.Add(currentList.ToList());
                    }

                    continue;
                }

                // Validate before adding
                if (currentList.Count > 0 && (_nums[i] - currentList[^1] > 3 || _nums[i] - currentList[^1] < 1))
                {
                    break;
                }
                currentList.Add(_nums[i]);

                // look ahead for valid splitting branches and recurse
                for (var p = 1; p <= 3; p++)
                {
                    
                    int? diff = i + p >= _nums.Length ? (int?) null : _nums[i + p] - _nums[i];

                    if (diff is null)
                    {
                        continue;
                    }
                     
                    var lookAheadValue = _nums[i + p];
                    var currentIndexValue = _nums[i];

                    if (diff >= 1 && diff <= 3)
                    {
                        Part2_BuildList(currentList, i + p, masterList);
                    }

                    // reset currentList back to where it was before recursive branching
                    // TODO: NEED TO RESET i too
                    currentList.RemoveRange(index, currentList.Count - index);
                    //i = currentList.Count;
                }
            }
        }
    }
}