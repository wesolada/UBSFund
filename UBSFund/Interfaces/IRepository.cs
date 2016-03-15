namespace UBSFund.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository
    {
        bool Add<T>(T entity) where T : class;
    }
}