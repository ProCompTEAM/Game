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
		
		public Item[] GetItems()
		{
			return Items.ToArray();
		}
		
		public void AddItem(Item item)
		{
			if(!IssetItem(item.ToInt32()) && Items.Count < MaxSlots) Items.Add(item);
		}
		
		public void TakeItem(Item item)
		{
			Items.Remove(item);
		}
		
		public bool TakeItem(int itemId, int subAmount)
		{
			foreach(Item i in Items)
				if(i.ToInt32() == itemId) 
			{
				if(i.Count - subAmount >= 0) 
				{
					i.Sub(subAmount);
					return true;
				}
			}
			return false;
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
		
		public int CalculateMass()
		{
			int mass = 0;
			
			foreach(Item i in Items) mass += (i.ToInt32() * i.Count);
			
			return mass;
		}
		
		public override string ToString()
		{
			string result = "";
			
			foreach(Item i in Items)
			{
				result += i + "," + i.Count + "," + i.Meta + ";";
			}
			
			return result.Substring(0, result.Length - 1);
		}
		
		public void SetDevelopmentKit()
		{
			for(int i = 1; i < 30; i++) Items.Add(new Item(i, 999));
		}
	}
}
