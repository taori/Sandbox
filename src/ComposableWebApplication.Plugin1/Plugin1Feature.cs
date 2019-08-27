﻿using System;
using ComposableWebApplication.SDK.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ComposableWebApplication.Plugin1
{
	public class Plugin1Feature : IFeature
	{
		/// <inheritdoc />
		public string Name => "Plugin 1";

		/// <inheritdoc />
		public void Register(IServiceCollection serviceProvider)
		{
		}
	}
}