using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Extensions
{
    public static class AuthorRepositoryExtensions
    {
        public static IQueryable<Author> ResolveQuery(this IQueryable<Author> queryable, AuthorQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Name))
                queryable = queryable.Where(a => a.Name == query.Name);

            if (!string.IsNullOrWhiteSpace(query.Surname))
                queryable = queryable.Where(a => a.Surname == query.Surname);

                queryable = queryable.ResolveOrderBy(query);
                
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return queryable.Skip(skipNumber).Take(query.PageSize);
        }
        private static IQueryable<Author> ResolveOrderBy(this IQueryable<Author> queryable, AuthorQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.OrderBy))
                return queryable;

            if (query.IsDescending)
            {
                switch (query.OrderBy.ToLower())
                {
                    case "name":
                        queryable = queryable.OrderBy(a => a.Name);
                        break;
                    case "surname":
                        queryable = queryable.OrderBy(a => a.Surname);
                        break;
                    default:
                        return queryable;
                }
            }
            else
            {
                switch (query.OrderBy.ToLower())
                {
                    case "name":
                        queryable = queryable.OrderByDescending(a => a.Name);
                        break;
                    case "surname":
                        queryable = queryable.OrderByDescending(a => a.Surname);
                        break;
                    default:
                        return queryable;
                }
            }
            return queryable;
        }
    }
}