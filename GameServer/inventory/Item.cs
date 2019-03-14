using System;

namespace GameServer.inventory
{
	public class Item : utils.Identifier
	{	
		int CurrentCount;
		public string Meta;
		
		public Item(int id, int count = 1, string meta = "") : base(id)
		{
			if(id == iSimple) count = 1;
			
			if(meta == "") meta = locale.Strings.GetDefaultItemMeta(id);
			
			CurrentCount = count;
			Meta = meta;
		}
		
		public int Count
		{
			get { return CurrentCount; }
		}
		
		public int Add(int amount)
		{
			CurrentCount += amount;
			return CurrentCount;
		}
		
		public int Sub(int amount)
		{
			CurrentCount -= amount;
			if(CurrentCount < 0) CurrentCount = 0;
			return CurrentCount;
		}
		
		public int Floor()
		{
			return CurrentCount = 0;
		}
	}
}
