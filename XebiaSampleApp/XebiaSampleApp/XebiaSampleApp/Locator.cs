
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using XebiaSampleApp.ViewModel;
using Autofac;
using XebiaSampleApp.Service;

namespace XebiaSampleApp
{
    public static class Locator
    {

        private static IContainer _container;

        private static ILifetimeScope _scope;


        public static readonly BindableProperty AutoWireViewModelProperty =
               BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(Locator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(Locator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(Locator.AutoWireViewModelProperty, value);
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

            //var viewModel = new PersonsPageViewModel();
            var viewModel = _scope.Resolve(viewModelType);
             view.BindingContext = viewModel;
        }


        public static void RegisterDependencies()
        {

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<PersonsPageViewModel>();
            containerBuilder.RegisterType<PersonDetailPageViewModel>();

            containerBuilder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            containerBuilder.RegisterType<PersonService>().As<IPersonService>().SingleInstance();


            if(_container !=null)
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
    }
}
