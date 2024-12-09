using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9
{
    internal class FileSystem
    {
        public readonly int[] sectors;
        public readonly int maxFileId;
        public FileSystem(string input)
        {
            List<int> sectorList = new();
            bool isFree = false;
            int id = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                int count = c - '0';
                if (isFree)
                {
                    for (int j = 0; j < count; j++)
                    {
                        sectorList.Add(-1);
                    }
                }
                else
                {
                    int fileId = maxFileId = id++;
                    for (int j = 0; j < count; j++)
                    {
                        sectorList.Add(fileId);
                    }
                }
                isFree = !isFree;
            }
            sectors = sectorList.ToArray();
        }

        public int GetFirstFree()
        {
            return Array.IndexOf(sectors, -1);
        }

        private int GetBlockLength(int index)
        {
            int length = 0;
            int value = sectors[index];
            for (int i = index; i < sectors.Length; i++)
            {
                if (sectors[i] == value)
                {
                    length++;
                }
                else
                {
                    break;
                }
            }
            return length;
        }

        public (int index, int length) GetFirstFreeWithMinLength(int minLength)
        {
            for (int i = 0; i < sectors.Length; i++)
            {
                if (sectors[i] == -1)
                {
                    int length = GetBlockLength(i);
                    if (length >= minLength)
                    {
                        return (i, length);
                    }
                    else
                    {
                        i += length - 1;
                    }
                }
            }
            throw new Exception("none found");
        }

        public (int index, int fileId) GetLastOccupied()
        {
            for (int i = sectors.Length - 1; i >= 0; i--)
            {
                var fileId = sectors[i];
                if (fileId >= 0)
                {
                    return (i, fileId);
                }
            }
            return (-1, -1);
        }

        public (int index, int length) GetFileIndexAndLen(int fileId)
        {
            var index = Array.IndexOf(sectors, fileId);
            int length = 0;
            for (int i = index; i < sectors.Length; i++)
            {
                if (sectors[i] == fileId)
                {
                    length++;
                }
                else
                {
                    break;
                }
            }
            return (index, length);
        }

        public void BunchUp()
        {
            while (true)
            {
                var firstFreeIndex = GetFirstFree();
                var lastOccupied = GetLastOccupied();
                if (firstFreeIndex > lastOccupied.index)
                {
                    break;
                }

                sectors[firstFreeIndex] = lastOccupied.fileId;
                sectors[lastOccupied.index] = -1;
            }
        }

        public void Defrag()
        {
            for (int fileId = maxFileId; fileId >= 0; fileId--)
            {
                var fileInfo = GetFileIndexAndLen(fileId);
                try
                {
                    var firstFree = GetFirstFreeWithMinLength(fileInfo.length);
                    if(firstFree.index> fileInfo.index)
                    {
                        continue;
                    }
                    for (int i = 0; i < fileInfo.length; i++)
                    {
                        sectors[firstFree.index + i] = fileId;
                        sectors[fileInfo.index + i] = -1;
                    }
                }
                catch (Exception e)
                {
                    continue; //skip file
                }
            }
        }

        public long GetChecksum()
        {
            long sum = 0;
            for (int i = 0; i < sectors.Length; i++)
            {
                var fileId = sectors[i];
                if (fileId >= 0)
                {
                    sum += fileId * i;
                }
            }
            return sum;
        }

        public override string ToString()
        {
            string tmp = "";
            for (int i = 0; i < sectors.Length; i++)
            {
                var sector = sectors[i];
                if (sector < 0)
                {
                    tmp += ". ";
                }
                else
                {
                    tmp += sector + " ";
                }
            }
            return tmp;
        }
    }
}
