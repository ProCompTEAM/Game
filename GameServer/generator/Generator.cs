﻿using System;

namespace GameServer.generator
{
	public class Generator
	{
		public const int MATRIX_SIZE = 100;
		
		protected int[,] matrix;
		
		public Generator()
		{
			matrix = new int[MATRIX_SIZE, MATRIX_SIZE];
			
			for(int i = 0; i < MATRIX_SIZE; i++)
				for(int j = 0; j < MATRIX_SIZE; j++)
					Set(i, j, 0);
		}
		
		
		public int[,] GetMatrix()
		{
			return matrix;
		}
		
		public void Set(int x, int y, int tileCode)
		{
			matrix[x,y] = tileCode;
		}
		
		public int Get(int x, int y)
		{
			return matrix[x,y];
		}
		
		public int CalculateMass()
		{
			int mass = 0;
			
			for(int i = 0; i < MATRIX_SIZE; i++)
				for(int j = 0; j < MATRIX_SIZE; j++)
					mass += Get(i, j);
			
			return mass;
		}
		
		public override string ToString()
		{
			string raw = "";
			
			for(int i = 0; i < MATRIX_SIZE; i++)
			{
				if(i > 0) raw += ";";
				
				for(int j = 0; j < MATRIX_SIZE; j++)
				{
					int id = Get(i, j);
					
					if(j > 0) raw += "+";
					raw += id;
				}
			}
			
			return raw;
		}

		
		public void OutInConsole()
		{
			Console.WriteLine("[LEVEL GENERATOR CODE]");
			
			string[] lines = this.ToString().Split(';');
			foreach(string l in lines) Console.WriteLine(" => " + l);
			
			Console.WriteLine("[/LEVEL GENERATOR CODE]");
		}
		
		public virtual void Generate()
		{
			
		}
	}
}
