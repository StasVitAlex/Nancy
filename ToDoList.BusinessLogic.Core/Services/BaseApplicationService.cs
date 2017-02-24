using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DataAccessLayer.Repositories.Interfaces;

namespace ToDoList.BusinessLogic.Core.Services
{
    public class BaseApplicationService
    {
        private readonly IUnitOfWork UnitOfWork;

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.GetMapper();
            }
        }

        protected BaseApplicationService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            this.UnitOfWork?.Dispose();
        }

        protected virtual TResult InvokeInUnitOfWorkScope<TResult>(Func<IUnitOfWork, TResult> func)
        {
            var result = default(TResult);

            this.TryInvokeServiceActionInUnitOfWorkScope(
                work =>
                {
                    result = func.Invoke(work);
                });

            return result;
        }

        protected virtual void InvokeInUnitOfWorkScope(Action<IUnitOfWork> action)
        {
            this.TryInvokeServiceActionInUnitOfWorkScope(action.Invoke);
        }

        private void TryInvokeServiceActionInUnitOfWorkScope(Action<IUnitOfWork> action)
        {
            this.TryInvokeServiceAction(
                () =>
                {

                    action.Invoke(UnitOfWork);

                });
        }

        private void TryInvokeServiceAction(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (InvalidOperationException exception)
            {
                throw exception;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("cannot invoke service action", exception);
            }
        }
    }
}
