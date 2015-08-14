using System;

namespace NHibernate.Sessions.Operations
{
	public abstract class CachedDatabaseQuery<T> : AbstractCachedDatabaseQuery<T>, ICachedDatabaseQuery<T>
	{
		public virtual T Execute(ISessionManager sessionManager, IDatabaseQueryCache databaseQueryCache = null)
		{
			return GetDatabaseResult(sessionManager, databaseQueryCache);
		}
	}

	internal class FunctionalCachedDatabaseQuery<T> : CachedDatabaseQuery<T>
	{
		readonly Func<ISessionManager, T> _query;
		readonly Action<CacheConfig> _configureCache;

		public FunctionalCachedDatabaseQuery(Func<ISessionManager, T> query, Action<CacheConfig> configureCache)
		{
			_query = query;
			_configureCache = configureCache;
		}

		protected override void ConfigureCache(CacheConfig cacheConfig)
		{
			if(_configureCache != null)
				_configureCache(cacheConfig);
		}

		protected override T QueryDatabase(ISessionManager sessionManager)
		{
			return _query(sessionManager);
		}
	}
}