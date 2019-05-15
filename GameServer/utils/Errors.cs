using System;

namespace GameServer.utils
{
	public static class Errors
	{
		public const string InvalidData = "error.data.invalid";
		public const string InvalidToken = "error.session.invalid";
		public const string InvalidName = "error.player.badname";
		public const string PlayerAlreadyExists = "error.player.ingame";
		public const string PlayerBanned = "error.player.banned";
		
		public static string Format(string errorObject, string errorMeta)
		{
			return string.Format("error.{0}.{1}", errorObject, errorMeta);
		}
	}
}
