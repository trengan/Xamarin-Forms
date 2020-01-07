using Autofac;
using RockPaperScissors.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using RockPaperScissors.ViewModel;
namespace RockPaperScissors
{
    public static class Container
    {
        private static IContainer _container;

        private static ILifetimeScope _scope;

        public static readonly BindableProperty AutoWireViewModelProperty =
               BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(Container), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(Container.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(Container.AutoWireViewModelProperty, value);
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".View.", ".ViewModel.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _scope.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }

        public static void RegisterDependencies()
        {

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ChooseMatchViewModel>();
            containerBuilder.RegisterType<PlayMatchViewModel>();

            containerBuilder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

            if (_container != null)
            {
                _container.Dispose();
            }

            _container = containerBuilder.Build();
            _scope = _container.BeginLifetimeScope();


        }

        public static TViewModel Resolve<TViewModel>()
        {
            return _scope.Resolve<TViewModel>();
        }

        public static ILifetimeScope ForTest_SetContainer(IContainer container)
        {
            _container = container;
            _scope = _container.BeginLifetimeScope();
            return _scope;
        }
    }
}

