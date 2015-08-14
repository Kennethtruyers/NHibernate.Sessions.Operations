using System;

namespace NHibernate.Sessions.Operations
{
	public class Databases : IDatabases
	{
		readonly IDatabaseQueryCache _databaseQueryCache;

		public Databases(ISessionManager sessionManager, IDatabaseQueryCache databaseQueryCache = null)
		{
			SessionManager = sessionManager;
			_databaseQueryCache = databaseQueryCache;
		}

		public ISessionManager SessionManager { get; private set; }

		public T Query<T>(IDatabaseQuery<T> query)
		{
			return query.Execute(SessionManager);
		}

		public T Query<T>(Func<ISessionManager, T> query)
		{
			return new FunctionDatabaseQuery<T>(query).Execute(SessionManager);
		}

		public T Query<T>(ICachedDatabaseQuery<T> query)
		{
			return query.Execute(SessionManager, _databaseQueryCache);
		}

		public T Query<T>(Func<ISessionManager, T> query, Action<CacheConfig> configureCache)
		{

			return new FunctionalCachedDatabaseQuery<T>(query, configureCache).Execute(SessionManager);
		}

		public TResult Query<T, TResult>(Func<ISessionManager, T> query, Func<T, TResult> transform, Action<CacheConfig> configureCache)
		{
			return new FunctionTransformedCachedDatabaseQuery<T, TResult>(query, transform, configureCache).Execute(SessionManager);
		}

		public void Command(IDatabaseCommand command)
		{
			command.Execute(SessionManager);
		}

		public void Command(Action<ISessionManager> command)
		{
			new FunctionalDatabaseCommand(command).Execute(SessionManager);
		}

		public T Command<T>(IDatabaseCommand<T> command)
		{
			return command.Execute(SessionManager);
		}

		public T Command<T>(Func<ISessionManager, T> command)
		{
			return new FunctionalDatabaseCommand<T>(command).Execute(SessionManager);
		}
	}
}
