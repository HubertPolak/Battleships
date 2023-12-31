﻿using Battleships.App.Extensions;
using Battleships.UI.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddGameServices()
    .AddUIServices()
    .AddSerilogLogging()
    .BuildServiceProvider();

var uiService = serviceProvider.GetService<IUIGenerator>();

uiService?.RenderGame();
