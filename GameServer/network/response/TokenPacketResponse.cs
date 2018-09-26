﻿using System;
using GameServer.network;

namespace GameServer.network.response
{
	public class TokenPacketResponse : network.Packet
	{
		Random random = new Random();
		
		public TokenPacketResponse(string Raw, string Address) : base(Raw, Address)
		{
			Id = Network.TOKEN_PACKET;
			
			InitializeAsResponse();
			
			SetData("token", GetToken());
		}
			
		public override string GetName()
		{
			return "Token Packet Response";
		}
		
		public string GetToken()
		{
			char[] letters_up = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
			char[] letters_down = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
			int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 9};
			char[] token = new char[20];
			int number_random = 0;
			for(int i = 0; i < 20; i++)
			{
				number_random = random.Next(1, 4);
				if(number_random == 1)
					token[i] = letters_up[random.Next(0, letters_up.Length)];
				else if(number_random == 2)
					token[i] = letters_down[random.Next(0, letters_down.Length)];
				else if(number_random == 3)
					token[i] = Convert.ToChar(numbers[random.Next(0, numbers.Length)].ToString());
			}
			return new string(token);
		}
	}
}
