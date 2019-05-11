using System;

namespace GameServer.level
{
	public static class Compressor
	{
		public static string Compress(string data)
        {
            string[] chunks = data.Split(';');
            string result = "";

            foreach (string chunk in chunks)
            {
                string[] parts = chunk.Split(':');
                string content = parts[1].Substring(1, parts[1].Length - 2);
                string[] ids = content.Split(',');

                result += parts[0] + ':' + '[';
                int count = 0;
                for (int i = 0; i < ids.Length - 1; i++)
                {
                    if (ids[i] == ids[i + 1])
                        count++;
                    else
                    {
                        if (count > 0)
                        {
                            result += ids[i] + '>' + (count + 1) + ',';
                            count = 0;
                        }
                        else
                        {
                        	
                            result += ids[i] + ',';
                            count = 0;
                        }
                    }
                    if (i == ids.Length - 2)
                    {
                        if (count > 1)
                        {
                            result += ids[i] + '>' + (count + 1);
                            count = 0;
                        }
                        else
                        {
                            result += ids[i];
                            count = 0;
                        }
                    }
                }
                result += "];";
            }
            result = result.Remove(result.Length - 1);

            return result;
        }



		public static string Decompress(string data)
        {
            string[] chunks = data.Split(';');
            string result = "";

            foreach (string chunk in chunks)
            {
                string[] parts = chunk.Split(':');
                string content = parts[1].Substring(1, parts[1].Length - 2);
                string[] ids = content.Split(',');

                result += parts[0] + ':' + '[';

                for (int i = 0; i < ids.Length; i++)
                {
                    string[] id = ids[i].Split('>');

                    if (id.Length > 1)
                    {
                        for (int j = 0; j < int.Parse(id[1]); j++)
                        {
                            result += id[0] + ',';
                        }
                    }
                    else
                    {
                        result += id[0] + ',';
                    }
                }
                result = result.Remove(result.Length - 1);
                result += "];";
            }
            result = result.Remove(result.Length - 1);
            return result;
        }
	}
}
