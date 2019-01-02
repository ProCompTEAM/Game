using System;
using System.Collections.Generic;

namespace GameServer.inventory
{
	public class Inventory
	{
		List<Item> Items = new List<Item>();
		
		public string Name;
		public readonly int MaxSlots;
		
		public Inventory(int maxSlots, string name = "")
		{
			MaxSlots = maxSlots;
			
			Name = name;
		}
		
		public void AddItem(Item item)
		{
			if(!IssetItem(item.ToInt32()) && Items.Count < MaxSlots) Items.Add(item);
		}
		
		public void TakeItem(Item item)
		{
			Items.Remove(item);
		}
		
		public bool IssetItem(Item item)
		{
			return (Items.IndexOf(item) != -1);
		}
		
		public bool IssetItem(int itemId)
		{
			foreach(Item i in Items)
				if(i.ToInt32() == itemId) return true;
			return false;
		}
		
		public void Clear()
		{
			Items.Clear();
		}
	}
}
