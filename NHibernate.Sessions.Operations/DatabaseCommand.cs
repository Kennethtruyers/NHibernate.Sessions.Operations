using System;

namespace NHibernate.Sessions.Operations
{
	public abstract class DatabaseCommand : DatabaseOperation, IDatabaseCommand
	{
		public abstract void Execute(ISessionManager sessionManager);
	}

	public abstract class DatabaseCommand<T> : DatabaseOperation, IDatabaseCommand<T>
	{
		public abstract T Execute(ISessionManager sessionManager);
	}

	internal class FunctionalDatabaseCommand : DatabaseCommand
	{
		readonly Action<ISessionManager> _command;

		public FunctionalDatabaseCommand(Action<ISessionManager> command)
		{
			_command = command;
		}

		public override void Execute(ISessionManager sessionManager)
		{
			_command(sessionManager);
		}
	}

	internal class FunctionalDatabaseCommand<T> : DatabaseCommand<T>
	{
		readonly Func<ISessionManager, T> _command;

		public FunctionalDatabaseCommand(Func<ISessionManager, T> command)
		{
			_command = command;
		}

		public override T Execute(ISessionManager sessionManager)
		{
			return _command(sessionManager);
		}
	}
}
