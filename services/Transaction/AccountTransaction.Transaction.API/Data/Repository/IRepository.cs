using AccountTransaction.Transaction.API.Models.Interface;

namespace AccountTransaction.Transaction.API.Data.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindById(int? id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<T> Insert(T domain);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<T> Update(T domain);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<T> Delete(T domain);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<List<T>> FindAll();
    }
}
