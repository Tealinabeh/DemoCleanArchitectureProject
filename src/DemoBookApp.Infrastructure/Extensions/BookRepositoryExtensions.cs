using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> ResolveQuery(this IQueryable<Book> queryable, BookQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.Title))
                queryable = queryable.Where(b => b.Title == query.Title);

            queryable.ResolvePriceSort(query);

            queryable.ResolveDateSort(query);

            queryable.ResolveAuthorSort(query);

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return queryable.Skip(skipNumber).Take(query.PageSize);
        }

        private static IQueryable<Book> ResolveAuthorSort(this IQueryable<Book> queryable, BookQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.AuthorName))
                queryable = queryable.Where(b => b.Author.Name == query.AuthorName);

            if (string.IsNullOrWhiteSpace(query.AuthorSurname))
                queryable = queryable.Where(b => b.Author.Name == query.AuthorSurname);
            return queryable;
        }

        private static IQueryable<Book> ResolveDateSort(this IQueryable<Book> queryable, BookQuery query)
        {
            if (query.IssuedAfter != null)
                queryable = queryable.Where(b => b.DateOfIssue >= query.IssuedAfter);

            if (query.IssuedBefore != null)
                queryable = queryable.Where(b => b.DateOfIssue <= query.IssuedBefore);
            return queryable;
        }

        private static IQueryable<Book> ResolvePriceSort(this IQueryable<Book> queryable, BookQuery query)
        {
            if (query.LowestPrice != null)
                queryable = queryable.Where(b => b.Price >= query.LowestPrice);

            if (query.HighestPrice != null)
                queryable = queryable.Where(b => b.Price <= query.HighestPrice);
            return queryable;
        }
    }
}