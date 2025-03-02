using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Extensions
{
    public static class AuthorRepositoryExtensions
    {
        public static IQueryable<Author> ResolveQuery(this IQueryable<Author> queryable, AuthorQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Name))
                queryable.Where(a => a.Name == query.Name);

            if (!string.IsNullOrWhiteSpace(query.Surname))
                queryable.Where(a => a.Name == query.Surname);

            if (!string.IsNullOrWhiteSpace(query.OrderBy))
                queryable.ResolveOrderBy(query);
                
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return queryable.Skip(skipNumber).Take(query.PageSize);
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private static IQueryable<Author> ResolveOrderBy(this IQueryable<Author> queryable, AuthorQuery query)
        {
            if (query.IsDescending)
            {
                switch (query.OrderBy.ToLower())
                {
                    case "name":
                        queryable.OrderBy(a => a.Name);
                        break;
                    case "surname":
                        queryable.OrderBy(a => a.Surname);
                        break;
                    default:
                        return queryable;
                }
            }
            switch (query.OrderBy.ToLower())
            {
                case "name":
                    queryable.OrderByDescending(a => a.Name);
                    break;
                case "surname":
                    queryable.OrderByDescending(a => a.Surname);
                    break;
                default:
                    return queryable;
            }
            return queryable;
        }
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}