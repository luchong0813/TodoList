using Microsoft.EntityFrameworkCore.Query;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using TodoList.Application.Common.Interfaces;

namespace TodoList.Application.Common
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        private readonly Expression<Func<T, bool>> _Criteria;

        public SpecificationBase()
        {

        }
        public SpecificationBase(Expression<Func<T,bool>> criteria)
        {
            _Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; private set; }
        public Func<IQueryable<T>, IIncludableQueryable<T, object>> Include { get; private set; }
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        public List<string> IncludeStrings { get; } = new();

        public void AddCritera(Expression<Func<T, bool>> critera) {
            //if (critera is not null) {
            //    Criteria.AndAlso(critera);
            //    return;
            //}
            //Criteria = critera;
        }
    }
}
