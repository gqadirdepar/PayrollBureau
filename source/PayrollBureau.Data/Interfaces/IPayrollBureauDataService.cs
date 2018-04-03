using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PayrollBureau.Data.Entities;
using PayrollBureau.Data.Models.Ordering;
using PayrollBureau.Data.Models.Paging;


namespace PayrollBureau.Data.Interfaces
{
    public interface IPayrollBureauDataService
    {
        #region Create
        T Create<T>(T t) where T : class;
        IEnumerable<T> Create<T>(IEnumerable<T> t) where T : class;
        #endregion

        #region Retrieve
        Employer RetrieveEmployerByUserId(string userId);
        T Retrieve<T>(int Id) where T : class;
        List<T> Retrieve<T>(Expression<Func<T, bool>> predicate) where T : class;
        T RetrieveByPredicate<T>(Expression<Func<T, bool>> predicate) where T : class;
        #endregion

        #region Update
        T UpdateEntityEntry<T>(T t) where T : class;
        PagedResult<T> RetrievePagedResult<T>(Expression<Func<T, bool>> predicate, List<OrderBy> orderBy = null, Paging paging = null) where T : class;
        #endregion

        #region Delete
        void Delete<T>(int Id) where T : class;
        void Delete<T>(Expression<Func<T, bool>> predicate) where T : class;
        void DeleteRange<T>(Expression<Func<T, bool>> predicate) where T : class;
        #endregion
    }
}
