using System;

namespace NHibernate.Sessions.Operations
{
	public interface IDatabases
	{
		ISessionManager SessionManager { get; }

		T Query<T>(IDatabaseQuery<T> query);
		T Query<T>(Func<ISessionManager, T> query);
		T Query<T>(ICachedDatabaseQuery<T> query);
		T Query<T>(Func<ISessionManager, T> query, Action<CacheConfig> configureCache);
		TResult Query<T, TResult>(Func<ISessionManager, T> query, Func<T, TResult> transform, Action<CacheConfig> configureCache);

		void Command(IDatabaseCommand command);
		void Command(Action<ISessionManager> command);
		T Command<T>(IDatabaseCommand<T> command);
		T Command<T>(Func<ISessionManager, T> command);
	}
}