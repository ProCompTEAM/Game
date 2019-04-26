using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.IO;

using GameServer.level.chunk;

namespace GameServer.level
{
	public class LevelsProvider : task.AsyncTask
	{
		public const int AUTOSAVE_TIMEOUT_S = 10;
		public readonly bool Available = false;
		
		public const int LEVEL_FORMAT_VERSION = 1;
		
		public static string LevelsDirectory
		{
			get { return Environment.CurrentDirectory + @"\levels\"; }
		}
		
		public LevelsProvider() : base("Levels Provider")
		{
			if(Server.Properties.GetProperty("save-levels") == utils.Config.SWITCH_ON) 
			{
				if(!Directory.Exists(LevelsDirectory))
					Directory.CreateDirectory(LevelsDirectory);
				
				Open();
				
				Available = true;
			}
		}

		protected override void Run(params object[] args)
		{
			while(true) 
			{
				foreach(Level level in Server.Levels) Save(level);
				
				Thread.Sleep(AUTOSAVE_TIMEOUT_S * 1000);
			}
		}
		
		static BinaryFormatter binFormat = new BinaryFormatter();
		
		public static void Save(Level level)
		{
			string dir = LevelsDirectory + level.Name.ToLower() + @"\";
			
			if(!Directory.Exists(dir)) Directory.CreateDirectory(dir);
			
			foreach(Chunk chunk in level.GetChunks())
			{
				//File format .BRL{version} = Building Race Level + VERSION of LevelStruct
				string file = dir + chunk.OffsetX + "." + chunk.OffsetY + ".brl" + LEVEL_FORMAT_VERSION;
				
				using (Stream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
				{
				    binFormat.Serialize(fs, chunk);
				}
			}
			
			Data.Debug("Saved level: " + level.Name);
		}
		
		public static Level Load(string name)
		{
			string dir = LevelsDirectory + name.ToLower() + @"\";
			
			if(Directory.Exists(dir))
			{
				Level level = new Level(name);
				
				foreach(string file in Directory.GetFiles(dir))
				{
					if(Path.GetExtension(file) == (".brl" + LEVEL_FORMAT_VERSION))
					{
						using (Stream fStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None))
						{
						    Chunk chunk = (Chunk) binFormat.Deserialize(fStream);
						    
						    level.SetChunk(chunk);
						}
					}
				}
				
				return level;
			}
			else return null;
		}
	}
}
