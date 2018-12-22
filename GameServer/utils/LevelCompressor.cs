using System;

namespace GameServer.utils
{
	public static class LevelCompressor
	{
		public static string Compress(string data)
        {
            string[] data_str = data.Split(' ');
            string new_data = "";
 
            for (int i = 0; i < data_str.Length; i++)
            {
                int count = 0;
                if (i == data_str.Length - 1)
                    new_data += data_str[i];
                for (int j = i + 1; j < data_str.Length; j++)
                {
                    if (data_str[i] != data_str[j] || j == data_str.Length - 1)
                    {
                    	if ((count > 2) || (count > 1 && Convert.ToInt32(data_str[i]) > 9))
                        {
                        	new_data += string.Format("{0}[{1}] ", data_str[i], count);
                            i += count;
                        }
                    	else new_data += data_str[i] + " ";
                    	
                        break;
                    }
                    count++;
                }
            }
 
            return new_data;
        }
		
		
        public static string Decompress(string data)
        {
            char[] data_char = data.ToCharArray();
            string[] str = data.Split(' ');
            string[] compress_num = new string[data_char.Length];
            string new_data = "";
 
            for (int i = 0, count = 0; i < data_char.Length; i++)
            {
                if(data_char[i] == '[')
                {
                    int amount = 0;
                    for(int j = i + 1; j < data_char.Length; j++, amount++)
                    {
                        if(data_char[j] == ']')
                        {
                            count++;
                            break;
                        }
                        compress_num[count] += data_char[j];
                    }
                }
            }
 
 
            for(int i = 0, count = 0; i < str.Length; i++)
            {
                if (str[i].IndexOf('[') != -1)
                {
                    for (int j = 0; j < Convert.ToInt32(compress_num[count]) + 1; j++)
                    {
                        new_data += str[i].Substring(0, str[i].IndexOf('[')) + " ";
                    }
                    count++;
                }
                else if (str[i].IndexOf('[') == -1)
                {
                   
                    new_data += str[i] + " ";
                }
            }
 
            return new_data.Substring(0, new_data.Length - 1);
        }
	}
}