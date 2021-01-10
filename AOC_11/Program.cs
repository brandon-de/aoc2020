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
        }

        private static void Part1(List<List<char>> seatRows)
        {
            for (var rowIndex = 0; rowIndex < seatRows.Count; rowIndex++)
            {
                for (var seatIndex = 0; seatIndex < seatRows[rowIndex].Count; seatIndex++)
                {
                    char seat = seatRows[rowIndex][seatIndex];

                    if (seat is _floor)
                    {
                        continue;
                    }

                    Action work = () =>
                    {
                        int adjacentOccupiedSeats = 0;
                        int adjacentEmptySeats = 0;
                        for (var adjacentRowIndex = rowIndex - 1; adjacentRowIndex <= rowIndex + 1; adjacentRowIndex++)
                        {
                            for (var adjacentSeatIndex = seatIndex - 1;
                                adjacentSeatIndex <= seatIndex + 1;
                                adjacentSeatIndex++)
                            {
                                // out of bounds, assume empty
                                if (adjacentRowIndex > seatRows.Count - 1 || adjacentRowIndex < 0 ||
                                    adjacentSeatIndex < 0 || adjacentSeatIndex > seatIndex - 1)
                                {
                                    adjacentEmptySeats++;
                                    continue;
                                }

                                switch (seatRows[adjacentRowIndex][adjacentSeatIndex])
                                {
                                    case _occupiedSeat:
                                        adjacentOccupiedSeats++;
                                        break;
                                    case _emptySeat:
                                        adjacentEmptySeats++;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
