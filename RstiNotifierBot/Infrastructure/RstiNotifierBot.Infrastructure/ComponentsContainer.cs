namespace RstiNotifierBot.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autofac;
    using Autofac.Builder;
    using Autofac.Core;
    using RstiNotifierBot.Infrastructure.BO.Dto;

    public abstract class ComponentsContainer
    {
        private ContainerBuilder _containerBuilder;
        private IContainer _container;
        
        #region Public Members

        public void ConfigureContainer()
        {
            if (_container != null)
            {
                return;
            }

            _containerBuilder = new ContainerBuilder();

            var director = new Director();
            RegisterBuilders(director);
            director.Construct(this);

            _container = _containerBuilder.Build();
        }

        public void Register<TClassType>(bool isSingleInstance = false,
            params RegisterParameter[] parameters)
        {
            if (_container != null)
            {
                return;
            }

            var registeredType = _containerBuilder.RegisterType(typeof(TClassType));
            SetRegisterSettings(registeredType, isSingleInstance, parameters);
        }

        public void Register<TClassType, TInterfaceType>(bool isSingleInstance = false,
            params RegisterParameter[] parameters)
        {
            if (_container != null)
            {
                return;
            }

            var registeredType = _containerBuilder
                .RegisterType(typeof(TClassType))
                .As(typeof(TInterfaceType));

            SetRegisterSettings(registeredType, isSingleInstance, parameters);
        }

        #endregion

        #region Protected Members

        protected abstract void RegisterBuilders(Director director);

        protected T GetComponent<T>() => _container.Resolve<T>();

        #endregion

        #region Private Members

        private void SetRegisterSettings(
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registeredType,
            bool isSingleInstance = false,
            params RegisterParameter[] parameters)
        {
            if (isSingleInstance)
            {
                registeredType.SingleInstance();
            }

            if (parameters.Length != 0)
            {
                var namedParams = GetParamsList(parameters);
                registeredType.WithParameters(namedParams);
            }
        }

        private IEnumerable<Parameter> GetParamsList(params RegisterParameter[] parameters) =>
            parameters.Select(x => new NamedParameter(x.Name, x.Value)).ToList();

        #endregion
    }
}
