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


            Console.WriteLine($"Part 1 answer: {Part1()}");
            Console.WriteLine($"Part 2 answer: {Part2(_nums.ToList(), new Dictionary<string, int>())}");
            Console.ReadLine();
        }

        private static int Part1()
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

            return numDiffOfOne * numDiffOfThree;
        }

        private static int Part2(List<int> nums, Dictionary<string, int> cache)
        {
            var key = string.Join(',', nums);

            if (cache.ContainsKey(key))
            {
                return cache[key];
            }

            var result = 1;
            for (var i = 1; i < nums.Count - 1; i++)
            {
                if (nums[i + 1] - nums[i - 1] <= 3)
                {
                    var splits = new List<int>() {nums[i - 1]}.Concat(nums.Skip(i + 1)).ToList();
                    result += Part2(splits, cache);
                }
            }

            cache.Add(key, result);
            return result;
        }
    }
}