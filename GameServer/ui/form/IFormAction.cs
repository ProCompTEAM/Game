using System;

namespace GameServer.ui.form
{
	public interface IFormAction
	{
		void FormAction(string FormID, string ActionType, string ControlName);
	}
}
