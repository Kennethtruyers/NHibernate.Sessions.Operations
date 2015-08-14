using System;

namespace NHibernate.Sessions.Operations
{
	public abstract class TransformedCachedDatabaseQuery<TDatabaseResult, TTransformedResult> : AbstractCachedDatabaseQuery<TDatabaseResult>, ICachedDatabaseQuery<TTransformedResult>
	{
		protected abstract TTransformedResult TransformDatabaseResult(TDatabaseResult databaseResult);

		public TTransformedResult Execute(ISessionManager sessionManager, IDatabaseQueryCache databaseQueryCache = null)
		{
			return TransformDatabaseResult(GetDatabaseResult(sessionManager, databaseQueryCache));
		}
	}

	internal class FunctionTransformedCachedDatabaseQuery<TDatabaseResult, TTransformedResult> : TransformedCachedDatabaseQuery<TDatabaseResult, TTransformedResult>
	{
		readonly Func<ISessionManager, TDatabaseResult> _query;
		readonly Func<TDatabaseResult, TTransformedResult> _transform;
		readonly Action<CacheConfig> _configureCache;

		public FunctionTransformedCachedDatabaseQuery(Func<ISessionManager, TDatabaseResult> query, Func<TDatabaseResult, TTransformedResult> transform, Action<CacheConfig> configureCache)
		{
			_query = query;
			_transform = transform;
			_configureCache = configureCache;
		}

		protected override void ConfigureCache(CacheConfig cacheConfig)
		{
			if (_configureCache != null)
				_configureCache(cacheConfig);
		}

		protected override TDatabaseResult QueryDatabase(ISessionManager sessionManager)
		{
			return _query(sessionManager);
		}

		protected override TTransformedResult TransformDatabaseResult(TDatabaseResult databaseResult)
		{
			return _transform(databaseResult);
		}
	}
}