using System;

namespace NHibernate.Sessions.Operations
{
	public abstract class DatabaseQuery<T> : DatabaseOperation, IDatabaseQuery<T>
	{
		public abstract T Execute(ISessionManager sessionManager);
	}

	internal class FunctionDatabaseQuery<T> : DatabaseQuery<T>
	{
		readonly Func<ISessionManager, T> _query;

		public FunctionDatabaseQuery(Func<ISessionManager, T> query)
		{
			_query = query;
		}

		public override T Execute(ISessionManager sessionManager)
		{
			return _query(sessionManager);
		}
	}
}
