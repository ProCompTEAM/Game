using System;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Text;

namespace GameServer.locale
{
	public static class Strings
	{
		public static ResourceManager LangResources = new ResourceManager("GameServer.locale.Lang", Assembly.GetExecutingAssembly());
		
		static string Lang = LangResources.GetString("default");
		
		static string[] SupportedLanguages = { "en", "ru" };
		
		public static string LangCode
		{
			get { return Lang; }
		}
		
		public const char STRINGS_SEPERATOR = '=';
		
		static string[] StringsItems = {};
		static string[] StringsLang = {};
		
		public static void ExecuteLang(string code)
		{
			if(Array.IndexOf(SupportedLanguages, code) != -1)
			{
				Lang = code;
				
				//loading inventory messages
				object data = LangResources.GetObject("inventory-" + Lang);
				StringsItems = Encoding.UTF8.GetString((byte[]) data).Split('\n');
				
				//loading all locale
				StringsLang = LangResources.GetString("lang-" + Lang).Split('\n');
			}
			else Server.ServerCritical(From("lang.error") + code);
		}
		
		public static string From(string label, params object[] args)
		{
			foreach(string s in StringsLang)
				if(s.Split(STRINGS_SEPERATOR)[0] == label)
					return string.Format(s.Split(STRINGS_SEPERATOR)[1], args).Replace('\r', ' ');
			return "lang." + label;
		}
		
		public static string GetDefaultItemMeta(int itemId)
		{
			foreach(string s in StringsItems)
				if(s.Split(STRINGS_SEPERATOR)[0] == itemId.ToString())
					return s.Split(STRINGS_SEPERATOR)[1].Replace('\r', ' ');
			return itemId.ToString();
		}
	}
}
