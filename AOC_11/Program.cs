namespace AOC_11
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Program
    {
        private const char _floor = '.';
        private const char _occupiedSeat = '#';
        private const char _emptySeat = 'L';

        public static void Main(string[] args)
        {
            var lines = File.ReadAllLines(@"AOC11.txt").ToList();

            var seats = lines.Select(line => line.ToCharArray().ToList()).ToList();

            Console.WriteLine($"Part 1 answer: {Part1(seats)}");

            Console.ReadLine();
        }

        private static int Part1(List<List<char>> oldSeatRows)
        {
            // initial copy
            var newSeatRows = oldSeatRows.Select(x => x.ToList()).ToList();

            var isMatch = false;
            while (!isMatch)
            {
                for (var rowIndex = 0; rowIndex < oldSeatRows.Count; rowIndex++)
                {
                    for (var seatIndex = 0; seatIndex < oldSeatRows[rowIndex].Count; seatIndex++)
                    {
                        char seat = oldSeatRows[rowIndex][seatIndex];

                        if (seat is _floor)
                        {
                            continue;
                        }

                        FindAdjacentSeats();

                        void FindAdjacentSeats()
                        {
                            var adjacentOccupiedSeats = 0;
                            var adjacentEmptySeats = 0;
                            for (var adjacentRowIndex = rowIndex - 1; adjacentRowIndex <= rowIndex + 1; adjacentRowIndex++)
                            {
                                for (var adjacentSeatIndex = seatIndex - 1; adjacentSeatIndex <= seatIndex + 1; adjacentSeatIndex++)
                                {
                                    // current seat skip
                                    if (adjacentSeatIndex == seatIndex && adjacentRowIndex == rowIndex)
                                    {
                                        continue;
                                    }

                                    // out of bounds, assume empty
                                    if (adjacentRowIndex > oldSeatRows.Count - 1 || adjacentRowIndex < 0 ||
                                        adjacentSeatIndex > oldSeatRows[adjacentRowIndex].Count - 1 ||
                                        adjacentSeatIndex < 0 || adjacentSeatIndex > seatIndex + 1)
                                    {
                                        adjacentEmptySeats++;
                                    }
                                    else
                                    {
                                        switch (oldSeatRows[adjacentRowIndex][adjacentSeatIndex])
                                        {
                                            case _occupiedSeat:
                                                adjacentOccupiedSeats++;
                                                break;
                                            case _emptySeat:
                                                adjacentEmptySeats++;
                                                break;
                                            case _floor:
                                                adjacentEmptySeats++;
                                                break;
                                            default:
                                                break;
                                        }
                                    }

                                    switch (seat)
                                    {
                                        case _occupiedSeat when adjacentOccupiedSeats == 4:
                                            newSeatRows[rowIndex][seatIndex] = _emptySeat;
                                            return;
                                        case _emptySeat when adjacentEmptySeats == 8:
                                            newSeatRows[rowIndex][seatIndex] = _occupiedSeat;
                                            return;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (CompareSeatArrangements())
                {
                    isMatch = true;
                }
                else
                {
                    oldSeatRows = newSeatRows.Select(x => x.ToList()).ToList();
                }

                bool CompareSeatArrangements()
                {
                    for (var i = 0; i < oldSeatRows.Count; i++)
                    {
                        for (var j = 0; j < oldSeatRows[i].Count; j++)
                        {
                            if (oldSeatRows[i][j] != newSeatRows[i][j])
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                }
            }

            return newSeatRows.SelectMany(newSeatRow => newSeatRow).Count(c => c is _occupiedSeat);
        }
    }
}
