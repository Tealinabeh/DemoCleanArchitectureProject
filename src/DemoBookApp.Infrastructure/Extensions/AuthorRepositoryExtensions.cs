using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Extensions
{
    public static class AuthorRepositoryExtensions
    {
        public static IQueryable<Author> SortByQuery(this IQueryable<Author> queryable, AuthorQuery query)
        {
            if (!string.IsNullOrEmpty(query.Name))
                queryable.Where(a => a.Name == query.Name);

            if (!string.IsNullOrEmpty(query.Surname))
                queryable.Where(a => a.Name == query.Surname);

            if (!string.IsNullOrEmpty(query.SortBy))
                queryable.ResolveSortBy(query);
                
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return queryable.Skip(skipNumber).Take(query.PageSize);
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private static IQueryable<Author> ResolveSortBy(this IQueryable<Author> queryable, AuthorQuery query)
        {
            if (query.IsDescending)
            {
                switch (query.SortBy.ToLower())
                {
                    case "name":
                        queryable.OrderBy(a => a.Name);
                        break;
                    case "surname":
                        queryable.OrderBy(a => a.Surname);
                        break;
                    default:
                        return queryable.Select(a => a);
                }
            }
            switch (query.SortBy.ToLower())
            {
                case "name":
                    queryable.OrderByDescending(a => a.Name);
                    break;
                case "surname":
                    queryable.OrderByDescending(a => a.Surname);
                    break;
                default:
                    return queryable.Select(a => a);
            }
            return queryable.Select(a => a);
        }
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}