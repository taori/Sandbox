using System;
using Microsoft.Extensions.DependencyInjection;

namespace ComposableWebApplication.SDK.Core
{
	public interface IFeature
	{
		string Name { get; }

		void Register(IServiceCollection serviceProvider);
	}
}
